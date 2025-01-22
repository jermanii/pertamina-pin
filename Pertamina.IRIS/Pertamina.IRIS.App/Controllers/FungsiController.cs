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
    public class FungsiController : WebControllerBase
    {
        private readonly IFungsiService _service;

        public FungsiController(IIdamanService idamanService, IFungsiService service) : base(idamanService)
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
        public IActionResult CreatePartial(FungsiViewModel model)
        {
            try
            {

                FungsiViewModel result = _service.Save(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return Json(error);
            }
        }

        [HttpGet]
        public IActionResult Update(Guid guid)
        {
            try
            {
                FungsiViewModel model = _service.GetFungsiById(guid);

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
        public IActionResult UpdatePartial(FungsiViewModel model)
        {
            try
            {
                FungsiViewModel result = _service.Update(model, UserName);

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
                FungsiViewModel result = _service.Delete(guid, UserName);

                return Json(result);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return Json(error);
            }
        }
    }
}
