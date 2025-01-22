using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Core;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class ProjectsToOfferDashboardRepository: IProjectsToOfferDashboardRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public ProjectsToOfferDashboardRepository(DB_PINMContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        private IQueryable<OpportunityViewModel> QueryList(IQueryable<TbltOpportunity> query)
        {
            return query
                .Join(_context.TbltOpportunityPicFungsis, a => a.Id, b => b.OpportunityId, (a, b) => new
                {
                    a.Id,
                    a.NamaProyek,
                    a.Capex,
                    a.PotentialRevenuePerYear,
                    a.Progress,
                    FungsiId = b.PicFungsiId,
                    PicLevelId = b.PicLevelId,
                })
                .Join(_context.TblmPicFungsis, c => c.FungsiId, d => d.Id, (c, d) => new
                {
                    c.Id,
                    c.NamaProyek,
                    c.Capex,
                    c.PotentialRevenuePerYear,
                    c.Progress,
                    c.FungsiId,
                    c.PicLevelId,
                    PicName = d.PicName,
                    FungsiPicId = d.Id,

                })
                .Join(_context.TbltOpportunityNegaraMitras, g => g.Id, h => h.OpportunityId, (g, h) => new
                {
                    g.Id,
                    g.NamaProyek,
                    g.Capex,
                    g.PotentialRevenuePerYear,
                    g.Progress,
                    g.FungsiId,
                    g.PicLevelId,
                    g.PicName,
                    NegaraMitraId = h.NegaraMitraId

                })
                .Join(_context.TblmNegaraMitras, i => i.NegaraMitraId, j => j.Id, (i, j) => new
                {
                    i.Id,
                    i.NamaProyek,
                    i.Capex,
                    i.PotentialRevenuePerYear,
                    i.Progress,
                    i.FungsiId,
                    i.PicLevelId,
                    i.PicName,
                    i.NegaraMitraId,
                    HubId = j.HubId
                })
                .Join(_context.TblmHubHeads, k => k.HubId, l => l.HubId, (k, l) => new
                {
                    k.Id,
                    k.NamaProyek,
                    k.Capex,
                    k.PotentialRevenuePerYear,
                    k.Progress,
                    k.FungsiId,
                    k.PicLevelId,
                    k.PicName,
                    k.NegaraMitraId,
                    k.HubId,
                    HubHeadName = l.Name
                })
                .Join(_context.TblmPicLevels, e => e.PicLevelId, f => f.Id, (e, f) => new
                {
                    e.Id,
                    e.NamaProyek,
                    e.Capex,
                    e.PotentialRevenuePerYear,
                    e.Progress,
                    e.FungsiId,
                    e.PicLevelId,
                    e.PicName,
                    e.NegaraMitraId,
                    e.HubId,
                    e.HubHeadName,
                    IsLead = f.IsLead,
                    LevelId = f.Id
                })
                 .Where(x => x.IsLead == true)
                .Select(select => new OpportunityViewModel
                {
                    RowNamaProyek = select.NamaProyek ?? "-",
                    CapexToString = (select.Capex.HasValue ? (select.Capex.Value / 1000000).ToString("0.###") : "-"),
                    PotentialRevenuePerYearToString = (select.PotentialRevenuePerYear.HasValue ? (select.PotentialRevenuePerYear.Value / 1000000).ToString("0.###") : "-"),
                    HubHeadName = select.HubHeadName ?? "-",
                    PICLead = select.PicName ?? "-",
                    Progress = select.Progress ?? "-"
                })
                .AsQueryable();

        }
        public async Task<ResponseDataTableBaseModel<List<OpportunityViewModel>>> GetList(RequestFormDtBaseModel request, OpportunityViewModel decodeModel)
        {
            try
            {
                var query = _context.TbltOpportunities.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TbltOpportunity>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.NamaProyek.ToLower().Contains(filter.Value.ToLower())))
                    .Or(x =>( x.PotentialRevenuePerYear.Value.ToString().Contains(filter.Value.ToLower())))
                    .Or(x => (x.Capex.Value.ToString().Contains(filter.Value.ToLower())))
                    .Or(x => (x.Progress.ToLower().Contains(filter.Value.ToLower())))
                    .Or(x => x.TbltOpportunityPicFungsis
                            .Any(x => x.PicFungsi.PicName.Contains(filter.Value)))
                    .Or(x => x.TbltOpportunityNegaraMitras.Any(p => p.NegaraMitra.Hub.TblmHubHeads.Any(q=>q.Name.ToLower().Contains(filter.Value.ToLower()))));
                    break;
                }

                query = query.Where(predicate);
                var q = query.ToList();
                query = query.Where(x => x.DeletedDate == null);
                IQueryable<OpportunityViewModel> joinquery = QueryList(query);


                //Sorting data
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDirection)))
                {
                    if (request.SortColumnDirection == "asc")
                        joinquery = joinquery.OrderBy(c => EF.Property<string>(c, request.SortColumn));
                    else
                        joinquery = joinquery.OrderByDescending(c => EF.Property<string>(c, request.SortColumn));
                }

                var list = await PaginatedList<OpportunityViewModel, OpportunityViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<OpportunityViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<OpportunityViewModel>>(false, ex.Message);
            }
        }

        public IQueryable<OpportunityLokasiProyekViewModel> GetListProvinceAcronyms()
        {
            var result = (from rec  in _context.TbltOpportunityLokasiProyeks
                          join provinsi in _context.TblmProvinsis
                          on rec.ProvinsiId equals provinsi.Id
                          where rec.DeletedDate == null
                          orderby rec.LokasiProyek
                          select new OpportunityLokasiProyekViewModel()
                          {
                              ProvinceAcronyms= provinsi.ProvinceAcronyms,
                          });
            return result;
        }
        public IQueryable<OpportunityLokasiProyekViewModel> GetListHsh(string provinceAcronyms)
        {
            var result = (from rec in _context.TbltOpportunityLokasiProyeks
                          join entitasOpportunity in _context.TbltOpportunityEntitasPertaminas
                          on rec.OpportunityId equals entitasOpportunity.OpportunityId
                          join provinsi in _context.TblmProvinsis // Assuming this table contains ProvinceAcronyms
                          on rec.ProvinsiId equals provinsi.Id
                          join entitas in _context.TblmEntitasPertaminas
                          on entitasOpportunity.EntitasPertaminaId equals entitas.Id
                          join Hsh in _context.TblmHshes
                          on entitas.HshId equals Hsh.Id
                          where rec.DeletedDate == null && provinsi.ProvinceAcronyms == provinceAcronyms
                          orderby rec.LokasiProyek
                          //group new { Hsh, provinsi } by Hsh.Id into groupHshId
                          select new OpportunityLokasiProyekViewModel
                          {
                              NamaProyeks =Hsh.Name,
                              LokasiProyek = provinsi.NamaProvinsi
                          });
            return result;
        }

        public List<OpportunityHighPriorityViewModel> GetOpportunityHighPriority()
        {
            List<OpportunityHighPriorityViewModel> result = new List<OpportunityHighPriorityViewModel>();
            result =_mapper.Map<List<OpportunityHighPriorityViewModel>>(_context.TbltOpportunityHighPriorities.OrderByDescending(x => x.Sequence).Take(5).OrderBy(x => x.Sequence).ToList());
            return result;
            
        }
        public OpportunityViewModel GetOpportunityById(Guid guid)
        {
            OpportunityViewModel result = new OpportunityViewModel();
            result =_mapper.Map<OpportunityViewModel>(_context.TbltOpportunities.Where(x=>x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
            
        }
        public IQueryable<OpportunityPicFungsiViewModel> GetOpportunityPicFungsiById(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunityHighPriorities
                          join opPicFungsi in _context.TbltOpportunityPicFungsis
                          on rec.OpportunityId equals opPicFungsi.OpportunityId
                          join picFungsi in _context.TblmPicFungsis
                          on opPicFungsi.PicFungsiId equals picFungsi.Id
                          where rec.DeletedDate == null && rec.OpportunityId==guid
                          orderby picFungsi.PicName
                         select new OpportunityPicFungsiViewModel
                          {
                              PicName = picFungsi.PicName,
                          });
            return result;
            
        }
        public IQueryable<OpportunityNegaraMitraViewModel> GetOpportunityNegaraMitraById(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunities
                          join opNegaraMitra in _context.TbltOpportunityNegaraMitras
                          on rec.Id equals opNegaraMitra.OpportunityId
                          join negaraMitra in _context.TblmNegaraMitras
                          on opNegaraMitra.NegaraMitraId equals negaraMitra.Id
                          where rec.DeletedDate == null && rec.Id==guid
                          orderby negaraMitra.NamaNegara
                         select new OpportunityNegaraMitraViewModel
                          {
                              NamaNegara = negaraMitra.NamaNegara,
                          });
            return result;
            
        } 
        public IQueryable<OpportunityEntitasPertaminaViewModel> GetOpportunityEntitasPertaminaById(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunities
                          join opEntitasPertamina in _context.TbltOpportunityEntitasPertaminas
                          on rec.Id equals opEntitasPertamina.OpportunityId
                          join entitasPertamina in _context.TblmEntitasPertaminas
                          on opEntitasPertamina.EntitasPertaminaId equals entitasPertamina.Id
                          where rec.DeletedDate == null && rec.Id==guid
                          orderby entitasPertamina.CompanyName
                         select new OpportunityEntitasPertaminaViewModel
                          {
                              CompanyName = entitasPertamina.CompanyName,
                          });
            return result;
            
        }
        public IQueryable<OpportunityKesiapanProyekViewModel> GetOpportunityKesiapanProyekById(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunities
                          join opKesiapanProyek in _context.TbltOpportunityKesiapanProyeks
                          on rec.Id equals opKesiapanProyek.OpportunityId
                          join kesiapanProyek in _context.TblmKesiapanProyeks
                          on opKesiapanProyek.KesiapanProyekId equals kesiapanProyek.Id
                          where rec.DeletedDate == null && rec.Id==guid
                          orderby kesiapanProyek.Name
                          select new OpportunityKesiapanProyekViewModel
                          {
                              Name = kesiapanProyek.Name,
                          });
            return result;
        } 
        public IQueryable<OpportunityLokasiProyekViewModel> GetOpportunityLokasiProyekById(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunities
                          join opLokasiProyek in _context.TbltOpportunityLokasiProyeks
                          on rec.Id equals opLokasiProyek.OpportunityId
                          join provinsi in _context.TblmProvinsis
                          on opLokasiProyek.ProvinsiId equals provinsi.Id
                          where rec.DeletedDate == null && rec.Id==guid
                          orderby provinsi.NamaProvinsi
                          select new OpportunityLokasiProyekViewModel
                          {
                              NamaProvinsi = provinsi.NamaProvinsi,
                          });
            return result;
        }
        public IQueryable<OpportunityStreamBusinessViewModel> GetOpportunityStremBusinessById(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunities
                          join opStreamBusiness in _context.TbltOpportunityStreamBusinesses
                          on rec.Id equals opStreamBusiness.OpportunityId
                          join streamBusiness in _context.TblmStreamBusinesses
                          on opStreamBusiness.StreamBusinessId equals streamBusiness.Id
                          where rec.DeletedDate == null && rec.Id==guid
                          orderby streamBusiness.OrderSeq
                          select new OpportunityStreamBusinessViewModel
                          {
                              QueryStreamBusinessName = streamBusiness.Name,
                          });
            return result;
        }
        public IQueryable<OpportunityTargetMitraViewModel> GetOpportunityTargetMitraById(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunities
                          join opTargetMitra in _context.TbltOpportunityTargetMitras
                          on rec.Id equals opTargetMitra.OpportunityId
                          join targetMitra in _context.TblmTargetMitras
                          on opTargetMitra.TargetMitraId equals targetMitra.Id
                          where rec.DeletedDate == null && rec.Id==guid
                          orderby targetMitra.Name
                          select new OpportunityTargetMitraViewModel
                          {
                              Name = targetMitra.Name,
                          });
            return result;
        }
        public IQueryable<OpportunityPartnerViewModel> GetOpportunityPartnerById(Guid guid)
        {
            var result = (from rec in _context.TbltOpportunities
                          join opPartner in _context.TbltOpportunityPartners
                          on rec.Id equals opPartner.OpportunityId
                          where rec.DeletedDate == null && rec.Id==guid
                          orderby opPartner.CreateDate
                          select new OpportunityPartnerViewModel
                          {
                              PartnerName = opPartner.PartnerName,
                          });
            return result;
        }
        public List<NegaraMitraViewModel> GetNegaraMitraByOpportunityId(Guid guid)
        {
            List<NegaraMitraViewModel> result = _context.TbltOpportunityNegaraMitras
               .Where(x => x.OpportunityId == guid)
               .Join(_context.TblmNegaraMitras
               .Join(_context.TblmKawasanMitras, x => x.KawasanMitraId, y => y.Id, (x, y) =>
                   new NegaraMitraViewModel
                   {
                       Id = x.Id,
                       NamaNegara = x.NamaNegara,
                       KawasanMitraId = x.KawasanMitraId,
                       NamaKawasan = y.NamaKawasan,
                       Flag = x.Flag
                   }), x => x.NegaraMitraId, y => y.Id, (x, y) =>
                   new NegaraMitraViewModel
                   {
                       Id = x.Id,
                       NamaNegara = y.NamaNegara,
                       KawasanMitraId = y.KawasanMitraId,
                       NamaKawasan = y.NamaKawasan,
                       Flag = y.Flag
                   })
               .ToList();
            return result;

        }
        public List<PicFungsiViewModel> GetPicFungsiByOpportunityId(Guid guid)
        {
            List<PicFungsiViewModel> result = _context.TbltOpportunityPicFungsis
               .Where(x => x.OpportunityId == guid && x.DeletedDate == null)
               .Join(_context.TblmPicFungsis, a => a.PicFungsiId, b => b.Id, (a, b) => new
               {
                   a.PicLevelId,
                   PicFungsiId = b.Id,
                   PicName = b.PicName,
                   Phone = b.Phone,
                   Email = b.Email,
                   ActiveStatus = b.ActiveStatus
               })
               //.Join(_context.TblmPicLevels, c => c.PicLevelId, d => d.Id, (c, d) => new
               //{
               //    c.PicFungsiId,
               //    c.PicName,
               //    c.Phone,
               //    c.Email,
               //    c.ActiveStatus,
               //    PicLevelId = d.Id,
               //    isLead = d.IsLead,

               //})
               .Select(select => new PicFungsiViewModel
               {
                   Id = select.PicFungsiId,
                   PicName = select.PicName,
                   Phone = select.Phone,
                   Email = select.Email,
                   ActiveStatus = select.ActiveStatus,
               }).ToList();
               
            return result;

        }
        public List<HubHeadViewModel> GetHubHeadByOpportunityId(Guid guid)
        {
            List<HubHeadViewModel> result = _context.TbltOpportunityNegaraMitras
               .Where(x => x.OpportunityId == guid && x.DeletedDate == null)
               .Join(_context.TblmNegaraMitras, a => a.NegaraMitraId, b => b.Id, (a, b) => new
               {
                   a.NegaraMitraId,
                   HubId = b.HubId,
                   NamaNegara = b.NamaNegara,

               })
               .Join(_context.TblmHubHeads, c => c.HubId, d => d.HubId, (c, d) => new
               {
                   c.NegaraMitraId,
                   c.NamaNegara,
                   Name=d.Name,
                   UserEmail=d.UserEmail,
                   isActive =d.IsActive,
                   ContactNumber =d.ContactNumber,
                   HubHeadId = d.Id,
                   HubId = d.HubId,

                   
               })
               .Select(select => new HubHeadViewModel
               {
                   Id =select.HubHeadId,
                   HubId =select.HubId,
                   Name = select.Name,
                   ContactNumber = select.ContactNumber,
                   UserEmail = select.UserEmail,
                   IsActive = select.isActive,

               }).ToList();
               
            return result;

        }
    }
}
