using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class KlasifikasiKendalaController : WebControllerBase
    {
        private readonly IKlasifikasiKendalaService _service;
        public KlasifikasiKendalaController(IIdamanService idamanService, IKlasifikasiKendalaService service) : base(idamanService)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(Guid guid)
        {
            return PartialView("CreatePartial");
        }

        [HttpPost]
        public IActionResult CreatePartial(KlasifikasiKendalaViewModel model)
        {
            try
            {
                KlasifikasiKendalaViewModel result = _service.Save(model, UserName);

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
                KlasifikasiKendalaViewModel result = _service.Delete(guid, UserName);
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
                KlasifikasiKendalaViewModel model = _service.GetKlasifikasiKendalaById(guid);

                return PartialView("UpdatePartial", model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return PartialView("UpdatePartial");
            }
        }

        [HttpPost]
        public IActionResult Update(KlasifikasiKendalaViewModel model)
        {
            try
            {
                KlasifikasiKendalaViewModel result = _service.Update(model,UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
    }
}
