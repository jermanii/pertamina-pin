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
    public class StreamBusinessController : WebControllerBase
    {
        private readonly IStreamBusinessService _service;

        public StreamBusinessController(IIdamanService idamanService, IStreamBusinessService service) : base(idamanService)
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
        public IActionResult CreatePartial(StreamBusinessViewModel model)
        {
            try
            {

                StreamBusinessViewModel result = _service.Save(model, UserName);

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
                StreamBusinessViewModel model = _service.GetStreamBusinessById(guid);

                return PartialView("UpdatePartial", model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return PartialView("UpdatePartial");
            }
        }
        [HttpPost]

        public IActionResult Update(StreamBusinessViewModel model)
        {
            try
            {
                StreamBusinessViewModel result = _service.Update(model, UserName);

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
                StreamBusinessViewModel result = _service.Delete(guid, UserName);
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
