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
    public interface IProjectsToOfferRepository
    {
        Task<ResponseDataTableBaseModel<List<OpportunityViewModel>>> GetList(RequestFormDtBaseModel request, OpportunityViewModel decodeModel);
        void Save(OpportunityViewModel model);
        OpportunityViewModel GetOpportunityById(Guid guid);
        OpportunityEntitasPertaminaViewModel GetOpportunityEntitasById(Guid guid);
        OpportunityStreamBusinessViewModel GetOpportunityStreamBusinessById(Guid guid);
        OpportunityPicFungsiViewModel GetOpportunityPicFungsiById(Guid guid,Guid? picLead);
        OpportunityKesiapanProyekViewModel GetOpportunityKesiapanProyekById(Guid guid);
        OpportunityNegaraMitraViewModel GetOpportunityNegaraMitraById(Guid guid);
        OpportunityTargetMitraViewModel GetOpportunityTargetMitraById(Guid guid);
        List<OpportunityPartnerViewModel> GetOpportunityPartnerById(Guid guid);
        List<OpportunityLokasiProyekViewModel> GetOpportunityLokasiById(Guid guid);

        #region async
        Task<OpportunityViewModel> GetOpportunityByIdAsync(Guid guid);
        Task<OpportunityEntitasPertaminaViewModel> GetOpportunityEntitasByIdAsync(Guid guid);
        Task<OpportunityStreamBusinessViewModel> GetOpportunityStreamBusinessByIdAsync(Guid guid);
        Task<OpportunityPicFungsiViewModel> GetOpportunityPicFungsiByIdAsync(Guid guid);
        Task<OpportunityPicFungsiViewModel> GetOpportunityPicFungsiLeadByIdAsync(Guid guid, Guid? picLevelLead);
        List<OpportunityPicFungsiViewModel> GetOpportunityPicFungsiMemberByIdAsync(Guid guid, Guid? picLevelMember);
        Task<OpportunityKesiapanProyekViewModel> GetOpportunityKesiapanProyekByIdAsync(Guid guid);
        Task<OpportunityNegaraMitraViewModel> GetOpportunityNegaraMitraByIdAsync(Guid guid);
        Task<OpportunityTargetMitraViewModel> GetOpportunityTargetMitraByIdAsync(Guid guid);
        Task<List<OpportunityPartnerViewModel>> GetOpportunityPartnerByIdAsync(Guid guid);
        Task<List<OpportunityLokasiProyekViewModel>> GetOpportunityLokasiByIdAsync(Guid guid);


        #endregion

        void Update(OpportunityViewModel model, string userName);
        void SaveToOpportunityEntitasPertamina(OpportunityEntitasPertaminaViewModel model);
        void SaveToPartner(OpportunityPartnerViewModel model);
        void SaveToOpportunityStreamBusiness(OpportunityStreamBusinessViewModel model);
        void SaveToOpportunityPicFungsi(OpportunityPicFungsiViewModel model);
        void SaveToOpportunityNegaraMitra(OpportunityNegaraMitraViewModel model);
        void SaveToOpportunityKesiapanProyek(OpportunityKesiapanProyekViewModel model);
        void SaveToOpportunityTargetMitra(OpportunityTargetMitraViewModel model);
        void SaveToOpportunityLokasiProyek(OpportunityLokasiProyekViewModel model);
        TbltOpportunity HasRecordById(Guid guid);
        List<TbltOpportunityEntitasPertamina> HasRecordByIdEntitas(Guid guid);
        void Delete(Guid guid, string userName);
        void DeleteEntitas(Guid guid, string userName);
        void DeleteStreamBusiness(Guid guid, string userName);
        void DeletePicFungsi(Guid guid, string userName);
        void DeleteKesiapanProyek(Guid guid, string userName);
        void DeleteNegaraMitra(Guid guid, string userName);
        void DeleteTargetMitra(Guid guid, string userName);
        void DeletePartner(Guid guid, string userName);
        void DeleteLokasiProyek(Guid guid, string userName);
        void DeleteExistingEntitasPertamina(Guid guid);
        void DeleteExistingPicFungsi(Guid guid);
        void DeleteExistingStreamBusiness(Guid guid);
        void DeleteExistingKesiapanProyek(Guid guid);
        void DeleteExistingPartners(Guid guid);
        void DeleteExistingNegaraMitra(Guid guid);
        void DeleteExistingTargetMitra(Guid guid);
        void DeleteExistingLokasiProyek(Guid guid);
        Guid? PicLevelLead();
        Guid? PicLevelMember();
        Task<List<HshViewModel>> GetRecordsHsh();
        List<HshViewModel> GetAllDataHsh();
        int? GetCountHsh(int relation);
        int? GetCountRecordsOpportunity();
        int? GetCountRecordsNegaraMitra();
        List<OpportunityPartnerViewModel> GetPartners(Guid guid);
        List<OpportunityEntitasPertaminaViewModel> GetEntitasPertamina(Guid guid);
        List<OpportunityStreamBusinessViewModel> GetStreamBusiness(Guid guid);
        List<OpportunityNegaraMitraViewModel> GetNamaNegara(Guid id);
        string GetNamaProyek(Guid id);
        bool? GetStatusDraft(Guid id);
        List<OpportunityKesiapanProyekViewModel> GetKesiapanProyek(Guid id);
        Task<IQueryable<OpportunityViewModel>> QueryOpportunityWithFilter(OpportunityViewModel model);
        Task<IQueryable<OpportunityNegaraMitraViewModel>> GetRecordsNegaraMitra(OpportunityViewModel model);
        List<string> GetReadPartners(Guid guid);
        List<PicFungsiViewModel> GetReadPicFungsiById(Guid guid);
        PicFungsiViewModel GetReadPicFungsiByIdLead(Guid guid);
        List<EntitasPertaminaViewModel> GetReadEntitasPertamina(Guid guid);
        List<NegaraMitraViewModel> GetNegaraMitraById(Guid guid);
        List<string> GetLokasiProyekProvinsi(Guid guid);
        List<StreamBusinessViewModel> GetReadStreamBusinessById(Guid guid);
        List<KesiapanProyekViewModel> GetReadKesiapanProyek(Guid guid);
        List<TargetMitraViewModel> GetReadTargetMitra(Guid guid);
        IEnumerable<OpportunityViewModel> GetAllOpportunityExcel();
        bool SaveAllData(OpportunityViewModel model);
        bool UpdateAllData(OpportunityViewModel model);
        Task<IQueryable<OpportunityNegaraMitraViewModel>> GetRecordsNegaraMitraWithSearch(OpportunityViewModel model, string strSearch);


    }
}
