using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class ExistingFootprintsDashboardController : WebControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IMetricsService _metricsService;
        private readonly IExistingFootprintsDashboardService _service;

        public ExistingFootprintsDashboardController(IIdamanService idamanService, IWebHostEnvironment environment, IMetricsService metricsService, IExistingFootprintsDashboardService service) : base(idamanService)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _metricsService = metricsService ?? throw new ArgumentNullException(nameof(metricsService));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IActionResult Index()
        {
            try
            {
                string wwwroot = _environment.WebRootPath;
                ExistingFootprintsDashboardViewModel model = new ExistingFootprintsDashboardViewModel();

                return View(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }

        [HttpGet]
        public IActionResult Detail(Guid guid, int year)
        {
            try
            {
                string wwwroot = _environment.WebRootPath;

                ExistingFootprintsDashboardHighPriorityViewModel model = new ExistingFootprintsDashboardHighPriorityViewModel();
                model = _service.GetExistingFootprintsDashboardDetail(wwwroot, guid, year);
                model.Partners = _service.GetExistingFootprintsPartners(model.Id);

                return PartialView("DetailPartial", model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }

        [HttpGet]
        public IActionResult MetricsView(Guid? negara, Guid? stream, Guid? entitas, bool isClickMap, string countryAcronym)
        {
            try
            {
                ExistingFootprintsDashboardViewModel model = new ExistingFootprintsDashboardViewModel();

                if (isClickMap)
                    negara = _service.GetCountryByAcronym(countryAcronym);

                model.Metrics = _metricsService.GetMetrics(EnumBaseModel.ExistingFootprintsMetrics, negara, stream, entitas);

                return PartialView("MetricsPartial", model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }

        [HttpGet]
        public IActionResult HighPriorityView(bool isHigh, bool isFilter, bool isSort, bool isScroll, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount)
        {
            try
            {
                string wwwroot = _environment.WebRootPath;
                ExistingFootprintsDashboardViewModel model = new ExistingFootprintsDashboardViewModel();

                if (isScroll)
                    model.PageCountSort = pageCount + 1;
                else
                    model.PageCountSort = pageCount;

                model.HighPriority = _service.GetHighPriority(wwwroot, isHigh, isFilter, isSort, isClickMap, countryAcronym, negara, stream, entitas, category, order, model.PageCountSort.Value);

                model.RecordHighPriorityCountSort = model.HighPriority.Count();
                model.AllRecordHighPriorityCountSort = _service.GetCountAllRecordSortHighPriority(wwwroot, isHigh, isFilter, isSort, isClickMap, countryAcronym, negara, stream, entitas, category, order, model.PageCountSort.Value);

                return PartialView("HighPriorityPartial", model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }
    }
}