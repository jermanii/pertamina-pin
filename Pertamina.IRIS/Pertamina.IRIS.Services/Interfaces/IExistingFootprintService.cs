using OfficeOpenXml;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IExistingFootprintService
    {
        Task<PaginationBaseModel<ExistingFootprintViewModel>> GetListPaging(RequestFormDtBaseModel request, ExistingFootprintViewModel decodeModel);
        Task<PaginationBaseModel<ExistingFootprintsOperatingMetricViewModel>> GetListPagingOpenMetricById(RequestFormDtBaseModel request, Guid guid);
        Task<ExistingFootprintViewModel> GetHubHeadByHubId(Guid hubId);
        Task<ExistingFootprintViewModel> GetExistingFootprintsById(Guid guid, Guid levelId);
        ExistingFootprintViewModel GetExistingFootprint(Guid guid);
        ExistingFootprintsOperatingMetricViewModel GetExistingFootprintOperatingMetric(Guid guid);
        ExistingFootprintViewModel IsExistingValidation(ExistingFootprintViewModel model);
        ExistingFootprintViewModel IsOperatingMetricValidation(ExistingFootprintViewModel model);
        List<ExistingFootprintsPartnersViewModel> GetExistingFootprintPartners(Guid guid);
        List<ExistingFootprintsPICViewModel> GetExistingFootprintPicFungsiMember(Guid guid);
        ExistingFootprintViewModel GetExistingFootprintToRead(Guid guid);
        ExistingFootprintsPICViewModel GetExistingFootprintPicFungsiLead(Guid guid);
        ExistingFootprintViewModel GetNegaraMitraById(Guid? negaraMitraId);
        ExistingFootprintViewModel Delete(Guid guid, string username);
        ExistingFootprintViewModel GetExistingToUpdate(Guid guid, Guid? picLevelLead, Guid? picLevelMember);
        //void ClearTemporaryDataExistingFootprint(Guid? guid);
        ExistingFootprintViewModel CreateExistingFootprint(Guid? guid);
        Select2BaseModel GetYearOperatingMetricById(Guid guid, int year);
        ExistingFootprintViewModel DeleteOperatingMetric(ExistingFootprintViewModel model);
        ExistingFootprintViewModel SubmitOperatingMetric(ExistingFootprintViewModel model, string username, ExistingFootprintViewModel getEFModel);
        ExistingFootprintViewModel UpdateOperatingMetric(ExistingFootprintViewModel model, string username, ExistingFootprintViewModel getEFModel);
        ExistingFootprintViewModel Update(ExistingFootprintViewModel model, string username);
        ExistingFootprintViewModel Submit(ExistingFootprintViewModel model, string username);
        ExistingFootprintViewModel SaveDraft(ExistingFootprintViewModel model, string username);
        string GetProjectTypeById(Guid? streamBusinessId);
        ExcelPackage ExportXLS(string searchGrid, string entitasPertaminaId, string projectTypeId, string totalAssetsMinOp, string totalAssetsMaxOp, string revenueMinOp, string revenueMaxOp, string ebitdaMinOp, string ebitdaMaxOp, string yearOp, string partnerCountryId);
    }
}
