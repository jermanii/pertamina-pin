using OfficeOpenXml;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IProjectsToOfferService
    {
        Task<PaginationBaseModel<OpportunityViewModel>> GetListPaging(RequestFormDtBaseModel request, OpportunityViewModel decodeModel);
        OpportunityViewModel Save(OpportunityViewModel model, string userName, string companyCode);
        OpportunityViewModel GetOpportunityById(Guid guid);
        //OpportunityEntitasPertaminaViewModel GetOpportunityEntitasById(Guid guid);
        OpportunityViewModel SaveDraft(OpportunityViewModel model, string userName, string companyCode);
        OpportunityViewModel GetOpportunityCount();
        OpportunityViewModel GetLevelPic();
        OpportunityViewModel IsExistingValidation(OpportunityViewModel model);
        OpportunityViewModel Update(OpportunityViewModel model, string userName);
        OpportunityViewModel Delete(Guid guid, string userName);
        OpportunityViewModel GenerelizeDataOpportunity(OpportunityViewModel rec);
        Task<OpportunityViewModel> GetOpportunityCountWithFilter(OpportunityViewModel model);
        Task<OpportunityViewModel> GetOpportunityCountWithFilter(OpportunityViewModel model, string strSearch);
        OpportunityViewModel GetReadOpportunityById(Guid guid);
        ExcelPackage ExportXLS(bool withData, string opportunityHolder, string negaraMitra, string streamBussiness, string entitasPertamina, string createDate, string searchQuery, bool isDraft);
        Task<OpportunityViewModel> GetOpportunityByIdAsync(Guid guid);
    }
}
