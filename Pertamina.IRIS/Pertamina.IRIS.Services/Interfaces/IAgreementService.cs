using OfficeOpenXml;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IAgreementService
    {
        Task<PaginationBaseModel<AgreementViewModel>> GetListPaging(RequestFormDtBaseModel request, AgreementViewModel decodeModel);
        AgreementViewModel GetAgreementCount();
        Task<AgreementViewModel> GetAgreementCountWithFilter(AgreementViewModel model);
        Task<AgreementViewModel> GetAgreementCountWithFilter(AgreementViewModel model, string strSearch);
        AgreementViewModel GenerelizeDataAgreement(AgreementViewModel rec);
        AgreementViewModel IsValidation(AgreementViewModel model);
        AgreementViewModel Save(AgreementViewModel model, string userName, string companyCode);
        Task<AgreementViewModel> GetAgreementAsyncById(Guid guid);
        AgreementViewModel GetAgreementById(Guid guid);
        AgreementViewModel Update(AgreementViewModel model, string userName);
        AgreementViewModel SaveDraft(AgreementViewModel model, string userName, string companyCode);
        AgreementViewModel Delete(Guid guid, string userName);
        Task<StatusBerlakuViewModel> StatusBerlakuCounting(DateTime endDate);
        Task<OpportunityViewModel> GetOpportunityAsyncById(Guid guid);
        Task<AgreementViewModel> GetHubHeadByHubId(Guid hubId);
        Task<AgreementViewModel> GetAdendumSequence(Guid guid,int sequence, bool addRepeater);
        AgreementViewModel CreatePicFunction();
        ExcelPackage ExportXLS(bool withData, string searchQuery, string statusBerlaku, string statusDiscussion, string agreementHolder, string entitasPertamina, string kawasanMitra, string negaraMitra, string jenisPerjanjian, string streamBusiness, string rangeCreateDate, string tanggalTtd, string tanggalBerakhir, bool draft, bool isG2G, bool isStrategicPartnerShip, bool isBdCoreBusiness, bool isGreenBusiness);
        Task<InitiativeViewModel> GetInitiativeAsyncById(Guid guid);
    }
}