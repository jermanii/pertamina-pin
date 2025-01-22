using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface  IProjectsToOfferDashboardService
    {
        OpportunityViewModel GetDetailOpportunityById(Guid guid);
        List<OpportunityLokasiProyekViewModel> GetProvinceAcronyms();
        Task<List<OpportunityViewModel>> GetOpportunityHighPriority();
        Task<PaginationBaseModel<OpportunityViewModel>> GetList(RequestFormDtBaseModel request, OpportunityViewModel decodeModel);    }
}
