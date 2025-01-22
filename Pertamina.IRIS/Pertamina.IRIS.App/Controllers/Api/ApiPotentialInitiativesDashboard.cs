using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Pertamina.IRIS.App.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPotentialInitiativesDashboard : BaseController
    {
        private readonly IPotentialInitiativesDashboardService _service;

        public ApiPotentialInitiativesDashboard(IIdamanService idamanService, IPotentialInitiativesDashboardService service) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Route("List")]
        [HttpPost]
        public async Task<ActionResult> List(string entitasPertamina, string streamBusiness, string negaraMitra, string kawasanMitra, string initiativeHolder, string initiativeStage, string initiativeStatus, string rangeCreateDate, bool isDraft)
        {
            try
            {
                var request = GetDataTableComponent();

                InitiativeViewModel decodeModel = new InitiativeViewModel();

                if (entitasPertamina != null)
                    decodeModel.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(entitasPertamina));
                if (initiativeStage != null)
                    decodeModel.InitiativeStageDecode = Encoding.UTF8.GetString(Convert.FromBase64String(initiativeStage));
                if (initiativeStatus != null)
                    decodeModel.InitiativeStatusDecode = Encoding.UTF8.GetString(Convert.FromBase64String(initiativeStatus));
                if (streamBusiness != null)
                    decodeModel.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(streamBusiness));
                if (negaraMitra != null)
                    decodeModel.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(negaraMitra));
                if (kawasanMitra != null)
                    decodeModel.KawasanMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(kawasanMitra));
                if (initiativeHolder != null)
                    decodeModel.InitiativeHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(initiativeHolder));
                if (rangeCreateDate != null)
                    decodeModel.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(rangeCreateDate));
                decodeModel.IsDraft = isDraft;

                var result = await _service.GetListPaging(request, decodeModel);

                List<InitiativeViewModel> datas = result.Data;

                List<InitiativeViewModel> records = new List<InitiativeViewModel>();

                foreach (var rec in datas)
                {
                    records.Add(_service.GenerelizeDataInitiative(rec));
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

        [Route("GetCountSearch")]
        [HttpPost]
        public async Task<IActionResult> GetCountSearch(InitiativeViewModel model)
        {
            try
            {
                if (model.EntitasPertaminaEncode != null)
                    model.EntitasPertaminaDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.EntitasPertaminaEncode));
                if (model.StreamBusinessEncode != null)
                    model.StreamBusinessDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.StreamBusinessEncode));
                if (model.NegaraMitraEncode != null)
                    model.NegaraMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.NegaraMitraEncode));
                if (model.KawasanMitraEncode != null)
                    model.KawasanMitraDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.KawasanMitraEncode));
                if (model.InitiativeHolderEncode != null)
                    model.InitiativeHolderDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.InitiativeHolderEncode));
                if (model.InitiativeStageEncode != null)
                    model.InitiativeStageDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.InitiativeStageEncode));
                if (model.InitiativeStatusEncode != null)
                    model.InitiativeStatusDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.InitiativeStatusEncode));
                if (model.RangeCreateDateEncode != null)
                    model.RangeCreateDateDecode = Encoding.UTF8.GetString(Convert.FromBase64String(model.RangeCreateDateEncode));

                InitiativeViewModel result = new InitiativeViewModel();
                result = await _service.GetInitiativeCountWithFilter(model, model.StrSearch);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }

        [Route("GetCountriesMap")]
        [HttpGet]
        public async Task<IActionResult> GetCountriesMap()
        {
            try
            {
                PotentialInitiativesDashboardMapViewModel result = new PotentialInitiativesDashboardMapViewModel();

                result = await _service.GetCountriesMap();

                return Json(result);
            }
            catch (Exception ex)
            {
                return Ok(new { error = ex.Message });
            }
        }
    }
}
