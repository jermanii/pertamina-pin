using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Pertamina.IRIS.App.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiTargetMitraController : BaseController
    {
        private readonly ITargetMitraService _service;

        public ApiTargetMitraController(IIdamanService idamanService, ITargetMitraService service) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Route("List")]
        [HttpPost]
        public async Task<IActionResult> List()
        {
            try
            {
                var request = GetDataTableComponent();

                var result = await _service.GetListPaging(request);

                List<TargetMitraViewModel> datas = result.Data;

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
    }
}
