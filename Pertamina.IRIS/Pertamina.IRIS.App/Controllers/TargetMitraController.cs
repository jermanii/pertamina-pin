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
    public class TargetMitraController : WebControllerBase
    {
        private readonly ITargetMitraService _service;

        public TargetMitraController(IIdamanService idamanService, ITargetMitraService service) : base(idamanService)
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
        public IActionResult CreatePartial(TargetMitraViewModel model)
        {
            try
            {

                TargetMitraViewModel result = _service.Save(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }

        }
        [HttpGet]

        public IActionResult Update(Guid guid)
        {
            try
            {
                TargetMitraViewModel model = _service.GetTargetMitraById(guid);

                return PartialView("UpdatePartial", model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return View("Error", error);
            }
        }
        [HttpPost]

        public IActionResult Update(TargetMitraViewModel model)
        {
            try
            {
                TargetMitraViewModel result = _service.Update(model, UserName);

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
                TargetMitraViewModel result = _service.Delete(guid, UserName);
                return Json(result);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

    }
}
