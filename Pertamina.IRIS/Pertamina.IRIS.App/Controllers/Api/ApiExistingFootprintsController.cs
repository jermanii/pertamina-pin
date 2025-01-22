using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ApiExistingFootprintsController : BaseController
    {
        //private readonly IDropDownListService _service;
        private readonly IExistingFootprintService _service;
        public ApiExistingFootprintsController(IIdamanService idamanService, IExistingFootprintService service) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #region Grid
        [Route("List")]
        [HttpPost]
        public async Task<IActionResult> List(string entitasPertaminaId, string projectTypeId, string totalAssetsMinOp, string totalAssetsMaxOp, string revenueMinOp, string revenueMaxOp, string ebitdaMinOp, string ebitdaMaxOp, string yearOp, string partnerCountryId)
        {
            try
            {
                var request = GetDataTableComponent();
                ExistingFootprintViewModel existingFootprint = new ExistingFootprintViewModel();

                var result = await _service.GetListPaging(request, existingFootprint);

                if (result.IsSuccess)
                {
                    var jsonData = new
                    {
                        draw = request.Draw,
                        recordsFiltered = result.RecordsFiltered,
                        recordsTotal = result.RecordsTotal,
                        data = result.Data,
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
        public async Task<IActionResult> ListEvent(string entitasPertaminaId, string projectTypeId, string totalAssetsMinOp, string totalAssetsMaxOp, string revenueMinOp, string revenueMaxOp, string ebitdaMinOp, string ebitdaMaxOp, string yearOp, string partnerCountryId)
        {
            try
            {
                var request = GetDataTableComponent();

                ExistingFootprintViewModel decodeModel = new ExistingFootprintViewModel();
                if (entitasPertaminaId != null)
                {
                    decodeModel.EntitasIdDecode = Encoding.UTF8.GetString(Convert.FromBase64String(entitasPertaminaId));
                }
                if (projectTypeId != null)
                {
                    decodeModel.ProjectTypeIdDecode = Encoding.UTF8.GetString(Convert.FromBase64String(projectTypeId));
                }
                if (totalAssetsMinOp != null)
                {
                    decodeModel.TotalAssetMinDecode= Encoding.UTF8.GetString(Convert.FromBase64String(totalAssetsMinOp));
                    if (decimal.TryParse(decodeModel.TotalAssetMinDecode, out decimal resultTotalAssets))
                    {
                        decodeModel.TotalAssetMin =resultTotalAssets;
                    }
                }
                if (totalAssetsMaxOp != null)
                {
                    decodeModel.TotalAssetMaxDecode= Encoding.UTF8.GetString(Convert.FromBase64String(totalAssetsMaxOp));
                    if (decimal.TryParse(decodeModel.TotalAssetMaxDecode, out decimal resultTotalAssets))
                    {
                        decodeModel.TotalAssetMax =resultTotalAssets;
                    }
                }
                if (revenueMinOp != null)
                {
                    decodeModel.RevenueMinDecode= Encoding.UTF8.GetString(Convert.FromBase64String(revenueMinOp));
                    if (decimal.TryParse(decodeModel.RevenueMinDecode, out decimal resultRevenue))
                    {
                        decodeModel.RevenueMin = resultRevenue;
                    }
                }
                if (revenueMaxOp != null)
                {
                    decodeModel.RevenueMaxDecode= Encoding.UTF8.GetString(Convert.FromBase64String(revenueMaxOp));
                    if (decimal.TryParse(decodeModel.RevenueMaxDecode, out decimal resultRevenue))
                    {
                        decodeModel.RevenueMax = resultRevenue;
                    }
                }
                if (ebitdaMinOp != null)
                {
                    decodeModel.EbitdaMinDecode= Encoding.UTF8.GetString(Convert.FromBase64String(ebitdaMinOp));
                    if (decimal.TryParse(decodeModel.EbitdaMinDecode, out decimal resultEbitda))
                    {
                        decodeModel.EbitdaMin = resultEbitda;
                    }
                }
                if (ebitdaMaxOp != null)
                {
                    decodeModel.EbitdaMaxDecode= Encoding.UTF8.GetString(Convert.FromBase64String(ebitdaMaxOp));
                    if (decimal.TryParse(decodeModel.EbitdaMaxDecode, out decimal resultEbitda))
                    {
                        decodeModel.EbitdaMax = resultEbitda;
                    }
                }
                if (yearOp != null)
                {
                    decodeModel.YearDecode= Encoding.UTF8.GetString(Convert.FromBase64String(yearOp));
                }
                if (partnerCountryId!= null)
                {
                    decodeModel.PartnerCountryIdDecode = Encoding.UTF8.GetString(Convert.FromBase64String(partnerCountryId));
                }
               



                var result = await _service.GetListPaging(request, decodeModel);

                if (result.IsSuccess)
                {
                    var jsonData = new
                    {
                        draw = request.Draw,
                        recordsFiltered = result.RecordsFiltered,
                        recordsTotal = result.RecordsTotal,
                        data = result.Data,
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
        [Route("ListOperatingMetricById")]
        [HttpPost]
        public async Task<IActionResult> ListOperatingMetricById(Guid guid)
        {
            try
            {
                var request = GetDataTableComponent();
                var result = await _service.GetListPagingOpenMetricById(request, guid);

                if (result.IsSuccess)
                {
                    var jsonData = new
                    {
                        draw = request.Draw,
                        recordsFiltered = result.RecordsFiltered,
                        recordsTotal = result.RecordsTotal,
                        data = result.Data,
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
        public async Task<IActionResult> GetCount(ExistingFootprintViewModel model)
        {
            try
            {
                ExistingFootprintViewModel result = new ExistingFootprintViewModel();
                //result = await _service.GetOpportunityCountWithFilter(model);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
        [Route("GetCountSearch")]
        [HttpPost]
        public async Task<IActionResult> GetCountSearch(ExistingFootprintViewModel model)
        {
            try
            {
                ExistingFootprintViewModel result = new ExistingFootprintViewModel();
                //result = await _service.GetOpportunityCountWithFilter(model, model.StrSearch);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
        #endregion
        #region Get By Id

        [Route("GetHubHeadByHubId")]
        [HttpGet]
        public async Task<IActionResult> GetHubHeadByHubId(Guid guid)
        {
            try
            {
                var result =await _service.GetHubHeadByHubId(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetExistingFootprintsById")]
        [HttpGet]
        public async Task<IActionResult> GetExistingFootprintsById(Guid guid, Guid levelId)
        {
            try
            {
                var result = await _service.GetExistingFootprintsById(guid, levelId);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetYearOperatingMetricById")]
        [HttpGet]
        public IActionResult GetYearOperatingMetricById(Guid guid, int year)
        {
            try
            {
                var result =  _service.GetYearOperatingMetricById(guid, year);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        #endregion
    }
}
