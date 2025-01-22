using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class EntitasPertaminaController : WebControllerBase
    {
        private readonly IEntitasPertaminaService _service;

        public EntitasPertaminaController(IIdamanService idamanService, IEntitasPertaminaService service) : base(idamanService)
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
        public IActionResult CreatePartial(EntitasPertaminaViewModel model)
        {
            try
            {
                EntitasPertaminaViewModel result = _service.Save(model, UserName);

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
                EntitasPertaminaViewModel model = _service.GetEntitasPertaminaById(guid);

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
        public IActionResult UpdatePartial(EntitasPertaminaViewModel model)
        {
            try
            {
                EntitasPertaminaViewModel result = _service.Update(model, UserName);

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
                EntitasPertaminaViewModel result = _service.Delete(guid, UserName);

                return Json(result);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpGet]
        public IActionResult ActiveInActive(Guid guid)
        {
            try
            {
                EntitasPertaminaViewModel result = _service.ActiveInActive(guid, UserName);

                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                string str = err.Message;
                return RedirectToAction("Index");
            }
        }
    }
}