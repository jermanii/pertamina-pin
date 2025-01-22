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
    public interface IExistingFootprintRepository
    {
        Task<ResponseDataTableBaseModel<List<ExistingFootprintViewModel>>> GetList(RequestFormDtBaseModel request, ExistingFootprintViewModel decodeModel);
        Task<ResponseDataTableBaseModel<List<ExistingFootprintsOperatingMetricViewModel>>> GetListOpenMetricById(RequestFormDtBaseModel request, Guid guid);
        Task<ExistingFootprintViewModel> GetHubHeadByHubId(Guid hubId);
        Task<ExistingFootprintViewModel> GetExistingFootprintById(Guid guid, Guid levelId);
        Task<ExistingFootprintViewModel> GetEntitasById(Guid entitasId);
        IQueryable<Select2BaseModel> GetDdlYear(Guid guid, int year);
        Guid GetMitraIdByHubId(Guid hubId);
        Guid GetHubIdIdByHubId(Guid hubId);
        Guid GetHubId();
        Guid GetPicLevelLeadId();
        Guid GetPicLevelMemberId();
        Guid GetOMIdByHubId(Guid existingFottprintsId);
        ExistingFootprintViewModel GetExistingFootprint(Guid guid);
        //bool ClearTemporaryDataExistingFootprint(Guid? guid);
        ExistingFootprintsOperatingMetricViewModel GetExistingFootprintOperatingMetric(Guid guid);
        ExistingFootprintViewModel IsExistingOperatingMetric(Guid guid);
        ExistingFootprintViewModel IsExistingPartner(Guid guid);
        ExistingFootprintViewModel IsExistingPic(Guid guid);
        ExistingFootprintViewModel IsCheckedYearOperatingMetric(Guid guid, Guid operatingMetricId);
        ExistingFootprintViewModel CreateExistingFootprint(ExistingFootprintViewModel model);
        List<ExistingFootprintsPartnersViewModel> GetExistingFootprintPartners(Guid guid);
        List<ExistingFootprintsPartnersViewModel> GetAllExistingFootprintPartners(Guid guid);
        List<ExistingFootprintsOperatingMetricViewModel> GetAllExistingFootprintOperatingMetrics(Guid guid);
        bool ResetExistingFootprintOperatingMetrics(Guid guid);
        ExistingFootprintsHubHeadViewModel GetExistingFootprintHubHead(Guid guid);
        ExistingFootprintsPICViewModel GetExistingFootprintPIC(Guid guid);
        ExistingFootprintViewModel GetNegaraMitraById(Guid? negaraMitraId);
        //ExistingFootprintsPICViewModel GetExistingFootprintPICFungsiLead(Guid guid, Guid picLevelLeadId);
        ExistingFootprintsPICViewModel GetPICFungsiName(Guid? picFungsiId);
        ExistingFootprintsOperatingMetricViewModel GetExistingFootprintOm(Guid guid);
        ExistingFootprintsPICViewModel GetExistingFootprintPICLead(Guid guid, Guid? picLevelLead);
        List<ExistingFootprintsPICViewModel> GetExistingFootprintPICMember(Guid guid, Guid? picLevelMember);
        List<ExistingFootprintsPICViewModel> GetAllExistingFootprintPICMember(Guid guid, Guid? picLevelMember);

        //ExistingFootprintsOperatingMetricViewModel SubmitOperatingMetric(ExistingFootprintsOperatingMetricViewModel model);
        //ExistingFootprintsOperatingMetricViewModel UpdateOperatingMetric(ExistingFootprintsOperatingMetricViewModel model);
        bool DeleteList(List<ExistingFootprintsPartnersViewModel> partners, List<ExistingFootprintsPICViewModel> pics, List<ExistingFootprintsOperatingMetricViewModel> oms);
        bool DeletePartial(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, List<ExistingFootprintsPICViewModel> pics, ExistingFootprintsOperatingMetricViewModel om);
        bool Update(ExistingFootprintViewModel model, List<ExistingFootprintsOperatingMetricViewModel> oms, string username);
        bool Delete(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, ExistingFootprintsOperatingMetricViewModel om);
        bool UpdatePartial(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, List<ExistingFootprintsPICViewModel> pics, ExistingFootprintsOperatingMetricViewModel om);
        bool SavePartial(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, List<ExistingFootprintsPICViewModel> pics, ExistingFootprintsOperatingMetricViewModel om);
        bool Save(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, List<ExistingFootprintsPICViewModel> pics, List<ExistingFootprintsOperatingMetricViewModel> oms);
        bool SaveDraft(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, List<ExistingFootprintsHubHeadViewModel> hubHeads, List<ExistingFootprintsPICViewModel> picFungsis);
        string GetProjectTypeById(Guid? streamBusinessId);
        IEnumerable<ExistingFootprintViewModel> GetAllExistingFootprintsExcel();
    }
}
