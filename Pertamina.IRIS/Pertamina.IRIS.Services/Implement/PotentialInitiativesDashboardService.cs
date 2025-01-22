using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class PotentialInitiativesDashboardService : IPotentialInitiativesDashboardService
    {
        private readonly IInitiativeRepository _repository;
        private readonly IPotentialInitiativeDashboardRepository _initiativeDashboardRepository;

        public PotentialInitiativesDashboardService(IInitiativeRepository repository,
            IPotentialInitiativeDashboardRepository initiativeDashboardRepository)
        {
            _repository = repository;
            _initiativeDashboardRepository = initiativeDashboardRepository;
        }
        public async Task<PaginationBaseModel<InitiativeViewModel>> GetListPaging(RequestFormDtBaseModel request, InitiativeViewModel decodeModel)
        {
            var resultData = await _initiativeDashboardRepository.GetList(request, decodeModel);

            var result = new PaginationBaseModel<InitiativeViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }

        public InitiativeViewModel GenerelizeDataInitiative(InitiativeViewModel rec)
        {
            List<InitiativePartnerViewModel> partners = new List<InitiativePartnerViewModel>();
            partners = _repository.GetPartners(rec.Id);
            string strPartner = string.Empty;
            foreach (var partner in partners)
            {
                if (partners.Count() > 1)
                {
                    strPartner += partner.PartnerName;
                    if (partner == partners.LastOrDefault())
                    {
                    }
                    else
                    {
                        strPartner += ", ";
                    }
                }
                else
                {
                    strPartner += partner.PartnerName;
                }
            }

            List<InitiativeEntitasPertaminaViewModel> entitasPertaminas = new List<InitiativeEntitasPertaminaViewModel>();
            entitasPertaminas = _repository.GetEntitasPertamina(rec.Id);
            string strEntitasPertamina = string.Empty;
            foreach (var entitasPertamina in entitasPertaminas)
            {
                if (entitasPertaminas.Count() > 1)
                {
                    strEntitasPertamina += entitasPertamina.CompanyName;
                    if (entitasPertamina == entitasPertaminas.LastOrDefault())
                    {
                    }
                    else
                    {
                        strEntitasPertamina += ", ";
                    }
                }
                else
                {
                    strEntitasPertamina += entitasPertamina.CompanyName;
                }
            }

            List<InitiativeStreamBusinessViewModel> streamBusinesses = new List<InitiativeStreamBusinessViewModel>();
            streamBusinesses = _repository.GetStreamBusiness(rec.Id);
            string strStreamBusiness = string.Empty;
            foreach (var streamBusiness in streamBusinesses)
            {
                if (streamBusinesses.Count() > 1)
                {
                    strStreamBusiness += streamBusiness.QueryStreamBusinessName;
                    if (streamBusiness == streamBusinesses.LastOrDefault())
                    {
                    }
                    else
                    {
                        strStreamBusiness += ", ";
                    }
                }
                else
                {
                    strStreamBusiness += streamBusiness.QueryStreamBusinessName;
                }
            }

            List<InitiativeNegaraMitraViewModel> negaraMitras = new List<InitiativeNegaraMitraViewModel>();
            negaraMitras = _repository.GetNamaNegara(rec.Id);
            string strNegaraMitra = string.Empty;
            string strKawasanMitra = string.Empty;
            foreach (var negaraMitra in negaraMitras)
            {
                if (negaraMitras.Count() > 1)
                {
                    strNegaraMitra += negaraMitra.NamaNegara;
                    strKawasanMitra += negaraMitra.NamaKawasan;
                    if (negaraMitra == negaraMitras.LastOrDefault())
                    {
                    }
                    else
                    {
                        strNegaraMitra += ", ";
                        strKawasanMitra += ", ";
                    }
                }
                else
                {
                    strNegaraMitra += negaraMitra.NamaNegara;
                    strKawasanMitra += negaraMitra.NamaKawasan;
                }
            }

            List<InitiativeMilestoneTimelineViewModel> milestoneTimeline = new List<InitiativeMilestoneTimelineViewModel>();
            var targetDefinitive = string.Empty;
            var targetMoU = string.Empty;
            milestoneTimeline = _repository.GetIntitativeMilestoneTimelineById(rec.Id);
            if (milestoneTimeline.Count > 0)
            {
                targetDefinitive = milestoneTimeline.FirstOrDefault().TargetDate.Value.ToString("dd/MM/yyyy");
                targetMoU = milestoneTimeline.LastOrDefault().TargetDate.Value.ToString("dd/MM/yyyy");

            }

            HubHeadViewModel hubHead = new HubHeadViewModel();
            hubHead = _initiativeDashboardRepository.GetHubHead(rec.Id);
            PicFungsiViewModel picLead = new PicFungsiViewModel();
            picLead = _initiativeDashboardRepository.GetPicLead(rec.Id);

            rec.RowStatus = _repository.GetStatusDraft(rec.Id);
            rec.RowJudulInitiative = _repository.GetJudulInitiative(rec.Id);
            rec.RowPartner = strPartner;
            rec.RowEntitasPertamina = strEntitasPertamina;
            rec.RowStreamBusiness = strStreamBusiness;
            rec.RowInitiativeStage = _repository.GetInitiativeStage(rec.Id);
            rec.RowInitiativeStatus = _repository.GetInitiativeStatus(rec.Id);
            rec.RowNegaraMitra = strNegaraMitra;
            rec.RowKawasanMitra = strKawasanMitra;
            rec.RowTargetDefinitive = targetDefinitive;
            rec.RowTargetMoU = targetMoU;
            rec.RowHubHeadName = hubHead != null ? hubHead.Name : "-";
            rec.RowPicLeadName = picLead != null ? picLead.PicName : "-";
            rec.RowPotentialRevenue = rec.PotentialRevenuePerYear != null ? (rec.PotentialRevenuePerYear.Value / 1000000).ToString("G29") : "-";
            rec.RowCapex = rec.Capex != null ? (rec.Capex.Value / 1000000).ToString("G29") : "-";
            rec.RowStatusMilestone = "-";
            rec.RowSupportRequired = "-";

            return rec;
        }

        public async Task<List<PotentialInitiativeHighPriorityViewModel>> GetInitiativeHighPriority(string wwwroot, SortViewModel sort)
        {
            var result = new List<PotentialInitiativeHighPriorityViewModel>();

            result = _initiativeDashboardRepository.GetHighPriority(wwwroot, sort);

            foreach (var item in result)
            {
                var listMilestones = _repository.GetIntitativeMilestoneTimelineById(item.InitiativeId);
                item.Capex = item.Capex / 1000000;
                item.RevenuePerYear = item.RevenuePerYear / 1000000;
                item.MilestoneTargetDate = listMilestones.LastOrDefault().TargetDate;
                item.MilestoneTargetDefinitive = listMilestones.Select(x => x.TargetDate).FirstOrDefault();
            }

            return result;
        }
        public async Task<InitiativeViewModel> GetInitiativeCountWithFilter(InitiativeViewModel model, string strSearch)
        {
            try
            {
                IQueryable<InitiativeViewModel> query = await _initiativeDashboardRepository.QueryInitiativeWithFilter(model);
                List<InitiativeStageViewModel> allRecordInitiativeStage = _initiativeDashboardRepository.GetRecordsInitiativeStage();
                List<InitiativeStageViewModel> setRecordInitiativeStage = new List<InitiativeStageViewModel>();
                foreach (var rec in allRecordInitiativeStage)
                {
                    rec.Count = query.Where(x => x.QueryInitiativeStageId == rec.Id && (x.JudulInisiasi.Contains(strSearch) ||
                                                                                        x.NilaiProyek.Contains(strSearch) ||
                                                                                        x.TargetWaktuProyek.Contains(strSearch) ||
                                                                                        x.Scope.Contains(strSearch) ||
                                                                                        x.Progress.Contains(strSearch) ||
                                                                                        x.IsuKendala.Contains(strSearch) ||
                                                                                        x.Referal.Contains(strSearch) ||
                                                                                        x.PotensiEskalasi.Contains(strSearch) ||
                                                                                        x.CatatanTambahan.Contains(strSearch) ||
                                                                                        x.SupportPemerintah.Contains(strSearch) ||
                                                                                        x.ValueForIndonesia.Contains(strSearch))).GroupBy(x => x.Id).Count();

                    setRecordInitiativeStage.Add(rec);
                }

                List<InitiativeStatusViewModel> allRecordInitiativeStatus = _initiativeDashboardRepository.GetRecordsInitiativeStatus();
                List<InitiativeStatusViewModel> setRecordInitiativeStatus = new List<InitiativeStatusViewModel>();
                foreach (var rec in allRecordInitiativeStatus)
                {
                    rec.Count = query.Where(x => x.QueryInitiativeStatusId == rec.Id && (x.JudulInisiasi.Contains(strSearch) ||
                                                                                        x.NilaiProyek.Contains(strSearch) ||
                                                                                        x.TargetWaktuProyek.Contains(strSearch) ||
                                                                                        x.Scope.Contains(strSearch) ||
                                                                                        x.Progress.Contains(strSearch) ||
                                                                                        x.IsuKendala.Contains(strSearch) ||
                                                                                        x.Referal.Contains(strSearch) ||
                                                                                        x.PotensiEskalasi.Contains(strSearch) ||
                                                                                        x.CatatanTambahan.Contains(strSearch) ||
                                                                                        x.SupportPemerintah.Contains(strSearch) ||
                                                                                        x.ValueForIndonesia.Contains(strSearch))).GroupBy(x => x.Id).Count();
                    setRecordInitiativeStatus.Add(rec);
                }

                List<HshViewModel> allRecordHsh = _initiativeDashboardRepository.GetRecordsHsh();
                List<HshViewModel> setRecordHsh = new List<HshViewModel>();
                foreach (var rec in allRecordHsh)
                {
                    rec.Count = query.Where(x => x.QueryHshId == rec.Id && (x.JudulInisiasi.Contains(strSearch) ||
                                                                            x.NilaiProyek.Contains(strSearch) ||
                                                                            x.TargetWaktuProyek.Contains(strSearch) ||
                                                                            x.Scope.Contains(strSearch) ||
                                                                            x.Progress.Contains(strSearch) ||
                                                                            x.IsuKendala.Contains(strSearch) ||
                                                                            x.Referal.Contains(strSearch) ||
                                                                            x.PotensiEskalasi.Contains(strSearch) ||
                                                                            x.CatatanTambahan.Contains(strSearch) ||
                                                                            x.SupportPemerintah.Contains(strSearch) ||
                                                                            x.ValueForIndonesia.Contains(strSearch))).GroupBy(x => x.Id).Count();
                    setRecordHsh.Add(rec);
                }

                model.CountInitiativeStage = setRecordInitiativeStage;
                model.CountInitiativeStatus = setRecordInitiativeStatus;
                model.CountInitiativeHolder = setRecordHsh;
                model.CountInitiative = query.Where(x => x.JudulInisiasi.Contains(strSearch) ||
                                                                            x.NilaiProyek.Contains(strSearch) ||
                                                                            x.TargetWaktuProyek.Contains(strSearch) ||
                                                                            x.Scope.Contains(strSearch) ||
                                                                            x.Progress.Contains(strSearch) ||
                                                                            x.IsuKendala.Contains(strSearch) ||
                                                                            x.Referal.Contains(strSearch) ||
                                                                            x.PotensiEskalasi.Contains(strSearch) ||
                                                                            x.CatatanTambahan.Contains(strSearch) ||
                                                                            x.SupportPemerintah.Contains(strSearch) ||
                                                                            x.ValueForIndonesia.Contains(strSearch)).GroupBy(x => x.Id).Count();

                model.CountNegaraMitra = _initiativeDashboardRepository.GetRecordsNegaraMitraWithSearch(model, strSearch).Result.Count();
                model.IsError = false;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        public async Task<PotentialInitiativesDashboardMapViewModel> GetCountriesMap()
        {
            PotentialInitiativesDashboardMapViewModel result = new PotentialInitiativesDashboardMapViewModel();

            result.HubGrouppedCountries = await _initiativeDashboardRepository.GetHubGrouppingCountries();
            result.CountriesAcronym = await _initiativeDashboardRepository.GetCountriesMap();
            result.Pointers = await _initiativeDashboardRepository.GetSubHoldingMap();

            return result;
        }
        public async Task<PotentialInitiativeHighPriorityViewModel> GetHighPriorityDetailById(Guid guid)
        {
            PotentialInitiativeHighPriorityViewModel result = new PotentialInitiativeHighPriorityViewModel();

            result = await _initiativeDashboardRepository.GetDetailHighPriorityById(guid);
            result.PicFungsiMembers = await _initiativeDashboardRepository.GetPicMembers(result.InitiativeId);
            result.Capex = result.Capex != null ? (result.Capex.Value / 1000000) : 0;
            result.RevenuePerYear = result.RevenuePerYear != null ? (result.RevenuePerYear.Value / 1000000) : 0;
            return result;

        }
    }
}
