using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class SignedAgreementDashboardController : WebControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IMetricsService _metricsService;
        private readonly ISignedAgreementDashboardService _service;

        public SignedAgreementDashboardController(IIdamanService idamanService, IWebHostEnvironment environment, IMetricsService metricsService, ISignedAgreementDashboardService service) : base(idamanService)
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
                SignedAgreementDashboardViewModel model = new SignedAgreementDashboardViewModel();

                return View(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }

        [HttpGet]
        public IActionResult Detail(Guid guid)
        {
            try
            {
                string wwwroot = _environment.WebRootPath;

                SignedAgreementDashboardHighPriorityViewModel model = new SignedAgreementDashboardHighPriorityViewModel();
                model = _service.GetSignedAgreementDashboardDetail(wwwroot, guid);
                model.Entitas = _service.GetSignedAgreementDashboardDetailPertaminaEntitas(guid);
                model.Streams = _service.GetSignedAgreementDashboardDetailStreams(guid);
                model.Partners = _service.GetSignedAgreementDashboardDetailPartners(guid);
                model.ProjectLocations = _service.GetSignedAgreementDashboardDetailProjectLocations(guid);

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
                SignedAgreementDashboardViewModel model = new SignedAgreementDashboardViewModel();

                if (isClickMap)
                    negara = _service.GetCountryByAcronym(countryAcronym);

                model.Metrics = _metricsService.GetMetrics(EnumBaseModel.SignedAgreementMetrics, negara, stream, entitas);

                return PartialView("MetricsPartial", model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }

        [HttpGet]
        public IActionResult HighPriorityView(bool isHigh, bool isScroll, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount)
        {
            try
            {
                string wwwroot = _environment.WebRootPath;
                SignedAgreementDashboardViewModel model = new SignedAgreementDashboardViewModel();

                if (isScroll)
                    model.PageCountSort = pageCount + 1;
                else
                    model.PageCountSort = pageCount;

                model.HighPriority = _service.GetStrategicAgreement(wwwroot, isHigh, isSort, isFilter, isClickMap, negara, stream, entitas, countryAcronym, category, order, model.PageCountSort.Value);
                model.KementrianLembaga = _service.GetKemntrianLembagaAgreement(model.HighPriority);

                model.RecordHighPriorityCountSort = model.HighPriority.Count();
                model.AllRecordHighPriorityCountSort = _service.GetCountAllStrategicAgreement(wwwroot, isHigh, isSort, isFilter, isClickMap, negara, stream, entitas, countryAcronym, category, order, model.PageCountSort.Value);

                return PartialView("HighPriorityPartial", model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }
    }
}