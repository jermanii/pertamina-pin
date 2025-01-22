using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Core;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Utility.Filtering.Interfaces;
using Pertamina.IRIS.Utility.Pagination.Interfaces;
using Pertamina.IRIS.Utility.Sorting.Interfaces;
using Pertamina.IRIS.Utility.Wording.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class ExistingFootprintsDashboardRepository : IExistingFootprintsDashboardRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;
        protected readonly IUpdatedWordingUtility _updatedWordingUtility;
        private readonly ISortingFuncUtility _sortingFunc;
        private readonly ITakeFuncUtility _takeFunc;
        private readonly IHighPriorityFilteringUtility _filteringUtility;

        public ExistingFootprintsDashboardRepository(DB_PINMContext context, IMapper mapper, IUpdatedWordingUtility updatedWordingUtility, ISortingFuncUtility sortingFunc, ITakeFuncUtility takeFunc, IHighPriorityFilteringUtility filteringUtility)
        {
            _context = context;
            _mapper = mapper;
            _updatedWordingUtility = updatedWordingUtility;
            _sortingFunc = sortingFunc;
            _takeFunc = takeFunc;
            _filteringUtility = filteringUtility;
        }

        private IQueryable<ExistingFootprintsDashboardHighPriorityViewModel> QueryAll(bool isHigh, bool isSingle, string wwwroot, int? year)
        {
            int? maxYear = 0;
            if (isHigh)
            {
                maxYear = _context.TbltExistingFootprintsOperatingMetrics
                .Join(_context.TbltExistingFootprintsHighPriorities, a => a.ExistingFootprintsId, b => b.ExistingFootprintsId, (a, b) => new
                {
                    metric = a,
                    high = b
                })
                .Max(r => r.metric.Year).HasValue ? _context.TbltExistingFootprintsOperatingMetrics.Max(r => r.Year) : DateTime.Now.Year;
            }

            if (isSingle)
            {
                maxYear = year;
            }

            var query = _context.TbltExistingFootprints.Where(header => header.DeletedDate == null)
                .LeftJoin(_context.TblmEntitasPertaminas.Where(entitas => entitas.DeletedDate == null),
                combined => combined.EntitasPertaminaId,
                entitas => entitas.Id,
                (combined, entitas) => new
                {
                    Header = combined,
                    Entitas = entitas
                })
                .LeftJoin(_context.TbltExistingFootprintsHubHeads.Where(hubHead => hubHead.DeletedDate == null),
                combined => combined.Header.Id,
                hubHead => hubHead.ExistingFootprintsId,
                (combined, hubHead) => new
                {
                    combined.Header,
                    combined.Entitas,
                    HubHead = hubHead
                })
                .LeftJoin(_context.TblmHubHeads.Where(head => head.DeletedDate == null),
                combined => combined.HubHead.HubHeadId,
                head => head.Id,
                (combined, head) => new
                {
                    combined.Header,
                    combined.Entitas,
                    combined.HubHead,
                    Head = head
                })
                .LeftJoin(_context.TbltExistingFootprintsPics.Where(pic => pic.DeletedDate == null && pic.PicLevelId == _context.TblmPicLevels.Where(x => x.IsLead == true).FirstOrDefault().Id),
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
                .LeftJoin(_context.TblmPicFungsis.Where(picFungsi => picFungsi.DeletedDate == null),
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
                .LeftJoin(_context.TblmNegaraMitras.LeftJoin(_context.TblmNegaraMitraInfomations, negara => negara.Id, info => info.NegaraMitraId, (negara, info) => new
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
                .LeftJoin(_context.TbltExistingFootprintsOperatingMetrics.Where(metric => metric.DeletedDate == null && metric.Year == (maxYear == 0 ? metric.Year : maxYear)),
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
                .LeftJoin(_context.TblmStreamBusinesses.Where(stream => stream.DeletedDate == null),
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
                .LeftJoin(_context.TblmHubs.Where(hub => hub.DeletedDate == null),
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
                .Select(x => new ExistingFootprintsDashboardHighPriorityViewModel
                {
                    Id = x.Header.Id,
                    CreatedDate = x.Header.CreatedDate,
                    UpdatedDate = x.Header.UpdatedDate,
                    ExistingFootprintsId = x.Header.Id,
                    EntitasPertaminaId = x.Entitas.Id,
                    CompanyName = x.Entitas.CompanyName,
                    NegaraMitraId = x.Negara.Negara.Id,
                    NamaNegara = x.Negara.Negara.NamaNegara,
                    CountryAcronyms = x.Negara.InfoNegara.CountryAcronyms,
                    Revenue = x.Metric.Revenue / 1000000,
                    TotalAsset = x.Metric.TotalAsset / 1000000,
                    Ebitda = x.Metric.Ebitda / 1000000,
                    Year = x.Metric.Year,
                    Flag = x.Negara.Negara.Flag,
                    ExistsFlag = File.Exists(wwwroot + x.Negara.Negara.Flag),
                    PathFlag = wwwroot + x.Negara.Negara.Flag,
                    HubId = x.Head.HubId,
                    HubName = x.Hub.Name ?? null,
                    HubHeadId = x.Head.Id,
                    HubHeadName = x.Head.Name ?? null,
                    HubHeadEmail = x.Head.UserEmail ?? null,
                    HubHeadContactNumber = x.Head.ContactNumber ?? null,
                    PicFungsiId = x.PICFungsi.Id,
                    PicFungsiName = x.PICFungsi.PicName,
                    PicFungsiPhone = x.PICFungsi.Phone ?? null,
                    PicFungsiEmail = x.PICFungsi.Email ?? null,
                    UpdatedWording = _updatedWordingUtility.GetUpdatedWording(x.Header.CreatedDate, x.Header.UpdatedDate),
                    StreamBusinessId = x.Header.StreamBusinessId,
                    StreamBusinessName = x.Stream.Name
                })
           .AsQueryable();

            if (isHigh)
            {
                query = query
                    .Join(_context.TbltExistingFootprintsHighPriorities.Where(highPriority => highPriority.DeletedDate == null),
                    header => header.Id,
                    highPriority => highPriority.ExistingFootprintsId,
                    (header, highPriority) => new
                    {
                        Header = header,
                        HighPriority = highPriority
                    })
                    .Select(x => new ExistingFootprintsDashboardHighPriorityViewModel
                    {
                        Id = x.Header.Id,
                        CreatedDate = x.Header.CreatedDate,
                        UpdatedDate = x.Header.UpdatedDate,
                        ExistingFootprintsId = x.Header.Id,
                        EntitasPertaminaId = x.Header.Id,
                        CompanyName = x.Header.CompanyName,
                        NegaraMitraId = x.Header.Id,
                        NamaNegara = x.Header.NamaNegara,
                        CountryAcronyms = x.Header.CountryAcronyms,
                        Revenue = x.Header.Revenue,
                        TotalAsset = x.Header.TotalAsset,
                        Ebitda = x.Header.Ebitda,
                        Year = x.Header.Year,
                        Sequence = x.HighPriority.Sequence,
                        Flag = x.Header.Flag,
                        ExistsFlag = File.Exists(wwwroot + x.Header.Flag),
                        PathFlag = wwwroot + x.Header.Flag,
                        HubId = x.Header.HubId,
                        HubName = x.Header.HubName ?? null,
                        HubHeadId = x.Header.HubHeadId,
                        HubHeadName = x.Header.HubHeadName ?? null,
                        PicFungsiId = x.Header.PicFungsiId,
                        PicFungsiName = x.Header.PicFungsiName,
                        UpdatedWording = _updatedWordingUtility.GetUpdatedWording(x.Header.CreatedDate, x.Header.UpdatedDate),
                        StreamBusinessId = x.Header.StreamBusinessId,
                        StreamBusinessName = x.Header.StreamBusinessName
                    }).AsQueryable();
            }

            return query;
        }
        public async Task<ResponseDataTableBaseModel<List<ExistingFootprintsDashboardHighPriorityViewModel>>> GetList(RequestFormDtBaseModel request, string wwwroot, string negaraMitra, string streamBusiness, string entitasPertamina)
        {
            try
            {
                var query = QueryAll(false, false, wwwroot, 0);

                Guid? negara = string.IsNullOrEmpty(negaraMitra) == true ? Guid.Empty : Guid.Parse(negaraMitra);
                Guid? stream = string.IsNullOrEmpty(streamBusiness) == true ? Guid.Empty : Guid.Parse(streamBusiness);
                Guid? entitas = string.IsNullOrEmpty(entitasPertamina) == true ? Guid.Empty : Guid.Parse(entitasPertamina);
                query = _filteringUtility.GetFilterListExistingFootprints(query, negara, stream, entitas);

                // Filtering data`
                var predicate = PredicateBuilder.New<ExistingFootprintsDashboardHighPriorityViewModel>(true);
                if (request.Filters.Count > 0)
                {
                    predicate = predicate.Or(x => (x.CompanyName.ToLower().Contains(request.Filters.FirstOrDefault().Value.ToLower())))
                       .Or(x => (x.PicFungsiName.ToLower().Contains(request.Filters.FirstOrDefault().Value.ToLower())))
                       .Or(x => (x.HubName.ToLower().Contains(request.Filters.FirstOrDefault().Value.ToLower())))
                       .Or(x => (x.StreamBusinessName.ToLower().Contains(request.Filters.FirstOrDefault().Value.ToLower())))
                       .Or(x => (x.Year.Value.ToString().Contains(request.Filters.FirstOrDefault().Value.ToLower())))
                       .Or(x => (x.NamaNegara.ToLower().Contains(request.Filters.FirstOrDefault().Value.ToLower())));
                }

                query = query.Where(predicate);
                var joinquery = query;

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
                    joinquery = joinquery.OrderByDescending(x => x.Year).AsQueryable();
                }

                var list = await PaginatedList<ExistingFootprintsDashboardHighPriorityViewModel, ExistingFootprintsDashboardHighPriorityViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<ExistingFootprintsDashboardHighPriorityViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<ExistingFootprintsDashboardHighPriorityViewModel>>(false, ex.Message);
            }
        }
        public List<ExistingFootprintsDashboardHighPriorityViewModel> GetHighPriority(string wwwroot, bool isHigh, bool isFilter, bool isSort, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount)
        {
            List<ExistingFootprintsDashboardHighPriorityViewModel> result = new List<ExistingFootprintsDashboardHighPriorityViewModel>();

            IQueryable<ExistingFootprintsDashboardHighPriorityViewModel> query = null;

            if (isHigh)
                query = QueryAll(true, false, wwwroot, 0).OrderBy(x => x.Sequence);
            else
                query = QueryAll(false, false, wwwroot, 0);

            if (isFilter)
                query = _filteringUtility.GetFilterListExistingFootprints(query, negara, stream, entitas);

            if (isClickMap)
                query = query.Where(x => x.CountryAcronyms == countryAcronym);

            if (isSort)
            {
                query = _sortingFunc.GetSortList(query, category, order);

                if (order == "asc")
                {
                    query = query.OrderBy(x => x.Year == null);
                }

                if (!isFilter)
                    query = query.OrderByDescending(x => x.Year);
            }

            query = _takeFunc.GetTakeList(query, pageCount);

            _context.ChangeTracker.Clear();

            return result = query.AsNoTracking().ToList();
        }
        public int? GetCountAllRecordSortHighPriority(string wwwroot, bool isHigh, bool isFilter, bool isSort, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount)
        {
            int? result = 0;

            IQueryable<ExistingFootprintsDashboardHighPriorityViewModel> query = null;

            if (isHigh)
                query = QueryAll(true, false, wwwroot, 0).OrderBy(x => x.Sequence);
            else
                query = QueryAll(false, false, wwwroot, 0);

            if (isFilter)
                query = _filteringUtility.GetFilterListExistingFootprints(query, negara, stream, entitas);

            if (isClickMap)
                query = query.Where(x => x.CountryAcronyms == countryAcronym);

            _context.ChangeTracker.Clear();

            return result = query.AsNoTracking().Count();
        }
        public List<ExistingFootprintsDashboardLegendViewModel> GetLegends()
        {
            List<ExistingFootprintsDashboardLegendViewModel> result = new List<ExistingFootprintsDashboardLegendViewModel>();

            var query = _context.TblmStreamBusinesses.Where(x => x.DeletedDate == null).AsQueryable();

            return result = _mapper.Map<List<ExistingFootprintsDashboardLegendViewModel>>(query.OrderBy(x => x.OrderSeq).ToList());
        }
        public async Task<List<string>> GetCountriesMap()
        {
            List<string> result = new List<string>();

            var query = _context.TbltExistingFootprints.Where(x => x.DeletedDate == null)
                .Join(_context.TblmNegaraMitraInfomations.Where(x => x.DeletedDate == null), a => a.NegaraMitraId, b => b.NegaraMitraId, (a, b) => new
                {
                    Countries = b.CountryAcronyms,
                })
                .AsQueryable();

            return result = await query.Select(x => x.Countries).Distinct().ToListAsync();
        }
        public async Task<List<string>> GetCountriesMap(Guid? negara, Guid? stream, Guid? entitas)
        {
            List<string> result = new List<string>();

            var query = _context.TbltExistingFootprints.Where(x => x.DeletedDate == null && (negara != null ? x.NegaraMitraId == negara : true) && (stream != null ? x.StreamBusinessId == stream : true) && (entitas != null ? x.EntitasPertaminaId == entitas : true))
                .Join(_context.TblmNegaraMitraInfomations.Where(x => x.DeletedDate == null), a => a.NegaraMitraId, b => b.NegaraMitraId, (a, b) => new
                {
                    Countries = b.CountryAcronyms,
                })
                .AsQueryable();

            return result = await query.Select(x => x.Countries).Distinct().ToListAsync();
        }
        public async Task<List<ExistingFootprintDashboardMapPointerViewModel>> GetSubHoldingMap()
        {
            List<ExistingFootprintDashboardMapPointerViewModel> result = new List<ExistingFootprintDashboardMapPointerViewModel>();

            var query = _context.TbltExistingFootprints.Where(x => x.DeletedDate == null)
                .Join(_context.TblmNegaraMitraInfomations.Where(x => x.DeletedDate == null), a => a.NegaraMitraId, b => b.NegaraMitraId, (a, b) => new
                {
                    CountryAcronyms = b.CountryAcronyms,
                    EntitasPertaminaId = a.EntitasPertaminaId
                })
                .Join(_context.TblmEntitasPertaminas.Where(x => x.DeletedDate == null), c => c.EntitasPertaminaId, d => d.Id, (c, d) => new
                {
                    header = c,
                    detail = d,
                })
                .Join(_context.TblmHshes.Where(x => x.DeletedDate == null), e => e.detail.HshId, f => f.Id, (e, f) => new
                ExistingFootprintDashboardMapPointerViewModel
                {
                    CountriesAcronym = e.header.CountryAcronyms,
                    SubHolding = f.Name,
                })
                .AsQueryable();

            return result = await query.ToListAsync();
        }
        public async Task<List<ExistingFootprintDashboardMapPointerViewModel>> GetSubHoldingMap(Guid? negara, Guid? stream, Guid? entitas)
        {
            List<ExistingFootprintDashboardMapPointerViewModel> result = new List<ExistingFootprintDashboardMapPointerViewModel>();

            var query = _context.TbltExistingFootprints.Where(x => x.DeletedDate == null && (negara != null ? x.NegaraMitraId == negara : true) && (stream != null ? x.StreamBusinessId == stream : true) && (entitas != null ? x.EntitasPertaminaId == entitas : true))
                .Join(_context.TblmNegaraMitraInfomations.Where(x => x.DeletedDate == null), a => a.NegaraMitraId, b => b.NegaraMitraId, (a, b) => new
                {
                    CountryAcronyms = b.CountryAcronyms,
                    EntitasPertaminaId = a.EntitasPertaminaId
                })
                .Join(_context.TblmEntitasPertaminas.Where(x => x.DeletedDate == null), c => c.EntitasPertaminaId, d => d.Id, (c, d) => new
                {
                    header = c,
                    detail = d,
                })
                .Join(_context.TblmHshes.Where(x => x.DeletedDate == null), e => e.detail.HshId, f => f.Id, (e, f) => new
                ExistingFootprintDashboardMapPointerViewModel
                {
                    CountriesAcronym = e.header.CountryAcronyms,
                    SubHolding = f.Name,
                })
                .AsQueryable();

            return result = await query.ToListAsync();
        }
        public ExistingFootprintsDashboardHighPriorityViewModel GetExistingFootprintsDashboardDetail(string wwwroot, Guid guid, int year)
        {
            ExistingFootprintsDashboardHighPriorityViewModel result = new ExistingFootprintsDashboardHighPriorityViewModel();

            return result = QueryAll(false, true, wwwroot, year).FirstOrDefault(x => x.Id == guid);
        }
        public List<ExistingFootprintsPartnersViewModel> GetExistingFootprintsPartners(Guid guid)
        {
            List<ExistingFootprintsPartnersViewModel> result = new List<ExistingFootprintsPartnersViewModel>();

            var query = _context.TbltExistingFootprintsPartners.Where(x => x.DeletedDate == null && x.ExistingFootPrintsId == guid).OrderBy(x => x.Location);

            return result = _mapper.Map<List<ExistingFootprintsPartnersViewModel>>(query);
        }
        public List<ExistingFootprintsDashboardHighPriorityViewModel> GetSortHighPriority(string wwwroot)
        {
            List<ExistingFootprintsDashboardHighPriorityViewModel> result = new List<ExistingFootprintsDashboardHighPriorityViewModel>();

            return result = QueryAll(false, false, wwwroot, 0).ToList();
        }
        public Guid? GetCountryByAcronym(string countryAcronym)
        {
            Guid? result = null;

            var query = _context.TblmNegaraMitras.Where(x => x.DeletedDate == null)
                .Join(_context.TblmNegaraMitraInfomations.Where(x => x.DeletedDate == null && x.CountryAcronyms == countryAcronym), a => a.Id, b => b.NegaraMitraId, (a, b) => new
                {
                    Id = a.Id,
                })
                .AsQueryable();

            return result = query.FirstOrDefault().Id;
        }
    }
}