using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Services.Interfaces;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Pertamina.IRIS.App.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKlasifikasiKendalaController : BaseController
    {
        private readonly IKlasifikasiKendalaService _service;
        public ApiKlasifikasiKendalaController(IIdamanService idamanService, IKlasifikasiKendalaService service) : base(idamanService)
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

                if (result.IsSuccess)
                {
                    var jsonData = new
                    {
                        draw = request.Draw,
                        recordsFiltered = result.RecordsFiltered,
                        recordsTotal = result.RecordsTotal,
                        data = result.Data
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
