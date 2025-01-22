using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class ExternalContactController : WebControllerBase
    {
        private readonly IExternalContactService _service;

        public ExternalContactController(IIdamanService idamanService, IExternalContactService service) : base(idamanService)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            try
            {
                ExternalContactViewModel model = new ExternalContactViewModel();
                return View(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); 
                error.ErrorMessage = err.Message + err.StackTrace; 
                return View("Error",error);
            }
        }
        [HttpPost]
        public IActionResult Add(ExternalContactViewModel model)
        {
            try
            {
                List<ExternalContactViewModel> result = _service.Save(model, UserName);

                return Json(result);
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
                ExternalContactViewModel result = _service.Delete(guid, UserName);
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

                ExternalContactViewModel model = _service.GetExternalContactById(guid);
                model.Id = guid;
                return View(model);

            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); 
                error.ErrorMessage = err.Message + err.StackTrace; 
                return View("Error",error);
            }
        }
        public IActionResult Read(Guid guid)
        {
            try
            {

                ExternalContactViewModel model = _service.GetExternalContactById(guid);
                model.Id = guid;
                return View(model);

            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return View("Error", error);
            }
        }
        [HttpGet]
        public IActionResult Duplicate(Guid guid)
        {
            try
            {

                ExternalContactViewModel model = _service.GetExternalContactById(guid);
                model.Id = guid;
                return View(model);

            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace; 
                return View("Error",error);
            }
        }
        [HttpPost]
        public IActionResult AddDuplicateForm(ExternalContactViewModel model)
        {
            try
            {
                List<ExternalContactViewModel> result = _service.Save(model, UserName);

                return Json(result);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
        [HttpPost]
        public IActionResult UpdateForm(ExternalContactViewModel model)
        {
            try
            {
                ExternalContactViewModel result = _service.Update(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpGet]
        public IActionResult GetExternalContactById(Guid guid)
        {
            try
            {
                ExternalContactViewModel model = _service.GetExternalContactById(guid);
                model.Id = guid;
                var result = model;
                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        public IActionResult DownloadData(string searchQuery)
        {
            byte[] downloadBytes;
            using (var package = _service.ExportXLS(true, searchQuery))

            {
                downloadBytes = package.GetAsByteArray();
            }
            string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            if (month.Length < 2)
            {
                month = "0" + month;
            }

            string date = DateTime.Now.Day.ToString();
            if (date.Length < 2)
            {
                date = "0" + date;
            }
            string hour = DateTime.Now.TimeOfDay.Hours.ToString();
            if (hour.Length < 2)
            {
                hour = "0" + hour;
            }
            string minute = DateTime.Now.TimeOfDay.Minutes.ToString();
            if (minute.Length < 2)
            {
                minute = "0" + minute;
            }

            return File(downloadBytes, XlsxContentType, "PIN_EXTERNAL_CONTACT_" + date + month + year + "_" + hour + ":" + minute + ".xlsx");
        }

        public IActionResult DownloadTemplate(string searchQuery, int pageNumber, int pageSize)
        {
            byte[] downloadBytes;
            using (var package = _service.ExportXLSThisPage(true, searchQuery, pageNumber, pageSize))

            {
                downloadBytes = package.GetAsByteArray();
            }
            string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(downloadBytes, XlsxContentType, "Template_External_Contact" + ".xlsx");
        }
    }
}
