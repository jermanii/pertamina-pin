using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Services.Implement;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Pertamina.IRIS.App.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSignedAgreementDashboardController : BaseController
    {
        private readonly ISignedAgreementDashboardService _service;
        private readonly IWebHostEnvironment _environment;

        public ApiSignedAgreementDashboardController(IIdamanService idamanService, ISignedAgreementDashboardService service, IWebHostEnvironment environment) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [Route("List")]
        [HttpPost]
        public async Task<ActionResult> List(string negaraMitra, string streamBusiness, string entitasPertamina)
        {
            try
            {
                var request = GetDataTableComponent();
                string wwwroot = _environment.WebRootPath;

                var result = await _service.GetListPaging(request, wwwroot, negaraMitra, streamBusiness, entitasPertamina);

                List<SignedAgreementDashboardHighPriorityViewModel> datas = result.Data;
                foreach (var data in datas)
                {
                    data.EntitasPertamina = string.Join(',', _service.GetSignedAgreementDashboardDetailPertaminaEntitas(data.Id).Select(x => x.CompanyName));
                }

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

        [Route("GetCountriesMap")]
        [HttpGet]
        public async Task<IActionResult> GetCountriesMap(bool isFilter, Guid? negara, Guid? stream, Guid? entitas)
        {
            try
            {
                SignedAgreementDashboardMapViewModel result = new SignedAgreementDashboardMapViewModel();

                if (isFilter)
                    result = await _service.GetCountriesMap(negara, stream, entitas);
                else
                    result = await _service.GetCountriesMap();

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }

        [Route("GetHighPriorityBasedOnDropDown")]
        [HttpGet]
        public async Task<IActionResult> GetHighPriorityBasedOnDropDown(string? streamBusiness, string? negaraMitra, string? entitasPertamina)
        {
            SignedAgreementDashboardViewModel model = new SignedAgreementDashboardViewModel();
            try
            {
                model = await _service.FilterHighPriorityBasedOnDropdown(streamBusiness, negaraMitra, entitasPertamina);

                return Json(model.HighPriority);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }

        [Route("GetHighPriorityBasedOnMap")]
        [HttpPost]
        public async Task<IActionResult> GetHighPriorityBasedOnMap([FromForm] string countryAcr)
        {
            SignedAgreementDashboardViewModel model = new SignedAgreementDashboardViewModel();
            try
            {
                model = await _service.FilterHighPriority(countryAcr);

                return Json(model.HighPriority);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
    }
}