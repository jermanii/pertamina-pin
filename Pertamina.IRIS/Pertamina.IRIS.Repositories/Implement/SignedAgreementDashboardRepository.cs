using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
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
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class SignedAgreementDashboardRepository : ISignedAgreementDashboardRepository
    {
        protected readonly DB_PINMContext _context;
        private readonly ISortingFuncUtility _sortingFunc;
        private readonly ITakeFuncUtility _takeFunc;
        private readonly IHighPriorityFilteringUtility _filteringUtility;
        protected readonly IMapper _mapper;
        protected readonly IUpdatedWordingUtility _updatedWordingUtility;

        public SignedAgreementDashboardRepository(DB_PINMContext context, ISortingFuncUtility sortingFunc, ITakeFuncUtility takeFunc, IHighPriorityFilteringUtility filteringUtility, IMapper mapper, IUpdatedWordingUtility updatedWordingUtility)
        {
            _sortingFunc = sortingFunc ?? throw new ArgumentNullException(nameof(sortingFunc));
            _takeFunc = takeFunc ?? throw new ArgumentNullException(nameof(takeFunc));
            _filteringUtility = filteringUtility ?? throw new ArgumentNullException(nameof(filteringUtility));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _updatedWordingUtility = updatedWordingUtility;
        }

        #region Private
        private IQueryable<SignedAgreementDashboardHighPriorityViewModel> QueryAll(string wwwroot, bool isHigh, bool isExpired, bool isExcludedCountryAvailable)
        {
            var query = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .LeftJoin(_context.TblmDiscussionStatuses
                , header => header.DiscussionStatusId
                , discussionStatus => discussionStatus.Id
                , (header, discussionStatus) => new
                {
                    Header = header,
                    DiscussionStatus = discussionStatus
                })
                .LeftJoin(_context.TbltAgreementPicFungsis.Where(x => x.DeletedDate == null && x.PicLevelId == _context.TblmPicLevels.Where(x => x.IsLead == true).FirstOrDefault().Id),
                combined => combined.Header.Id,
                pic => pic.AgreementId,
                (combined, pic) => new
                {
                    combined.Header,
                    combined.DiscussionStatus,
                    PIC = pic
                })
                .LeftJoin(_context.TblmPicFungsis.Where(x => x.DeletedDate == null),
                combined => combined.PIC.PicFungsiId,
                picFungsi => picFungsi.Id,
                (combined, picFungsi) => new
                {
                    combined.Header,
                    combined.DiscussionStatus,
                    combined.PIC,
                    PICFungsi = picFungsi
                })
                .LeftJoin(_context.TbltAgreementNegaraMitras.Where(x => x.DeletedDate == null),
                combined => combined.Header.Id,
                negaraMitra => negaraMitra.AgreementId,
                (combined, negaraMitra) => new
                {
                    combined.Header,
                    combined.DiscussionStatus,
                    combined.PIC,
                    combined.PICFungsi,
                    NegaraMitra = negaraMitra
                })
                .LeftJoin(_context.TblmNegaraMitras
                .LeftJoin(_context.TblmNegaraMitraInfomations, negara => negara.Id, info => info.NegaraMitraId, (negara, info) => new
                {
                    Negara = negara,
                    InfoNegara = info,
                }).LeftJoin(_context.TblmKawasanMitras, negara => negara.Negara.KawasanMitraId, kawasan => kawasan.Id, (negara, kawasan) => new
                {
                    Negara = negara,
                    Kawasan = kawasan
                })
                .Where(negara => negara.Negara.Negara.DeletedDate == null),
                combined => combined.NegaraMitra.NegaraMitraId,
                negara => negara.Negara.Negara.Id,
                (combined, negara) => new
                {
                    combined.Header,
                    combined.DiscussionStatus,
                    combined.PIC,
                    combined.PICFungsi,
                    combined.NegaraMitra,
                    MasterNegara = negara
                })
                .LeftJoin(_context.TblmJenisPerjanjians.Where(x => x.DeletedDate == null)
                , combined => combined.Header.JenisPerjanjianId
                , masterJenisPerjanjian => masterJenisPerjanjian.Id
                , (combined, masterJenisPerjanjian) => new
                {
                    combined.Header,
                    combined.DiscussionStatus,
                    combined.PIC,
                    combined.PICFungsi,
                    combined.NegaraMitra,
                    combined.MasterNegara,
                    MasterJenisPerjanjian = masterJenisPerjanjian
                })
                .LeftJoin(_context.TblmKlasifikasiKendalas.Where(x => x.DeletedDate == null)
                , combined => combined.Header.KlasifikasiKendalaId
                , klasifikasiKendala => klasifikasiKendala.Id
                , (combined, klasifikasiKendala) => new
                {
                    combined.Header,
                    combined.DiscussionStatus,
                    combined.PIC,
                    combined.PICFungsi,
                    combined.NegaraMitra,
                    combined.MasterNegara,
                    combined.MasterJenisPerjanjian,
                    KlasifikasiKendala = klasifikasiKendala
                })
                .LeftJoin(_context.TblmStatusBerlakus.Where(x => x.DeletedDate == null)
                , combined => combined.Header.StatusBerlakuId
                , statusBerlaku => statusBerlaku.Id
                , (combined, statusBerlaku) => new
                {
                    combined.Header,
                    combined.DiscussionStatus,
                    combined.PIC,
                    combined.PICFungsi,
                    combined.NegaraMitra,
                    combined.MasterNegara,
                    combined.MasterJenisPerjanjian,
                    combined.KlasifikasiKendala,
                    StatusBerlaku = statusBerlaku
                })
                .Select(combined => new
                {
                    combined.Header,
                    combined.DiscussionStatus,
                    combined.PIC,
                    combined.PICFungsi,
                    combined.NegaraMitra,
                    combined.MasterNegara,
                    combined.MasterJenisPerjanjian,
                    combined.KlasifikasiKendala,
                    combined.StatusBerlaku,
                    IsGreenBusiness = _context.TbltAgreementStreamBusinesses.Where(x => x.DeletedDate == null && x.AgreementId == combined.Header.Id)
                    .Join(_context.TblmStreamBusinesses.Where(x => x.IsGreenBusiness == true)
                    , trasaction => trasaction.StreamBusinessId
                    , master => master.Id
                    , (transaction, master) => new
                    {
                        transaction,
                        master,
                    }).Any(),
                    HasLembaga = _context.TbltAgreementKementrianLembagas.Join(_context.TblmKementrianLembagas
                    , kementrianLembaga => kementrianLembaga.KementrianLembagaId
                    , masterLembaga => masterLembaga.Id
                    , (kementrianLembaga, masterLembaga) => new
                    {
                        KementrianLembaga = kementrianLembaga,
                        masterLembaga = masterLembaga,
                    })
                    .Where(kl => kl.KementrianLembaga.DeletedDate == null && kl.KementrianLembaga.AgreementId == combined.Header.Id)
                    .Any(),
                    KementrianLembaga = _context.TbltAgreementKementrianLembagas.Join(_context.TblmKementrianLembagas
                    , kementrianLembaga => kementrianLembaga.KementrianLembagaId
                    , masterLembaga => masterLembaga.Id
                    , (kementrianLembaga, masterLembaga) => new
                    {
                        KementrianLembaga = kementrianLembaga,
                        masterLembaga = masterLembaga,
                    })
                    .Where(kl => kl.KementrianLembaga.DeletedDate == null && kl.KementrianLembaga.AgreementId == combined.Header.Id)
                    .FirstOrDefault(),
                })
                .Select(rec => new SignedAgreementDashboardHighPriorityViewModel
                {
                    Id = rec.Header.Id,
                    JudulPerjanjian = rec.Header.JudulPerjanjian,
                    KodeAgreement = rec.Header.KodeAgreement,
                    JenisPerjanjian = rec.MasterJenisPerjanjian.Name,
                    StatusBerlakuName = rec.StatusBerlaku.StatusName,
                    StatusBerlakuId = rec.StatusBerlaku.Id,
                    ValidStatusColorName = rec.StatusBerlaku.ColorName,
                    ValidStatusColorHexa = rec.StatusBerlaku.ColorHexa,
                    DiscussionStatus = rec.DiscussionStatus.Name,
                    KlasifikasiKendala = rec.KlasifikasiKendala.Name,
                    DeskripsiKendala = rec.Header.DeskripsiKendala,
                    TanggalTtd = rec.Header.TanggalTtd,
                    HasLembaga = rec.HasLembaga,
                    TargetDateAwal = rec.Header.TanggalTtd.HasValue ? rec.Header.TanggalTtd.Value.ToString("d/MM/yy") : "",
                    TargetDateAkhir = rec.Header.TanggalBerakhir.HasValue ? rec.Header.TanggalBerakhir.Value.ToString("d/MM/yy") : "",
                    TanggalBerakhir = rec.Header.TanggalBerakhir.HasValue ? rec.Header.TanggalBerakhir.Value : DateTime.MinValue,
                    PotentialRevenuePerYearFormat = rec.Header.PotentialRevenuePerYear.HasValue ? (rec.Header.PotentialRevenuePerYear.Value / 1000000).ToString("##0.00") : null,
                    PotentialRevenuePerYear = rec.Header.PotentialRevenuePerYear.HasValue ? rec.Header.PotentialRevenuePerYear.Value : 0,
                    CapexFormat = rec.Header.Capex.HasValue ? (rec.Header.Capex.Value / 1000000).ToString("##0.00") : null,
                    Capex = rec.Header.Capex.HasValue ? rec.Header.Capex.Value : 0,
                    NamaNegara = rec.MasterNegara.Negara.Negara.NamaNegara,
                    CountryFlag = rec.MasterNegara.Negara.Negara.Flag,
                    ExistsFlag = File.Exists(wwwroot + rec.MasterNegara.Negara.Negara.Flag),
                    CountryRegion = rec.MasterNegara.Kawasan.NamaKawasan + " - " + rec.MasterNegara.Negara.Negara.NamaNegara,
                    CountryAcronyms = rec.MasterNegara.Negara.InfoNegara.CountryAcronyms,
                    NegaraMitraId = rec.MasterNegara.Negara.Negara.Id,
                    PicFungsiName = rec.PICFungsi.PicName,
                    PicFungsiEmail = rec.PICFungsi.Email,
                    PicFungsiPhone = rec.PICFungsi.Phone,
                    Streams = _context.TbltAgreementStreamBusinesses
                    .Where(x => x.DeletedDate == null && x.AgreementId == rec.Header.Id)
                    .Select(x => new SignedAgreementStreamBusinessViewModel
                    {
                        AgreementId = x.AgreementId,
                        StreamBusinessId = x.StreamBusinessId
                    }).AsNoTracking().ToList(),
                    Entitas = _context.TbltAgreementEntitasPertaminas
                    .Where(x => x.DeletedDate == null && x.AgreementId == rec.Header.Id)
                    .Select(x => new SignedAgreementEntitasPertaminaViewModel
                    {
                        AgreementId = x.AgreementId,
                        EntitasPertaminaId = x.EntitasPertaminaId
                    }).AsNoTracking().ToList(),
                    IsGtg = rec.Header.IsGtg,
                    IsStrategic = rec.MasterJenisPerjanjian.IsStrategic,
                    IsGreenBusiness = rec.IsGreenBusiness,
                    UpdatedWording = _updatedWordingUtility.GetUpdatedWording(rec.Header.CreateDate, rec.Header.CreateDate),
                })
                .AsQueryable();

            if (isHigh)
            {
                query = query
                    .Join(_context.TbltAgreementHighPriorities.Where(x => x.DeletedDate == null)
                    , header => header.Id
                    , high => high.AgreementId
                    , (header, high) => new
                    {
                        Header = header,
                        High = high,
                    }).Select(rec => new SignedAgreementDashboardHighPriorityViewModel
                    {
                        Id = rec.Header.Id,
                        JudulPerjanjian = rec.Header.JudulPerjanjian,
                        KodeAgreement = rec.Header.KodeAgreement,
                        JenisPerjanjian = rec.Header.JenisPerjanjian,
                        DiscussionStatus = rec.Header.DiscussionStatus,
                        ValidStatusColorName = rec.Header.ValidStatusColorName,
                        ValidStatusColorHexa = rec.Header.ValidStatusColorHexa,
                        KlasifikasiKendala = rec.Header.KlasifikasiKendala,
                        DeskripsiKendala = rec.Header.DeskripsiKendala,
                        TanggalTtd = rec.Header.TanggalTtd,
                        HasLembaga = rec.Header.HasLembaga,
                        LembagaDesc = rec.Header.LembagaDesc,
                        LembagaName = rec.Header.LembagaName,
                        TargetDateAwal = rec.Header.TargetDateAwal,
                        TargetDateAkhir = rec.Header.TargetDateAkhir,
                        PotentialRevenuePerYearFormat = rec.Header.PotentialRevenuePerYearFormat,
                        PotentialRevenuePerYear = rec.Header.PotentialRevenuePerYear,
                        CapexFormat = rec.Header.CapexFormat,
                        Capex = rec.Header.Capex,
                        NamaNegara = rec.Header.NamaNegara,
                        CountryFlag = rec.Header.CountryFlag,
                        ExistsFlag = rec.Header.ExistsFlag,
                        CountryRegion = rec.Header.CountryRegion,
                        CountryAcronyms = rec.Header.CountryAcronyms,
                        NegaraMitraId = rec.Header.NegaraMitraId,
                        PicFungsiName = rec.Header.PicFungsiName,
                        PicFungsiEmail = rec.Header.PicFungsiEmail,
                        PicFungsiPhone = rec.Header.PicFungsiPhone,
                        Streams = rec.Header.Streams,
                        Entitas = rec.Header.Entitas,
                        IsGtg = rec.Header.IsGtg,
                        IsStrategic = rec.Header.IsStrategic,
                        IsGreenBusiness = rec.Header.IsGreenBusiness,
                        UpdatedWording = rec.Header.UpdatedWording,
                    })
                    .AsQueryable();
            }

            if (!isExpired)
            {
                query = query.Where(x => _context.TblmStatusBerlakus.Where(x => x.IsExpired == false).Select(x => x.Id).ToList().Contains(x.StatusBerlakuId.Value));
            }

            if (!isExcludedCountryAvailable)
            {
                query = query.Where(x => !_context.TblmNegaraMitraExcludeds.Where(x => x.DeletedDate == null).Select(x => x.NegaraMitraId).ToList().Contains(x.NegaraMitraId.Value));
            }

            return query.OrderByDescending(x => x.KodeAgreement);
        }
        private IQueryable<SignedAgreementDashboardHighPriorityViewModel> GetHighPriorityA()
        {
            var query = from x in _context.TbltAgreementHighPriorities
                        join a in _context.TbltAgreements on x.AgreementId equals a.Id
                        join p in _context.TblmJenisPerjanjians on a.JenisPerjanjianId equals p.Id
                        join k in _context.TblmKlasifikasiKendalas on a.KlasifikasiKendalaId equals k.Id
                        join d in _context.TbltAgreementNegaraMitras on a.Id equals d.AgreementId
                        join e in _context.TblmNegaraMitras on d.NegaraMitraId equals e.Id
                        join f in _context.TblmHubHeads on e.HubId equals f.HubId
                        join g in _context.TblmHubs on f.HubId equals g.Id
                        join h in _context.TblmDiscussionStatuses on a.DiscussionStatusId equals h.Id
                        where x.DeletedDate == null
                        orderby x.Sequence
                        select new SignedAgreementDashboardHighPriorityViewModel
                        {
                            Id = x.Id,
                            NegaraMitraId = d.NegaraMitraId,
                            JudulPerjanjian = a.JudulPerjanjian,
                            PotentialRevenuePerYear = a.PotentialRevenuePerYear ?? 0,
                            Capex = a.Capex ?? 0,
                            Valuation = a.Valuation ?? 0,
                            CountryFlag = e.Flag,
                            HubHeadName = f.Name ?? "",
                            DiscussionStatusColorName = h.ColorName,
                            DiscussionStatusColorHexa = h.ColorHexa,
                            DiscussionStatus = h.Name,
                            AgreementId = x.AgreementId,
                            CreatedDate = x.CreatedDate,
                            UpdatedDate = x.UpdatedDate,
                            NamaNegara = e.NamaNegara,
                            KlasifikasiKendala = k.Name,
                            DeskripsiKendala = a.DeskripsiKendala,
                            KodeAgreement = a.KodeAgreement,
                            NamaPerjanjian = p.Name
                        };

            return query;

        }
        private IQueryable<SignedAgreementDashboardHighPriorityViewModel> GetHighPriorityB()
        {
            var query = from a in _context.TbltAgreements
                        join p in _context.TblmJenisPerjanjians on a.JenisPerjanjianId equals p.Id
                        join k in _context.TblmKlasifikasiKendalas on a.KlasifikasiKendalaId equals k.Id
                        join d in _context.TbltAgreementNegaraMitras on a.Id equals d.AgreementId
                        join e in _context.TblmNegaraMitras on d.NegaraMitraId equals e.Id
                        join n in _context.TblmNegaraMitraInfomations on e.Id equals n.NegaraMitraId
                        join f in _context.TblmHubHeads on e.HubId equals f.HubId
                        join g in _context.TblmHubs on f.HubId equals g.Id
                        join h in _context.TblmDiscussionStatuses on a.DiscussionStatusId equals h.Id
                        where a.DeletedDate == null
                        select new SignedAgreementDashboardHighPriorityViewModel
                        {
                            NegaraMitraId = d.NegaraMitraId,
                            JudulPerjanjian = a.JudulPerjanjian,
                            PotentialRevenuePerYear = a.PotentialRevenuePerYear ?? 0,
                            Capex = a.Capex ?? 0,
                            Valuation = a.Valuation ?? 0,
                            CountryFlag = e.Flag,
                            HubHeadName = f.Name ?? "",
                            DiscussionStatusColorName = h.ColorName,
                            DiscussionStatusColorHexa = h.ColorHexa,
                            DiscussionStatus = h.Name,
                            AgreementId = a.Id,
                            CreatedDate = a.CreateDate,
                            UpdatedDate = a.UpdateDate,
                            NamaNegara = e.NamaNegara,
                            KlasifikasiKendala = k.Name,
                            DeskripsiKendala = a.DeskripsiKendala,
                            KodeAgreement = a.KodeAgreement,
                            NamaPerjanjian = p.Name,
                            CountryAcronyms = n.CountryAcronyms,
                        };
            return query;
        }
        private string GetStreamBusinessFromStroredProcedure(Guid AgreementId)
        {
            string result = _context.Database.ExecuteSqlRaw("EXEC GetStreamBusinessAgreementConcatenatedGuids @AgreementId = {0}", AgreementId).ToString();

            return result;
        }
        #endregion

        public async Task<ResponseDataTableBaseModel<List<SignedAgreementDashboardHighPriorityViewModel>>> GetList(RequestFormDtBaseModel request, string wwwroot, string negaraMitra, string streamBusiness, string entitasPertamina)
        {
            try
            {
                bool isExpired = false, isExcludeCountryAvailable = false;

                if (!string.IsNullOrEmpty(negaraMitra) || !string.IsNullOrEmpty(streamBusiness) || !string.IsNullOrEmpty(entitasPertamina))
                {
                    isExpired = true;
                    isExcludeCountryAvailable = true;
                }

                var query = QueryAll(wwwroot, false, isExpired, isExcludeCountryAvailable);


                Guid? negara = string.IsNullOrEmpty(negaraMitra) == true ? Guid.Empty : Guid.Parse(negaraMitra);
                Guid? stream = string.IsNullOrEmpty(streamBusiness) == true ? Guid.Empty : Guid.Parse(streamBusiness);
                Guid? entitas = string.IsNullOrEmpty(entitasPertamina) == true ? Guid.Empty : Guid.Parse(entitasPertamina);
                query = _filteringUtility.GetFilterListSignedAgreement(query, negara, stream, entitas);


                // Filtering data`
                var predicate = PredicateBuilder.New<SignedAgreementDashboardHighPriorityViewModel>(true);
                if (request.Filters.Count > 0)
                {
                    predicate = predicate.Or(x => (x.JudulPerjanjian.ToLower().Contains(request.Filters.FirstOrDefault().Value.ToLower())))
                        .Or(x => (x.PicFungsiName.ToLower().Contains(request.Filters.FirstOrDefault().Value.ToLower())))
                        .Or(x => (x.KodeAgreement.ToLower().Contains(request.Filters.FirstOrDefault().Value.ToLower())));
                }

                query = query.Where(predicate);

                // Sorting data
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDirection)))
                {
                    if (request.SortColumnDirection == "asc")
                        query = query.OrderBy(c => EF.Property<string>(c, request.SortColumn));
                    else
                        query = query.OrderByDescending(c => EF.Property<string>(c, request.SortColumn));
                }

                var list = await PaginatedList<SignedAgreementDashboardHighPriorityViewModel, SignedAgreementDashboardHighPriorityViewModel>.CreateAsync(query, request.PageValue, request.PageSize, query.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<SignedAgreementDashboardHighPriorityViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<SignedAgreementDashboardHighPriorityViewModel>>(false, ex.Message);
            }
        }
        public List<SignedAgreementDashboardHighPriorityViewModel> GetStrategicPriorityAgreement(string wwwroot, bool isHigh, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount)
        {
            List<SignedAgreementDashboardHighPriorityViewModel> result = new List<SignedAgreementDashboardHighPriorityViewModel>();

            bool isExpired = false
                , isExcludedCountryAvailable = false;

            if (isFilter || isClickMap)
            {
                isExpired = true;
                isExcludedCountryAvailable = true;
            }

            var query = QueryAll(wwwroot, false, isExpired, isExcludedCountryAvailable);

            if (isHigh)
            {
                var predicate = PredicateBuilder.New<SignedAgreementDashboardHighPriorityViewModel>(true);
                predicate = predicate.Or(x => (x.IsGtg == true))
                    .Or(x => x.IsStrategic == true)
                    .Or(x => (x.Capex + x.PotentialRevenuePerYear) > 500000000)
                    .Or(x => x.IsGreenBusiness == true);

                query = query.Where(predicate);
            }

            if (isFilter)
                query = _filteringUtility.GetFilterListSignedAgreement(query, negara, stream, entitas);

            if (isClickMap)
                query = query.Where(x => x.CountryAcronyms == countryAcronym);

            if (isSort)
            {
                query = _sortingFunc.GetSortList(query, category, order);
            }

            query = _takeFunc.GetTakeList(query, pageCount);

            result = query.AsNoTracking().ToList();

            _context.ChangeTracker.Clear();

            return result;
        }
        public int? GetCountAllStrategicAgreement(string wwwroot, bool isHigh, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount)
        {
            int? result = 0;

            List<SignedAgreementDashboardHighPriorityViewModel> list = new List<SignedAgreementDashboardHighPriorityViewModel>();

            bool isExpired = false
                , isExcludedCountryAvailable = false;

            if (isFilter || isClickMap)
            {
                isExpired = true;
                isExcludedCountryAvailable = true;
            }

            var query = QueryAll(wwwroot, false, isExpired, isExcludedCountryAvailable);

            if (isHigh)
            {
                var predicate = PredicateBuilder.New<SignedAgreementDashboardHighPriorityViewModel>(true);
                predicate = predicate.Or(x => (x.IsGtg == true))
                    .Or(x => x.IsStrategic == true)
                    .Or(x => (x.Capex + x.PotentialRevenuePerYear) > 500000000)
                    .Or(x => x.IsGreenBusiness == true);

                query = query.Where(predicate);
            }

            if (isFilter)
                query = _filteringUtility.GetFilterListSignedAgreement(query, negara, stream, entitas);

            if (isClickMap)
                query = query.Where(x => x.CountryAcronyms == countryAcronym);

            result = query.AsNoTracking().Count();

            _context.ChangeTracker.Clear();

            return result;
        }
        public string GetPICName(Guid? agreementId)
        {
            var result = _context.TbltAgreementPicFungsis
                .Where(a => a.AgreementId == agreementId)
                .Join(
                    _context.TblmPicFungsis,
                    a => a.PicFungsiId,
                    b => b.Id,
                    (a, b) => b.PicName
                )
                .FirstOrDefault();

            return result;
        }
        public async Task<string> GetPICNameAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return ""; // Return an empty string if AgreementId is null
            }

            var result = await _context.TbltAgreementPicFungsis
                .Where(a => a.AgreementId == agreementId)
                .Join(
                    _context.TblmPicFungsis,
                    a => a.PicFungsiId,
                    b => b.Id,
                    (a, b) => b.PicName
                )
                .FirstOrDefaultAsync();

            return result ?? ""; // Return an empty string if result is null
        }
        public string GetPICNameLead(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return null; // Handle null AgreementId as needed
            }

            var result = (from a in _context.TbltAgreementPicFungsis
                          join b in _context.TblmPicFungsis on a.PicFungsiId equals b.Id
                          join c in _context.TblmPicLevels on a.PicLevelId equals c.Id
                          where a.AgreementId == agreementId && c.IsLead == true
                          select b.PicName)
                         .FirstOrDefault();

            return result ?? "";
        }
        public string GetLembagaName(Guid? agreementId)
        {
            var result = _context.TbltAgreementKementrianLembagas
                .Where(a => a.AgreementId == agreementId)
                .Join(
                    _context.TblmKementrianLembagas,
                    a => a.KementrianLembagaId,
                    b => b.Id,
                    (a, b) => b.LembagaName
                )
                .AsNoTracking().ToList();

            if (result.Count > 0)
            {
                return string.Join(", ", result);
            }

            return string.Empty;
        }
        public async Task<string> GetLembagaNameAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return string.Empty; // Return an empty string if AgreementId is null
            }

            var result = await _context.TbltAgreementKementrianLembagas
                .Where(a => a.AgreementId == agreementId)
                .Join(
                    _context.TblmKementrianLembagas,
                    a => a.KementrianLembagaId,
                    b => b.Id,
                    (a, b) => b.LembagaName
                )
                .ToListAsync();

            return string.Join(", ", result); // If result is empty, string.Join returns an empty string
        }
        public async Task<string> GetLembagaDescAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return string.Empty; // Return an empty string if AgreementId is null
            }

            var result = await _context.TbltAgreementKementrianLembagas
                .Where(a => a.AgreementId == agreementId)
                .Join(
                    _context.TblmKementrianLembagas,
                    a => a.KementrianLembagaId,
                    b => b.Id,
                    (a, b) => b.Description
                )
                .ToListAsync();

            return string.Join(", ", result); // If result is empty, string.Join returns an empty string
        }
        public async Task<string> GetStreamBusinessNameAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return ""; // Return an empty string if AgreementId is null
            }

            var result = await _context.TbltAgreementStreamBusinesses
                .Where(a => a.AgreementId == agreementId)
                .Join(
                    _context.TblmStreamBusinesses,
                    a => a.StreamBusinessId,
                    b => b.Id,
                    (a, b) => b.Name
                )
                .FirstOrDefaultAsync();

            return result ?? ""; // Return an empty string if no result is found
        }
        public async Task<string> GetPartnersAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return string.Empty; // Return an empty string if AgreementId is null
            }

            var result = await _context.TbltAgreementPartners
                .Where(a => a.AgreementId == agreementId)
                .Select(a => a.PartnerName)
                .ToListAsync();

            return string.Join("| ", result); // If result is empty, string.Join returns an empty string
        }
        public async Task<string> GetLokasiProyekAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return string.Empty; // Return an empty string if AgreementId is null
            }

            var result = await _context.TbltAgreementLokasiProyeks
                .Where(a => a.AgreementId == agreementId)
                .Select(a => a.LokasiProyek)
                .ToListAsync();

            return string.Join(", ", result); // If result is empty, string.Join will return an empty string
        }
        public async Task<string> GetTargetDateAwalAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return null; // Handle null AgreementId by returning null
            }

            var earliestTargetDate = await _context.TbltAgreementMilestoneTimelines
                .Where(t => t.AgreementId == agreementId)
                .OrderBy(t => t.Sequence)
                .Select(t => t.TargetDate)
                .FirstOrDefaultAsync();

            return string.Format("{0:d/MM/yy}", earliestTargetDate);
        }
        public string GetTargetDateAkhir(Guid? agreementId)
        {
            var latestTargetDate = _context.TbltAgreementMilestoneTimelines
                .Where(t => t.AgreementId == agreementId)
                .OrderByDescending(t => t.Sequence)
                .Select(t => t.TargetDate)
                .FirstOrDefault();

            return string.Format("{0:d/MM/yy}", latestTargetDate);
        }
        public async Task<string> GetTargetDateAkhirAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return null; // Handle null AgreementId by returning null
            }

            var latestTargetDate = await _context.TbltAgreementMilestoneTimelines
                .Where(t => t.AgreementId == agreementId)
                .OrderByDescending(t => t.Sequence)
                .Select(t => t.TargetDate)
                .FirstOrDefaultAsync();

            return string.Format("{0:d/MM/yy}", latestTargetDate);
        }
        public async Task<List<SignedAgreementDashboardHighPriorityViewModel>> GetFilterHighPriority(string countryAcr, string? streamBusinessId = null, string? negaraMitraId = null, string? entitasPertaminaId = null)
        {
            IQueryable<SignedAgreementDashboardHighPriorityViewModel> query = null;
            if (countryAcr == null
                && streamBusinessId == null
                && negaraMitraId == null
                && entitasPertaminaId == null)
            {
                query = GetHighPriorityA();
            }
            else
            {
                query = GetHighPriorityB();
            }


            if (!string.IsNullOrEmpty(countryAcr))
            {
                query = query.Where(x => x.CountryAcronyms == countryAcr);
            }

            // Apply StreamBusinessId filter if provided
            if (!string.IsNullOrEmpty(streamBusinessId))
            {
                query = query.Where(x => x.StreamBusinessId == Guid.Parse(streamBusinessId));
            }

            // Apply NegaraMitraId filter if provided
            if (!string.IsNullOrEmpty(negaraMitraId))
            {
                query = query.Where(x => x.NegaraMitraId == Guid.Parse(negaraMitraId));
            }

            // Apply EntitasPertaminaId filter if provided
            if (!string.IsNullOrEmpty(entitasPertaminaId))
            {
                query = query.Where(x => x.EntitasPertaminaId == Guid.Parse(entitasPertaminaId));
            }

            // Execute the query and get the list
            var result = await query.ToListAsync();

            // Process each item in the result (as you did in the original code)
            foreach (var item in result)
            {
                item.PicFungsiName = await GetPICNameAsync(item.AgreementId);
                item.TargetDateAwal = await GetTargetDateAwalAsync(item.AgreementId);
                item.TargetDateAkhir = await GetTargetDateAkhirAsync(item.AgreementId);
                item.CountryFlag = item.CountryFlag == null ? "/assets/media/flags/globe.svg" : item.CountryFlag.Replace("\\", "/");
                item.UpdatedWording = _updatedWordingUtility.GetUpdatedWording(item.CreatedDate, item.UpdatedDate);
                item.LembagaName = await GetLembagaNameAsync(item.AgreementId);
                item.LembagaDesc = await GetLembagaDescAsync(item.AgreementId);
                item.StreamBusinessName = await GetStreamBusinessNameAsync(item.AgreementId);
                item.EntitasPertamina = await GetEntityPertaminaAsync(item.AgreementId);
                item.PartnerName = await GetPartnersAsync(item.AgreementId);
                item.LokasiProyek = await GetLokasiProyekAsync(item.AgreementId);
                item.CapexFormat = ConvertToMillion(item.Capex.HasValue ? item.Capex.Value : 0);
                item.PotentialRevenuePerYearFormat = ConvertToMillion(item.PotentialRevenuePerYear.HasValue ? item.PotentialRevenuePerYear.Value : 0);
                item.ValuationFormat = ConvertToMillion(item.Valuation.HasValue ? item.Valuation.Value : 0);
            }

            return result;
        }
        public IQueryable<SignedAgreementDashboardDataGridViewModel> GetGridView(RequestFormDtBaseModel request, string StreamBusinessId, string NegaraMitraId, string EntitasPertaminaId)
        {

            var searchValue = string.Empty;
            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                searchValue = request.SearchValue.ToLower();
            }
            var query = from a in _context.TbltAgreements
                        join d in _context.TbltAgreementNegaraMitras on a.Id equals d.AgreementId
                        join e in _context.TblmNegaraMitras on d.NegaraMitraId equals e.Id
                        join f in _context.TblmHubHeads on e.HubId equals f.HubId
                        join g in _context.TblmHubs on f.HubId equals g.Id
                        join h in _context.TblmDiscussionStatuses on a.DiscussionStatusId equals h.Id
                        where a.DeletedDate == null
                        select new SignedAgreementDashboardDataGridViewModel
                        {
                            AgreementId = a.Id,
                            NegaraMitraId = d.NegaraMitraId,
                            JudulPerjanjian = a.JudulPerjanjian,
                            PotentialRevenuePerYear = a.PotentialRevenuePerYear ?? 0,
                            Capex = a.Capex ?? 0,
                            HubHeadName = f.Name ?? "",
                            CreatedDate = a.CreateDate,
                            NamaNegara = e.NamaNegara,
                            KodeAgreement = a.KodeAgreement
                        };

            var result = query.ToList();
            foreach (var item in result)
            {
                item.PICName = GetPICNameLead(item.AgreementId);
                item.EntitasPertamina = GetEntityPertamina(item.AgreementId);
                item.MoUSigningDate = string.Format("{0:d/MM/yy}", item.CreatedDate);
                item.CapexFormat = ConvertToMillion(item.Capex.Value);
                item.PotentialRevenuePerYearFormat = ConvertToMillion(item.PotentialRevenuePerYear.Value);

            }

            var records = result.AsQueryable();
            if (decimal.TryParse(searchValue, out decimal searchDecimalValue))
            {
                records = records.Where(a =>
                    a.JudulPerjanjian.ToLower().Contains(searchValue) ||
                    a.PICName.ToLower().Contains(searchValue) ||
                    a.HubHeadName.ToLower().Contains(searchValue) ||
                    a.NamaNegara.ToLower().Contains(searchValue) ||
                    a.KodeAgreement.ToLower().Contains(searchValue) ||
                    a.PotentialRevenuePerYear == searchDecimalValue ||  // Exact match for decimal
                    a.Capex == searchDecimalValue  // Exact match for decimal
                );
            }
            else
            {
                // Continue filtering only for string values if the input isn't a valid decimal
                if (!string.IsNullOrEmpty(searchValue))
                {
                    records = records.Where(a =>
                       a.JudulPerjanjian.ToLower().Contains(searchValue) ||
                       a.PICName.ToLower().Contains(searchValue) ||
                       a.HubHeadName.ToLower().Contains(searchValue) ||
                       a.NamaNegara.ToLower().Contains(searchValue) ||
                       a.PICName.ToLower().Contains(searchValue) ||
                       a.KodeAgreement.ToLower().Contains(searchValue)
                   );
                }

            }

            return records;
        }
        public string GetEntityPertamina(Guid? agreementId)
        {
            var result = _context.TbltAgreementEntitasPertaminas
                .Where(a => a.AgreementId == agreementId)
                .Join(
                    _context.TblmEntitasPertaminas,
                    a => a.EntitasPertaminaId,
                    b => b.Id,
                    (a, b) => b.CompanyName
                )
                .ToList();


            if (result.Count > 0)
            {
                return string.Join(", ", result);
            }

            return string.Empty;
        }
        public async Task<string> GetEntityPertaminaAsync(Guid? agreementId)
        {
            if (agreementId == null)
            {
                return string.Empty; // Return an empty string if AgreementId is null
            }

            var result = await _context.TbltAgreementEntitasPertaminas
                .Where(a => a.AgreementId == agreementId)
                .Join(
                    _context.TblmEntitasPertaminas,
                    a => a.EntitasPertaminaId,
                    b => b.Id,
                    (a, b) => b.CompanyName
                )
                .ToListAsync();

            return string.Join(", ", result); // Returns an empty string if result is empty
        }
        public async Task<List<string>> GetCountriesMap()
        {
            // Starting the query
            var query = from a in _context.TbltAgreements
                        join b in _context.TbltAgreementNegaraMitras on a.Id equals b.AgreementId
                        join c in _context.TblmNegaraMitraInfomations on b.NegaraMitraId equals c.NegaraMitraId
                        join d in _context.TbltAgreementEntitasPertaminas on a.Id equals d.AgreementId
                        join s in _context.TbltAgreementStreamBusinesses on a.Id equals s.AgreementId
                        where a.DeletedDate == null
                        select new
                        {
                            CountryAcronyms = c.CountryAcronyms,       // Accessing Country Acronyms
                            StreamBusinessId = s.StreamBusinessId,     // Accessing Stream Business Id
                            NegaraMitraId = b.NegaraMitraId,           // Accessing Negara Mitra Id
                            EntitasPertaminaId = d.EntitasPertaminaId  // Accessing Entitas Pertamina Id
                        };

            // Return only the distinct Country Acronyms
            var result = await query
                             .Select(x => x.CountryAcronyms)
                             .Distinct()
                             .ToListAsync();

            return result;
        }
        public async Task<List<string>> GetCountriesMap(Guid? negara, Guid? stream, Guid? entitas)
        {
            // Starting the query
            var query = from a in _context.TbltAgreements
                        join b in _context.TbltAgreementNegaraMitras on a.Id equals b.AgreementId
                        join c in _context.TblmNegaraMitraInfomations on b.NegaraMitraId equals c.NegaraMitraId
                        join d in _context.TbltAgreementEntitasPertaminas on a.Id equals d.AgreementId
                        join s in _context.TbltAgreementStreamBusinesses on a.Id equals s.AgreementId
                        where a.DeletedDate == null
                        select new
                        {
                            CountryAcronyms = c.CountryAcronyms,       // Accessing Country Acronyms
                            StreamBusinessId = s.StreamBusinessId,     // Accessing Stream Business Id
                            NegaraMitraId = b.NegaraMitraId,           // Accessing Negara Mitra Id
                            EntitasPertaminaId = d.EntitasPertaminaId  // Accessing Entitas Pertamina Id
                        };

            // Return only the distinct Country Acronyms
            var result = await query
                .Where(x => (negara != null ? x.NegaraMitraId == negara : true) && (stream != null ? x.StreamBusinessId == stream : true) && (entitas != null ? x.EntitasPertaminaId == entitas : true))
                .Select(x => x.CountryAcronyms)
                .Distinct()
                .ToListAsync();

            return result;
        }
        public async Task<List<SignedAgreementDashboardMapPointerViewModel>> GetSubHoldingMap()
        {
            var result = await (from a in _context.TbltAgreements
                                join b in _context.TbltAgreementNegaraMitras on a.Id equals b.AgreementId
                                join c in _context.TblmNegaraMitraInfomations on b.NegaraMitraId equals c.NegaraMitraId
                                join d in _context.TbltAgreementEntitasPertaminas on b.AgreementId equals d.AgreementId
                                join e in _context.TblmEntitasPertaminas on d.EntitasPertaminaId equals e.Id
                                join s in _context.TbltAgreementStreamBusinesses on a.Id equals s.AgreementId
                                join f in _context.TblmHshes on e.HshId equals f.Id
                                where a.DeletedDate == null
                                select new SignedAgreementDashboardMapPointerViewModel
                                {
                                    CountriesAcronym = c.CountryAcronyms,
                                    SubHolding = f.Name,
                                })
                            .ToListAsync();

            return result;
        }
        public string ConvertToMillion(decimal? revenue)
        {
            string result = string.Empty;

            if (revenue.HasValue)
            {
                result = (revenue.Value / 1000000).ToString("##0.00");
            }

            return result;
        }
        public IQueryable<SignedAgreementDashboardHighPriorityViewModel> GetHighPriority(string wwwroot)
        {
            IQueryable<SignedAgreementDashboardHighPriorityViewModel> result = null;

            result = QueryAll(wwwroot, true, false, false);

            return result;
        }
        public List<SignedAgreementKementriaanLembagaViewModel> GetKemntrianLembagaAgreement(Guid id)
        {
            List<SignedAgreementKementriaanLembagaViewModel> result = new List<SignedAgreementKementriaanLembagaViewModel>();

            var query = _context.TbltAgreementKementrianLembagas.Where(x => x.DeletedDate == null && x.AgreementId == id)
                .Join(_context.TblmKementrianLembagas.Where(x => x.DeletedDate == null)
                , transaction => transaction.KementrianLembagaId
                , master => master.Id
                , (transaction, master) => new SignedAgreementKementriaanLembagaViewModel
                {
                    Id = transaction.Id,
                    AgreementId = transaction.AgreementId,
                    KementrianLembagaId = transaction.KementrianLembagaId,
                    LembagaName = master.LembagaName,
                    Description = master.Description,
                })
                .AsQueryable();

            return result = query.ToList();
        }
        public List<SignedAgreementStreamBusinessViewModel> GetStreamBusiness(Guid AgreementId)
        {
            List<SignedAgreementStreamBusinessViewModel> result = new List<SignedAgreementStreamBusinessViewModel>();

            var query = _context.TbltAgreementStreamBusinesses.Where(x => x.DeletedDate == null && x.AgreementId == AgreementId)
                .Select(x => new SignedAgreementStreamBusinessViewModel
                {
                    Id = x.Id,
                    AgreementId = x.AgreementId,
                    StreamBusinessId = x.StreamBusinessId,
                })
                .ToList();

            return result = query;
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
        public SignedAgreementDashboardHighPriorityViewModel GetSignedAgreementDashboardDetail(string wwwroot, Guid guid)
        {
            SignedAgreementDashboardHighPriorityViewModel result = new SignedAgreementDashboardHighPriorityViewModel();

            return result = QueryAll(wwwroot, false, true, true).FirstOrDefault(x => x.Id == guid);
        }
        public List<SignedAgreementPartnerViewModel> GetSignedAgreementDashboardDetailPartners(Guid guid)
        {
            List<SignedAgreementPartnerViewModel> result = new List<SignedAgreementPartnerViewModel>();

            result = _context.TbltAgreementPartners.Where(x => x.DeletedDate == null && x.AgreementId == guid)
                .Select(x => new SignedAgreementPartnerViewModel
                {
                    Id = x.Id,
                    PartnerName = x.PartnerName
                }).ToList();

            return result;
        }
        public List<SignedAgreementLokasiProyekViewModel> GetSignedAgreementDashboardDetailProjectLocations(Guid guid)
        {
            List<SignedAgreementLokasiProyekViewModel> result = new List<SignedAgreementLokasiProyekViewModel>();

            result = _context.TbltAgreementLokasiProyeks.Where(x => x.DeletedDate == null && x.AgreementId == guid)
                .Select(x => new SignedAgreementLokasiProyekViewModel
                {
                    Id = x.Id,
                    LokasiProyek = x.LokasiProyek
                }).ToList();

            return result;
        }
        public List<SignedAgreementEntitasPertaminaViewModel> GetSignedAgreementDashboardDetailPertaminaEntitas(Guid guid)
        {
            List<SignedAgreementEntitasPertaminaViewModel> result = new List<SignedAgreementEntitasPertaminaViewModel>();

            result = _context.TbltAgreementEntitasPertaminas.Where(x => x.DeletedDate == null && x.AgreementId == guid)
                .Join(_context.TblmEntitasPertaminas
                , trasaction => trasaction.EntitasPertaminaId
                , master => master.Id
                , (trasaction, master) => new
                {
                    Trasaction = trasaction,
                    Master = master
                })
                .Select(x => new SignedAgreementEntitasPertaminaViewModel
                {
                    Id = x.Trasaction.Id,
                    CompanyName = x.Master.CompanyName
                }).ToList();

            return result;
        }
        public List<SignedAgreementStreamBusinessViewModel> GetSignedAgreementDashboardDetailStreams(Guid guid)
        {
            List<SignedAgreementStreamBusinessViewModel> result = new List<SignedAgreementStreamBusinessViewModel>();

            result = _context.TbltAgreementStreamBusinesses.Where(x => x.DeletedDate == null && x.AgreementId == guid)
                .Join(_context.TblmStreamBusinesses
                , trasaction => trasaction.StreamBusinessId
                , master => master.Id
                , (trasaction, master) => new
                {
                    Trasaction = trasaction,
                    Master = master
                })
                .Select(x => new SignedAgreementStreamBusinessViewModel
                {
                    Id = x.Trasaction.Id,
                    Name = x.Master.Name
                }).ToList();

            return result;
        }
    }
}