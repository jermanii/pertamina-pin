using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class PicFungsiController : WebControllerBase
    {
        private readonly IPicFungsiService _service;

        public PicFungsiController(IIdamanService idamanService, IPicFungsiService service) : base(idamanService)
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
        public IActionResult CreatePartial(PicFungsiViewModel model)
        {
            try
            {
                PicFungsiViewModel result = _service.Save(model, UserName);

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
                PicFungsiViewModel model = _service.GetPicFungsiById(guid);

                return PartialView("UpdatePartial", model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return PartialView("UpdatePartial");
            }
        }

        [HttpPost]
        public IActionResult Update(PicFungsiViewModel model)
        {
            try
            {
                PicFungsiViewModel result = _service.Update(model, UserName);

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
                PicFungsiViewModel result = _service.Delete(guid, UserName);

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
                PicFungsiViewModel result = _service.ActiveInActive(guid, UserName);

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