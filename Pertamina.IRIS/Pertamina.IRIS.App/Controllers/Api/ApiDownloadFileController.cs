using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization; 

namespace Pertamina.IRIS.App.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiDownloadFileController : BaseController
    {
        private readonly IApplicationFileDownloadService _service;
        public ApiDownloadFileController(IIdamanService idamanService, IApplicationFileDownloadService service) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        } 


        [Route("DownloadFile")]
        [HttpGet]
        public async Task<IActionResult> DownloadFile(Guid idIBI,string title, string feature)
        {
            var result = await _service.DownloadZip(idIBI, feature);
            return File(result, "application/zip", title + ".zip");
        }

        [Route("DownloadSingleFile")]
        [HttpGet]
        public async Task<IActionResult> DownloadSingleFile(Guid guid, string title, string feature)
        {
            var result = await _service.DownloadSingleFile(guid, feature);
            return File(result.FileLength, result.FileType, result.FileName);
        }
    }
}
