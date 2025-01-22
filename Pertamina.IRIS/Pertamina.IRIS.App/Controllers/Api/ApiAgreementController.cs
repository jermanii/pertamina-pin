using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAgreementController : BaseController
    {
        private readonly IAgreementService _service;

        public ApiAgreementController(IIdamanService idamanService, IAgreementService service) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Route("List")]
        [HttpPost]
        public async Task<ActionResult> List(string entitasPertamina, string streamBusiness, string jenisPerjanjian, string negaraMitra, string kawasanMitra, string statusBerlaku, string statusDiscussion, string agreementHolder, string rangeTanggalTtd, string rangeTanggalBerakhir, string rangeCreateDate, bool isDraft, bool isG2G, bool isStrategicPartnerShip, bool isBdCoreBusiness, bool isGreenBusiness)
        {
            try
            {
                var request = GetDataTableComponent();
                

               AgreementViewModel decodeModel = new AgreementViewModel();

                if (entitasPertamina != null)
                    decodeModel.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(entitasPertamina));
                if (streamBusiness != null)
                    decodeModel.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(streamBusiness));
                if (jenisPerjanjian != null)
                    decodeModel.JenisPerjanjianDecode = Encoding.UTF8.GetString(Convert.FromBase64String(jenisPerjanjian));
                if (negaraMitra != null)
                    decodeModel.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(negaraMitra));
                if (kawasanMitra != null)
                    decodeModel.KawasanMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(kawasanMitra));
                if (statusBerlaku != null)
                    decodeModel.StatusBerlakuDecode = Encoding.UTF8.GetString(Convert.FromBase64String(statusBerlaku));
                if (statusDiscussion != null)
                    decodeModel.StatusDiscussionDecode = Encoding.UTF8.GetString(Convert.FromBase64String(statusDiscussion));
                if (agreementHolder != null)
                    decodeModel.AgreementHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(agreementHolder));
                if (rangeTanggalTtd != null)
                    decodeModel.RangeTanggalTtdDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeTanggalTtd));
                if (rangeTanggalBerakhir != null)
                    decodeModel.RangeTanggalBerakhirDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeTanggalBerakhir));
                if (rangeCreateDate != null)
                    decodeModel.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeCreateDate));
                decodeModel.IsDraft = isDraft;
                decodeModel.IsGtg = isG2G;
                decodeModel.IsBdCoreBusinessDecode = isBdCoreBusiness;
                decodeModel.IsStrategicPartnerShipDecode = isStrategicPartnerShip;
                decodeModel.IsGreenBusinessDecode = isGreenBusiness;

                decodeModel.CreateBy = UserName;
                var result = await _service.GetListPaging(request, decodeModel);

                List<AgreementViewModel> datas = result.Data;

                List<AgreementViewModel> records = new List<AgreementViewModel>();

                foreach (var rec in datas)
                {
                    records.Add(_service.GenerelizeDataAgreement(rec));
                }

                if (result.IsSuccess)
                {
                    var jsonData = new
                    {
                        draw = request.Draw,
                        recordsFiltered = result.RecordsFiltered,
                        recordsTotal = result.RecordsTotal,
                        data = records
                    };

                    return Json(jsonData);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }

        [Route("ListEvent")]
        [HttpPost]
        public async Task<IActionResult> ListEvent(string entitasPertamina, string streamBusiness, string jenisPerjanjian, string negaraMitra, string kawasanMitra, string statusBerlaku, string statusDiscussion, string agreementHolder, string rangeTanggalTtd, string rangeTanggalBerakhir, string rangeCreateDate, bool isDraft, bool isG2G, bool isStrategicPartnerShip, bool isBdCoreBusiness, bool isGreenBusiness)
        {
            try
            {
                var request = GetDataTableComponent();

                AgreementViewModel decodeModel = new AgreementViewModel();

                if (entitasPertamina != null)
                    decodeModel.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(entitasPertamina));
                if (streamBusiness != null)
                    decodeModel.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(streamBusiness));
                if (jenisPerjanjian != null)
                    decodeModel.JenisPerjanjianDecode = Encoding.UTF8.GetString(Convert.FromBase64String(jenisPerjanjian));
                if (negaraMitra != null)
                    decodeModel.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(negaraMitra));
                if (kawasanMitra != null)
                    decodeModel.KawasanMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(kawasanMitra));
                if (statusBerlaku != null)
                    decodeModel.StatusBerlakuDecode = Encoding.UTF8.GetString(Convert.FromBase64String(statusBerlaku));
                if (statusDiscussion != null)
                    decodeModel.StatusDiscussionDecode = Encoding.UTF8.GetString(Convert.FromBase64String(statusDiscussion));
                if (agreementHolder != null)
                    decodeModel.AgreementHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(agreementHolder));
                if (rangeTanggalTtd != null)
                    decodeModel.RangeTanggalTtdDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeTanggalTtd));
                if (rangeTanggalBerakhir != null)
                    decodeModel.RangeTanggalBerakhirDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeTanggalBerakhir));
                if (rangeCreateDate != null)
                    decodeModel.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeCreateDate));
                decodeModel.IsDraft = isDraft;
                decodeModel.IsGtg = isG2G;
                decodeModel.IsBdCoreBusinessDecode = isBdCoreBusiness;
                decodeModel.IsStrategicPartnerShipDecode = isStrategicPartnerShip;
                decodeModel.IsGreenBusinessDecode = isGreenBusiness;

                decodeModel.CreateBy = UserName;
                var result = await _service.GetListPaging(request, decodeModel);

                List<AgreementViewModel> datas = result.Data;

                List<AgreementViewModel> records = new List<AgreementViewModel>();

                foreach (var rec in datas)
                {
                    records.Add(_service.GenerelizeDataAgreement(rec));
                }

                if (result.IsSuccess)
                {
                    var jsonData = new
                    {
                        draw = request.Draw,
                        recordsFiltered = result.RecordsFiltered,
                        recordsTotal = result.RecordsTotal,
                        data = records
                    };

                    return Json(jsonData);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }

        [Route("GetCount")]
        [HttpPost]
        public async Task<IActionResult> GetCount(AgreementViewModel model)
        {
            try
            {
                if (model.EntitasPertaminaEncode != null)
                    model.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.EntitasPertaminaEncode));
                if (model.StreamBusinessEncode != null)
                    model.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StreamBusinessEncode));
                if (model.JenisPerjanjianEncode != null)
                    model.JenisPerjanjianDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.JenisPerjanjianEncode));
                if (model.NegaraMitraEncode != null)
                    model.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.NegaraMitraEncode));
                if (model.KawasanMitraEncode != null)
                    model.KawasanMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.KawasanMitraEncode));
                if (model.StatusBerlakuEncode != null)
                    model.StatusBerlakuDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StatusBerlakuEncode));
                if (model.StatusDiscussionEncode != null)
                    model.StatusDiscussionDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StatusDiscussionEncode));
                if (model.AgreementHolderEncode != null)
                    model.AgreementHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.AgreementHolderEncode));
                if (model.RangeTanggalTtdEncode != null)
                    model.RangeTanggalTtdDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.RangeTanggalTtdEncode));
                if (model.RangeCreateDateEncode != null)
                    model.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.RangeCreateDateEncode));

                AgreementViewModel result = new AgreementViewModel();
                result = await _service.GetAgreementCountWithFilter(model);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }

        [Route("GetCountSearch")]
        [HttpPost]
        public async Task<IActionResult> GetCountSearch(AgreementViewModel model)
        {
            try
            {
                if (model.EntitasPertaminaEncode != null)
                    model.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.EntitasPertaminaEncode));
                if (model.StreamBusinessEncode != null)
                    model.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StreamBusinessEncode));
                if (model.JenisPerjanjianEncode != null)
                    model.JenisPerjanjianDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.JenisPerjanjianEncode));
                if (model.NegaraMitraEncode != null)
                    model.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.NegaraMitraEncode));
                if (model.KawasanMitraEncode != null)
                    model.KawasanMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.KawasanMitraEncode));
                if (model.StatusBerlakuEncode != null)
                    model.StatusBerlakuDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StatusBerlakuEncode));
                if (model.StatusDiscussionEncode != null)
                    model.StatusDiscussionDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StatusDiscussionEncode));
                if (model.AgreementHolderEncode != null)
                    model.AgreementHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.AgreementHolderEncode));
                if (model.RangeTanggalTtdEncode != null)
                    model.RangeTanggalTtdDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.RangeTanggalTtdEncode));
                if (model.RangeTanggalBerakhirEncode != null)
                    model.RangeTanggalBerakhirDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.RangeTanggalBerakhirEncode));
                if (model.RangeCreateDateEncode != null)
                    model.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.RangeCreateDateEncode));

                AgreementViewModel result = new AgreementViewModel();
                result = await _service.GetAgreementCountWithFilter(model, model.StrSearch);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }

        [Route("StatusBerlakuCounting")]
        [HttpGet]
        public async Task<IActionResult> StatusBerlakuCounting(DateTime endDate)
        {
            try
            {
                StatusBerlakuViewModel model = await _service.StatusBerlakuCounting(endDate);
                var result = model;
                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Route("GetInitiativeById")]
        [HttpGet]
        public async Task<IActionResult> GetInitiativeById(Guid guid)
        {
            try
            {

                InitiativeViewModel model = await _service.GetInitiativeAsyncById(guid);
                model.Id = guid;
                var result = model;
                return Json(new { items = result });

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Route("GetOpportunityById")]
        [HttpGet]
        public async Task<IActionResult> GetOpportunityById(Guid guid)
        {
            try
            {

                OpportunityViewModel model = await _service.GetOpportunityAsyncById(guid);
                model.Id = guid;
                var result = model;
                return Json(new { items = result });

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Route("GetAgreementById")]
        [HttpGet]
        public async Task<IActionResult> GetAgreementById(Guid guid)
        {
            try
            {
                AgreementViewModel model = await _service.GetAgreementAsyncById(guid);
                model.Id = guid;
                var result = model;
                return Json(new { items = result });

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        public async Task<IActionResult> GetAgById(Guid guid)
        {
            try
            {
                AgreementViewModel model = await _service.GetAgreementAsyncById(guid);
                model.Id = guid;
                var result = model;
                return Json(new { items = result });

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Route("CheckCompanyAgreementById")]
        [HttpGet]
        public async Task<IActionResult> CheckCompanyAgreementById(Guid guid, string encodedFilters)
        {
            try
            {
                TempData["EncodedFilters"] = encodedFilters;
                AgreementViewModel model = await _service.GetAgreementAsyncById(guid);
                if (model.CompanyCode == ProfileSession.companyCode || RolesSession.Contains(EnumBaseModel.RoleOwner))
                {
                    model.IsError = false;
                    return Json(new { items = model });
                }
                else
                {
                    model.IsError = true;
                    model.ErrorMessage = "No Authorize";
                    return Json(new { items = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Route("GetHubHeadByHubId")]
        [HttpGet]
        public async Task<IActionResult> GetHubHeadByHubId(Guid guid)
        {
            try
            {
                var result = await _service.GetHubHeadByHubId(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Route("GetAdendumSequence")]
        [HttpGet]
        public async Task<IActionResult> GetAdendumSequence(int sequence, Guid guid)
        {
            try
            {
                bool addRepeater = true;
                var result = await _service.GetAdendumSequence(guid, sequence, addRepeater);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Route("GetRedirectViewFilter")]
        [HttpGet]
        public async Task<IActionResult> GetRedirectViewFilter(Guid guid, string encodedFilters)
        {
            try
            {
                TempData["EncodedFilters"] = encodedFilters;
                var url = $"/Agreement/Read?guid={guid}";
                return Json(new { redirectUrl= url });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}
