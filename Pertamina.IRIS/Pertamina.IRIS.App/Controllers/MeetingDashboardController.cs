using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class MeetingDashboardController : WebControllerBase
    {
        private readonly IMetricsService _metricsService;
        public MeetingDashboardController(IIdamanService idamanService, IMetricsService metricsService) : base(idamanService)
        { 
            _metricsService = metricsService;
        }
        public IActionResult Index()
        {
            MeetingDashboardViewModel model = new MeetingDashboardViewModel();
            try
            {
                model.Metrics = _metricsService.GetMetrics(EnumBaseModel.PotentialInitiativeMetrics);

                return View(model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return View(model);
            }
        }
    }
}
