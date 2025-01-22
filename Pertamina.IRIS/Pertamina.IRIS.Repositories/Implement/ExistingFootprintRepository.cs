using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Core;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Utility.Wording.Implement;
using Pertamina.IRIS.Utility.Wording.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class ExistingFootprintRepository : IExistingFootprintRepository
    {
        protected readonly DB_PINMContext _contex;
        protected readonly IMapper _mapper;
        protected readonly IUpdatedWordingUtility _updatedWordingUtility;

        public ExistingFootprintRepository(DB_PINMContext contex, IMapper mapper, IUpdatedWordingUtility updatedWordingUtility)
        {
            _contex=contex;
            _mapper=mapper;
            _updatedWordingUtility = updatedWordingUtility;
        }
        #region Grid
        private IQueryable<ExistingFootprintViewModel> QueryList(IQueryable<TbltExistingFootprint> query)
        {
            var result = query
            .LeftJoin(_contex.TbltExistingFootprintsOperatingMetrics, c => c.Id, d => d.ExistingFootprintsId, (c, d) => new
            {
                c.Id,
                c.EntitasPertaminaId,
                c.StreamBusinessId,
                c.HubId,
                c.NegaraMitraId,
                OperatingMetricsRevenue = d.Revenue,
                OperatingMetricsEbitda = d.Ebitda,
                OperatingMetricsTAssets = d.TotalAsset,
                OperatingMetricsTYear = d.Year,

            })
             .AsEnumerable()
              .GroupBy(g => g.Id)
              .Select(g => g.OrderByDescending(x => x.OperatingMetricsTYear).FirstOrDefault())
           .Join(_contex.TbltExistingFootprintsHubHeads, c => c.Id, d => d.ExistingFootprintsId, (c, d) => new
           {
               c.Id,
               c.EntitasPertaminaId,
               c.StreamBusinessId,
               c.HubId,
               c.NegaraMitraId,
               c.OperatingMetricsRevenue,
               c.OperatingMetricsEbitda,
               c.OperatingMetricsTAssets,
               c.OperatingMetricsTYear,
               HubHeadID = d.HubHeadId,

           })
           .Join(_contex.TbltExistingFootprintsPics, e => e.Id, f => f.ExistingFootprintsId, (e, f) => new
           {
               e.Id,
               e.EntitasPertaminaId,
               e.StreamBusinessId,
               e.NegaraMitraId,
               e.OperatingMetricsRevenue,
               e.OperatingMetricsEbitda,
               e.OperatingMetricsTAssets,
               e.OperatingMetricsTYear,
               e.HubId,
               PicFungsiId = f.PicFungsiId,
               PicLevelId = f.PicLevelId,

           })

           //.AsEnumerable()
           // .GroupBy(g => g.Id)
           // .Select(g => g.OrderByDescending(x => x.PicLevelId).FirstOrDefault())
           .Join(_contex.TblmPicFungsis, k => k.PicFungsiId, l => l.Id, (k, l) => new
           {

               k.Id,
               k.EntitasPertaminaId,
               k.StreamBusinessId,
               k.NegaraMitraId,
               k.OperatingMetricsRevenue,
               k.OperatingMetricsEbitda,
               k.OperatingMetricsTAssets,
               k.OperatingMetricsTYear,
               k.HubId,
               k.PicLevelId,
               k.PicFungsiId,
               PicFungsiName = l.PicName,

           })
           .Join(_contex.TblmPicLevels, m => m.PicLevelId, n => n.Id, (m, n) => new
           {

               m.Id,
               m.EntitasPertaminaId,
               m.StreamBusinessId,
               m.NegaraMitraId,
               m.OperatingMetricsRevenue,
               m.OperatingMetricsEbitda,
               m.OperatingMetricsTAssets,
               m.OperatingMetricsTYear,
               m.HubId,
               m.PicLevelId,
               m.PicFungsiId,
               m.PicFungsiName,
               PicLevelName = n.Name,
               IsLead = n.IsLead,

           })
           .Where(x => x.IsLead == true)
           .Join(_contex.TblmEntitasPertaminas, g => g.EntitasPertaminaId, h => h.Id, (g, h) => new
           {
               g.Id,
               g.EntitasPertaminaId,
               g.StreamBusinessId,
               g.NegaraMitraId,
               g.OperatingMetricsRevenue,
               g.OperatingMetricsEbitda,
               g.OperatingMetricsTAssets,
               g.OperatingMetricsTYear,
               g.HubId,
               g.PicLevelId,
               g.PicFungsiId,
               g.PicFungsiName,
               g.PicLevelName,
               EntitasId = h.Id,
               EntitasCode = h.Code,
           })
           .Join(_contex.TblmStreamBusinesses, i => i.StreamBusinessId, j => j.Id, (g, h) => new
           {
               g.Id,
               g.EntitasPertaminaId,
               g.NegaraMitraId,
               g.StreamBusinessId,
               g.OperatingMetricsRevenue,
               g.OperatingMetricsEbitda,
               g.OperatingMetricsTAssets,
               g.OperatingMetricsTYear,
               g.HubId,
               g.PicLevelId,
               g.PicFungsiId,
               g.PicFungsiName,
               g.PicLevelName,
               g.EntitasId,
               g.EntitasCode,
               SBId = h.Id,
               SBProject = h.Name,
               SBpProjectDescription = h.Description
           })
           .Join(_contex.TblmHubs, o => o.HubId, p => p.Id, (o, p) => new
           {
               o.Id,
               o.EntitasPertaminaId,
               o.StreamBusinessId,
               o.NegaraMitraId,
               o.OperatingMetricsRevenue,
               o.OperatingMetricsEbitda,
               o.OperatingMetricsTAssets,
               o.OperatingMetricsTYear,
               o.HubId,
               o.PicLevelId,
               o.PicFungsiId,
               o.PicFungsiName,
               o.PicLevelName,
               o.EntitasId,
               o.EntitasCode,
               o.SBId,
               o.SBProject,
               o.SBpProjectDescription,
               HubName = p.Name,

           })
           .Join(_contex.TblmNegaraMitras, o => o.NegaraMitraId, p => p.Id, (o, p) => new
           {
               o.Id,
               o.EntitasPertaminaId,
               o.StreamBusinessId,
               o.NegaraMitraId,
               o.OperatingMetricsRevenue,
               o.OperatingMetricsEbitda,
               o.OperatingMetricsTAssets,
               o.OperatingMetricsTYear,
               o.HubId,
               o.PicLevelId,
               o.PicFungsiId,
               o.PicFungsiName,
               o.PicLevelName,
               o.EntitasId,
               o.EntitasCode,
               o.SBId,
               o.SBProject,
               o.SBpProjectDescription,
               o.HubName,
               NegaraMitra = p.NamaNegara,

           })
           .Join(_contex.TblmHubHeads, q => q.HubId, r => r.HubId, (q, r) => new
           {
               q.Id,
               q.EntitasPertaminaId,
               q.StreamBusinessId,
               q.NegaraMitraId,
               q.OperatingMetricsRevenue,
               q.OperatingMetricsEbitda,
               q.OperatingMetricsTAssets,
               q.OperatingMetricsTYear,
               q.HubId,
               q.PicLevelId,
               q.PicFungsiId,
               q.PicFungsiName,
               q.PicLevelName,
               q.EntitasId,
               q.EntitasCode,
               q.SBId,
               q.SBProject,
               q.SBpProjectDescription,
               q.HubName,
               q.NegaraMitra,
               HubHeadId = r.Id,
               HubHeadName = r.Name,

           })

           .Select(select => new ExistingFootprintViewModel
           {
               Id = select.Id,
               HubId = select.HubId,
               HubHeadId = select.HubHeadId,
               GridId = select.Id,
               GridRevenue = select.OperatingMetricsRevenue ?? 0,
               GridTotalAsset = select.OperatingMetricsTAssets ?? 0,
               GridEbitda = select.OperatingMetricsEbitda ?? 0,
               GridYear = select.OperatingMetricsTYear ?? 0,
               GridHubName = select.HubName?? "-",
               GridHubHeadName = select.HubHeadName ?? "-",
               GridEntitas = select.EntitasCode,
               GridPicFungsiName = select.PicFungsiName,
               GridPicLevelName = select.PicLevelName,
               GridNegaraMitra = select.NegaraMitra ?? "-",
               GridProjectType = select.SBProject ?? "-",
               GridProjectTypeDescription = select.SBpProjectDescription ?? "-",
           })

           .AsQueryable();
            return result;
        }

        private IQueryable<ExistingFootprintViewModel> QueryAll()
        {
            int? maxYear = 0;

            var query = _contex.TbltExistingFootprints.Where(header => header.DeletedDate == null)
                .LeftJoin(_contex.TblmEntitasPertaminas.Where(entitas => entitas.DeletedDate == null),
                combined => combined.EntitasPertaminaId,
                entitas => entitas.Id,
                (combined, entitas) => new
                {
                    Header = combined,
                    Entitas = entitas
                })
                .LeftJoin(_contex.TbltExistingFootprintsHubHeads.Where(hubHead => hubHead.DeletedDate == null),
                combined => combined.Header.Id,
                hubHead => hubHead.ExistingFootprintsId,
                (combined, hubHead) => new
                {
                    combined.Header,
                    combined.Entitas,
                    HubHead = hubHead
                })
                .LeftJoin(_contex.TblmHubHeads.Where(head => head.DeletedDate == null),
                combined => combined.HubHead.HubHeadId,
                head => head.Id,
                (combined, head) => new
                {
                    combined.Header,
                    combined.Entitas,
                    combined.HubHead,
                    Head = head
                })
                .LeftJoin(_contex.TbltExistingFootprintsPics.Where(pic => pic.DeletedDate == null && pic.PicLevelId == _contex.TblmPicLevels.Where(x => x.IsLead == true).FirstOrDefault().Id),
                combined => combined.Header.Id,
                pic => pic.ExistingFootprintsId,
                (combined, pic) => new
                {
                    combined.Header,
                    combined.Entitas,
                    combined.HubHead,
                    combined.Head,
                    PIC = pic
                })
                .LeftJoin(_contex.TblmPicFungsis.Where(picFungsi => picFungsi.DeletedDate == null),
                combined => combined.PIC.PicFungsiId,
                picFungsi => picFungsi.Id,
                (combined, picFungsi) => new
                {
                    combined.Header,
                    combined.Entitas,
                    combined.HubHead,
                    combined.Head,
                    combined.PIC,
                    PICFungsi = picFungsi
                })

                .LeftJoin(_contex.TblmNegaraMitras.LeftJoin(_contex.TblmNegaraMitraInfomations, negara => negara.Id, info => info.NegaraMitraId, (negara, info) => new
                {
                    Negara = negara,
                    InfoNegara = info,
                }).Where(negara => negara.Negara.DeletedDate == null),
                combined => combined.Header.NegaraMitraId,
                negara => negara.Negara.Id,
                (combined, negara) => new
                {
                    Header = combined.Header,
                    Entitas = combined.Entitas,
                    HubHead = combined.HubHead,
                    Head = combined.Head,
                    PIC = combined.PIC,
                    PICFungsi = combined.PICFungsi,
                    Negara = negara
                })
                .LeftJoin(_contex.TbltExistingFootprintsOperatingMetrics.Where(metric => metric.DeletedDate == null && metric.Year == (maxYear == 0 ? metric.Year : maxYear)),
                combined => combined.Header.Id,
                metric => metric.ExistingFootprintsId,
                (combined, metric) => new
                {
                    Header = combined.Header,
                    Entitas = combined.Entitas,
                    HubHead = combined.HubHead,
                    Head = combined.Head,
                    PIC = combined.PIC,
                    PICFungsi = combined.PICFungsi,
                    Negara = combined.Negara,
                    Metric = metric
                })
                .LeftJoin(_contex.TblmStreamBusinesses.Where(stream => stream.DeletedDate == null),
                combined => combined.Header.StreamBusinessId,
                stream => stream.Id,
                (combined, stream) => new
                {
                    Header = combined.Header,
                    Entitas = combined.Entitas,
                    HubHead = combined.HubHead,
                    Head = combined.Head,
                    PIC = combined.PIC,
                    PICFungsi = combined.PICFungsi,
                    Negara = combined.Negara,
                    Metric = combined.Metric,
                    Stream = stream
                })
                .LeftJoin(_contex.TblmHubs.Where(hub => hub.DeletedDate == null),
                combined => combined.Header.HubId,
                hub => hub.Id,
                (combined, hub) => new
                {
                    Header = combined.Header,
                    Entitas = combined.Entitas,
                    HubHead = combined.HubHead,
                    Head = combined.Head,
                    PIC = combined.PIC,
                    PICFungsi = combined.PICFungsi,
                    Negara = combined.Negara,
                    Metric = combined.Metric,
                    Stream = combined.Stream,
                    Hub = hub
                })
                .Select(x => new ExistingFootprintViewModel
                {
                    Id = x.Header.Id,
                    CreatedDate = x.Header.CreatedDate,
                    UpdatedDate = x.Header.UpdatedDate,
                    EntitasPertaminaId = x.Entitas.Id,
                    GridEntitas = x.Entitas.CompanyName ?? "N/A",
                    NegaraMitraId = x.Negara.Negara.Id,
                    GridNegaraMitra = x.Negara.Negara.NamaNegara ?? "N/A",
                    GridRevenueToString = (x.Metric.Revenue / 1000000).ToString() ?? "N/A",
                    GridTotalAssetToString = (x.Metric.TotalAsset / 1000000).ToString() ?? "N/A",
                    GridEbitdaToString = (x.Metric.Ebitda / 1000000).ToString() ?? "N/A",
                    GridYearToString = x.Metric.Year.ToString() ?? "N/A",
                    Revenue = (x.Metric.Revenue / 1000000),
                    TotalAsset = (x.Metric.TotalAsset / 1000000),
                    Ebitda = (x.Metric.Ebitda / 1000000),
                    GridYear = x.Metric.Year,
                    HubId = x.Head.HubId,
                    HubName = x.Hub.Name ?? "N/A",
                    HubHeadId = x.Head.Id,
                    GridHubHeadName = x.Head.Name ?? "N/A",
                    PicFungsiId = x.PICFungsi.Id,
                    GridPicFungsiName = x.PICFungsi.PicName ?? "N/A",
                    StreamBusinessId = x.Header.StreamBusinessId,
                    GridProjectType = x.Stream.Name ?? "N/A"
                })
                .AsQueryable();

            return query;
        }
        public async Task<ResponseDataTableBaseModel<List<ExistingFootprintViewModel>>> GetList(RequestFormDtBaseModel request, ExistingFootprintViewModel decodeModel)
        {
            try
            {
                var query = QueryAll();

                // Filtering data`
                var predicate = PredicateBuilder.New<ExistingFootprintViewModel>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.GridEntitas.ToLower().Contains(filter.Value.ToLower())))
                        .Or(x => (x.GridPicFungsiName.ToLower().Contains(filter.Value.ToLower())))
                        .Or(x => (x.GridHubHeadName.ToLower().Contains(filter.Value.ToLower())))
                        .Or(x => (x.GridProjectType.ToLower().Contains(filter.Value.ToLower())))
                        .Or(x => (x.GridYearToString.ToLower().Contains(filter.Value.ToLower())))
                        .Or(x => (x.GridNegaraMitra.ToLower().Contains(filter.Value.ToLower())));
                }

                query = query.Where(predicate);
                var joinquery = query;

                if (!string.IsNullOrEmpty(decodeModel.EntitasIdDecode))
                {
                    string[] strEntitas = decodeModel.EntitasIdDecode.Split('_');
                    List<Guid> guidList = new List<Guid>();

                    foreach(var str in strEntitas)
                    {
                        Guid newGuid = Guid.Parse(str);
                        guidList.Add(newGuid);
                    }
                    joinquery = joinquery.Where(x => guidList.Contains(x.EntitasPertaminaId.Value)).AsQueryable();
                }
                if (!string.IsNullOrEmpty(decodeModel.ProjectTypeIdDecode))
                {
                    string[] strEntitas = decodeModel.ProjectTypeIdDecode.Split('_');
                    List<Guid> guidList = new List<Guid>();

                    foreach (var str in strEntitas)
                    {
                        Guid newGuid = Guid.Parse(str);
                        guidList.Add(newGuid);
                    }
                    joinquery = joinquery.Where(x => guidList.Contains(x.StreamBusinessId.Value)).AsQueryable();
                }
                if (decodeModel.TotalAssetMin != null)
                {
                    joinquery = joinquery.Where(x => x.TotalAsset!= null && x.TotalAsset >=(decodeModel.TotalAssetMin)).AsQueryable();
                }
                if (decodeModel.TotalAssetMax != null)
                {
                    joinquery = joinquery.Where(x => x.TotalAsset  != null && x.TotalAsset <= (decodeModel.TotalAssetMax)).AsQueryable();
                }
                if (decodeModel.RevenueMin != null)
                {
                    joinquery = joinquery.Where(x => x.Revenue  != null && x.Revenue >= (decodeModel.RevenueMin)).AsQueryable();
                }
                if (decodeModel.RevenueMax != null)
                {
                    joinquery = joinquery.Where(x => x.Revenue  != null && x.Revenue <= (decodeModel.RevenueMax)).AsQueryable();
                } 
                if (decodeModel.EbitdaMin != null)
                {
                    joinquery = joinquery.Where(x => x.Ebitda  != null && x.Ebitda >= (decodeModel.EbitdaMin)).AsQueryable();
                }
                if (decodeModel.EbitdaMax != null)
                {
                    joinquery = joinquery.Where(x => x.Ebitda  != null && x.Ebitda <= (decodeModel.EbitdaMax)).AsQueryable();
                }
                
                if (decodeModel.YearDecode != null)
                {
                    string[] strYear = decodeModel.YearDecode.Split('_');
                    List<int> intList = new List<int>();

                    foreach (var str in strYear)
                    {
                        if (int.TryParse(str, out int resultYear))
                        {
                            intList.Add(resultYear);
                        }
                    }
                    joinquery = joinquery.Where(x => intList.Contains(x.GridYear.Value)).AsQueryable();
                }
                if (decodeModel.PartnerCountryIdDecode != null)
                {
                    string[] strPartner = decodeModel.PartnerCountryIdDecode.Split('_');
                    List<Guid> guidList = new List<Guid>();

                    foreach (var str in strPartner)
                    {
                        Guid newGuid = Guid.Parse(str);
                        guidList.Add(newGuid);
                    }
                    joinquery = joinquery.Where(x => guidList.Contains(x.NegaraMitraId.Value)).AsQueryable();
                }

                // Sorting data
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDirection)))
                {
                    if (request.SortColumnDirection == "asc")
                        joinquery = joinquery.OrderBy(c => EF.Property<string>(c, request.SortColumn));
                    else
                        joinquery = joinquery.OrderByDescending(c => EF.Property<string>(c, request.SortColumn));
                }
                else
                {
                    joinquery = joinquery.OrderByDescending(x => x.CreatedDate).AsQueryable();
                }

                var list = await PaginatedList<ExistingFootprintViewModel, ExistingFootprintViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<ExistingFootprintViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<ExistingFootprintViewModel>>(false, ex.Message);
            }
        }
        public async Task<ResponseDataTableBaseModel<List<ExistingFootprintsOperatingMetricViewModel>>> GetListOpenMetricById(RequestFormDtBaseModel request, Guid guid)
        {
            try
            {
                var query = _contex.TbltExistingFootprintsOperatingMetrics.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TbltExistingFootprintsOperatingMetric>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.TotalAsset.HasValue && x.TotalAsset.Value.ToString().Contains(filter.Value)))
                        .Or(x => (x.Year.HasValue && x.Year.Value.ToString().Contains(filter.Value)))
                        .Or(x => (x.Ebitda.HasValue && x.Ebitda.Value.ToString().Contains(filter.Value)))
                        .Or(x => (x.Revenue.HasValue && x.Revenue.Value.ToString().Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null && x.ExistingFootprintsId == guid);
                var resultQuery = query.Select(select => new ExistingFootprintsOperatingMetricViewModel
                {
                    Id = select.Id,
                    ExistingFootprintsId = select.ExistingFootprintsId,
                    Year = select.Year,
                    Revenue = select.Revenue,
                    TotalAsset = select.TotalAsset,
                    Ebitda = select.Ebitda
                }).OrderByDescending(x => x.Year);
                //query = QueryListOpeningMetric(query);
                var firstJoinResult = query;

                // Sorting data
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDirection)))
                {
                    if (request.SortColumnDirection == "asc")
                        query = query.OrderBy(c => EF.Property<string>(c, request.SortColumn));
                    else
                        query = query.OrderByDescending(c => EF.Property<string>(c, request.SortColumn));
                }
                else
                {
                    query = query.OrderBy(x => x.Year).AsQueryable();
                }

                var list = await PaginatedList<ExistingFootprintsOperatingMetricViewModel, ExistingFootprintsOperatingMetricViewModel>.CreateAsync(resultQuery, request.PageValue, request.PageSize, query.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<ExistingFootprintsOperatingMetricViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<ExistingFootprintsOperatingMetricViewModel>>(false, ex.Message);
            }
        }
        #endregion

        public async Task<ExistingFootprintViewModel> GetHubHeadByHubId(Guid hubId)
        {
            var query = await _contex.TblmHubHeads
            .Where(x => x.HubId == hubId && x.DeletedDate == null)
            .Join(
                _contex.TblmHubs.Where(x => x.Id == hubId), // Inner sequence
                hubHead => hubHead.HubId,                 // Outer key selector
                hub => hub.Id,                            // Inner key selector
                (hubHead, hub) => new { hubHead, hub }    // Result selector
            )
            .Select(joined => new ExistingFootprintViewModel
            {
                HubHeadId = joined.hubHead.Id,
                HubId = hubId,
                HubHeadName = joined.hubHead.Name ?? "-",
                HubName = joined.hubHead.Name,
            })
            .FirstOrDefaultAsync();


            return query;
        }
        public async Task<ExistingFootprintViewModel> GetEntitasById(Guid entitasId)
        {
            var query = await _contex.TblmEntitasPertaminas
              .Where(x => x.Id == entitasId && x.DeletedDate == null)
              .Select(select => new ExistingFootprintViewModel
               {
                  EntitasPertaminaId = select.Id,
                  GridEntitas = select.CompanyName
               })
              .FirstOrDefaultAsync();


            return query;
        }

        public async Task<ExistingFootprintViewModel> GetExistingFootprintById(Guid guid, Guid levelId)
        {
            bool filter = false;
            ExistingFootprintViewModel model = new ExistingFootprintViewModel();
            model.PicFungsi = _mapper.Map<ExistingFootprintsPICViewModel>
                (await _contex.TbltExistingFootprintsPics
                .Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null && x.PicLevelId != levelId).FirstOrDefaultAsync());

            model.PicFungsis = _mapper.Map<List<ExistingFootprintsPICViewModel>>(
                await _contex.TbltExistingFootprintsPics
                .Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null && x.PicLevelId == levelId).ToListAsync());

            model.OperatingMetricss = _mapper.Map<List<ExistingFootprintsOperatingMetricViewModel>>(
                await _contex.TbltExistingFootprintsOperatingMetrics
                .Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null).ToListAsync());
            model.Partnerss = _mapper.Map<List<ExistingFootprintsPartnersViewModel>>(
                await _contex.TbltExistingFootprintsPartners
                .Where(x => x.ExistingFootPrintsId == guid && x.DeletedDate == null).ToListAsync());

            foreach (var partner in model.Partnerss)
            {
                if (partner.CreatedDate == null)
                {
                    filter = true;
                    break;
                }
            }
            if (filter)
            {
                model.Partnerss =  _mapper.Map<List<ExistingFootprintsPartnersViewModel>>(
                await _contex.TbltExistingFootprintsPartners
                .Where(x => x.ExistingFootPrintsId == guid && x.DeletedDate == null && x.CreatedDate == null).ToListAsync());

            }
            filter = false;
            foreach (var pic in model.PicFungsis)
            {
                if (pic.CreatedDate == null)
                {
                    filter = true;
                    break;
                }
            }
            if (filter)
            {
                model.PicFungsis =  _mapper.Map<List<ExistingFootprintsPICViewModel>>(
                await _contex.TbltExistingFootprintsPics
                .Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null && x.CreatedDate == null&& x.PicLevelId == levelId).ToListAsync());

            }
            filter = false;
            if (model.PicFungsi != null)
            {
                if (model.PicFungsi.CreatedDate== null)
                {
                    model.PicFungsi = _mapper.Map<ExistingFootprintsPICViewModel>
                       (await _contex.TbltExistingFootprintsPics
                       .Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null && x.PicLevelId != levelId && x.CreatedDate == null).FirstOrDefaultAsync());
                }
            }

            return model;
        }
        public IQueryable<Select2BaseModel> GetDdlYear(Guid guid, int year)
        {
            var result = (from rec in _contex.TbltExistingFootprintsOperatingMetrics
                          where rec.DeletedDate == null && rec.ExistingFootprintsId == guid && rec.Year == year
                          select new Select2BaseModel()
                          {
                              id = rec.Year.ToString(),
                              text = rec.Year.ToString(),
                          });
            return result;
        }

        public ExistingFootprintViewModel CreateExistingFootprint(ExistingFootprintViewModel model)
        {
            var entitys = new TbltExistingFootprint()
            {
                Id = model.Id,
                HubId = model.HubId,
            };
            _contex.Set<TbltExistingFootprint>().Add(entitys);
            _contex.SaveChanges();
            var result = _mapper.Map<ExistingFootprintViewModel>(entitys);

            return result;
        }
        public Guid GetMitraIdByHubId(Guid hubId)
        {
            var query = _contex.TblmNegaraMitras
             .Where(x => x.HubId == hubId && x.DeletedDate == null).FirstOrDefault();

            return query.Id;

        }
        public Guid GetHubId()
        {
            var query = _contex.TblmHubs
             .Where(x => x.DeletedDate == null).FirstOrDefault();

            return query.Id;

        }
        public Guid GetPicLevelLeadId()
        {
            var query = _contex.TblmPicLevels
             .Where(x => x.DeletedDate == null&& x.IsLead== true).FirstOrDefault();

            return query.Id;

        }
        public Guid GetPicLevelMemberId()
        {
            var query = _contex.TblmPicLevels
             .Where(x => x.DeletedDate == null&& x.IsLead== false).FirstOrDefault();

            return query.Id;

        }
        public Guid GetOMIdByHubId(Guid existingFottprintsId)
        {
            var query = _contex.TbltExistingFootprintsOperatingMetrics
             .Where(x => x.ExistingFootprintsId == existingFottprintsId && x.DeletedDate == null).FirstOrDefault();

            return query.Id;

        }
        public Guid GetHubIdIdByHubId(Guid hubId)
        {
            var query = _contex.TblmHubs
             .Where(x => x.Id == hubId && x.DeletedDate == null).FirstOrDefault();

            return query.Id;

        }
        public bool UpdatePartial(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, List<ExistingFootprintsPICViewModel> pics, ExistingFootprintsOperatingMetricViewModel om)
        {
            bool result = false;
            using (var dbContextTransaction = _contex.Database.BeginTransaction())
            {
                try
                {
                    var modelEntity = _contex.Set<TbltExistingFootprint>().Find(model.Id);
                    if (modelEntity != null)
                    {
                        modelEntity.StreamBusinessId=model.StreamBusinessId;
                        modelEntity.EntitasPertaminaId= model.EntitasPertaminaId;
                        modelEntity.NegaraMitraId=model.NegaraMitraId;
                        modelEntity.HubId=model.HubId;
                        modelEntity.UpdatedDate=model.UpdatedDate;
                    }
                    _contex.Set<TbltExistingFootprint>().Update(modelEntity);
                    if (partners.Count > 0)
                    {
                        foreach (var partner in partners)
                        {
                            var partnerEntity = new TbltExistingFootprintsPartner
                            {
                                Id  = partner.Id,
                                ExistingFootPrintsId =partner.ExistingFootPrintsId,
                                Partners=partner.Partners,
                                Location = partner.Location
                            };
                            _contex.Set<TbltExistingFootprintsPartner>().Add(partnerEntity);
                        }
                    }
                    if (hd.HubHeadId != Guid.Empty)
                    {
                        _contex.Set<TbltExistingFootprintsHubHead>().Add(_mapper.Map<TbltExistingFootprintsHubHead>(hd));
                    }
                    if (pic.Id != Guid.Empty)
                    {
                        _contex.Set<TbltExistingFootprintsPic>().Add(_mapper.Map<TbltExistingFootprintsPic>(pic));
                    }

                    var operatingMetricEntity = _contex.TbltExistingFootprintsOperatingMetrics.Find(om.Id);
                    if (operatingMetricEntity != null)
                    {
                        operatingMetricEntity.Id=om.Id;
                        operatingMetricEntity.Year=om.Year;
                        operatingMetricEntity.Ebitda=om.Ebitda;
                        operatingMetricEntity.Revenue=om.Revenue;
                        operatingMetricEntity.TotalAsset=om.TotalAsset;
                        operatingMetricEntity.CreatedBy=om.CreatedBy
                            ;

                    }
                    _contex.Set<TbltExistingFootprintsOperatingMetric>().Update(operatingMetricEntity);


                    if (pics.Count > 0)
                    {
                        foreach (var picEntitiys in pics)
                        {
                            var picEntity = new TbltExistingFootprintsPic
                            {
                                Id  = picEntitiys.Id,
                                ExistingFootprintsId =picEntitiys.ExistingFootprintsId,
                                PicFungsiId=picEntitiys.PicFungsiId,
                                PicLevelId = picEntitiys.PicLevelId
                            };
                            _contex.Set<TbltExistingFootprintsPic>().Add(picEntity);
                        }
                    }
                    int queryCommit = _contex.SaveChanges();
                    dbContextTransaction.Commit();
                    if (queryCommit > 0)
                    {
                        result = true;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    dbContextTransaction.Rollback();
                    return result;
                }


            }


        }
        public bool DeletePartial(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, List<ExistingFootprintsPICViewModel> pics, ExistingFootprintsOperatingMetricViewModel om)
        {

            bool result = false;
            using (var dbContextTransaction = _contex.Database.BeginTransaction())
            {
                try
                {

                    var modelEntity = _contex.Set<TbltExistingFootprint>().Find(model.Id);
                    if (modelEntity != null)
                    {
                        modelEntity.StreamBusinessId=model.StreamBusinessId;
                        modelEntity.EntitasPertaminaId= model.EntitasPertaminaId;
                        modelEntity.NegaraMitraId=model.NegaraMitraId;
                        modelEntity.HubId=model.HubId;
                        modelEntity.UpdatedDate=model.UpdatedDate;
                    }
                    _contex.Set<TbltExistingFootprint>().Update(modelEntity);
                    if (partners.Count > 0)
                    {
                        foreach (var partner in partners)
                        {
                            var partnerEntity = new TbltExistingFootprintsPartner
                            {
                                Id  = partner.Id,
                                ExistingFootPrintsId =partner.ExistingFootPrintsId,
                                Partners=partner.Partners,
                                Location = partner.Location
                            };
                            _contex.Set<TbltExistingFootprintsPartner>().Add(partnerEntity);
                        }
                    }
                    if (hd.HubHeadId != Guid.Empty)
                    {
                        _contex.Set<TbltExistingFootprintsHubHead>().Add(_mapper.Map<TbltExistingFootprintsHubHead>(hd));
                    }
                    if (pic.Id != Guid.Empty)
                    {
                        _contex.Set<TbltExistingFootprintsPic>().Add(_mapper.Map<TbltExistingFootprintsPic>(pic));
                    }
                    var getOperatingMetricEntity = _contex.Set<TbltExistingFootprintsOperatingMetric>().Find(model.OperatingMetrics.Id);
                    if (getOperatingMetricEntity != null)
                    {
                        if (getOperatingMetricEntity.CreatedDate != null)
                        {
                            getOperatingMetricEntity.DeletedDate= DateTime.Now;
                            _contex.Set<TbltExistingFootprintsOperatingMetric>().Update(getOperatingMetricEntity);
                        }
                        else
                        {
                            _contex.Set<TbltExistingFootprintsOperatingMetric>().RemoveRange(getOperatingMetricEntity);
                        }
                    }

                    if (pics.Count > 0)
                    {
                        foreach (var picEntitiys in pics)
                        {
                            var picEntity = new TbltExistingFootprintsPic
                            {
                                Id  = picEntitiys.Id,
                                ExistingFootprintsId =picEntitiys.ExistingFootprintsId,
                                PicFungsiId=picEntitiys.PicFungsiId,
                                PicLevelId = picEntitiys.PicLevelId
                            };
                            _contex.Set<TbltExistingFootprintsPic>().Add(picEntity);
                        }
                    }
                    int queryCommit = _contex.SaveChanges();
                    dbContextTransaction.Commit();
                    if (queryCommit > 0)
                    {
                        result = true;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    dbContextTransaction.Rollback();
                    return result;
                }


            }
        }
        public bool SavePartial(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, List<ExistingFootprintsPICViewModel> pics, ExistingFootprintsOperatingMetricViewModel om)
        {
            bool result = false;
            using (var dbContextTransaction = _contex.Database.BeginTransaction())
            {
                try
                {

                    var modelEntity = _contex.Set<TbltExistingFootprint>().Find(model.Id);
                    if (modelEntity != null)
                    {
                        modelEntity.StreamBusinessId=model.StreamBusinessId;
                        modelEntity.EntitasPertaminaId= model.EntitasPertaminaId;
                        modelEntity.NegaraMitraId=model.NegaraMitraId;
                        modelEntity.HubId=model.HubId;
                        modelEntity.UpdatedDate=model.UpdatedDate;
                    }
                    _contex.Set<TbltExistingFootprint>().Update(modelEntity);
                    if (partners.Count > 0)
                    {
                        foreach (var partner in partners)
                        {
                            var partnerEntity = new TbltExistingFootprintsPartner
                            {
                                Id  = partner.Id,
                                ExistingFootPrintsId =partner.ExistingFootPrintsId,
                                Partners=partner.Partners,
                                Location = partner.Location
                            };
                            _contex.Set<TbltExistingFootprintsPartner>().Add(partnerEntity);
                        }
                    }
                    if (hd.HubHeadId != Guid.Empty)
                    {
                        _contex.Set<TbltExistingFootprintsHubHead>().Add(_mapper.Map<TbltExistingFootprintsHubHead>(hd));
                    }
                    if (pic.Id != Guid.Empty)
                    {
                        _contex.Set<TbltExistingFootprintsPic>().Add(_mapper.Map<TbltExistingFootprintsPic>(pic));
                    }
                    _contex.Set<TbltExistingFootprintsOperatingMetric>().Add(_mapper.Map<TbltExistingFootprintsOperatingMetric>(om));
                    if (pics.Count > 0)
                    {
                        foreach (var picEntitiys in pics)
                        {
                            var picEntity = new TbltExistingFootprintsPic
                            {
                                Id  = picEntitiys.Id,
                                ExistingFootprintsId =picEntitiys.ExistingFootprintsId,
                                PicFungsiId=picEntitiys.PicFungsiId,
                                PicLevelId = picEntitiys.PicLevelId
                            };
                            _contex.Set<TbltExistingFootprintsPic>().Add(picEntity);
                        }
                    }
                    int queryCommit = _contex.SaveChanges();
                    dbContextTransaction.Commit();
                    if (queryCommit > 0)
                    {
                        result = true;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    dbContextTransaction.Rollback();
                    return result;
                }


            }


        }
        public bool Save(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, List<ExistingFootprintsPICViewModel> pics, List<ExistingFootprintsOperatingMetricViewModel> oms)
        {
            bool result = false;
            using (var dbContextTransaction = _contex.Database.BeginTransaction())
            {
                try
                {
                    _contex.Set<TbltExistingFootprint>().Add(new TbltExistingFootprint
                    {
                        Id  = model.Id,
                        CreatedBy=model.CreatedBy,
                        CreatedDate=model.CreatedDate,
                        UpdatedDate=null,
                        StreamBusinessId=model.StreamBusinessId,
                        EntitasPertaminaId= model.EntitasPertaminaId,
                        HubId =model.HubId,
                        NegaraMitraId=model.NegaraMitraId,
                    });

                    foreach (var partner in partners)
                    {
                        var partnerEntity = _contex.Set<TbltExistingFootprintsPartner>().Find(partner.Id);
                        if (partnerEntity != null)
                        {
                            partnerEntity.CreatedBy = partner.CreatedBy;
                            partnerEntity.CreatedDate = partner.CreatedDate;
                            partnerEntity.ExistingFootPrintsId = partner.ExistingFootPrintsId;
                            partnerEntity.Partners = partner.Partners;
                            partnerEntity.Location = partner.Location;

                            _contex.Set<TbltExistingFootprintsPartner>().Update(partnerEntity);
                        }
                        else
                        {
                            _contex.Set<TbltExistingFootprintsPartner>().Add(new TbltExistingFootprintsPartner
                            {
                                Id = partner.Id,
                                CreatedBy = partner.CreatedBy,
                                CreatedDate = partner.CreatedDate,
                                ExistingFootPrintsId = partner.ExistingFootPrintsId,
                                Partners = partner.Partners,
                                Location = partner.Location
                            });
                        }
                    }

                    var hubHeadEntity = _contex.Set<TbltExistingFootprintsHubHead>().Find(hd.Id);
                    if (hubHeadEntity != null)
                    {
                        hubHeadEntity.CreatedBy = hd.CreatedBy;
                        hubHeadEntity.CreatedDate = hd.CreatedDate;
                        hubHeadEntity.ExistingFootprintsId = model.Id;
                        hubHeadEntity.HubHeadId = hd.HubHeadId;

                        _contex.Set<TbltExistingFootprintsHubHead>().Update(hubHeadEntity);
                    }
                    else
                    {
                        _contex.Set<TbltExistingFootprintsHubHead>().Add(new TbltExistingFootprintsHubHead
                        {
                            Id= hd.Id,
                            CreatedBy = hd.CreatedBy,
                            CreatedDate = hd.CreatedDate,
                            ExistingFootprintsId = model.Id,
                            HubHeadId = hd.HubHeadId
                        });
                    }

                    var picLeadEntity = _contex.Set<TbltExistingFootprintsPic>().Find(pic.Id);
                    if (picLeadEntity != null)
                    {
                        picLeadEntity.CreatedBy = pic.CreatedBy;
                        picLeadEntity.CreatedDate = pic.CreatedDate;
                        picLeadEntity.ExistingFootprintsId = model.Id;
                        picLeadEntity.PicFungsiId = pic.PicFungsiId;
                        picLeadEntity.PicLevelId = pic.PicLevelId;

                        _contex.Set<TbltExistingFootprintsPic>().Update(picLeadEntity);
                    }
                    else
                    {
                        _contex.Set<TbltExistingFootprintsPic>().Add(new TbltExistingFootprintsPic
                        {
                            Id = pic.Id,
                            CreatedBy = pic.CreatedBy,
                            CreatedDate = pic.CreatedDate,
                            ExistingFootprintsId = model.Id,
                            PicFungsiId = pic.PicFungsiId,
                            PicLevelId = pic.PicLevelId
                        });
                    }

                    foreach (var picMember in pics)
                    {
                        var picMemberEntity = _contex.Set<TbltExistingFootprintsPic>().Find(picMember.Id);
                        if (picMemberEntity != null)
                        {
                            picMemberEntity.CreatedBy = picMember.CreatedBy;
                            picMemberEntity.CreatedDate = picMember.CreatedDate;
                            picMemberEntity.ExistingFootprintsId = model.Id;
                            picMemberEntity.PicFungsiId = picMember.PicFungsiId;
                            picMemberEntity.PicLevelId = picMember.PicLevelId;

                            _contex.Set<TbltExistingFootprintsPic>().Update(picMemberEntity);
                        }
                        else
                        {
                            _contex.Set<TbltExistingFootprintsPic>().Add(new TbltExistingFootprintsPic
                            {
                                Id = picMember.Id,
                                CreatedBy = picMember.CreatedBy,
                                CreatedDate = picMember.CreatedDate,
                                ExistingFootprintsId = model.Id,
                                PicFungsiId = picMember.PicFungsiId,
                                PicLevelId = picMember.PicLevelId
                            });
                        }
                    }

                    foreach (var om in oms)
                    {
                        var OmEntitiy = _contex.Set<TbltExistingFootprintsOperatingMetric>().Add(new TbltExistingFootprintsOperatingMetric

                        {
                            Id = om.Id,
                            CreatedBy=om.CreatedBy,
                            CreatedDate=om.CreatedDate,
                            Year= om.Year,
                            ExistingFootprintsId=om.ExistingFootprintsId,
                            Revenue = om.Revenue,
                            TotalAsset = om.TotalAsset,
                            Ebitda = om.Ebitda,

                        });
                    }
                    int queryCommit = _contex.SaveChanges();
                    dbContextTransaction.Commit();
                    if (queryCommit > 0)
                    {
                        result = true;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    dbContextTransaction.Rollback();
                    return result;
                }


            }


        }
        public bool SaveDraft(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, List<ExistingFootprintsHubHeadViewModel> hubHeads, List<ExistingFootprintsPICViewModel> picFungsis)
        {
            bool result = false;
            using (var dbContextTransaction = _contex.Database.BeginTransaction())
            {
                try
                {

                    return result;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    dbContextTransaction.Rollback();
                    return result;
                }


            }


        }
        #region crud
        public ExistingFootprintViewModel GetExistingFootprint(Guid guid)
        {
            ExistingFootprintViewModel result = _mapper.Map<ExistingFootprintViewModel>(_contex.TbltExistingFootprints.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        //public bool ClearTemporaryDataExistingFootprint(Guid? guid)
        //{
        //    bool result = false;

        //    using (var dbContextTransaction = _contex.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            if(guid != Guid.Empty)
        //            {
        //                var existingFootprints = _contex.TbltExistingFootprints
        //                                    .Where(x => x.CreatedDate == null && x.Id != guid.Value)
        //                                    .ToList();
        //                var existingFootprintsHubHeads = _contex.TbltExistingFootprintsHubHeads
        //                                    .Where(x => x.CreatedDate == null)
        //                                    .ToList();
        //                var existingFootprintsPartners = _contex.TbltExistingFootprintsPartners
        //                                        .Where(x => x.CreatedDate == null)
        //                                        .ToList();
        //                var existingFootprintsOperatingMetrics = _contex.TbltExistingFootprintsOperatingMetrics
        //                                        .Where(x => x.CreatedDate == null && x.ExistingFootprintsId != guid.Value)
        //                                        .ToList();
        //                var existingFootprintsPIC = _contex.TbltExistingFootprintsPics
        //                                       .Where(x => x.CreatedDate == null)
        //                                       .ToList();

        //                if (existingFootprints.Any() ||existingFootprintsHubHeads.Any() ||existingFootprintsPartners.Any() ||existingFootprintsOperatingMetrics.Any() ||existingFootprintsPIC.Any())
        //                {
        //                    //var footprintIds = existingFootprints.Select(x => x.Id).ToList();
        //                    var hubHeadIds = existingFootprintsHubHeads.Select(x => x.ExistingFootprintsId).ToList();
        //                    var picIds = existingFootprintsPIC.Select(x => x.ExistingFootprintsId).ToList();
        //                    var partnerIds = existingFootprintsPartners.Select(x => x.ExistingFootPrintsId).ToList();
        //                    var omIds = existingFootprintsOperatingMetrics.Select(x => x.ExistingFootprintsId).ToList();
        //                    var hubHead = _contex.TbltExistingFootprintsHubHeads
        //                                                  .Where(x => hubHeadIds.Contains(x.ExistingFootprintsId) && x.CreatedDate == null)
        //                                                  .ToList();

        //                    if (hubHead.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsHubHeads.RemoveRange(hubHead);
        //                    }

        //                    var partner = _contex.TbltExistingFootprintsPartners
        //                                                  .Where(x => partnerIds.Contains(x.ExistingFootPrintsId)&& x.CreatedDate == null)
        //                                                  .ToList();
        //                    if (partner.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsPartners.RemoveRange(partner);
        //                    }
        //                    var pic = _contex.TbltExistingFootprintsPics
        //                                                  .Where(x => picIds.Contains(x.ExistingFootprintsId) && x.CreatedDate == null)
        //                                                  .ToList();
        //                    if (pic.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsPics.RemoveRange(pic);
        //                    }
        //                    var om = _contex.TbltExistingFootprintsOperatingMetrics
        //                                                  .Where(x => omIds.Contains(x.ExistingFootprintsId) && x.CreatedDate == null)
        //                                                  .ToList();
        //                    if (om.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsOperatingMetrics.RemoveRange(om);
        //                    }
        //                    _contex.TbltExistingFootprints.RemoveRange(existingFootprints);
        //                }
        //            }
        //            else
        //            {
        //                var existingFootprints = _contex.TbltExistingFootprints
        //                                  .Where(x => x.CreatedDate == null)
        //                                  .ToList();
        //                var existingFootprintsHubHeads = _contex.TbltExistingFootprintsHubHeads
        //                                    .Where(x => x.CreatedDate == null)
        //                                    .ToList();
        //                var existingFootprintsPartners = _contex.TbltExistingFootprintsPartners
        //                                        .Where(x => x.CreatedDate == null)
        //                                        .ToList();
        //                var existingFootprintsOperatingMetrics = _contex.TbltExistingFootprintsOperatingMetrics
        //                                        .Where(x => x.CreatedDate == null)
        //                                        .ToList();
        //                var checkedDeleteBeforeDeleteOperatingMetrics = _contex.TbltExistingFootprintsOperatingMetrics
        //                                        .Where(x => x.DeletedDate != null && x.UpdatedDate ==null)
        //                                        .ToList();
        //                var existingFootprintsPIC = _contex.TbltExistingFootprintsPics
        //                                       .Where(x => x.CreatedDate == null)
        //                                       .ToList();

        //                if (existingFootprints.Any() ||existingFootprintsHubHeads.Any() ||existingFootprintsPartners.Any() ||existingFootprintsOperatingMetrics.Any() ||existingFootprintsPIC.Any())
        //                {
        //                    var footprintIds = existingFootprints.Select(x => x.Id).ToList();
        //                    var partners = existingFootprintsPartners.Select(x => x.Id).ToList();
        //                    var hubHeads = existingFootprintsHubHeads.Select(x => x.Id).ToList();
        //                    var pics = existingFootprintsPIC.Select(x => x.Id).ToList();
        //                    var oms = existingFootprintsOperatingMetrics.Select(x => x.Id).ToList();
        //                    var operatingMetrics = _contex.TbltExistingFootprintsOperatingMetrics
        //                                                  .Where(x => footprintIds.Contains(x.ExistingFootprintsId))
        //                                                  .ToList();

        //                    if (operatingMetrics.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsOperatingMetrics.RemoveRange(operatingMetrics);
        //                    }
        //                    var hubHead = _contex.TbltExistingFootprintsHubHeads
        //                                                  .Where(x => hubHeads.Contains(x.Id))
        //                                                  .ToList();

        //                    if (hubHead.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsHubHeads.RemoveRange(hubHead);
        //                    }

        //                    var partner = _contex.TbltExistingFootprintsPartners
        //                                                  .Where(x => partners.Contains(x.Id))
        //                                                  .ToList();
        //                    if (partner.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsPartners.RemoveRange(partner);
        //                    }
        //                    var pic = _contex.TbltExistingFootprintsPics
        //                                                  .Where(x => pics.Contains(x.Id))
        //                                                  .ToList();
        //                    if (pic.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsPics.RemoveRange(pic);
        //                    }
        //                    _contex.TbltExistingFootprints.RemoveRange(existingFootprints);
        //                    var om = _contex.TbltExistingFootprintsOperatingMetrics
        //                                                  .Where(x => oms.Contains(x.Id))
        //                                                  .ToList();

        //                    if (om.Any())
        //                    {
        //                        _contex.TbltExistingFootprintsOperatingMetrics.RemoveRange(om);
        //                    }
        //                }
        //            }
        //            int queryCommit = _contex.SaveChanges();

        //            dbContextTransaction.Commit();
        //            if (queryCommit > 0)
        //            {
        //                result = true;
        //            }
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            string message = ex.Message;
        //            dbContextTransaction.Rollback();
        //        }
        //    }

        //    return result;
        //}

        public ExistingFootprintsOperatingMetricViewModel GetExistingFootprintOperatingMetric(Guid guid)
        {
            ExistingFootprintsOperatingMetricViewModel result = _mapper.Map<ExistingFootprintsOperatingMetricViewModel>(_contex.TbltExistingFootprintsOperatingMetrics.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public ExistingFootprintViewModel IsExistingOperatingMetric(Guid guid)
        {

            ExistingFootprintViewModel result = new ExistingFootprintViewModel();
            result.OperatingMetrics= _mapper.Map<ExistingFootprintsOperatingMetricViewModel>(_contex.TbltExistingFootprintsOperatingMetrics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null).FirstOrDefault());

            return result;
        }
        public ExistingFootprintViewModel IsCheckedYearOperatingMetric(Guid guid, Guid operatingMetricId)
        {

            ExistingFootprintViewModel result = new ExistingFootprintViewModel();
            result.OperatingMetrics= _mapper.Map<ExistingFootprintsOperatingMetricViewModel>(_contex.TbltExistingFootprintsOperatingMetrics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null && x.Id != operatingMetricId).FirstOrDefault());

            return result;
        }
        public ExistingFootprintViewModel IsExistingPartner(Guid guid)
        {

            ExistingFootprintViewModel result = new ExistingFootprintViewModel();
            result.Partner= _mapper.Map<ExistingFootprintsPartnersViewModel>(_contex.TbltExistingFootprintsPartners.Where(x => x.ExistingFootPrintsId == guid && x.DeletedDate == null).FirstOrDefault());

            return result;
        }
        public ExistingFootprintViewModel IsExistingPic(Guid guid)
        {

            ExistingFootprintViewModel result = new ExistingFootprintViewModel();
            result.PicFungsi= _mapper.Map<ExistingFootprintsPICViewModel>(_contex.TbltExistingFootprintsPics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null).FirstOrDefault());

            return result;
        }
        public List<ExistingFootprintsPartnersViewModel> GetExistingFootprintPartners(Guid guid)
        {
            bool filter = false;
            var entity = _contex.TbltExistingFootprintsPartners
                .Where(x => x.ExistingFootPrintsId == guid && x.DeletedDate == null)
                .ToList();

            foreach (var partner in entity)
            {
                if (partner.CreatedDate == null)
                {
                    filter = true;
                    break;
                }
            }
            if (filter)
            {
                entity = _contex.TbltExistingFootprintsPartners
                .Where(x => x.ExistingFootPrintsId == guid && x.DeletedDate == null && x.CreatedDate == null)
                .ToList();
            }

            var result = _mapper.Map<List<ExistingFootprintsPartnersViewModel>>(entity);
            return result;
        }
        public List<ExistingFootprintsPartnersViewModel> GetAllExistingFootprintPartners(Guid guid)
        {
            var entity = _contex.TbltExistingFootprintsPartners
                .Where(x => x.ExistingFootPrintsId == guid && x.DeletedDate == null)
                .ToList();

            var result = _mapper.Map<List<ExistingFootprintsPartnersViewModel>>(entity);
            return result;
        }
        public List<ExistingFootprintsOperatingMetricViewModel> GetAllExistingFootprintOperatingMetrics(Guid guid)
        {
            var entity = _contex.TbltExistingFootprintsOperatingMetrics
                .Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null)
                .ToList();

            var result = _mapper.Map<List<ExistingFootprintsOperatingMetricViewModel>>(entity);
            return result;
        }
        public bool ResetExistingFootprintOperatingMetrics(Guid guid)
        {
            var entity = _contex.Set<TbltExistingFootprintsOperatingMetric>().Where(x => x.ExistingFootprintsId == guid).ToList();

            if (entity.Any())
            {
                _contex.Set<List<TbltExistingFootprintsOperatingMetric>>().Remove(entity);
                _contex.SaveChanges();
                return true;
            }
            return false;
        }

        public ExistingFootprintsPICViewModel GetExistingFootprintPIC(Guid guid)
        {
            ExistingFootprintsPICViewModel result = _mapper.Map<ExistingFootprintsPICViewModel>(_contex.TbltExistingFootprintsPics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public ExistingFootprintViewModel GetNegaraMitraById(Guid? negaraMitraId)
        {
            ExistingFootprintViewModel result = new ExistingFootprintViewModel();
            NegaraMitraViewModel getNegaraMitra = _mapper.Map<NegaraMitraViewModel>(_contex.TblmNegaraMitras.Where(x => x.Id == negaraMitraId && x.DeletedDate == null).FirstOrDefault());
            result.GridNegaraMitra = getNegaraMitra.NamaNegara;
            return result;
        }
        public ExistingFootprintsPICViewModel GetPICFungsiName(Guid? picFungsiId)
        {
            ExistingFootprintsPICViewModel result = new ExistingFootprintsPICViewModel();
            PicFungsiViewModel getPicFungsi = _mapper.Map<PicFungsiViewModel>(_contex.TblmPicFungsis.Where(x => x.Id== picFungsiId && x.DeletedDate == null).FirstOrDefault());
            result.PicFungsiName = getPicFungsi.PicName;
            return result;

        }
        public ExistingFootprintsPICViewModel GetExistingFootprintPICLead(Guid guid, Guid? picLevelLead)
        {
            bool filter = false;
            ExistingFootprintsPICViewModel result = _mapper.Map<ExistingFootprintsPICViewModel>(_contex.TbltExistingFootprintsPics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null && x.PicLevelId == picLevelLead).FirstOrDefault());
            if (result != null)
            {
                if (result.CreatedDate== null)
                {
                    filter = true;
                }
            }
            if (filter)
            {
                result = _mapper.Map<ExistingFootprintsPICViewModel>(_contex.TbltExistingFootprintsPics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null && x.PicLevelId == picLevelLead && x.CreatedDate == null).FirstOrDefault());

            }
            return result;
        }
        public List<ExistingFootprintsPICViewModel> GetExistingFootprintPICMember(Guid guid, Guid? picLevelMember)
        {
            bool filter = false;
            List<ExistingFootprintsPICViewModel> result = _mapper.Map<List<ExistingFootprintsPICViewModel>>(_contex.TbltExistingFootprintsPics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null  && x.PicLevelId == picLevelMember).ToList());
            foreach (var picMember in result)
            {
                if (picMember.CreatedDate== null)
                {
                    filter = true;
                    break;
                }
            }
            if (filter)
            {
                result = _mapper.Map<List<ExistingFootprintsPICViewModel>>(_contex.TbltExistingFootprintsPics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null  && x.PicLevelId == picLevelMember && x.CreatedDate == null).ToList());

            }
            return result;
        }
        public List<ExistingFootprintsPICViewModel> GetAllExistingFootprintPICMember(Guid guid, Guid? picLevelMember)
        {
            List<ExistingFootprintsPICViewModel> result = _mapper.Map<List<ExistingFootprintsPICViewModel>>(_contex.TbltExistingFootprintsPics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null  && x.PicLevelId == picLevelMember).ToList());
            return result;
        }
        public ExistingFootprintsOperatingMetricViewModel GetExistingFootprintOm(Guid guid)
        {
            ExistingFootprintsOperatingMetricViewModel result = _mapper.Map<ExistingFootprintsOperatingMetricViewModel>(_contex.TbltExistingFootprintsOperatingMetrics.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public ExistingFootprintsHubHeadViewModel GetExistingFootprintHubHead(Guid guid)
        {
            ExistingFootprintsHubHeadViewModel result = _mapper.Map<ExistingFootprintsHubHeadViewModel>(_contex.TbltExistingFootprintsHubHeads.Where(x => x.ExistingFootprintsId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }

        public bool Delete(ExistingFootprintViewModel model, List<ExistingFootprintsPartnersViewModel> partners, ExistingFootprintsHubHeadViewModel hd, ExistingFootprintsPICViewModel pic, ExistingFootprintsOperatingMetricViewModel om)
        {
            bool result = false;
            using (var dbContextTransaction = _contex.Database.BeginTransaction())
            {
                try
                {
                    var findFootPrintsEntitiy = _contex.Set<TbltExistingFootprint>().Find(model.Id);
                    findFootPrintsEntitiy.UpdatedBy=model.UpdatedBy;
                    findFootPrintsEntitiy.UpdatedDate=model.UpdatedDate;
                    findFootPrintsEntitiy.DeletedDate=model.DeletedDate;
                    _contex.Set<TbltExistingFootprint>().Update(findFootPrintsEntitiy);

                    foreach (var partner in partners)
                    {
                        var findPartnerEntitiy = _contex.Set<TbltExistingFootprintsPartner>().Find(partner.Id);

                        findPartnerEntitiy.UpdatedBy =partner.UpdatedBy;
                        findPartnerEntitiy.UpdatedDate=partner.UpdatedDate;
                        findPartnerEntitiy.DeletedDate=partner.DeletedDate;
                        _contex.Set<TbltExistingFootprintsPartner>().Update(findPartnerEntitiy);
                    }
                    var findHubHeadEntitiy = _contex.Set<TbltExistingFootprintsHubHead>().Find(hd.Id);
                    findHubHeadEntitiy.UpdatedBy=model.UpdatedBy;
                    findHubHeadEntitiy.UpdatedDate=model.UpdatedDate;
                    findHubHeadEntitiy.DeletedDate=model.DeletedDate;
                    _contex.Set<TbltExistingFootprintsHubHead>().Update(findHubHeadEntitiy);


                    var findPicEntitiy = _contex.Set<TbltExistingFootprintsPic>().Find(pic.Id);
                    findPicEntitiy.UpdatedBy=model.UpdatedBy;
                    findPicEntitiy.UpdatedDate=model.UpdatedDate;
                    findPicEntitiy.DeletedDate=model.DeletedDate;
                    _contex.Set<TbltExistingFootprintsPic>().Update(findPicEntitiy);

                    var findOm = _contex.Set<TbltExistingFootprintsOperatingMetric>().Find(om.Id);
                    findOm.UpdatedBy=model.UpdatedBy;
                    findOm.UpdatedDate=model.UpdatedDate;
                    findOm.DeletedDate=model.DeletedDate;
                    _contex.Set<TbltExistingFootprintsOperatingMetric>().Update(findOm);

                    int queryCommit = _contex.SaveChanges();
                    dbContextTransaction.Commit();
                    if (queryCommit > 0)
                    {
                        result = true;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    dbContextTransaction.Rollback();
                    return result;
                }
            }
        }
        public bool DeleteList(List<ExistingFootprintsPartnersViewModel> partners, List<ExistingFootprintsPICViewModel> pics, List<ExistingFootprintsOperatingMetricViewModel> oms)
        {
            bool result = false;
            using (var dbContextTransaction = _contex.Database.BeginTransaction())
            {
                try
                {
                    if (partners.Count > 0)
                    {
                        foreach (var partner in partners)
                        {
                            var findPartnerEntitiy = _contex.Set<TbltExistingFootprintsPartner>().Find(partner.Id);
                            _contex.Set<TbltExistingFootprintsPartner>().RemoveRange(findPartnerEntitiy);
                        }
                    }
                    if (pics.Count >0)
                    {
                        foreach (var pic in pics)
                        {
                            var findPic = _contex.Set<TbltExistingFootprintsPic>().Find(pic.Id);
                            _contex.Set<TbltExistingFootprintsPic>().RemoveRange(findPic);
                        }
                    }
                    if (oms.Count >0)
                    {
                        foreach (var om in oms)
                        {
                            var findOm = _contex.Set<TbltExistingFootprintsOperatingMetric>().Find(om.Id);
                            _contex.Set<TbltExistingFootprintsOperatingMetric>().RemoveRange(findOm);
                        }
                    }

                    int queryCommit = _contex.SaveChanges();
                    dbContextTransaction.Commit();
                    if (queryCommit > 0)
                    {
                        result = true;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    dbContextTransaction.Rollback();
                    return result;
                }
            }
        }
        public bool Update(ExistingFootprintViewModel model, List<ExistingFootprintsOperatingMetricViewModel> oms, string username)
        {
            bool result = false;
            using (var dbContextTransaction = _contex.Database.BeginTransaction())
            {
                try
                {
                    DateTime now = DateTime.Now;
                    
                    var findFootPrintsEntitiy = _contex.Set<TbltExistingFootprint>().Find(model.Id);
                    findFootPrintsEntitiy.UpdatedBy=username;
                    findFootPrintsEntitiy.UpdatedDate=now;
                    findFootPrintsEntitiy.StreamBusinessId = model.StreamBusinessId;
                    findFootPrintsEntitiy.EntitasPertaminaId = model.EntitasPertaminaId;
                    findFootPrintsEntitiy.HubId = model.HubId;
                    findFootPrintsEntitiy.NegaraMitraId = model.NegaraMitraId;
                    _contex.Set<TbltExistingFootprint>().Update(findFootPrintsEntitiy);

                    var findPartnerEntitiy = _contex.TbltExistingFootprintsPartners.Where(x=>x.ExistingFootPrintsId == model.Id);
                    _contex.TbltExistingFootprintsPartners.RemoveRange(findPartnerEntitiy);
                    foreach (var partner in model.Partnerss)
                    {

                        _contex.Set<TbltExistingFootprintsPartner>().Add(new TbltExistingFootprintsPartner
                        {
                            Id = Guid.NewGuid(),
                            CreatedBy = username,
                            CreatedDate = now,
                            UpdatedDate = now,
                            UpdatedBy = username,
                            ExistingFootPrintsId = model.Id,
                            Partners = partner.Partners,
                            Location = partner.Location
                        });
                    }
                    var findHubHeadEntitiy = _contex.TbltExistingFootprintsHubHeads.Where(x=>x.ExistingFootprintsId == model.Id).FirstOrDefault();
                    if(findHubHeadEntitiy != null)
                    {
                        findHubHeadEntitiy.UpdatedBy=username;
                        findHubHeadEntitiy.UpdatedDate=now;
                        findHubHeadEntitiy.HubHeadId=model.HubHeadId;
                        _contex.Set<TbltExistingFootprintsHubHead>().Update(findHubHeadEntitiy);
                    }
                   
                    var findPic = _contex.TbltExistingFootprintsPics.Where(x => x.ExistingFootprintsId == model.Id);
                    _contex.TbltExistingFootprintsPics.RemoveRange(findPic);
                    var findPicEntitiy = _contex.TbltExistingFootprintsPics.Where(x=>x.ExistingFootprintsId == model.Id && x.PicLevelId== model.PicLevelId).FirstOrDefault();
                    if(findPicEntitiy != null)
                    {
                        findPicEntitiy.UpdatedBy = username;
                        findPicEntitiy.UpdatedDate = now;
                        findPicEntitiy.PicFungsiId = model.PicFungsiId;
                        findPicEntitiy.PicLevelId = model.PicLevelId;
                        _contex.Set<TbltExistingFootprintsPic>().Update(findPicEntitiy);
                    }
                    else
                    {
                        TbltExistingFootprintsPic tPic = new TbltExistingFootprintsPic();
                        tPic.Id = Guid.NewGuid();
                        tPic.CreatedBy = username;
                        tPic.CreatedDate = now;
                        tPic.UpdatedBy = username;
                        tPic.UpdatedDate = now;
                        tPic.ExistingFootprintsId = model.Id;
                        tPic.PicFungsiId = model.PicFungsiId;
                        tPic.PicLevelId = model.PicLevelId;
                        _contex.Set<TbltExistingFootprintsPic>().Add(tPic);
                    }
                    
                    foreach (var picList in model.PicFungsis)
                    {
                        _contex.Set<TbltExistingFootprintsPic>().Add(new TbltExistingFootprintsPic
                        {
                            Id = Guid.NewGuid(),
                            CreatedBy = username,
                            CreatedDate = now,
                            UpdatedDate = now,
                            UpdatedBy = username,
                            ExistingFootprintsId = model.Id,
                            PicFungsiId = picList.PicFungsiId,
                            PicLevelId = model.PicLevelMemberId,
                        });
                    }


                    var findOperatingMetrics = _contex.TbltExistingFootprintsOperatingMetrics.Where(x => x.ExistingFootprintsId == model.Id);
                    _contex.TbltExistingFootprintsOperatingMetrics.RemoveRange(findOperatingMetrics);
                    foreach (var om in oms)
                    {

                        _contex.Set<TbltExistingFootprintsOperatingMetric>().Add(new TbltExistingFootprintsOperatingMetric
                        {
                            Id = Guid.NewGuid(),
                            CreatedDate = now,
                            CreatedBy = username,
                            UpdatedDate = now,
                            UpdatedBy = username,
                            ExistingFootprintsId =model.Id,
                            Year = om.Year,
                            Ebitda = om.Ebitda,
                            TotalAsset = om.TotalAsset,
                            Revenue = om.Revenue,
                        });
                    }
                    int queryCommit = _contex.SaveChanges();
                
                    if (queryCommit > 0)
                    {
                        result = true;
                        dbContextTransaction.Commit();
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    dbContextTransaction.Rollback();
                    return result;
                }
            }
        }
        public string GetProjectTypeById(Guid? streamBusinessId)
        {
            var query = _contex.TblmStreamBusinesses
             .Where(x => x.Id == streamBusinessId && x.DeletedDate == null).FirstOrDefault();

            return query.Name;
        }

        public IEnumerable<ExistingFootprintViewModel> GetAllExistingFootprintsExcel()
        {
            IQueryable<ExistingFootprintViewModel> items = _contex.VwExportExistingFootprints.Select(existingFootprints => new ExistingFootprintViewModel
            {
                CellEntitas = existingFootprints.EntitasPertaminaName ?? "",
                CellEntitasId = existingFootprints.EntitasPertaminaId.HasValue ==true? existingFootprints.EntitasPertaminaId.ToString():null,
                CellPicFungsiName = existingFootprints.Pic ?? "",
                CellHubHeadName = existingFootprints.HubHead ?? "",
                CellProjectType = existingFootprints.StreamBusinessName ?? "",
                CellProjectTypeId = existingFootprints.StreamBusinessId.HasValue == true ? existingFootprints.StreamBusinessId.ToString() :null,
                CellEbitda = existingFootprints.Ebitda.HasValue == true ? existingFootprints.Ebitda : null,
                CellRevenue = existingFootprints.Revenue.HasValue == true ? existingFootprints.Revenue : null,
                CellTotalAsset = existingFootprints.TotalAsset.HasValue == true ? existingFootprints.TotalAsset : null,
                CellYear = existingFootprints.LastYearDataMetrics.HasValue == true ? existingFootprints.LastYearDataMetrics : null,
                CellNegaraMitra = existingFootprints.PartnerCountry ?? "",
                CellNegaraMitraId = existingFootprints.NegaraMitraId.HasValue == true ? existingFootprints.NegaraMitraId.ToString():null,
            });
            return items;
        }
        #endregion
    }
}
