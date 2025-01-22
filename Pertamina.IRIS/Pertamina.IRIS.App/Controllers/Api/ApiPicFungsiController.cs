using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPicFungsiController : BaseController
    {
        private readonly IPicFungsiService _service;

        public ApiPicFungsiController(IIdamanService idamanService, IPicFungsiService service) : base(idamanService)
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

                List<PicFungsiViewModel> datas = result.Data;

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
