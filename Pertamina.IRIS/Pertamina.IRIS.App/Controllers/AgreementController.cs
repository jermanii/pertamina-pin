using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class AgreementController : WebControllerBase
    {
        private readonly IAgreementService _service;

        public AgreementController(IIdamanService idamanService, IAgreementService service) : base(idamanService)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            AgreementViewModel model = new AgreementViewModel();

            try
            {
                model = _service.GetAgreementCount();

                if (TempData["EncodedFilters"] != null && !string.IsNullOrEmpty(TempData["EncodedFilters"].ToString()))
                {
                    ViewData["EncodedFilters"] = TempData["EncodedFilters"].ToString();
                }

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
                AgreementViewModel agreement = new AgreementViewModel();
                agreement = _service.CreatePicFunction();
                return View(agreement);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return View("Error", error);
            }
        }
        [HttpPost]
        public IActionResult Add(AgreementViewModel model)
        {
            try
            {
                var isValidation = _service.IsValidation(model);
                if (isValidation.IsError == true)
                {
                    return Json(isValidation);
                }
                model = _service.Save(model, UserName, ProfileSession.companyCode);

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
                AgreementViewModel model = _service.GetAgreementById(guid);
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
        public IActionResult Update(Guid guid)
        {
            try
            {
                AgreementViewModel model = _service.GetAgreementById(guid);
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
        [HttpPost]
        public IActionResult UpdateForm(AgreementViewModel model)
        {
            try
            {
                var isValidation = _service.IsValidation(model);
                if (isValidation.IsError == true)
                {
                    return Json(isValidation);
                }
                AgreementViewModel agreementModel = _service.GetAgreementById(model.Id);
                model.IsDraft = agreementModel.IsDraft;
                model.KodeAgreement = agreementModel.KodeAgreement;
                AgreementViewModel result = _service.Update(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
        [HttpPost]
        public IActionResult SaveDraft(AgreementViewModel model)
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
        public IActionResult DownloadData(string searchQuery, string statusBerlaku, string statusDiscussion, string agreementHolder, string entitasPertamina, string kawasanMitra, string negaraMitra, string jenisPerjanjian, string streamBusiness, string tanggalTtd, string tanggalBerakhir, string rangeCreateDate, bool isDraft, bool isG2G, bool isStrategicPartnerShip, bool isBdCoreBusiness, bool isGreenBusiness)

        {
            byte[] downloadBytes;

            using (var package = _service.ExportXLS(true, searchQuery, statusBerlaku, statusDiscussion, agreementHolder, entitasPertamina, kawasanMitra, negaraMitra, jenisPerjanjian, streamBusiness, rangeCreateDate, tanggalTtd, tanggalBerakhir, isDraft, isG2G, isStrategicPartnerShip, isBdCoreBusiness, isGreenBusiness))

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

            return File(downloadBytes, XlsxContentType, "PIN_AGREEMENT_" + date + month + year + "_" + hour + ":" + minute + ".xlsx");
        }
        [HttpGet]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                AgreementViewModel result = _service.Delete(guid, UserName);
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