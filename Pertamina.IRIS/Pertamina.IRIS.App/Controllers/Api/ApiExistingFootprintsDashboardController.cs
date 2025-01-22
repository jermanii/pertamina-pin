using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Pertamina.IRIS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace Pertamina.IRIS.App.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiExistingFootprintsDashboardController : BaseController
    {
        private readonly IExistingFootprintsDashboardService _service;
        private readonly IWebHostEnvironment _environment;

        public ApiExistingFootprintsDashboardController(IIdamanService idamanService, IExistingFootprintsDashboardService service, IWebHostEnvironment environment) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [Route("List")]
        [HttpPost]
        public async Task<IActionResult> List(string negaraMitra, string streamBusiness, string entitasPertamina)
        {
            try
            {
                var request = GetDataTableComponent();
                string wwwroot = _environment.WebRootPath;

                var result = await _service.GetListPaging(request, wwwroot, negaraMitra, streamBusiness, entitasPertamina);

                List<ExistingFootprintsDashboardHighPriorityViewModel> datas = result.Data;

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
                ExistingFootprintDashboardMapViewModel result = new ExistingFootprintDashboardMapViewModel();

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
    }
}