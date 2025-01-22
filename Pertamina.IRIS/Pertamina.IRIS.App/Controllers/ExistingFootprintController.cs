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
    public class ExistingFootprintController : WebControllerBase
    {
        private readonly IExistingFootprintService _service;
        public ExistingFootprintController(IIdamanService idamanService, IExistingFootprintService service) : base(idamanService)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var guid = Guid.Empty;
            //_service.ClearTemporaryDataExistingFootprint( guid);
            return View();
        }
        public IActionResult Create(Guid? guid)
        {

            ExistingFootprintViewModel model = new ExistingFootprintViewModel();
            var result = _service.CreateExistingFootprint(guid);
            model.PicLevelMemberId = result.PicLevelMemberId;
            model.PicLevelId = result.PicLevelId;
            return View(model);
        }
        public IActionResult Update(Guid guid)
        {
            try
            {
                ExistingFootprintViewModel model = new ExistingFootprintViewModel();
                var result = _service.CreateExistingFootprint(guid);
                model.Id = result.Id;
                model = _service.GetExistingToUpdate(guid, result.PicLevelId, result.PicLevelMemberId);
                model.PicLevelMemberId = result.PicLevelMemberId;
                model.PicLevelId = result.PicLevelId;
                model.Id = guid;
                if (model.PicFungsi != null)
                {
                    model.PicFungsiId = model.PicFungsi.PicFungsiId;
                }
                return View(model);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
        public IActionResult Read(Guid guid)
        {
            try
            {
                ExistingFootprintViewModel model = _service.GetExistingFootprint(guid);
                model = _service.GetExistingFootprintToRead(guid);
                return View(model);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
        public IActionResult Delete(Guid guid)
        {
            try
            {
                ExistingFootprintViewModel model = _service.Delete(guid, UserName);
                return Json(model);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpPost]
        public IActionResult SaveDraft(ExistingFootprintViewModel model)
        {
            try
            {
                model = _service.SaveDraft(model, UserName);
                return Json(model);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }

        }
        [HttpPost]
        public IActionResult Submit(ExistingFootprintViewModel model)
        {
            try
            {
                var isExistingValidation = _service.IsExistingValidation(model);
                if (isExistingValidation.IsError ==true)
                {
                    return Json(isExistingValidation);
                }
                model = _service.Submit(model, UserName);
                return Json(model);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }

        }
        public IActionResult UpdateForm(ExistingFootprintViewModel model)
        {
            try
            {
                var isExistingValidation = _service.IsExistingValidation(model);
                if (isExistingValidation.IsError ==true)
                {
                    return Json(isExistingValidation);
                }
                model = _service.Update(model, UserName);
                return Json(model);
            }
            catch (Exception err)
            {
                string str = err.Message;
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }

        }
        public IActionResult DownloadData(string searchGrid, string entitasPertaminaId, string projectTypeId, string totalAssetsMinOp,string totalAssetsMaxOp, string revenueMinOp,string revenueMaxOp, string ebitdaMinOp,string ebitdaMaxOp, string yearOp, string partnerCountryId)
        {
            byte[] downloadBytes;

            using (var package = _service.ExportXLS(searchGrid,entitasPertaminaId,projectTypeId, totalAssetsMinOp,totalAssetsMaxOp, revenueMinOp,revenueMaxOp, ebitdaMinOp,ebitdaMaxOp,yearOp, partnerCountryId))
            {
                downloadBytes = package.GetAsByteArray();
            }
            string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            if(month.Length< 2)
            {
                month = "0" + month;
            }
            string date = DateTime.Now.ToString();
            if(date.Length< 2)
            {
                date = "0" + date;
            }
            string hour = DateTime.Now.Hour.ToString();
            if(hour.Length< 2)
            {
                hour = "0" + hour;
            }
            string minute = DateTime.Now.Minute.ToString();
            if(minute.Length< 2)
            {
                minute = "0" + minute;
            }
            return File(downloadBytes, XlsxContentType, "PIN_EXISTING_FOOTPRINTS" + date + month + year + "_" + hour + minute + ".xlsx");
        }
    }
}
