using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using Pertamina.IRIS.Utility.Wording.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class ProjectsToOfferDashboardService :IProjectsToOfferDashboardService
    {
        private readonly IProjectsToOfferDashboardRepository _repository;
        private readonly IUpdatedWordingUtility _updateUtility;

        public ProjectsToOfferDashboardService(
            IProjectsToOfferDashboardRepository repository, IUpdatedWordingUtility updateUtility)
        {
            _repository = repository;
            _updateUtility = updateUtility;
        }
        public async Task<PaginationBaseModel<OpportunityViewModel>> GetList(RequestFormDtBaseModel request, OpportunityViewModel decodeModel)
        {
            var resultData = await _repository.GetList(request, decodeModel);

            var result = new PaginationBaseModel<OpportunityViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }

        public List<OpportunityLokasiProyekViewModel> GetProvinceAcronyms()
        {
            List<OpportunityLokasiProyekViewModel> results = new List<OpportunityLokasiProyekViewModel>();   
            results = _repository.GetListProvinceAcronyms().ToList();
            foreach (var item in results) {
                OpportunityLokasiProyekViewModel result = new OpportunityLokasiProyekViewModel();
                item.NamaProyekss = _repository.GetListHsh(item.ProvinceAcronyms).Distinct().ToList();
            }
           
            return results;
        }
        public async Task<List<OpportunityViewModel>> GetOpportunityHighPriority()
        {
            var highPriority = _repository.GetOpportunityHighPriority();
            var result = new List<OpportunityViewModel>();
            foreach (var item in highPriority) 
            {
                var opportunity = new OpportunityViewModel();
                opportunity = _repository.GetOpportunityById(item.OpportunityId);
                if (opportunity != null)
                {
                    if (opportunity.PotentialRevenuePerYear == null)
                    {
                        opportunity.PotentialRevenuePerYearToString= "$ mn";
                    }
                    else
                    {
                        opportunity.PotentialRevenuePerYearToString = string.Format("${0}mn", (opportunity.PotentialRevenuePerYear/1000000).Value.ToString("0.###"));
                    }
                    if (opportunity.Capex == null)
                    {
                        opportunity.CapexToString= "$ mn";
                    }
                    else
                    {
                        opportunity.CapexToString = string.Format("${0}mn", (opportunity.Capex/1000000).Value.ToString("0.###"));
                    }
                }
                opportunity.NegaraMitra= _repository.GetNegaraMitraByOpportunityId(item.OpportunityId).FirstOrDefault();
                opportunity.PicFungsi= _repository.GetPicFungsiByOpportunityId(item.OpportunityId).FirstOrDefault();
                opportunity.HubHead = _repository.GetHubHeadByOpportunityId(item.OpportunityId).FirstOrDefault();

                opportunity.PicFungsis= _repository.GetPicFungsiByOpportunityId(item.OpportunityId);
                opportunity.PICName = new List<string>();
                foreach (var listNamePic in opportunity.PicFungsis)
                {
                    var name = listNamePic.PicName;
                    opportunity.PICName.Add(name);
                }

                if (opportunity.NegaraMitra.Flag != null)
                {
                    opportunity.ExistsFlag = true;
                }
                if (opportunity.TypeOfPartnerNeeded == null)
                {
                    opportunity.TypeOfPartnerNeeded= "-";
                }
                opportunity.UpdatedWording = _updateUtility.GetUpdatedWording(opportunity.CreateDate, opportunity.UpdateDate);
                result.Add(opportunity);
              
            }
            return result;
        
        }
        public OpportunityViewModel GetDetailOpportunityById (Guid guid)
        {
            OpportunityViewModel model = new OpportunityViewModel();
            model = _repository.GetOpportunityById(guid);
            model.Capex = model.Capex/1000000;
            model.PotentialRevenuePerYear = model.PotentialRevenuePerYear/1000000;
            model.UpdatedWording = _updateUtility.GetUpdatedWording(model.CreateDate, model.UpdateDate);
            
            model.OpPicFungsis = _repository.GetOpportunityPicFungsiById(guid).ToList();
            model.OpEntitasPertaminas = _repository.GetOpportunityEntitasPertaminaById(guid).ToList();
            model.OpLokasiProyeks = _repository.GetOpportunityLokasiProyekById(guid).ToList();
            model.OpPartners = _repository.GetOpportunityPartnerById(guid).ToList();

            model.OpStreamBusiness = _repository.GetOpportunityStremBusinessById(guid).FirstOrDefault();
            model.OpKesiapanProyek = _repository.GetOpportunityKesiapanProyekById(guid).FirstOrDefault();
            model.OpNegaraMitra = _repository.GetOpportunityNegaraMitraById(guid).FirstOrDefault();
            model.OpTargetMitra = _repository.GetOpportunityTargetMitraById(guid).FirstOrDefault();
            return model;
        }
    }
}
