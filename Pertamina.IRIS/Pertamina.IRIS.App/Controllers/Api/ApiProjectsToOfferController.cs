using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
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
    public class ApiProjectsToOfferController : BaseController
    {
        private readonly IProjectsToOfferService _service;

        public ApiProjectsToOfferController(IIdamanService idamanService, IProjectsToOfferService service) : base(idamanService)
        {
            _service = service;
        }

        [Route("List")]
        [HttpPost]
        public async Task<IActionResult> List(string entitasPertamina, string streamBusiness, string negaraMitra, string opportunityHolder, string rangeCreateDate, bool isDraft)
        {
            try
            {
                var request = GetDataTableComponent();

                OpportunityViewModel decodeModel = new OpportunityViewModel();

                if (entitasPertamina != null)
                    decodeModel.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(entitasPertamina));
                if (streamBusiness != null)
                    decodeModel.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(streamBusiness));
                if (negaraMitra != null)
                    decodeModel.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(negaraMitra));
                if (opportunityHolder != null)
                    decodeModel.OpportunityHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(opportunityHolder));
                if (rangeCreateDate != null)
                    decodeModel.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeCreateDate));
                decodeModel.IsDraft = isDraft;

                var result = await _service.GetListPaging(request, decodeModel);

                List<OpportunityViewModel> datas = result.Data;

                List<OpportunityViewModel> records = new List<OpportunityViewModel>();

                foreach (var rec in datas)
                {
                    records.Add(_service.GenerelizeDataOpportunity(rec));
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
        public async Task<IActionResult> ListEvent(string entitasPertamina, string streamBusiness, string negaraMitra, string opportunityHolder, string rangeCreateDate, bool isDraft)
        {
            try
            {
                var request = GetDataTableComponent();

                OpportunityViewModel decodeModel = new OpportunityViewModel();

                if (entitasPertamina != null)
                    decodeModel.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(entitasPertamina));
                if (streamBusiness != null)
                    decodeModel.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(streamBusiness));
                if (negaraMitra != null)
                    decodeModel.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(negaraMitra));
                if (opportunityHolder != null)
                    decodeModel.OpportunityHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(opportunityHolder));
                if (rangeCreateDate != null)
                    decodeModel.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeCreateDate));
                decodeModel.IsDraft = isDraft;

                var result = await _service.GetListPaging(request, decodeModel);

                List<OpportunityViewModel> datas = result.Data;

                List<OpportunityViewModel> records = new List<OpportunityViewModel>();

                foreach (var rec in datas)
                {
                    records.Add(_service.GenerelizeDataOpportunity(rec));
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
        public async Task<IActionResult> GetCount(OpportunityViewModel model)
        {
            try
            {
                if (model.EntitasPertaminaEncode != null)
                    model.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.EntitasPertaminaEncode));
                if (model.StreamBusinessEncode != null)
                    model.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StreamBusinessEncode));
                if (model.NegaraMitraEncode != null)
                    model.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.NegaraMitraEncode));
                if (model.OpportunityHolderEncode != null)
                    model.OpportunityHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.OpportunityHolderEncode));
                if (model.RangeCreateDateEncode != null)
                    model.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.RangeCreateDateEncode));

                OpportunityViewModel result = new OpportunityViewModel();
                result = await _service.GetOpportunityCountWithFilter(model);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
        [Route("GetCountSearch")]
        [HttpPost]
        public async Task<IActionResult> GetCountSearch(OpportunityViewModel model)
        {
            try
            {
                if (model.EntitasPertaminaEncode != null)
                    model.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.EntitasPertaminaEncode));
                if (model.StreamBusinessEncode != null)
                    model.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StreamBusinessEncode));
                if (model.NegaraMitraEncode != null)
                    model.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.NegaraMitraEncode));
                if (model.OpportunityHolderEncode != null)
                    model.OpportunityHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.OpportunityHolderEncode));
                if (model.RangeCreateDateEncode != null)
                    model.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.RangeCreateDateEncode));

                OpportunityViewModel result = new OpportunityViewModel();
                result = await _service.GetOpportunityCountWithFilter(model, model.StrSearch);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
        [Route("GetOpportunityById")]
        [HttpGet]
        public async Task<IActionResult> GetOpportunityById(Guid guid)
        {
            try
            {
                OpportunityViewModel model = await _service.GetOpportunityByIdAsync(guid);
                model.Id = guid;
                var result = model;
                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("CheckCompanyOpportunityById")]
        [HttpGet]
        public async Task<IActionResult> CheckCompanyOpportunityById(Guid guid)
        {
            try
            {
                OpportunityViewModel model = await _service.GetOpportunityByIdAsync(guid);
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
    }
}