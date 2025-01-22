using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiLandingPageController : WebControllerBase
    {
        private readonly ILandingPageService _service;

        public ApiLandingPageController(IIdamanService idamanService, ILandingPageService service) : base(idamanService)
        {
            _service = service;
        }

        [Route("GetLinkPowerBi")]
        [HttpGet]
        public async Task<IActionResult> GetLinkPowerBi()
        {
            try
            {

                LinkPowerBiViewModel link = await _service.GetLinkPowerBI();
                if (link != null)
                {
                    return Json(new { url = link.Url });
                }
                else
                {
                    return Json(new { url = "" }); // Jika tidak ada record yang ditemukan, mengirimkan URL kosong
                }

            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); 
                error.ErrorMessage = err.Message + err.StackTrace; 
                return View("Error",error);
            }
        }

        [Route("GetDownloadUserManual")]
        [HttpGet]
        public async Task<IActionResult> GetDownloadUserManual()
        {
            try
            {
                string path = await _service.GetDownloadUserManual();
                string filePath = Path.Combine(path);

                if (!System.IO.File.Exists(filePath))
                {
                    ErrorModel error = new ErrorModel();
                    error.ErrorMessage = "File not found.";
                    return View("Error", error);
                }

                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                string contentType = "application/octet-stream";
                string fileName = Path.GetFileName(filePath);
                
                return File(stream, contentType, fileName);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return View("Error", error);
            }
        }
    }
}
