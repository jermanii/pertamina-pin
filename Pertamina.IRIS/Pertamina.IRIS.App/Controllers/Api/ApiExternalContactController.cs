using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Services.Interfaces;
using System.Threading.Tasks;
using System;
using Pertamina.IRIS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Pertamina.IRIS.App.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiExternalContactController : BaseController
    {
        private readonly IExternalContactService _service;
        public ApiExternalContactController(IIdamanService idamanService, IExternalContactService service) : base(idamanService)
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

        [Route("Upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            ExternalContactViewModel model = new ExternalContactViewModel();
            try
            {
                var data = Request.Form;
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString();

                    using (var stream = file.OpenReadStream())
                    {
                        model = await _service.readExcelPackage(stream, fileName);

                    }
                }

                return Json(model);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}
