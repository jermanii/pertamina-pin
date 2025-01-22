using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IAgreementRepository
    {
        Task<ResponseDataTableBaseModel<List<AgreementViewModel>>> GetList(RequestFormDtBaseModel request, AgreementViewModel decodeModel);
        int GetCountStatusBerlaku(int relation);
        IQueryable<AgreementViewModel> GetAgreementWithFilter(AgreementViewModel decodeModel);
        IQueryable<AgreementViewModel> GetAgreementGroupByWithFilter(AgreementViewModel decodeModel);
        int GetCountStatusDiscussion(int relation);
        int? GetCountHsh(int relation);
        int? GetSequenceAgreementAddendumById(Guid guid);
        string ToRoman(int number);
        decimal ParseRevenue(string revenueToParse);
        Task<AgreementViewModel> GetAgreementAsyncById(Guid guid);
        AgreementViewModel GetAgreementById(Guid guid);
        AgreementViewModel GetAgreementByIdCrud(Guid guid);
        AgreementEntitasPertaminaViewModel GetAgreementEntitasById(Guid guid);
        AgreementStreamBusinessViewModel GetAgreementStreamBusinessById(Guid guid);
        AgreementNegaraMitraViewModel GetAgreementNegaraMitraById(Guid guid);
        AgreementHubHeadViewModel GetAgreementHubHeadById(Guid guid);
        AgreementPicFungsiViewModel GetAgreementPicFungsiById(Guid guid);
        AgreementPicFungsiViewModel GetAgreementPicFungsiLeadById(Guid guid, Guid? picLeadId);
        List<AgreementPicFungsiViewModel> GetAgreementPicFungsisById(Guid guid, Guid? picMemberId);
        List<AgreementLokasiProyekViewModel> GetAgreementLokasiById(Guid guid);
        AgreementAddendumViewModel GetAddendumLastById(Guid guid);
        List<AgreementAddendumViewModel> GetAgreementAddendumById(Guid guid);
        List<AgreementPartnerViewModel> GetAgreementPartnerById(Guid guid);
        List<AgreementKementrianLembagaViewModel> GetAgreementKementrianLembagaById(Guid guid);
        StreamBusinessViewModel GetStreamBusinessById(Guid guid);
        List<StatusBerlakuViewModel> GetAllDataStatusBerlaku();
        List<StatusDiscussionViewModel> GetAllDataStatusDiscussion();
        List<HshViewModel> GetAllDataHsh();
        Task<List<StatusBerlakuViewModel>> GetAllDataStatusBerlakuAsync();
        Task<List<StatusDiscussionViewModel>> GetAllDataStatusDiscussionAsync();
        Task<List<HshViewModel>> GetAllDataHshAsync();

        int? GetAllAgreement();
        int? GetAllAgreementWithFilter(AgreementViewModel decodeModel);
        int? GetAllAgreementWithFilter(AgreementViewModel decodeModel, string strSearch);
        int? GetAllNegaraMitra();
        int? GetAllNegaraMitraWithFilter(AgreementViewModel decodeModel);
        int? GetAllNegaraMitraWithFilter(AgreementViewModel decodeModel, string strSearch);

        #region Grid
        bool? GetStatusDraft(Guid id);
        string GetJudulPerjanjian(Guid id);
        List<AgreementPartnerViewModel> GetPartners(Guid id);
        List<AgreementEntitasPertaminaViewModel> GetEntitasPertamina(Guid id);
        List<AgreementStreamBusinessViewModel> GetStreamBusiness(Guid id);
        string GetJenisPerjanjian(Guid id);
        List<AgreementNegaraMitraViewModel> GetNamaNegara(Guid id);
        string GetStatusBerlaku(Guid id);
        string GetStatusBerlakuColorHexa(Guid id);
        string GetStatusBerlakuColorName(Guid id);
        string GetDiscussionStatus(Guid id);
        string GetDiscussionStatusColorHexa(Guid id);
        string GetDiscussionStatusColorName(Guid id);
        #endregion

        void Save(AgreementViewModel model);
        void SavePic(AgreementPicFungsiViewModel model);
        void SaveEntitasPertamina(AgreementEntitasPertaminaViewModel model);
        void SaveNegaraMitra(AgreementNegaraMitraViewModel model);
        void SaveAddendum(AgreementAddendumViewModel model);
        void SaveHubHead(AgreementHubHeadViewModel model);
        void SaveStreamBusiness(AgreementStreamBusinessViewModel model);
        void SaveToPartner(AgreementPartnerViewModel model);
        void SaveToLokasiProyek(AgreementLokasiProyekViewModel model);
        void SaveLembaga(AgreementKementrianLembagaViewModel model);
        Task<InitiativeViewModel> GetInitiativeById(Guid guid);
        Task<OpportunityViewModel> GetOpportunityById(Guid guid);
        Task<AgreementViewModel> GetHubHeadByHubId(Guid hubId);
        Task<AgreementViewModel> GetAdendumSequence(Guid guid,int sequence, bool addRepeater);
        List<NegaraMitraExcludedViewModel> GetAllNegareMitraExcluded();
        StatusBerlakuViewModel GetStatusExpired();
        int? GetSequenceAmandement(Guid guid);
        Guid GetPicLevelLeadId();
        Guid GetPicLevelMemberId();

        void Update(AgreementViewModel model, string userName);
        void DeleteExistingStreamBusiness(Guid guid);
        void DeleteExistingFungsiPic(Guid guid);
        void DeleteExistingEntitasPertamina(Guid guid);
        bool CheckedFungsiPicIdByAgreementId(Guid guid, Guid? fungsiPicId);
        List<Guid> CheckIsStrategicPartnership(bool isStrategic);
        List<Guid> CheckBdCoreBusiness(bool bdCoreBusiness);
        List<Guid> CheckIsGreenBusiness(bool IsGreenBusiness);
        void DeleteExistingNegaraMitra(Guid guid);
        void DeleteExistingPartner(Guid guid);
        void DeleteExistingLokasiProyek(Guid guid);
        void Delete(Guid guid, string userName);
        void DeleteEntitas(Guid guid, string userName);
        void DeletePicFungsi(Guid guid, string userName);
        void DeletePartner(Guid guid, string userName);
        void DeleteNegaraMitra(Guid guid, string userName);
        void DeleteLokasiProyek(Guid guid, string userName);
        void DeleteStreamBusiness(Guid guid, string userName);
        void DeleteKementrianLembaga(Guid guid, string userName);
        void DeleteExistingLembaga(Guid guid);
        bool IsExistInAgreement(Guid guid);
        bool IsExistInInitiative(Guid guid);


        #region Read
        AgreementJenisPerjanjian GetJenisPerjanjianById(Guid guid);
        IEnumerable<PicFungsiViewModel> GetPicFungsiById(Guid guid, Guid? levelId);
        List<EntitasPertaminaViewModel> GetEntitasPertaminaById(Guid guid);
        StatusBerlakuViewModel GetStatusBerlakuById(Guid guid);
        StatusDiscussionViewModel GetStatusDiscussionById(Guid guid);
        List<string> GetPartnerById(Guid guid);
        List<NegaraMitraViewModel> GetNegaraMitraById(Guid guid);
        List<string> GetLokasiProyekById(Guid guid);
        List<StreamBusinessViewModel> GetAllStreamBusinessById(Guid guid);
        FaktorKendalaViewModel GetFaktorKendalaById(Guid guid);
        KlasifikasiKendalaViewModel GetKlasifikasiKendalaById(Guid guid);
        List<string> GetKementrianLembagaById(Guid guid);
        AgreementViewModel GetRelatedAgreementById(Guid guid);
        AgreementViewModel GetTrafficLightReadById(Guid guid);
        List<AgreementAddendumViewModel> GetAddendumRomanById(Guid guid);
        IEnumerable<AgreementViewModel> GetAllAgreementExcel();
        AgreementKementrianLembagaViewModel GetAgreementLembagaById(Guid guid);
        Task<StatusBerlakuViewModel> StatusBerlakuCounting(int? sequence);
        StatusBerlakuViewModel GetStatusBerlakuName(Guid? guid);
        #endregion
    }
}
