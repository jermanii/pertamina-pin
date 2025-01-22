using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IProjectsToOfferDashboardRepository
    {
        OpportunityViewModel GetOpportunityById(Guid guid);
        IQueryable<OpportunityPicFungsiViewModel> GetOpportunityPicFungsiById(Guid guid);
        IQueryable<OpportunityNegaraMitraViewModel> GetOpportunityNegaraMitraById(Guid guid);
        IQueryable<OpportunityEntitasPertaminaViewModel> GetOpportunityEntitasPertaminaById(Guid guid);
        IQueryable<OpportunityKesiapanProyekViewModel> GetOpportunityKesiapanProyekById(Guid guid);
        IQueryable<OpportunityLokasiProyekViewModel> GetOpportunityLokasiProyekById(Guid guid);
        IQueryable<OpportunityTargetMitraViewModel> GetOpportunityTargetMitraById(Guid guid);
        IQueryable<OpportunityPartnerViewModel> GetOpportunityPartnerById(Guid guid);
        IQueryable<OpportunityLokasiProyekViewModel> GetListProvinceAcronyms();
        IQueryable<OpportunityStreamBusinessViewModel> GetOpportunityStremBusinessById(Guid guid);
        IQueryable<OpportunityLokasiProyekViewModel> GetListHsh(string provinceAcronyms);
        List<HubHeadViewModel> GetHubHeadByOpportunityId(Guid guid);
        List<PicFungsiViewModel> GetPicFungsiByOpportunityId(Guid guid);
        List<NegaraMitraViewModel> GetNegaraMitraByOpportunityId(Guid guid);
        List<OpportunityHighPriorityViewModel> GetOpportunityHighPriority();
        Task<ResponseDataTableBaseModel<List<OpportunityViewModel>>> GetList(RequestFormDtBaseModel request, OpportunityViewModel decodeModel);    }
}
