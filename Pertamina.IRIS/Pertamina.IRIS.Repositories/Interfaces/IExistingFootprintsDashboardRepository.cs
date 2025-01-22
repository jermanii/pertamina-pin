using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IExistingFootprintsDashboardRepository
    {
        Task<ResponseDataTableBaseModel<List<ExistingFootprintsDashboardHighPriorityViewModel>>> GetList(RequestFormDtBaseModel request, string wwwroot, string negaraMitra, string streamBusiness, string entitasPertamina);
        List<ExistingFootprintsDashboardHighPriorityViewModel> GetHighPriority(string wwwroot, bool isHigh, bool isFilter, bool isSort, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount);
        int? GetCountAllRecordSortHighPriority(string wwwroot, bool isHigh, bool isFilter, bool isSort, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount);
        List<ExistingFootprintsDashboardLegendViewModel> GetLegends();
        Task<List<string>> GetCountriesMap();
        Task<List<string>> GetCountriesMap(Guid? negara, Guid? stream, Guid? entitas);
        Task<List<ExistingFootprintDashboardMapPointerViewModel>> GetSubHoldingMap();
        Task<List<ExistingFootprintDashboardMapPointerViewModel>> GetSubHoldingMap(Guid? negara, Guid? stream, Guid? entitas);
        ExistingFootprintsDashboardHighPriorityViewModel GetExistingFootprintsDashboardDetail(string wwwroot, Guid guid, int year);
        List<ExistingFootprintsPartnersViewModel> GetExistingFootprintsPartners(Guid guid);
        List<ExistingFootprintsDashboardHighPriorityViewModel> GetSortHighPriority(string wwwroot);
        Guid? GetCountryByAcronym(string countryAcronym);
    }
}
