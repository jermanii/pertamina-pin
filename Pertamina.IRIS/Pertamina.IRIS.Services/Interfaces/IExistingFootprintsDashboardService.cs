using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IExistingFootprintsDashboardService
    {
        Task<PaginationBaseModel<ExistingFootprintsDashboardHighPriorityViewModel>> GetListPaging(RequestFormDtBaseModel request, string wwwroot, string negaraMitra, string streamBusiness, string entitasPertamina);
        List<ExistingFootprintsDashboardHighPriorityViewModel> GetHighPriority(string wwwroot, bool isHigh, bool isFilter, bool isSort, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount);
        int? GetCountAllRecordSortHighPriority(string wwwroot, bool isHigh, bool isFilter, bool isSort, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount);
        List<ExistingFootprintsDashboardLegendViewModel> GetLegends();
        Task<ExistingFootprintDashboardMapViewModel> GetCountriesMap();
        Task<ExistingFootprintDashboardMapViewModel> GetCountriesMap(Guid? negara, Guid? stream, Guid? entitas);
        ExistingFootprintsDashboardHighPriorityViewModel GetExistingFootprintsDashboardDetail(string wwwroot, Guid guid, int year);
        List<ExistingFootprintsPartnersViewModel> GetExistingFootprintsPartners(Guid guid);
        Guid? GetCountryByAcronym(string countryAcronym);
    }
}