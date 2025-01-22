using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class PotentialInitiativesDashboardController : WebControllerBase
    {
        private readonly IPotentialInitiativesDashboardService _potentialInitiativesDashboard;
        private readonly IWebHostEnvironment _environment;
        private readonly IMetricsService _metricsService;
        public PotentialInitiativesDashboardController(IIdamanService idamanService,
                                                       IPotentialInitiativesDashboardService potentialInitiativesDashboard,
                                                       IMetricsService metricsService,
                                                       IWebHostEnvironment environment) : base(idamanService)
        {
            _potentialInitiativesDashboard = potentialInitiativesDashboard;
            _metricsService = metricsService;
            _environment = environment;
        }
        public async Task<IActionResult> Index(string columnSort, string sortOrder, string lembagaSort)
        {
            string wwwroot = _environment.WebRootPath;
            var sort = new SortViewModel();
            if (!(string.IsNullOrEmpty(columnSort) || string.IsNullOrEmpty(sortOrder)))
            {
                sort = new SortViewModel(columnSort, sortOrder);
            }            
            PotentialInitiativesDashboardViewModel model = new PotentialInitiativesDashboardViewModel();
            model.HighPriorityInitiatives = await _potentialInitiativesDashboard.GetInitiativeHighPriority(wwwroot, sort);
            model.Metrics = _metricsService.GetMetrics(EnumBaseModel.PotentialInitiativeMetrics);
            ViewBag.ColumnSort = columnSort;
            ViewBag.SortOrder = sortOrder;
            //ViewBag.LembagaSort = lembagaSort;
            return View(model);
        }

        public IActionResult RedirectTab(string tab)
        {
            return RedirectToAction("Index", tab);
        }

        public async Task<IActionResult> Detail(Guid guid)
        {
            var result = await _potentialInitiativesDashboard.GetHighPriorityDetailById(guid);
            return PartialView("DetailPartial", result);
        }
    }
}
