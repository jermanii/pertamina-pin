using Microsoft.EntityFrameworkCore;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IPotentialInitiativeDashboardRepository
    {
        Task<ResponseDataTableBaseModel<List<InitiativeViewModel>>> GetList(RequestFormDtBaseModel request, InitiativeViewModel decodeModel);
        HubHeadViewModel GetHubHead(Guid guid);
        PicFungsiViewModel GetPicLead(Guid guid);
        KementrianLembagaViewModel GetKementrianLembaga(Guid guid);
        Task<IQueryable<InitiativeViewModel>> QueryInitiativeWithFilter(InitiativeViewModel decodeModel);
        List<InitiativeStageViewModel> GetRecordsInitiativeStage();
        List<InitiativeStatusViewModel> GetRecordsInitiativeStatus();
        List<HshViewModel> GetRecordsHsh();
        Task<IQueryable<InitiativeNegaraMitraViewModel>> GetRecordsNegaraMitraWithSearch(InitiativeViewModel model, string strSearch);
        List<PotentialInitiativeHighPriorityViewModel> GetHighPriority(string wwwroot, SortViewModel sort);
        Task<List<PotentialInitiativesDashboardHubGroupViewModel>> GetHubGrouppingCountries();
        Task<List<string>> GetCountriesMap();
        Task<List<PotentialInitiativesDashboardMapPointerViewModel>> GetSubHoldingMap();
        Task<List<PicFungsiViewModel>> GetPicMembers(Guid guid);
        Task<PotentialInitiativeHighPriorityViewModel> GetDetailHighPriorityById(Guid guid);
    }
}
