using OfficeOpenXml;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IInitiativeService
    {
        Task<PaginationBaseModel<InitiativeViewModel>> GetListPaging(RequestFormDtBaseModel request, InitiativeViewModel decodeModel);
        InitiativeViewModel Save(InitiativeViewModel model, string userName, string companyCode);
        InitiativeViewModel SaveDraft(InitiativeViewModel model, string userName, string companyCode);
        Task<InitiativeViewModel> GetInitiativeByIdAsync(Guid guid);
        InitiativeViewModel GetInitiativeById(Guid guid);
        InitiativeViewModel Update(InitiativeViewModel model, string userName);
        InitiativeViewModel GetInitiativeCount();
        InitiativeViewModel GenerelizeDataInitiative(InitiativeViewModel rec);
        Task<InitiativeViewModel> GetInitiativeCountWithFilter(InitiativeViewModel model);
        Task<InitiativeViewModel> GetInitiativeCountWithFilter(InitiativeViewModel model, string strSearch);
        InitiativeViewModel Delete(Guid guid, string userName);
        InitiativeViewModel GetReadInitiativeById(Guid guid);
        ExcelPackage ExportXLS(bool withData, string initiativeStage, string initiativeStatus, string initiativeHolder, string negaraMitra, string kawasanMitra, string streamBussiness, string entitasPertamina, string searchQuery, string rangeCreateDate, bool draft);
        Task<StatusBerlakuViewModel> StatusBerlakuCounting(DateTime endDate);
        Task<OpportunityViewModel> GetOpportunityById(Guid guid);

    }
}
