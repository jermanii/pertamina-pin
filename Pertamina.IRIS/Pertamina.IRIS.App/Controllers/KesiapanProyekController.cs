using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class KesiapanProyekController : WebControllerBase
    {
        private readonly IKesiapanProyekService _service;
        public KesiapanProyekController(IIdamanService idamanService, IKesiapanProyekService service) : base(idamanService)
        {
            _service = service;
        }
        public IActionResult Index(KesiapanProyekViewModel model)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create(Guid guid)
        {
            return PartialView("CreatePartial");
        }

        [HttpPost]
        public IActionResult CreatePartial(KesiapanProyekViewModel model)
        {
            try
            {
                KesiapanProyekViewModel result = _service.Save(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                KesiapanProyekViewModel result = _service.Delete(guid, UserName);
                return Json(result);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpGet]
        public IActionResult Update(Guid guid)
        {
            try
            {
                KesiapanProyekViewModel model = _service.GetKesiapanProyekById(guid);
                
                return PartialView("UpdatePartial", model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return PartialView("UpdatePartial");
            }
        }

        [HttpPost]
        public IActionResult Update(KesiapanProyekViewModel model)
        {
            try
            {
                KesiapanProyekViewModel result = _service.Update(model, UserName);
                
                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
    }
}