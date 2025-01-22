using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class InitiativeController : WebControllerBase
    {
        private readonly IInitiativeService _service;
        private readonly IProjectsToOfferService _opportunityService;

        public InitiativeController(IIdamanService idamanService, IInitiativeService service, IProjectsToOfferService opportunityService) : base(idamanService)
        {
            _service = service;
            _opportunityService = opportunityService;
        }
        public IActionResult Index()
        {
            InitiativeViewModel model = new InitiativeViewModel();

            try
            {
                model = _service.GetInitiativeCount();

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
                InitiativeViewModel initiative = new InitiativeViewModel();
                return View(initiative);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); 
                error.ErrorMessage = err.Message + err.StackTrace; 
                return View("Error",error);
            }
        }
        [HttpPost]
        public IActionResult Add(InitiativeViewModel model)
        {
            try
            {
                model = _service.Save(model, UserName, ProfileSession.companyCode);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
        [HttpPost]
        public IActionResult SaveDraft(InitiativeViewModel model)
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
        public IActionResult Read(Guid guid)
        {
            try
            {
                InitiativeViewModel model = _service.GetReadInitiativeById(guid);
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
        public IActionResult GetOpportunityById(Guid guid)
        {
            try
            {

                OpportunityViewModel model = _opportunityService.GetOpportunityById(guid);
                model.Id = guid;
                var result = model;
                return Json(new { items = result });

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult Update(Guid guid)
        {
            try
            {
                InitiativeViewModel model = _service.GetInitiativeById(guid);
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
        public IActionResult UpdateForm(InitiativeViewModel model)
        {
            try
            {
                InitiativeViewModel result = _service.Update(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
        public IActionResult DownloadData(string initiativeStage, string initiativeStatus, string initiativeHolder, string negaraMitra, string kawasanMitra, string streamBussiness, string entitasPertamina, string searchQuery, string rangeCreateDate, bool isDraft)
        {
            byte[] downloadBytes;
           
            using (var package = _service.ExportXLS(true, initiativeStage, initiativeStatus, initiativeHolder, negaraMitra, kawasanMitra, streamBussiness, entitasPertamina, searchQuery, rangeCreateDate, isDraft))

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

            return File(downloadBytes, XlsxContentType, "PIN_INITIATIVE_" + date + month + year + "_" + hour + ":" + minute + ".xlsx");
        }
        [HttpGet]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                InitiativeViewModel result = _service.Delete(guid, UserName);
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