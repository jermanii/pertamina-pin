using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiDropDownListController : WebControllerBase
    {
        private readonly IDropDownListService _service;
        public ApiDropDownListController(IIdamanService idamanService, IDropDownListService service) : base(idamanService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        #region Get All
        [Route("DdlHsh")]
        [HttpGet]
        public IActionResult DdlHsh(string query)
        {
            try
            {
                var result = _service.DdlHsh(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlEntitasPertamina")]
        [HttpGet]
        public IActionResult DdlEntitasPertamina(string query)
        {
            try
            {
                var result = _service.DdlEntitasPertamina(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlEntitasPertaminaGrid")]
        [HttpGet]
        public IActionResult DdlEntitasPertaminaGrid(string query)
        {
            try
            {
                var result = _service.DdlEntitasPertaminaGrid(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlProvinsiIndonesia")]
        [HttpGet]
        public IActionResult DdlProvinsiIndonesia(string query)
        {
            try
            {
                var result = _service.DdlProvinsiIndonesia(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlFungsi")]
        [HttpGet]
        public IActionResult DdlFungsi(string query)
        {
            try
            {
                var result = _service.DdlFungsi(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }        
        [Route("DdlStreamBusiness")]
        [HttpGet]
        public IActionResult DdlStreamBusiness(string query)
        {
            try
            {
                var result = _service.DdlStreamBusiness(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlInterest")]
        [HttpGet]
        public IActionResult DdlInterest(string query)
        {
            try
            {
                var result = _service.DdlInterest(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlInitiativeStatus")]
        [HttpGet]
        public IActionResult DdlInitiativeStatus(string query)
        {
            try
            {
                var result = _service.DdlInitiativeStatus(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlInitiativeTypes")]
        [HttpGet]
        public IActionResult DdlInitiativeTypes(string query)
        {
            try
            {
                var result = _service.DdlInitiativeTypes(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlInitiativeStages")]
        [HttpGet]
        public IActionResult DdlInitiativeStages(string query)
        {
            try
            {
                var result = _service.DdlInitiativeStages(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlKawasanMitra")]
        [HttpGet]
        public IActionResult DdlKawasanMitra(string query)
        {
            try
            {
                var result = _service.DdlKawasanMitra(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlNegaraMitra")]
        [HttpGet]
        public IActionResult DdlNegaraMitra(string query)
        {
            try
            {
                var result = _service.DdlNegaraMitra(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlNegaraMitraOnlyNegara")]
        [HttpGet]
        public IActionResult DdlNegaraMitraOnlyNegara(string query)
        {
            try
            {
                var result = _service.DdlNegaraMitraOnlyNegara(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlNegaraMitraWithoutKawasan")]
        [HttpGet]
        public IActionResult DdlNegaraMitraWithoutKawasan(string query)
        {
            try
            {
                var result = _service.DdlNegaraMitraWithoutKawasan(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlPic")]
        [HttpGet]
        public IActionResult DdlPic(string query)
        {
            try
            {
                var result = _service.DdlPic(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlPicLevel")]
        [HttpGet]
        public IActionResult DdlPicLevel(string query)
        {
            try
            {
                var result = _service.DdlPicLevel(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlHubHead")]
        [HttpGet]
        public IActionResult DdlHubHead(string query)
        {
            try
            {
                var result = _service.DdlHubHead(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlHub")]
        [HttpGet]
        public IActionResult DdlHub(string query)
        {
            try
            {
                var result = _service.DdlHub(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlHshEntitasPertamina")]
        [HttpGet]
        public IActionResult DdlHshEntitasPertamina(string query)
        {
            try
            {
                var result = _service.DdlHshEntitasPertamina(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlRelatedAgreement")]
        [HttpGet]
        public IActionResult DdlRelatedAgreement(string query)
        {
            try
            {
                var result = _service.DdlRelatedAgreement(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlOpportunityEntitasPertamina")]
        [HttpGet]
        public IActionResult DdlOpportunityEntitasPertamina(string query)
        {
            try
            {
                var result = _service.DdlOpportunityEntitasPertamina(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlOpportunityPic")]
        [HttpGet]
        public IActionResult DdlOpportunityPic(string query)
        {
            try
            {
                var result = _service.DdlOpportunityPic(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlOpportunityKesiapanProyek")]
        [HttpGet]
        public IActionResult DdlOpportunityKesiapanProyek(string query)
        {
            try
            {
                var result = _service.DdlOpportunityKesiapanProyek(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlOpportunityNegara")]
        [HttpGet]
        public IActionResult DdlOpportunityNegara(string query)
        {
            try
            {
                var result = _service.DdlOpportunityNegara(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlOpportunityTargetMitra")]
        [HttpGet]
        public IActionResult DdlOpportunityTargetMitra(string query)
        {
            try
            {
                var result = _service.DdlOpportunityTargetMitra(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlStatusBerlaku")]
        [HttpGet]
        public IActionResult DdlStatusBerlaku(string query)
        {
            try
            {
                var result = _service.DdlStatusBerlaku(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlTrafficLight")]
        [HttpGet]
        public IActionResult DdlTrafficLight(string query)
        {
            try
            {
                var result = _service.DdlTrafficLight(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlInitiativeForm")]
        [HttpGet]
        public IActionResult DdlInitiativeForm(string query)
        {
            try
            {
                var result = _service.DdlInitiativeForm(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlAgreementForm")]
        [HttpGet]
        public IActionResult DdlAgreementForm(string query)
        {
            try
            {
                var result = _service.DdlAgreementForm(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlNamaProyek")]
        [HttpGet]
        public IActionResult DdlNamaProyek(string query, string value)
        {
            try
            {
                var result = _service.DdlNamaProyek(query, value);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlNamaProyekInitiative")]
        [HttpGet]
        public IActionResult DdlNamaProyekInitiative(string query, string value)
        {
            try
            {
                var result = _service.DdlNamaProyekInitiative(query, value);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlStatusDiscussion")]
        [HttpGet]
        public IActionResult DdlStatusDiscussion(string query)
        {
            try
            {
                var result = _service.DdlStatusDiscussion(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlJenisPerjanjian")]
        [HttpGet]
        public IActionResult DdlJenisPerjanjian(string query)
        {
            try
            {
                var result = _service.DdlJenisPerjanjian(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlFaktorKendala")]
        [HttpGet]
        public IActionResult DdlFaktorKendala(string query)
        {
            try
            {
                var result = _service.DdlFaktorKendala(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlKlasifikasiKendala")]
        [HttpGet]
        public IActionResult DdlKlasifikasiKendala(string query)
        {
            try
            {
                var result = _service.DdlKlasifikasiKendala(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlKeterlibatanLembaga")]
        [HttpGet]
        public IActionResult DdlKeterlibatanLembaga(string query)
        {
            try
            {
                var result = _service.DdlKeterlibatanLembaga(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlYear")]
        [HttpGet]
        public IActionResult DdlYear(string query)
        {
            try
            {
                var result = _service.DdlYear(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlExistingFootprintsSortingHighPriority")]
        [HttpGet]
        public IActionResult DdlExistingFootprintsSortingHighPriority(string query)
        {
            try
            {
                var result = _service.DdlExistingFootprintsSortingHighPriority(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("ddlSignedAgreementSortingHighPriority")]
        [HttpGet]
        public IActionResult DdlSignedAgreementSortingHighPriority(string query)
        {
            try
            {
                var result = _service.DdlSignedAgreementSortingHighPriority(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        #endregion

        #region Get By Id
        [Route("GetDdlHsh")]
        [HttpGet]
        public IActionResult GetDdlHsh(Guid guid)
        {
            try
            {
                var result = _service.GetDdlHsh(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlEntitasPertamina")]
        [HttpGet]
        public IActionResult GetDdlEntitasPertamina(Guid guid)
        {
            try
            {
                var result = _service.GetDdlEntitasPertamina(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlHub")]
        [HttpGet]
        public IActionResult GetDdlHub(Guid guid)
        {
            try
            {
                var result = _service.GetDdlHub(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlOpportunityStreamBusiness")]
        [HttpGet]
        public IActionResult GetDdlOpportunityStreamBusiness(Guid guid)
        {
            try
            {
                var result = _service.GetDdlOpportunityStreamBusiness(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlFungsi")]
        [HttpGet]
        public IActionResult GetDdlFungsi(Guid guid)
        {
            try
            {
                var result = _service.GetDdlFungsi(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlPicLevel")]
        [HttpGet]
        public IActionResult GetDdlPicLevel(Guid guid)
        {
            try
            {
                var result = _service.GetDdlPicLevel(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlPicFungsi")]
        [HttpGet]
        public IActionResult GetDdlPicFungsi(Guid guid)
        {
            try
            {
                var result = _service.GetDdlPicFungsi(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlInterest")]
        [HttpGet]
        public IActionResult GetDdlInterest(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInterest(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlInitiativeStatus")]
        [HttpGet]
        public IActionResult GetDdlInitiativeStatus(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInitiativeStatus(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlInitiativeTypes")]
        [HttpGet]
        public IActionResult GetDdlInitiativeTypes(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInitiativeTypes(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlInitiativeStages")]
        [HttpGet]
        public IActionResult GetDdlInitiativeStages(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInitiativeStages(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlKawasanMitra")]
        [HttpGet]
        public IActionResult GetDdlKawasanMitra(Guid guid)
        {
            try
            {
                var result = _service.GetDdlKawasanMitra(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlNegaraMitra")]
        [HttpGet]
        public IActionResult GetDdlNegaraMitra(Guid guid)
        {
            try
            {
                var result = _service.GetDdlNegaraMitra(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        } 
        [Route("GetDdlNegaraMitraOnlyNegara")]
        [HttpGet]
        public IActionResult GetDdlNegaraMitraOnlyNegara(Guid guid)
        {
            try
            {
                var result = _service.GetDdlNegaraMitraOnlyNegara(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        
        [Route("GetDdlRelatedAgreement")]
        [HttpGet]
        public IActionResult GetDdlRelatedAgreement(Guid guid)
        {
            try
            {
                var result = _service.GetDdlRelatedAgreement(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlOpportunity")]
        [HttpGet]
        public IActionResult GetDdlOpportunity(Guid guid)
        {
            try
            {
                var result = _service.GetDdlOpportunity(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlStreamBusiness")]
        [HttpGet]
        public IActionResult GetDdlStreamBusiness(Guid guid)
        {
            try
            {
                var result = _service.GetDdlStreamBusiness(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlHshEntitasPertamina")]
        [HttpGet]
        public IActionResult GetDdlHshEntitasPertamina(Guid guid)
        {
            try
            {
                var result = _service.GetDdlHshEntitasPertamina(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlOpportunityEntitasPertamina")]
        [HttpGet]
        public IActionResult GetDdlOpportunityEntitasPertamina(Guid guid)
        {
            try
            {
                var result = _service.GetDdlOpportunityEntitasPertamina(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlInitiativeEntitasPertamina")]
        [HttpGet]
        public IActionResult GetDdlInitiativeEntitasPertamina(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInitiativeEntitasPertamina(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlInitiativePicFungsi")]
        [HttpGet]
        public IActionResult GetDdlInitiativePicFungsi(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInitiativePicFungsi(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlInitiativeStreamBusiness")]
        [HttpGet]
        public IActionResult GetDdlInitiativeStreamBusiness(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInitiativeStreamBusiness(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlOpportunityPicFungsi")]
        [HttpGet]
        public IActionResult GetDdlOpportunityPicFungsi(Guid guid)
        {
            try
            {
                var result = _service.GetDdlOpportunityPicFungsi(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlOpportunityKesiapanProyek")]
        [HttpGet]
        public IActionResult GetDdlOpportunityKesiapanProyek(Guid guid)
        {
            try
            {
                var result = _service.GetDdlOpportunityKesiapanProyek(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlOpportunityNegara")]
        [HttpGet]
        public IActionResult GetDdlOpportunityNegara(Guid guid)
        {
            try
            {
                var result = _service.GetDdlOpportunityNegara(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlOpportunityTargetMitra")]
        [HttpGet]
        public IActionResult GetDdlOpportunityTargetMitra(Guid guid)
        {
            try
            {
                var result = _service.GetDdlOpportunityTargetMitra(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlInitiativeNegara")]
        [HttpGet]
        public IActionResult GetDdlInitiativeNegara(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInitiativeNegara(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementStreamBusiness")]
        [HttpGet]
        public IActionResult GetDdlAgreementStreamBusiness(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementStreamBusiness(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementPicFungsi")]
        [HttpGet]
        public IActionResult GetDdlAgreementPicFungsi(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementPicFungsi(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementEntitasPertamina")]
        [HttpGet]
        public IActionResult GetDdlAgreementEntitasPertamina(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementEntitasPertamina(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementNegara")]
        [HttpGet]
        public IActionResult GetDdlAgreementNegara(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementNegara(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementJenisPerjanjian")]
        [HttpGet]
        public IActionResult GetDdlAgreementJenisPerjanjian(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementJenisPerjanjian(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementStatusDiscussion")]
        [HttpGet]
        public IActionResult GetDdlAgreementStatusDiscussion(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementStatusDiscussion(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementFaktorKendala")]
        [HttpGet]
        public IActionResult GetDdlAgreementFaktorKendala(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementFaktorKendala(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementTrafficLight")]
        [HttpGet]
        public IActionResult GetDdlAgreementTrafficLight(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementTrafficLight(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementKlasifikasiKendala")]
        [HttpGet]
        public IActionResult GetDdlAgreementKlasifikasiKendala(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementKlasifikasiKendala(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementKeterlibatanLembaga")]
        [HttpGet]
        public IActionResult GetDdlAgreementKeterlibatanLembaga(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementKeterlibatanLembaga(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("GetDdlAgreementRelatedAgreement")]
        [HttpGet]
        public IActionResult GetDdlAgreementRelatedAgreement(Guid guid)
        {
            try
            {
                var result = _service.GetDdlAgreementRelatedAgreement(guid);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlTypeOfStudy")]
        [HttpGet]
        public IActionResult DdlTypeOfStudy(string query)
        {
            try
            {
                var result = _service.DdlTypeOfStudy(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlConfidentiality")]
        [HttpGet]
        public IActionResult DdlConfidentiality(string query)
        {
            try
            {
                var result = _service.DdlConfidentiality(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        [Route("DdlOwnerEntity")]
        [HttpGet]
        public IActionResult DdlOwnerEntity(string query)
        {
            try
            {
                var result = _service.DdlOwnerEntity(query);

                return Json(new { items = result });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [Route("GetDdlInitiativeKeterlibatanLembaga")]
        [HttpGet]
        public IActionResult GetDdlInitiativeKeterlibatanLembaga(Guid guid)
        {
            try
            {
                var result = _service.GetDdlInitiativeKeterlibatanLembaga(guid);

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
