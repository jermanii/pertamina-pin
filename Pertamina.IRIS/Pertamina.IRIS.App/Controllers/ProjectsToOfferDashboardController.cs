using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class ProjectsToOfferDashboardController:WebControllerBase
    {

        private readonly IProjectsToOfferDashboardService _projectsToOfferDashboardService;
        private readonly IMetricsService _metricsService;
        public ProjectsToOfferDashboardController(IIdamanService idamanService, IMetricsService metricsService, IProjectsToOfferDashboardService projectsToOfferDashboardService):base(idamanService) 
        {
            _projectsToOfferDashboardService = projectsToOfferDashboardService;
            _metricsService = metricsService ?? throw new ArgumentNullException(nameof(metricsService));
        }
        public async Task<IActionResult> Index(string sortingHighPriority, string selectRadio, string reset)
        {
            ProjectsToOfferDashboardViewModel model = new ProjectsToOfferDashboardViewModel();
            try
            {
                model.HighPrioritys = await _projectsToOfferDashboardService.GetOpportunityHighPriority();
                if (!string.IsNullOrEmpty(sortingHighPriority) && !string.IsNullOrEmpty(selectRadio) && reset == null)
                {
                    //Apply sorting by asc
                    if(sortingHighPriority == "TargetProject" && selectRadio =="asc")
                    {
                        model.HighPrioritys =  model.HighPrioritys.OrderBy(h=>h.CreateDate).ToList();
                    }
                    if (sortingHighPriority == "PotentialRevenue" && selectRadio =="asc")
                    {
                        model.HighPrioritys =  model.HighPrioritys.OrderBy(h => h.PotentialRevenuePerYear).ToList();
                    }
                    if (sortingHighPriority == "EstimatedCapex" && selectRadio =="asc")
                    {
                        model.HighPrioritys =  model.HighPrioritys.OrderBy(h => h.CapexToString).ToList();
                    }
                    //Apply sorting by desc
                    if (sortingHighPriority == "TargetProject" && selectRadio =="desc")
                    {
                        model.HighPrioritys =  model.HighPrioritys.OrderByDescending(h => h.CreateDate).ToList();
                    }
                    if (sortingHighPriority == "PotentialRevenue" && selectRadio =="desc")
                    {
                        model.HighPrioritys =  model.HighPrioritys.OrderByDescending(h => h.PotentialRevenuePerYear).ToList();
                    }
                    if (sortingHighPriority == "EstimatedCapex" && selectRadio =="desc")
                    {
                        model.HighPrioritys =  model.HighPrioritys.OrderByDescending(h => h.CapexToString).ToList();
                    }

                }
                if (reset != null) 
                {
                    model.HighPrioritys =  await _projectsToOfferDashboardService.GetOpportunityHighPriority();
                }
                model.Metrics = _metricsService.GetMetrics(EnumBaseModel.ProjectsToOfferMetrics);
                ViewBag.SortingHighPriority = sortingHighPriority;
                ViewBag.SortingHighPriority= sortingHighPriority;
                ViewBag.SortingHighPriority =  sortingHighPriority;
                ViewBag.Asc = selectRadio;
                ViewBag.Desc=selectRadio;

                return View(model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return View(model);
            }
        }

        public IActionResult Detail(Guid guid )
        {
            ProjectsToOfferDashboardViewModel model = new ProjectsToOfferDashboardViewModel();
            model.Opportunity = _projectsToOfferDashboardService.GetDetailOpportunityById(guid);
            return PartialView("DetailPartial", model);

        }

    }
}
