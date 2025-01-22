using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IPotentialInitiativesDashboardService
    {
        Task<List<PotentialInitiativeHighPriorityViewModel>> GetInitiativeHighPriority(string wwwroot, SortViewModel sort);
        Task<PaginationBaseModel<InitiativeViewModel>> GetListPaging(RequestFormDtBaseModel request, InitiativeViewModel decodeModel);
        InitiativeViewModel GenerelizeDataInitiative(InitiativeViewModel rec);
        Task<InitiativeViewModel> GetInitiativeCountWithFilter(InitiativeViewModel model, string strSearch);
        Task<PotentialInitiativesDashboardMapViewModel> GetCountriesMap();
        Task<PotentialInitiativeHighPriorityViewModel> GetHighPriorityDetailById(Guid guid);
    }
}
