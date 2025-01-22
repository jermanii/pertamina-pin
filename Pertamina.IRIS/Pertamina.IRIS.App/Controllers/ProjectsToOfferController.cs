using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Web;

namespace Pertamina.IRIS.App.Controllers
{
    public class ProjectsToOfferController : WebControllerBase
    {
        private readonly IProjectsToOfferService _service;

        public ProjectsToOfferController(IIdamanService idamanService, IProjectsToOfferService service) : base(idamanService)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            OpportunityViewModel model = new OpportunityViewModel();

            try
            {
                model = _service.GetOpportunityCount();

                return View(model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return View(model);
            }
        }

        public IActionResult Create()
        {
            try
            {
                OpportunityViewModel model = new OpportunityViewModel();
                var picLevel = _service.GetLevelPic();
                model.PICLevelLeadId = picLevel.PICLevelLeadId;
                model.PICLevelMemberId = picLevel.PICLevelMemberId;
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
                OpportunityViewModel model = _service.GetReadOpportunityById(guid);
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

        [HttpGet]
        public IActionResult Update(Guid guid)
        {   
            try
            {
                
                OpportunityViewModel model = _service.GetOpportunityById(guid);
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
        public IActionResult Add(OpportunityViewModel model)
        {
            try
            {
                var isExistingValidation = _service.IsExistingValidation(model);
                if (isExistingValidation.IsError ==true)
                {
                    return Json(isExistingValidation);
                }
                model = _service.Save(model, UserName, ProfileSession.companyCode);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpPost]
        public IActionResult UpdateForm(OpportunityViewModel model)
        {
            try
            {
                var isExistingValidation = _service.IsExistingValidation(model);
                if (isExistingValidation.IsError ==true)
                {
                    return Json(isExistingValidation);
                }
                OpportunityViewModel result = _service.Update(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpPost]
        public IActionResult SaveDraft(OpportunityViewModel model)
        {
            try
            {
                model = _service.SaveDraft(model, UserName, ProfileSession.companyCode);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
        public IActionResult DownloadData(string opportunity, string negaraMitra, string streamBussiness, string entitasPertamina, string createDate, string searchQuery, bool isDraft)
        {
            byte[] downloadBytes;
            
            using (var package = _service.ExportXLS(true, opportunity, negaraMitra, streamBussiness, entitasPertamina, createDate, searchQuery, isDraft))

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

            return File(downloadBytes, XlsxContentType, "PIN_PROJECTS TO OFFER_" + date + month + year + "_" + hour + ":" + minute + ".xlsx");
        }

        [HttpGet]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                OpportunityViewModel result = _service.Delete(guid, UserName);
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