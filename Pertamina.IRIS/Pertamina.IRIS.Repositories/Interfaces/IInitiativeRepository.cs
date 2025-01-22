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
    public interface IInitiativeRepository
    {
        Task<ResponseDataTableBaseModel<List<InitiativeViewModel>>> GetList(RequestFormDtBaseModel request, InitiativeViewModel decodeModel);
        void Save(InitiativeViewModel model);
        void SavePic(InitiativePicFungsiViewModel model);
        void SaveEntitasPertamina(InitiativeEntitasPertaminaViewModel model);
        void SaveStreamBusiness(InitiativeStreamBusinessViewModel model);
        void SaveToPartner(InitiativePartnerViewModel model);
        void SaveToLokasiProyek(InitiativeLokasiProyekViewModel model);
        void SaveNegaraMitra(InitiativeNegaraMitraViewModel model);
        void SaveKementrianLembaga(InitiativeKementrianLembagaViewModel model);
        InitiativeViewModel GetInitiativeById(Guid guid);
        Task<InitiativeViewModel> GetInitiativeAsyncById(Guid guid);
        InitiativeEntitasPertaminaViewModel GetInitiativeEntitasById(Guid guid);
        InitiativeStreamBusinessViewModel GetInitiativeStreamBusinessById(Guid guid);
        InitiativeNegaraMitraViewModel GetInitiativeNegaraMitraById(Guid guid);
        InitiativePicFungsiViewModel GetInitiativePicFungsiById(Guid guid);
        List<InitiativeLokasiProyekViewModel> GetInitiativeLokasiById(Guid guid);
        List<InitiativePartnerViewModel> GetPartners(Guid guid);
        List<InitiativePartnerViewModel> GetInitiativePartnerById(Guid guid);
        InitiativeKementrianLembagaViewModel GetInitiativeKementrianLembagaById(Guid guid);
        void Update(InitiativeViewModel model, string userName);
        TbltInitiative HasRecordById(Guid guid);
        void DeleteExistingStreamBusiness(Guid guid);
        void DeleteExistingNegaraMitra(Guid guid);
        void DeleteExistingFungsiPic(Guid guid);
        void DeleteExistingEntitasPertamina(Guid guid);
        void DeleteExistingPartner(Guid guid);
        void DeleteExistingLokasiProyek(Guid guid);
        void DeleteExistingKementrianLembaga(Guid guid);
        List<InitiativeStageViewModel> GetRecordsInitiativeStage();
        int? GetCountInitiativeStage(int relation);
        List<InitiativeStatusViewModel> GetRecordsInitiativeStatus();
        int? GetCountInitiativeStatus(int relation);
        List<InitiativeEntitasPertaminaViewModel> GetEntitasPertamina(Guid id);
        List<HshViewModel> GetRecordsHsh();
        int? GetCountHsh(int relation);
        int? GetCountRecordsInitiative();
        int? GetCountRecordsNegaraMitra();
        List<InitiativeStreamBusinessViewModel> GetStreamBusiness(Guid id);
        List<InitiativeNegaraMitraViewModel> GetNamaNegara(Guid id);
        bool? GetStatusDraft(Guid id);
        string GetJudulInitiative(Guid id);
        string GetInitiativeStage(Guid id);
        string GetInitiativeStatus(Guid id);
        Task<IQueryable<InitiativeViewModel>> QueryInitiativeWithFilter(InitiativeViewModel decodeModel);
        Task<IQueryable<InitiativeNegaraMitraViewModel>> GetRecordsNegaraMitra(InitiativeViewModel model);
        Task<IQueryable<InitiativeNegaraMitraViewModel>> GetRecordsNegaraMitraWithSearch(InitiativeViewModel model, string strSearch);
        void Delete(Guid guid, string userName);
        void DeletePicFungsi(Guid guid, string userName);
        void DeleteEntitas(Guid guid, string userName);
        void DeletePartner(Guid guid, string userName);
        void DeleteNegaraMitra(Guid guid, string userName);
        void DeleteLokasiProyek(Guid guid, string userName);
        void DeleteStreamBusiness(Guid guid, string userName);
        InterestViewModel GetReadInitiativeInterestById(Guid guid);
        InitiativeStatusViewModel GetReadInitiativeStatusById(Guid guid);
        InitiativeTypesViewModel GetReadInitiativeTypesById(Guid guid);
        InitiativeStageViewModel GetReadInitiativeStageById(Guid guid);
        List<PicFungsiViewModel> GetReadPicFungsiById(Guid guid);
        List<EntitasPertaminaViewModel> GetReadEntitasPertamina(Guid guid);
        List<string> GetReadPartners(Guid guid);
        List<NegaraMitraViewModel> GetNegaraMitraById(Guid guid);
        List<string> GetReadLokasiProyekById(Guid guid);
        AgreementViewModel GetReadRelatedAgreementById(Guid guid);
        List<StreamBusinessViewModel> GetReadStreamBusinessById(Guid guid);
        IEnumerable<InitiativeViewModel> GetAllInitiativeExcel();
        Task<StatusBerlakuViewModel> StatusBerlakuCounting(int? sequence);
        List<InitiativeHighPriorityViewModel> GetIntitativeHighPriority();
        List<InitiativeMilestoneTimelineViewModel> GetIntitativeMilestoneTimelineById(Guid guid);

    }
}
