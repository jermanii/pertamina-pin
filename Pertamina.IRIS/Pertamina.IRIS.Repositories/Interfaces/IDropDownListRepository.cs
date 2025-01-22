using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IDropDownListRepository
    {
        IQueryable<Select2BaseModel> DdlHsh(string request);
        IQueryable<Select2BaseModel> DdlEntitasPertamina(string query);
        IQueryable<Select2BaseModel> DdlEntitasPertaminaGrid(string request);
        IQueryable<Select2BaseModel> DdlFungsi(string query);
        IQueryable<Select2BaseModel> DdlStreamBusiness(string request);
        IQueryable<Select2BaseModel> DdlOpportunityEntitasPertamina(string request);
        IQueryable<Select2BaseModel> DdlOpportunityPic(string request);
        IQueryable<Select2BaseModel> DdlOpportunityKesiapanProyek(string request);
        IQueryable<Select2BaseModel> DdlOpportunityNegara(string request);
        IQueryable<Select2BaseModel> DdlOpportunityTargetMitra(string request);
        IQueryable<Select2BaseModel> DdlOpportunity(string request);
        IQueryable<Select2BaseModel> DdlInterest(string request);
        IQueryable<Select2BaseModel> DdlInitiativeStatus(string request);
        IQueryable<Select2BaseModel> DdlInitiativeTypes(string request);
        IQueryable<Select2BaseModel> DdlInitiativeStages(string request);
        IQueryable<Select2BaseModel> DdlKawasanMitra(string request);
        IQueryable<Select2BaseModel> DdlNegaraMitra(string request);
        IQueryable<Select2BaseModel> DdlNegaraMitraOnlyNegara(string request);
        IQueryable<Select2BaseModel> DdlNegaraMitraWithoutKawasan(string request);
        IQueryable<Select2BaseModel> DdlPic(string query);
        IQueryable<Select2BaseModel> DdlPicLevel(string query);
        IQueryable<Select2BaseModel> DdlProvinsiIndonesia(string request);
        IQueryable<Select2BaseModel> DdlHubHead(string query);
        IQueryable<Select2BaseModel> DdlHub(string query);
        IQueryable<Select2BaseModel> DdlRelatedAgreement(string query);
        IQueryable<Select2BaseModel> DdlHshEntitasPertamina(string query);
        IQueryable<Select2BaseModel> DdlStatusBerlaku(string query);
        IQueryable<Select2BaseModel> DdlTrafficLight(string query);
        IQueryable<Select2BaseModel> DdlStatusDiscussion(string query);
        IQueryable<Select2BaseModel> DdlJenisPerjanjian(string query);

        IQueryable<Select2BaseModel> GetDdlFungsi(Guid guid);
        IQueryable<Select2BaseModel> GetDdlPicLevel(Guid guid);
        IQueryable<Select2BaseModel> GetDdlPicFungsi(Guid guid);
        IQueryable<Select2BaseModel> GetDdlEntitasPertamina(Guid guid);
        IQueryable<Select2BaseModel> GetDdlHub(Guid guid);
        IQueryable<Select2BaseModel> GetDdlHsh(Guid guid);
        IQueryable<Select2BaseModel> GetDdlStreamBusiness(Guid guid);
        IQueryable<Select2BaseModel> GetDdlInterest(Guid guid);
        IQueryable<Select2BaseModel> GetDdlInitiativeStatus(Guid guid);
        IQueryable<Select2BaseModel> GetDdlInitiativeTypes(Guid guid);
        IQueryable<Select2BaseModel> GetDdlInitiativeStages(Guid guid);
        IQueryable<Select2BaseModel> GetDdlKawasanMitra(Guid guid);
        IQueryable<Select2BaseModel> GetDdlNegaraMitra(Guid guid);
        IQueryable<Select2BaseModel> GetDdlNegaraMitraOnlyNegara(Guid guid);
        IQueryable<Select2BaseModel> GetDdlOpportunity(Guid guid);
        IQueryable<Select2BaseModel> GetDdlHshEntitasPertamina(Guid guid);
        IQueryable<Select2BaseModel> DdlInitiativeForm(string request);
        IQueryable<Select2BaseModel> DdlNamaProyek(string query, string value);
        IEnumerable<Select2BaseModel> GetDdlOpportunityEntitasPertamina(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlOpportunityPic(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlOpportunityKesiapanProyek(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlOpportunityNegara(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlOpportunityTargetMitra(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlOpportunityStreamBusiness(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlInitiativeEntitasPertamina(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlInitiativePicFungsi(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlInitiativeStreamBusiness(Guid guid);
         IQueryable<Select2BaseModel> GetDdlRelatedAgreement(Guid guid);
        IQueryable<Select2BaseModel> DdlAgreementForm(string request);
        IQueryable<Select2BaseModel> DdlFaktorKendala(string query);
        IQueryable<Select2BaseModel> DdlKlasifikasiKendala(string query);
        IQueryable<Select2BaseModel> DdlKeterlibatanLembaga(string query);
        IQueryable<Select2BaseModel> DdlNamaProyekInitiative(string query, string value);
        IQueryable<Select2BaseModel> DdlYear(string query);
        IEnumerable<Select2BaseModel> GetDdlInitiativeNegara(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementStreamBusiness(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementPicFungsi(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementEntitasPertamina(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementNegara(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementJenisPerjanjian(Guid guid);

        IEnumerable<Select2BaseModel> GetDdlAgreementStatusDiscussion(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementFaktorKendala(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementTrafficLight(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementKlasifikasiKendala(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementKeterlibatanLembaga(Guid guid);
        IEnumerable<Select2BaseModel> GetDdlAgreementRelatedAgreement(Guid guid);
        IQueryable<Select2BaseModel> DdlTypeOfStudy(string query);
        IQueryable<Select2BaseModel> DdlConfidentiality(string query);
        IQueryable<Select2BaseModel> DdlOwnerEntity(string query);
        IQueryable<Select2BaseModel> DdlExistingFootprintsSortingHighPriority(string query, string existingFootprintsSortingHighPriority);
        IQueryable<Select2BaseModel> DdlSignedAgreementSortingHighPriority(string query, string signedAgreementSortingHighPriority);
        IQueryable<Select2BaseModel> GetDdlInitiativeKeterlibatanLembaga(Guid guid);
    }
}
