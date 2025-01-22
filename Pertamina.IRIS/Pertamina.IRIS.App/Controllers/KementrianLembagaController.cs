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
    public class KementrianLembagaController : WebControllerBase
    {
        private readonly IKementrianLembagaService _service;
        public KementrianLembagaController(IIdamanService idamanService, IKementrianLembagaService service) : base(idamanService)
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
        public IActionResult CreatePartial(KementrianLembagaViewModel model)
        {
            try
            {
                KementrianLembagaViewModel result = _service.Save(model, UserName);

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
                KementrianLembagaViewModel result = _service.Delete(guid, UserName);
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
                KementrianLembagaViewModel model = _service.GetKementrianById(guid);

                return PartialView("UpdatePartial", model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return PartialView("UpdatePartial");
            }
        }

        [HttpPost]
        public IActionResult Update(KementrianLembagaViewModel model)
        {
            try
            {
                KementrianLembagaViewModel result = _service.Update(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
    }
}
