using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface ISignedAgreementDashboardService
    {
        Task<PaginationBaseModel<SignedAgreementDashboardHighPriorityViewModel>> GetListPaging(RequestFormDtBaseModel request, string wwwroot, string StreamBusinessId, string NegaraMitraId, string EntitasPertaminaId);
        Task<SignedAgreementDashboardMapViewModel> GetCountriesMap();
        Task<SignedAgreementDashboardMapViewModel> GetCountriesMap(Guid? negara, Guid? stream, Guid? entitas);
        Task<SignedAgreementDashboardViewModel> FilterHighPriority(string countryAcr);
        Task<SignedAgreementDashboardViewModel> FilterHighPriorityBasedOnDropdown(string? streamBusiness, string? negaraMitra, string? entitasPertamina);
        List<SignedAgreementDashboardHighPriorityViewModel> GetHighPriority(string wwwroot);
        List<SignedAgreementKementriaanLembagaViewModel> GetKemntrianLembagaAgreement(List<SignedAgreementDashboardHighPriorityViewModel> highPriority);
        List<SignedAgreementDashboardHighPriorityViewModel> GetStrategicAgreement(string wwwroot, bool isHigh, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount);
        int? GetCountAllStrategicAgreement(string wwwroot, bool isHigh, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount);
        Guid? GetCountryByAcronym(string countryAcronym);
        SignedAgreementDashboardHighPriorityViewModel GetSignedAgreementDashboardDetail(string wwwroot, Guid guid);
        List<SignedAgreementPartnerViewModel> GetSignedAgreementDashboardDetailPartners(Guid guid);
        List<SignedAgreementLokasiProyekViewModel> GetSignedAgreementDashboardDetailProjectLocations(Guid guid);
        List<SignedAgreementEntitasPertaminaViewModel> GetSignedAgreementDashboardDetailPertaminaEntitas(Guid guid);
        List<SignedAgreementStreamBusinessViewModel> GetSignedAgreementDashboardDetailStreams(Guid guid);
    }
}
