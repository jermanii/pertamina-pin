using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface ISignedAgreementDashboardRepository
    {
        Task<ResponseDataTableBaseModel<List<SignedAgreementDashboardHighPriorityViewModel>>> GetList(RequestFormDtBaseModel request, string wwwroot, string negaraMitra, string streamBusiness, string entitasPertamina);
        List<SignedAgreementDashboardHighPriorityViewModel> GetStrategicPriorityAgreement(string wwwroot, bool isHigh, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount);
        int? GetCountAllStrategicAgreement(string wwwroot, bool isHigh, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount);
        Task<List<SignedAgreementDashboardHighPriorityViewModel>> GetFilterHighPriority(string countryAcr, string? streamBusinessId = null, string? negaraMitraId = null, string? entitasPertaminaId = null);
        IQueryable<SignedAgreementDashboardDataGridViewModel> GetGridView(RequestFormDtBaseModel request, string StreamBusinessId, string NegaraMitraId, string EntitasPertaminaId);
        string GetPICName(Guid? agreementId);
        string GetLembagaName(Guid? agreementId);
        Task<List<string>> GetCountriesMap();
        Task<List<string>> GetCountriesMap(Guid? negara, Guid? stream, Guid? entitas);
        Task<List<SignedAgreementDashboardMapPointerViewModel>> GetSubHoldingMap();
        IQueryable<SignedAgreementDashboardHighPriorityViewModel> GetHighPriority(string wwwroot);
        List<SignedAgreementKementriaanLembagaViewModel> GetKemntrianLembagaAgreement(Guid id);
        List<SignedAgreementStreamBusinessViewModel> GetStreamBusiness(Guid AgreementId);
        Guid? GetCountryByAcronym(string countryAcronym);
        SignedAgreementDashboardHighPriorityViewModel GetSignedAgreementDashboardDetail(string wwwroot, Guid guid);
        List<SignedAgreementPartnerViewModel> GetSignedAgreementDashboardDetailPartners(Guid guid);
        List<SignedAgreementLokasiProyekViewModel> GetSignedAgreementDashboardDetailProjectLocations(Guid guid);
        List<SignedAgreementEntitasPertaminaViewModel> GetSignedAgreementDashboardDetailPertaminaEntitas(Guid guid);
        List<SignedAgreementStreamBusinessViewModel> GetSignedAgreementDashboardDetailStreams(Guid guid);
    }
}