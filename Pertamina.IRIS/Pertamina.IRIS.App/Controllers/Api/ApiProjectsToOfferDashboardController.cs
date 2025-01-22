using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiProjectsToOfferDashboardController :BaseController
    {
        private IProjectsToOfferDashboardService _projectsToOfferDashboardService;
        public ApiProjectsToOfferDashboardController(IIdamanService idamanService, IProjectsToOfferDashboardService projectsToOfferDashboardService) : base(idamanService) 
        {
            _projectsToOfferDashboardService = projectsToOfferDashboardService;
        }

        [Route("List")]
        [HttpPost]
        public async Task<ActionResult> List()
        {
            try
            {
                bool isDraft = false;
                var request = GetDataTableComponent();

                OpportunityViewModel decodeModel = new OpportunityViewModel();
                decodeModel.IsDraft = isDraft;

                var result = await _projectsToOfferDashboardService.GetList(request, decodeModel);

                List<OpportunityViewModel> datas = result.Data;
                if (result.IsSuccess)
                {
                    var jsonData = new
                    {
                        draw = request.Draw,
                        recordsFiltered = result.RecordsFiltered,
                        recordsTotal = result.RecordsTotal,
                        data = datas
                    };

                    return Json(jsonData);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
        [Route("urlProvincesMap")]
        [HttpGet]
        public async Task<ActionResult> urlProvincesMap()
        {
            try
            {
                OpportunityLokasiProyekViewModel decodeModel = new OpportunityLokasiProyekViewModel();
                var result = _projectsToOfferDashboardService.GetProvinceAcronyms();
                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
    }
}
