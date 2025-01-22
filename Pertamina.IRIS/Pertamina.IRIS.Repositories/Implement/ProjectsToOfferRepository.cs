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
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class ProjectsToOfferRepository : IProjectsToOfferRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public ProjectsToOfferRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Private
        private IQueryable<OpportunityViewModel> QueryList(IQueryable<TbltOpportunity> query)
        {
            return query
                .LeftJoin(_context.TbltOpportunityEntitasPertaminas
                .Join(_context.TblmEntitasPertaminas
                .Join(_context.TblmHshes, ad => ad.HshId, sf => sf.Id, (ad, sf) => new OpportunityEntitasPertaminaViewModel()
                {
                    Id = ad.Id,
                    EntitasPertaminaId = ad.Id,
                    CompanyName = ad.CompanyName,
                    HshId = sf.Id,
                    HshName = sf.Name
                }), ad => ad.EntitasPertaminaId, sf => sf.Id, (ad, sf) => new OpportunityEntitasPertaminaViewModel()
                {
                    Id = ad.Id,
                    OpportunityId = ad.OpportunityId,
                    HshId = sf.HshId,
                    EntitasPertaminaId = ad.EntitasPertaminaId,
                    CompanyName = sf.CompanyName,
                }), ad => ad.Id, sf => sf.OpportunityId, (ad, sf) => new OpportunityViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    QueryNamaProyek = ad.NamaProyek,
                    QueryHshId = sf.HshId,
                    QueryEntitasPertaminaId = sf.EntitasPertaminaId,
                    QueryEntitasPertaminaName = sf.CompanyName,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    Deliverables = ad.Deliverables,
                    NilaiProyek = ad.NilaiProyek,
                    Timeline=ad.Timeline,
                    ProjectProfile = ad.ProjectProfile,
                    Progress = ad.Progress,
                    CatatanTambahan = ad.CatatanTambahan
                })
                .LeftJoin(_context.TbltOpportunityNegaraMitras
                .Join(_context.TblmNegaraMitras
                .Join(_context.TblmKawasanMitras, ad => ad.KawasanMitraId, sf => sf.Id, (ad, sf) => new OpportunityNegaraMitraViewModel()
                {
                    Id = ad.Id,
                    NegaraMitraId = ad.Id,
                    NamaNegara = ad.NamaNegara,
                    KawasanMitraId = ad.KawasanMitraId,
                    NamaKawasan = sf.NamaKawasan,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                }), ad => ad.NegaraMitraId, sf => sf.Id, (ad, sf) => new OpportunityNegaraMitraViewModel()
                {
                    Id = ad.Id,
                    OpportunityId = ad.OpportunityId,
                    NegaraMitraId = ad.NegaraMitraId,
                    NamaNegara = sf.NamaNegara,
                    KawasanMitraId = sf.KawasanMitraId,
                    NamaKawasan = sf.NamaKawasan,
                }), ad => ad.Id, sf => sf.OpportunityId, (ad, sf) => new OpportunityViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    QueryNamaProyek = ad.QueryNamaProyek,
                    QueryHshId = ad.QueryHshId,
                    QueryEntitasPertaminaId = ad.QueryEntitasPertaminaId,
                    QueryEntitasPertaminaName = ad.QueryEntitasPertaminaName,
                    QueryNegaraMitraId = sf.NegaraMitraId,
                    QueryNamaNegara = sf.NamaNegara,
                    QueryKawasanMitraId = sf.KawasanMitraId,
                    QueryNamaKawasan = sf.NamaKawasan,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    Deliverables = ad.Deliverables,
                    NilaiProyek = ad.NilaiProyek,
                    Timeline = ad.Timeline,
                    ProjectProfile = ad.ProjectProfile,
                    Progress = ad.Progress,
                    CatatanTambahan = ad.CatatanTambahan
                })
                .LeftJoin(_context.TbltOpportunityStreamBusinesses
                .Join(_context.TblmStreamBusinesses, ad => ad.StreamBusinessId, sf => sf.Id, (ad, sf) => new OpportunityStreamBusinessViewModel()
                {
                    Id = ad.Id,
                    OpportunityId = ad.OpportunityId,
                    StreamBusinessId = ad.StreamBusinessId,
                    QueryStreamBusinessName = sf.Name,
                }), ad => ad.Id, sf => sf.OpportunityId, (ad, sf) =>
                new OpportunityViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    QueryNamaProyek = ad.QueryNamaProyek,
                    QueryHshId = ad.QueryHshId,
                    QueryEntitasPertaminaId = ad.QueryEntitasPertaminaId,
                    QueryEntitasPertaminaName = ad.QueryEntitasPertaminaName,
                    QueryStreamBusinessId = sf.StreamBusinessId,
                    QueryStreamBusinessName = sf.QueryStreamBusinessName,
                    QueryNegaraMitraId = ad.QueryNegaraMitraId,
                    QueryNamaNegara = ad.QueryNamaNegara,
                    QueryKawasanMitraId = ad.QueryKawasanMitraId,
                    QueryNamaKawasan = ad.QueryNamaKawasan,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    Deliverables = ad.Deliverables,
                    NilaiProyek = ad.NilaiProyek,
                    Timeline = ad.Timeline,
                    ProjectProfile = ad.ProjectProfile,
                    Progress = ad.Progress,
                    CatatanTambahan = ad.CatatanTambahan
                })
                .OrderByDescending(x => x.IsDraft)
                .AsQueryable();
        }
        #endregion

        public void Save(OpportunityViewModel model)
        {
            _context.Set<TbltOpportunity>().Add(_mapper.Map<TbltOpportunity>(model));
            _context.SaveChanges();
        }
        public OpportunityViewModel GetOpportunityById(Guid guid)
        {
            OpportunityViewModel result = _mapper.Map<OpportunityViewModel>(_context.TbltOpportunities.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }

        public OpportunityEntitasPertaminaViewModel GetOpportunityEntitasById(Guid guid)
        {
            OpportunityEntitasPertaminaViewModel result = _mapper.Map<OpportunityEntitasPertaminaViewModel>(_context.TbltOpportunityEntitasPertaminas.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }

        public OpportunityStreamBusinessViewModel GetOpportunityStreamBusinessById(Guid guid)
        {
            OpportunityStreamBusinessViewModel result = _mapper.Map<OpportunityStreamBusinessViewModel>(_context.TbltOpportunityStreamBusinesses.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public OpportunityPicFungsiViewModel GetOpportunityPicFungsiById(Guid guid, Guid? picLead )
        {
            OpportunityPicFungsiViewModel result = _mapper.Map<OpportunityPicFungsiViewModel>(_context.TbltOpportunityPicFungsis.Where(x => x.OpportunityId == guid && x.DeletedDate == null && x.PicLevelId == picLead).FirstOrDefault());
            return result;
        }
        public OpportunityKesiapanProyekViewModel GetOpportunityKesiapanProyekById(Guid guid)
        {
            OpportunityKesiapanProyekViewModel result = _mapper.Map<OpportunityKesiapanProyekViewModel>(_context.TbltOpportunityKesiapanProyeks.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public OpportunityNegaraMitraViewModel GetOpportunityNegaraMitraById(Guid guid)
        {
            OpportunityNegaraMitraViewModel result = _mapper.Map<OpportunityNegaraMitraViewModel>(_context.TbltOpportunityNegaraMitras.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public OpportunityTargetMitraViewModel GetOpportunityTargetMitraById(Guid guid)
        {
            OpportunityTargetMitraViewModel result = _mapper.Map<OpportunityTargetMitraViewModel>(_context.TbltOpportunityTargetMitras.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public List<OpportunityPartnerViewModel> GetOpportunityPartnerById(Guid guid)
        {
            List<OpportunityPartnerViewModel> result = _mapper.Map<List<OpportunityPartnerViewModel>>(_context.TbltOpportunityPartners.Where(x => x.OpportunityId == guid && x.DeletedDate == null).ToList().OrderBy(x => x.PartnerName));
            return result;
        }
        public List<OpportunityLokasiProyekViewModel> GetOpportunityLokasiById(Guid guid)
        {
            List<OpportunityLokasiProyekViewModel> result = _mapper.Map<List<OpportunityLokasiProyekViewModel>>(_context.TbltOpportunityLokasiProyeks.Where(x => x.OpportunityId == guid && x.DeletedDate == null).ToList().OrderBy(x => x.LokasiProyek));
            return result;
        }

        #region
        public async Task<OpportunityViewModel> GetOpportunityByIdAsync(Guid guid)
        {
            OpportunityViewModel result = await Task.FromResult(_mapper.Map<OpportunityViewModel>(_context.TbltOpportunities.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }

        public async Task<OpportunityEntitasPertaminaViewModel> GetOpportunityEntitasByIdAsync(Guid guid)
        {
            OpportunityEntitasPertaminaViewModel result = await Task.FromResult(_mapper.Map<OpportunityEntitasPertaminaViewModel>(_context.TbltOpportunityEntitasPertaminas.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }

        public async Task<OpportunityStreamBusinessViewModel> GetOpportunityStreamBusinessByIdAsync(Guid guid)
        {
            OpportunityStreamBusinessViewModel result = await Task.FromResult(_mapper.Map<OpportunityStreamBusinessViewModel>(_context.TbltOpportunityStreamBusinesses.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }

        public async Task<OpportunityPicFungsiViewModel> GetOpportunityPicFungsiByIdAsync(Guid guid)
        {
            OpportunityPicFungsiViewModel result = await Task.FromResult(_mapper.Map<OpportunityPicFungsiViewModel>(_context.TbltOpportunityPicFungsis.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }
        public async Task<OpportunityPicFungsiViewModel> GetOpportunityPicFungsiLeadByIdAsync(Guid guid, Guid? picLevelLead)
        {
            OpportunityPicFungsiViewModel result = await Task.FromResult(_mapper.Map<OpportunityPicFungsiViewModel>(_context.TbltOpportunityPicFungsis.Where(x => x.OpportunityId == guid && x.DeletedDate == null && x.PicLevelId == picLevelLead).FirstOrDefault()));
            return result;
        }
        public List<OpportunityPicFungsiViewModel> GetOpportunityPicFungsiMemberByIdAsync(Guid guid, Guid? picLevelMember)
        {
            var entities =_context.TbltOpportunityPicFungsis
                .Where(x => x.OpportunityId == guid && x.DeletedDate == null && x.PicLevelId == picLevelMember)
                .ToList();

            var result = _mapper.Map<List<OpportunityPicFungsiViewModel>>(entities);

            return result;
        }


        public async Task<OpportunityKesiapanProyekViewModel> GetOpportunityKesiapanProyekByIdAsync(Guid guid)
        {
            OpportunityKesiapanProyekViewModel result = await Task.FromResult(_mapper.Map<OpportunityKesiapanProyekViewModel>(_context.TbltOpportunityKesiapanProyeks.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }

        public async Task<OpportunityNegaraMitraViewModel> GetOpportunityNegaraMitraByIdAsync(Guid guid)
        {
            OpportunityNegaraMitraViewModel result = await Task.FromResult(_mapper.Map<OpportunityNegaraMitraViewModel>(_context.TbltOpportunityNegaraMitras.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }

        public async Task<OpportunityTargetMitraViewModel> GetOpportunityTargetMitraByIdAsync(Guid guid)
        {
            OpportunityTargetMitraViewModel result = await Task.FromResult(_mapper.Map<OpportunityTargetMitraViewModel>(_context.TbltOpportunityTargetMitras.Where(x => x.OpportunityId == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }

        public async Task<List<OpportunityPartnerViewModel>> GetOpportunityPartnerByIdAsync(Guid guid)
        {
            List<OpportunityPartnerViewModel> result = await Task.FromResult(_mapper.Map<List<OpportunityPartnerViewModel>>(_context.TbltOpportunityPartners.Where(x => x.OpportunityId == guid && x.DeletedDate == null).ToList().OrderBy(x => x.PartnerName)));
            return result;
        }

        public async Task<List<OpportunityLokasiProyekViewModel>> GetOpportunityLokasiByIdAsync(Guid guid)
        {
            List<OpportunityLokasiProyekViewModel> result = await Task.FromResult(_mapper.Map<List<OpportunityLokasiProyekViewModel>>(_context.TbltOpportunityLokasiProyeks.Where(x => x.OpportunityId == guid && x.DeletedDate == null).ToList().OrderBy(x => x.LokasiProyek)));
            return result;
        }
        #endregion

        public void SaveToOpportunityEntitasPertamina(OpportunityEntitasPertaminaViewModel model)
        {
            _context.Set<TbltOpportunityEntitasPertamina>().Add(_mapper.Map<TbltOpportunityEntitasPertamina>(model));
            _context.SaveChanges();
        }
        public void SaveToPartner(OpportunityPartnerViewModel model)
        {
            _context.Set<TbltOpportunityPartner>().Add(_mapper.Map<TbltOpportunityPartner>(model));
            _context.SaveChanges();
        }
        public void SaveToOpportunityStreamBusiness(OpportunityStreamBusinessViewModel model)
        {
            _context.Set<TbltOpportunityStreamBusiness>().Add(_mapper.Map<TbltOpportunityStreamBusiness>(model));
            _context.SaveChanges();
        }
        public void SaveToOpportunityPicFungsi(OpportunityPicFungsiViewModel model)
        {
            _context.Set<TbltOpportunityPicFungsi>().Add(_mapper.Map<TbltOpportunityPicFungsi>(model));
            _context.SaveChanges();
        }
        public void SaveToOpportunityNegaraMitra(OpportunityNegaraMitraViewModel model)
        {
            _context.Set<TbltOpportunityNegaraMitra>().Add(_mapper.Map<TbltOpportunityNegaraMitra>(model));
            _context.SaveChanges();
        }
        public void SaveToOpportunityKesiapanProyek(OpportunityKesiapanProyekViewModel model)
        {
            _context.Set<TbltOpportunityKesiapanProyek>().Add(_mapper.Map<TbltOpportunityKesiapanProyek>(model));
            _context.SaveChanges();
        }
        public void SaveToOpportunityTargetMitra(OpportunityTargetMitraViewModel model)
        {
            _context.Set<TbltOpportunityTargetMitra>().Add(_mapper.Map<TbltOpportunityTargetMitra>(model));
            _context.SaveChanges();
        }
        public void SaveToOpportunityLokasiProyek(OpportunityLokasiProyekViewModel model)
        {
            _context.Set<TbltOpportunityLokasiProyek>().Add(_mapper.Map<TbltOpportunityLokasiProyek>(model));
            _context.SaveChanges();
        }
        public bool SaveAllData (OpportunityViewModel model)
        {
            bool result = false;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var now = DateTime.Now;
                    _context.Set<TbltOpportunity>().Add(new TbltOpportunity
                    {
                        Id  = model.Id,
                        CreateBy=model.CreateBy,
                        CreateDate=now,
                        IsDraft = model.IsDraft,
                        CompanyCode =model.CompanyCode,
                        NamaProyek = model.NamaProyek,
                        Deliverables = model.Deliverables,
                        ProjectProfile = model.ProjectProfile,
                        NilaiProyek = model.NilaiProyek,
                        Timeline = model.Timeline,
                        Progress = model.Progress,
                        CatatanTambahan = model.CatatanTambahan,
                        TindakLanjut = model.TindakLanjut,
                        Capex = model.Capex,
                        PotentialRevenuePerYear = model.PotentialRevenuePerYear,
                        TypeOfPartnerNeeded = model.TypeOfPartnerNeeded
                    });

                    var partnerEntity = _context.TbltOpportunityPartners.Where(x => x.OpportunityId == model.Id);
                    _context.Set<TbltOpportunityPartner>().RemoveRange(partnerEntity);
                    if(model.OpPartners != null)
                    {
                        foreach (var partner in model.OpPartners)
                        {
                            if(partner.PartnerName != null)
                            {
                                _context.Set<TbltOpportunityPartner>().Add(new TbltOpportunityPartner
                                {
                                    Id =  Guid.NewGuid(),
                                    CreateBy = model.CreateBy,
                                    CreateDate = now,
                                    OpportunityId = model.Id,
                                    PartnerName = partner.PartnerName,
                                });
                            }
                            
                        }
                    }

                    if(model.EntitasPertaminaId != null)
                    {
                        foreach (var entitas in model.EntitasPertaminaId)
                        {
                            _context.Set<TbltOpportunityEntitasPertamina>().Add(new TbltOpportunityEntitasPertamina
                            {
                                Id = Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                EntitasPertaminaId = entitas,
                                OpportunityId = model.Id,
                            });
                        }
                    }
                    if(model.NegaraMitraId != null)
                    {
                        foreach (var negaraMitra in model.NegaraMitraId)
                        {
                            _context.Set<TbltOpportunityNegaraMitra>().Add(new TbltOpportunityNegaraMitra
                            {
                                Id= Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                NegaraMitraId = negaraMitra,
                                OpportunityId = model.Id,
                            });
                        }
                    } 
                    if(model.NegaraMitraId != null)
                    {
                        foreach (var kesiapanProyek in model.KesiapanProyekId)
                        {
                            _context.Set<TbltOpportunityKesiapanProyek>().Add(new TbltOpportunityKesiapanProyek
                            {
                                Id = Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                KesiapanProyekId = kesiapanProyek,
                                OpportunityId = model.Id,
                            });
                        }
                    }
                    
                    
                    if(model.TargetMitraId != null) 
                    {
                        foreach (var targetMitra in model.TargetMitraId)
                        {
                            _context.Set<TbltOpportunityTargetMitra>().Add(new TbltOpportunityTargetMitra
                            {
                                Id= Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                TargetMitraId = targetMitra,
                                OpportunityId = model.Id,
                            });
                        }
                    }
                   
                   if(model.StreamBusinessId != null)
                    {
                        _context.Set<TbltOpportunityStreamBusiness>().Add(new TbltOpportunityStreamBusiness
                        {
                            Id= Guid.NewGuid(),
                            CreateBy = model.CreateBy,
                            CreateDate = now,
                            StreamBusinessId = model.StreamBusinessId,
                            OpportunityId = model.Id,

                        });
                    } 
                    


                    if(model.OpLokasiProyeks != null)
                    {
                        foreach (var lokasiProyek in model.OpLokasiProyeks)
                        {
                            if(lokasiProyek.LokasiProyek != null)
                            {
                                _context.Set<TbltOpportunityLokasiProyek>().Add(new TbltOpportunityLokasiProyek
                                {
                                    Id= Guid.NewGuid(),
                                    CreateBy = model.CreateBy,
                                    CreateDate = now,
                                    LokasiProyek =lokasiProyek.LokasiProyek,
                                    OpportunityId = model.Id,

                                });
                            }
                        }
                    }

                    var picLead = _context.TbltOpportunityPicFungsis.Where(x => x.OpportunityId == model.Id);
                    _context.Set<TbltOpportunityPicFungsi>().RemoveRange(picLead);

                    if(model.PicFungsiId != null)
                    {
                        if (model.PicFungsiId[0] != null)
                        {
                            _context.Set<TbltOpportunityPicFungsi>().Add(new TbltOpportunityPicFungsi
                            {
                                Id= Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                OpportunityId = model.Id,
                                PicFungsiId = model.PicFungsiId[0],
                                PicLevelId = model.PICLevelLeadId
                            });
                        }
                    }
                   

                    if (model.OpPicFungsis != null)
                    {
                        foreach (var picMember in model.OpPicFungsis)
                        {
                            if(picMember.PicFungsiId != null)
                            {
                                _context.Set<TbltOpportunityPicFungsi>().Add(new TbltOpportunityPicFungsi
                                {
                                    Id= Guid.NewGuid(),
                                    CreateBy = picMember.CreateBy,
                                    CreateDate = picMember.CreateDate,
                                    OpportunityId = model.Id,
                                    PicFungsiId = picMember.PicFungsiId,
                                    PicLevelId = model.PICLevelMemberId,
                                });
                            }
                            
                        }
                    }
                    

                    int queryCommit = _context.SaveChanges();
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


        public IEnumerable<OpportunityViewModel> GetAllOpportunityExcel()
        {
            IQueryable<OpportunityViewModel> items = _context.VwExportProjectToOffers.Select(opportunity => new OpportunityViewModel
            {

                NamaProyek = opportunity.NamaProyek ?? "N/A",
                CellNamaProyek = opportunity.NamaProyek ?? "N/A",
                CellStreamBusiness = opportunity.StreamBusiness ?? "N/A",
                CellEntitasPertamina = opportunity.EntitasPertamina ?? "N/A",
                CellHsh = opportunity.HshName ?? "N/A",
                CellHshId = opportunity.HshId ?? "N/A",
                CellCapex = opportunity.Capex.ToString() ?? "N/A",
                CellPotentialRevenue = opportunity.PotentialRevenue.ToString() ?? "N/A",
                CellTypeOfPartnerNeeded = opportunity.TypeOfPartnerNeeded ?? "N/A",
                CellFungsiPic = opportunity.PicLead ?? "N/A",
                CellFungsiPicMember = opportunity.PicMember ?? "N/A",
                CellExistingPartner = opportunity.ExistingParties ?? "N/A",
                CellTargetMitra = opportunity.TargetMitra ?? "N/A",
                CellKawasanProyek = opportunity.KawasanMitra ?? "N/A",
                CellLokasiProyekNegara = opportunity.ProyekCountry ?? "N/A",
                CellLokasiProyekProvinsi = opportunity.LokasiProyek ?? "N/A",
                CellProjectProfile = opportunity.ProjectProfile ?? "N/A",
                CellDeliverables = opportunity.Deliverables ?? "",
                CellNilaiProyek = opportunity.NilaiProyek ?? "N/A",
                CellTimeline = opportunity.Timeline ?? "N/A",
                CellProgress = opportunity.Progress ?? "N/A",
                CellKesiapanProyek = opportunity.KesiapanProyek ?? "N/A",
                CellCatatanTambahan = opportunity.CatatanTambahan ?? "N/A",
                CellTindakLanjut = opportunity.TindakLanjut ?? "N/A",
                CellNegaraMitraId = opportunity.NegaraMitraId ?? "N/A",
                CellStreamBusinessId = opportunity.StreamBusinessId ?? "N/A",
                CellEntitasPertaminaId = opportunity.EntitasPertaminaId ?? "N/A",
                CellCreateDate = opportunity.TanggalBuat ?? "N/A",
                IsDraft = opportunity.IsDraft
            });

            return items;
        }


        public bool UpdateAllData(OpportunityViewModel model)
        {
            bool result = false;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    DateTime now = DateTime.Now;

                    TbltOpportunity record = HasRecordById(model.Id);
                    record.NamaProyek = model.NamaProyek;
                    record.Deliverables = model.Deliverables;
                    record.NilaiProyek = model.NilaiProyek;
                    record.Timeline = model.Timeline;
                    record.ProjectProfile = model.ProjectProfile;
                    record.Capex = model.Capex;
                    record.PotentialRevenuePerYear = model.PotentialRevenuePerYear;
                    record.TypeOfPartnerNeeded = model.TypeOfPartnerNeeded;
                    record.Progress = model.Progress;
                    record.CatatanTambahan = model.CatatanTambahan;
                    record.TindakLanjut = model.TindakLanjut;
                    record.IsDraft = false;
                    record.UpdateBy = model.UpdateBy;
                    record.UpdateDate = now;
                    _context.Set<TbltOpportunity>().Update(record);

                   
                    if (model.OpPartners != null)
                    {
                        var partnerEntity = _context.TbltOpportunityPartners.Where(x => x.OpportunityId == model.Id);
                        _context.Set<TbltOpportunityPartner>().RemoveRange(partnerEntity);
                        foreach (var partner in model.OpPartners)
                        {
                            if (partner.PartnerName != null)
                            {
                                _context.Set<TbltOpportunityPartner>().Add(new TbltOpportunityPartner
                                {
                                    Id =  Guid.NewGuid(),
                                    CreateBy = model.CreateBy,
                                    CreateDate = now,
                                    UpdateBy = model.UpdateBy,
                                    UpdateDate = now,
                                    OpportunityId = model.Id,
                                    PartnerName = partner.PartnerName,
                                });
                            }

                        }
                    }

                    
                    if (model.EntitasPertaminaId != null)
                    {
                        var pertaminaEntity = _context.TbltOpportunityEntitasPertaminas.Where(x => x.OpportunityId == model.Id);
                        _context.Set<TbltOpportunityEntitasPertamina>().RemoveRange(pertaminaEntity);
                        foreach (var entitas in model.EntitasPertaminaId)
                        {
                            _context.Set<TbltOpportunityEntitasPertamina>().Add(new TbltOpportunityEntitasPertamina
                            {
                                Id = Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                UpdateBy = model.UpdateBy,
                                UpdateDate = now,
                                EntitasPertaminaId = entitas,
                                OpportunityId = model.Id,
                            });
                        }
                    }
                    if (model.NegaraMitraId != null)
                    {
                        var projectPartner = _context.TbltOpportunityNegaraMitras.Where(x => x.OpportunityId == model.Id);
                        _context.Set<TbltOpportunityNegaraMitra>().RemoveRange(projectPartner);
                        foreach (var negaraMitra in model.NegaraMitraId)
                        {
                            _context.Set<TbltOpportunityNegaraMitra>().Add(new TbltOpportunityNegaraMitra
                            {
                                Id= Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                UpdateBy = model.UpdateBy,
                                UpdateDate = now,
                                NegaraMitraId = negaraMitra,
                                OpportunityId = model.Id,
                            });
                        }
                    }
                    if (model.KesiapanProyekId != null)
                    {
                        var kesiapanProyekEntity = _context.TbltOpportunityKesiapanProyeks.Where(x => x.OpportunityId == model.Id);
                        _context.Set<TbltOpportunityKesiapanProyek>().RemoveRange(kesiapanProyekEntity);
                        foreach (var kesiapanProyek in model.KesiapanProyekId)
                        {
                            _context.Set<TbltOpportunityKesiapanProyek>().Add(new TbltOpportunityKesiapanProyek
                            {
                                Id = Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                UpdateBy = model.UpdateBy,
                                UpdateDate = now,
                                KesiapanProyekId = kesiapanProyek,
                                OpportunityId = model.Id,
                            });
                        }
                    }


                    if (model.TargetMitraId != null)
                    {
                        var targetMitraEntity = _context.TbltOpportunityTargetMitras.Where(x => x.OpportunityId == model.Id);
                        _context.Set<TbltOpportunityTargetMitra>().RemoveRange(targetMitraEntity);
                        foreach (var targetMitra in model.TargetMitraId)
                        {
                            _context.Set<TbltOpportunityTargetMitra>().Add(new TbltOpportunityTargetMitra
                            {
                                Id= Guid.NewGuid(),
                                CreateBy = model.CreateBy,
                                CreateDate = now,
                                UpdateBy = model.UpdateBy,
                                UpdateDate = now,
                                TargetMitraId = targetMitra,
                                OpportunityId = model.Id,
                            });
                        }
                    }

                    if (model.StreamBusinessId != null)
                    {
                        var streamBusiness = _context.TbltOpportunityStreamBusinesses.Where(x => x.OpportunityId == model.Id).FirstOrDefault();
                       if(streamBusiness != null)
                        {
                            streamBusiness.Id= streamBusiness.Id;
                            streamBusiness.CreateBy = model.CreateBy;
                            streamBusiness.CreateDate = now;
                            streamBusiness.UpdateBy = model.UpdateBy;
                            streamBusiness.UpdateDate = now;
                            streamBusiness.StreamBusinessId = model.StreamBusinessId;
                            streamBusiness.OpportunityId = model.Id;
                       }
                        _context.Set<TbltOpportunityStreamBusiness>().Update(streamBusiness);
                    }

                    if (model.OpLokasiProyeks != null)
                    {
                        var lokasiProyeks = _context.TbltOpportunityLokasiProyeks.Where(x => x.OpportunityId == model.Id);
                        _context.Set<TbltOpportunityLokasiProyek>().RemoveRange(lokasiProyeks);
                        foreach (var lokasiProyek in model.OpLokasiProyeks)
                        {
                            if (lokasiProyek.LokasiProyek != null)
                            {
                                _context.Set<TbltOpportunityLokasiProyek>().Add(new TbltOpportunityLokasiProyek
                                {
                                    Id= Guid.NewGuid(),
                                    CreateBy = model.CreateBy,
                                    CreateDate = now,
                                    UpdateBy = model.UpdateBy,
                                    UpdateDate = now,
                                    LokasiProyek =lokasiProyek.LokasiProyek,
                                    OpportunityId = model.Id,

                                });
                            }
                        }
                    }

                    if (model.PicFungsiId != null)
                    {
                        if (model.PicFungsiId[0] != null)
                        {
                            var picLead = _context.TbltOpportunityPicFungsis.Where(x => x.OpportunityId == model.Id).FirstOrDefault();
                            if (picLead != null)
                            {
                                picLead.Id= picLead.Id;
                                picLead.CreateBy = model.CreateBy;
                                picLead.CreateDate = now;
                                picLead.UpdateBy = model.UpdateBy;
                                picLead.CreateDate = now;
                                picLead.OpportunityId = model.Id;
                                picLead.PicFungsiId = model.PicFungsiId[0];
                                picLead.PicLevelId = model.PICLevelLeadId;
                            }
                            _context.Set<TbltOpportunityPicFungsi>().Update(picLead);

                        }
                    }


                    if (model.OpPicFungsis != null)
                    {
                        var picMemberEntity = _context.TbltOpportunityPicFungsis.Where(x => x.OpportunityId == model.Id && x.PicLevelId == model.PICLevelMemberId);
                        _context.Set<TbltOpportunityPicFungsi>().RemoveRange(picMemberEntity);
                        foreach (var picMember in model.OpPicFungsis)
                        {
                            if (picMember.PicFungsiId != null)
                            {
                                _context.Set<TbltOpportunityPicFungsi>().Add(new TbltOpportunityPicFungsi
                                {
                                    Id= Guid.NewGuid(),
                                    CreateBy = model.CreateBy,
                                    CreateDate = now,
                                    UpdateBy = model.UpdateBy,
                                    UpdateDate = now,
                                    OpportunityId = model.Id,
                                    PicFungsiId = picMember.PicFungsiId,
                                    PicLevelId = model.PICLevelMemberId,
                                });
                            }

                        }
                    }


                    int queryCommit = _context.SaveChanges();
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
        public void Update(OpportunityViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TbltOpportunity record = HasRecordById(model.Id);
            record.NamaProyek = model.NamaProyek;
            record.Deliverables = model.Deliverables;
            record.NilaiProyek = model.NilaiProyek;
            record.Timeline = model.Timeline;
            record.ProjectProfile = model.ProjectProfile;
            record.Progress = model.Progress;
            record.CatatanTambahan = model.CatatanTambahan;
            record.TindakLanjut = model.TindakLanjut;
            record.IsDraft = false;
            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TbltOpportunity>().Update(record);
            _context.SaveChanges();

        }

        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TbltOpportunity record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TbltOpportunity>().Update(record);
            _context.SaveChanges();
        }
        public void DeleteStreamBusiness(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltOpportunityStreamBusiness> records = _context.TbltOpportunityStreamBusinesses.Where(x => x.DeletedDate == null && x.OpportunityId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltOpportunityStreamBusiness>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteEntitas(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltOpportunityEntitasPertamina> records = _context.TbltOpportunityEntitasPertaminas.Where(x => x.DeletedDate == null && x.OpportunityId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltOpportunityEntitasPertamina>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeletePicFungsi(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltOpportunityPicFungsi> records = _context.TbltOpportunityPicFungsis.Where(x => x.DeletedDate == null && x.OpportunityId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltOpportunityPicFungsi>().Update(rec);
                _context.SaveChanges();
            }

        }
        public void DeleteKesiapanProyek(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltOpportunityKesiapanProyek> records = _context.TbltOpportunityKesiapanProyeks.Where(x => x.DeletedDate == null && x.OpportunityId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltOpportunityKesiapanProyek>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteNegaraMitra(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltOpportunityNegaraMitra> records = _context.TbltOpportunityNegaraMitras.Where(x => x.DeletedDate == null && x.OpportunityId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltOpportunityNegaraMitra>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteTargetMitra(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltOpportunityTargetMitra> records = _context.TbltOpportunityTargetMitras.Where(x => x.DeletedDate == null && x.OpportunityId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltOpportunityTargetMitra>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeletePartner(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltOpportunityPartner> records = _context.TbltOpportunityPartners.Where(x => x.DeletedDate == null && x.OpportunityId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltOpportunityPartner>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteLokasiProyek(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltOpportunityLokasiProyek> records = _context.TbltOpportunityLokasiProyeks.Where(x => x.DeletedDate == null && x.OpportunityId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltOpportunityLokasiProyek>().Update(rec);
                _context.SaveChanges();
            }
        }
        public TbltOpportunity HasRecordById(Guid guid)
        {
            TbltOpportunity result = _context.TbltOpportunities.Where(x => x.DeletedDate == null && x.Id == guid).FirstOrDefault();
            return result;
        }

        public List<TbltOpportunityEntitasPertamina> HasRecordByIdEntitas(Guid guid)
        {
            List<TbltOpportunityEntitasPertamina> result = _context.TbltOpportunityEntitasPertaminas.Where(x => x.OpportunityId == guid).ToList();
            return result;
        }

        public void DeleteExistingStreamBusiness(Guid guid)
        {
            var entitas = _context.TbltOpportunityStreamBusinesses.Where(x => x.OpportunityId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }

        public void DeleteExistingPartners(Guid guid)
        {
            var partner = _context.TbltOpportunityPartners.Where(x => x.OpportunityId == guid).ToList();
            _context.RemoveRange(partner);
            _context.SaveChanges();
        }

        public void DeleteExistingEntitasPertamina(Guid guid)
        {
            var entitas = _context.TbltOpportunityEntitasPertaminas.Where(x => x.OpportunityId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }

        public void DeleteExistingPicFungsi(Guid guid)
        {
            var picFungsi = _context.TbltOpportunityPicFungsis.Where(x => x.OpportunityId == guid).ToList();
            _context.RemoveRange(picFungsi);
            _context.SaveChanges();
        }

        public void DeleteExistingKesiapanProyek(Guid guid)
        {
            var kesiapanProyek = _context.TbltOpportunityKesiapanProyeks.Where(x => x.OpportunityId == guid).ToList();
            _context.RemoveRange(kesiapanProyek);
            _context.SaveChanges();
        }
        public void DeleteExistingNegaraMitra(Guid guid)
        {
            var negaraMitra = _context.TbltOpportunityNegaraMitras.Where(x => x.OpportunityId == guid).ToList();
            _context.RemoveRange(negaraMitra);
            _context.SaveChanges();
        }
        public void DeleteExistingTargetMitra(Guid guid)
        {
            var targetMitra = _context.TbltOpportunityTargetMitras.Where(x => x.OpportunityId == guid).ToList();
            _context.RemoveRange(targetMitra);
            _context.SaveChanges();
        }
        public Guid? PicLevelLead()
        {
            var picLevel = _context.TblmPicLevels.Where(x => x.IsLead == true).FirstOrDefault();
            var result = picLevel.Id;
            return result;
        }
        public Guid? PicLevelMember()
        {
            var picLevel = _context.TblmPicLevels.Where(x => x.IsLead == false).FirstOrDefault();
            var result = picLevel.Id;
            return result;
        }
        public void DeleteExistingLokasiProyek(Guid guid)
        {
            var entitas = _context.TbltOpportunityLokasiProyeks.Where(x => x.OpportunityId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }


        #region Grid
        public async Task<ResponseDataTableBaseModel<List<OpportunityViewModel>>> GetList(RequestFormDtBaseModel request, OpportunityViewModel decodeModel)
        {
            try
            {
                var query = _context.TbltOpportunities.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TbltOpportunity>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.NamaProyek.ToLower().Contains(filter.Value.ToLower())) ||
                    (x.Deliverables.ToLower().Contains(filter.Value.ToLower())) ||
                    (x.ProjectProfile.ToLower().Contains(filter.Value.ToLower())) ||
                    (x.NilaiProyek.ToLower().Contains(filter.Value.ToLower())) ||
                    (x.Timeline.ToLower().Contains(filter.Value.ToLower())) ||
                    (x.Progress.ToLower().Contains(filter.Value.ToLower())) ||
                    (x.CatatanTambahan.ToLower().Contains(filter.Value.ToLower())));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);
                IQueryable<OpportunityViewModel> joinquery = QueryList(query);

                if (decodeModel.IsDraft.Value)
                    joinquery = joinquery.Where(x => x.IsDraft == decodeModel.IsDraft).AsQueryable();
                
                if (!string.IsNullOrEmpty(decodeModel.EntitasPertaminaDecode))
                {
                    string[] strEntitasPertaminas = decodeModel.EntitasPertaminaDecode.Split('_');

                    List<Guid> entitasPertaminas = new List<Guid>();
                    foreach (var str in strEntitasPertaminas)
                    {
                        Guid newGuid = Guid.Parse(str);
                        entitasPertaminas.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => entitasPertaminas.Contains(x.QueryEntitasPertaminaId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.StreamBusinessDecode))
                {
                    string[] strStreamBusinesss = decodeModel.StreamBusinessDecode.Split('_');

                    List<Guid> streamBusinesss = new List<Guid>();
                    foreach (var str in strStreamBusinesss)
                    {
                        Guid newGuid = Guid.Parse(str);
                        streamBusinesss.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => streamBusinesss.Contains(x.QueryStreamBusinessId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.OpportunityHolderDecode))
                {
                    string[] strOpportunityHolders = decodeModel.OpportunityHolderDecode.Split('_');

                    List<Guid> opportunityHolders = new List<Guid>();
                    foreach (var str in strOpportunityHolders)
                    {
                        Guid newGuid = Guid.Parse(str);
                        opportunityHolders.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => opportunityHolders.Contains(x.QueryHshId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.NegaraMitraDecode))
                {
                    string[] strNegaraMitras = decodeModel.NegaraMitraDecode.Split('_');

                    List<Guid> negaraMitras = new List<Guid>();
                    foreach (var str in strNegaraMitras)
                    {
                        Guid newGuid = Guid.Parse(str);
                        negaraMitras.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => negaraMitras.Contains(x.QueryNegaraMitraId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.RangeCreateDateDecode))
                {
                    if (decodeModel.RangeCreateDateDecode.Contains("-"))
                    {
                        string trimRangeCreateDate = decodeModel.RangeCreateDateDecode.Replace(" ", "").Trim();

                        string[] arrayRangeCreateDate = trimRangeCreateDate.Split("-");

                        DateTime start = DateTime.ParseExact(arrayRangeCreateDate[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        DateTime end = DateTime.ParseExact(arrayRangeCreateDate[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        joinquery = joinquery.Where(x => x.CreateDate.Value.Date >= start && x.CreateDate.Value.Date <= end).AsQueryable();
                    }
                }

                joinquery = joinquery.GroupBy(x => x.Id, (a, b) => new OpportunityViewModel
                {
                    Id = a
                }).Join(_context.TbltOpportunities, x => x.Id, y => y.Id, (x, y) => new OpportunityViewModel
                {
                    Id = x.Id,
                    IsDraft = y.IsDraft,
                    RowNamaProyek = y.NamaProyek,
                    CreateDate = y.CreateDate
                });

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
                    joinquery = joinquery.OrderByDescending(x => x.IsDraft).ThenByDescending(x => x.CreateDate).AsQueryable(); ;
                }

                var list = await PaginatedList<OpportunityViewModel, OpportunityViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<OpportunityViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<OpportunityViewModel>>(false, ex.Message);
            }
        }
        public async Task<List<HshViewModel>> GetRecordsHsh()
        {
            IQueryable<TblmHsh> query = _context.TblmHshes.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            List<HshViewModel> result = await Task.FromResult(_mapper.Map<List<HshViewModel>>(query));

            return result.ToList();
        }
        public List<HshViewModel> GetAllDataHsh()
        {
            IQueryable<TblmHsh> result = _context.TblmHshes.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<HshViewModel>>(result);
        }
        public int? GetCountHsh(int relation)
        {
            int result = 0;

            IQueryable<OpportunityEntitasPertaminaViewModel> GetAllRecord = _context.TbltOpportunities.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TbltOpportunityEntitasPertaminas, a => a.Id, b => b.OpportunityId, (a, b) =>
                new OpportunityEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    OpportunityId = a.Id,
                    EntitasPertaminaId = b.EntitasPertaminaId
                })
                .Join(_context.TblmEntitasPertaminas, a => a.EntitasPertaminaId, b => b.Id, (a, b) =>
                new OpportunityEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    OpportunityId = a.OpportunityId,
                    EntitasPertaminaId = b.Id,
                    HshId = b.HshId
                })
                .Join(_context.TblmHshes.Where(x => x.RelationSequence == relation), a => a.HshId, b => b.Id, (a, b) =>
                new OpportunityEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    OpportunityId = a.OpportunityId,
                    EntitasPertaminaId = b.Id,
                    HshId = b.Id,
                })
                .AsQueryable();

            result = GetAllRecord.GroupBy(x => x.OpportunityId).Count();

            return result;
        }
        public int? GetCountRecordsOpportunity()
        {
            int result = 0;

            IQueryable<OpportunityViewModel> GetAllRecord = _context.TbltOpportunities.Where(x => x.DeletedDate == null && x.IsDraft == false).Select(x =>
            new OpportunityViewModel()
            {
                Id = x.Id
            }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public int? GetCountRecordsNegaraMitra()
        {
            int result = 0;

            IQueryable<OpportunityNegaraMitraViewModel> GetAllRecord = _context.TbltOpportunities.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TbltOpportunityNegaraMitras, a => a.Id, b => b.OpportunityId, (a, b) =>
                new OpportunityNegaraMitraViewModel()
                {
                    Id = b.Id,
                    OpportunityId = a.Id,
                    NegaraMitraId = b.NegaraMitraId
                }).AsQueryable();

            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId).Select(x => new OpportunityNegaraMitraViewModel
            {
                Id = x.Key.Value
            }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }

        // Generelize Data
        public List<OpportunityPartnerViewModel> GetPartners(Guid guid)
        {
            IQueryable<TbltOpportunityPartner> result = _context.TbltOpportunityPartners.Where(x => x.DeletedDate == null && x.OpportunityId == guid).OrderBy(x => x.PartnerName).AsQueryable();

            return _mapper.Map<List<OpportunityPartnerViewModel>>(result);
        }
        public List<OpportunityEntitasPertaminaViewModel> GetEntitasPertamina(Guid guid)
        {
            IQueryable<OpportunityEntitasPertaminaViewModel> result = _context.TbltOpportunityEntitasPertaminas
                .Where(x => x.DeletedDate == null && x.OpportunityId == guid)
                .Join(_context.TblmEntitasPertaminas, x => x.EntitasPertaminaId, y => y.Id, (x, y) =>
                      new OpportunityEntitasPertaminaViewModel
                      {
                          Id = x.Id,
                          CompanyName = y.CompanyName,
                          CreateDate = x.CreateDate
                      })
                .OrderBy(x => x.CreateDate).AsQueryable();

            return result.ToList();
        }
        public List<OpportunityStreamBusinessViewModel> GetStreamBusiness(Guid guid)
        {
            IQueryable<OpportunityStreamBusinessViewModel> result = _context.TbltOpportunityStreamBusinesses
                .Where(x => x.DeletedDate == null && x.OpportunityId == guid)
                .Join(_context.TblmStreamBusinesses, x => x.StreamBusinessId, y => y.Id, (x, y) =>
                     new OpportunityStreamBusinessViewModel
                     {
                         Id = x.Id,
                         QueryStreamBusinessName = y.Name,
                         CreateDate = x.CreateDate
                     })
                .OrderBy(x => x.CreateDate)
                .AsQueryable();

            return result.ToList();
        }
        public List<OpportunityNegaraMitraViewModel> GetNamaNegara(Guid id)
        {
            IQueryable<OpportunityNegaraMitraViewModel> result = _context.TbltOpportunityNegaraMitras
                .Where(x => x.DeletedDate == null && x.OpportunityId == id)
                .Join(_context.TblmNegaraMitras
                .Join(_context.TblmKawasanMitras, x => x.KawasanMitraId, y => y.Id, (x, y) =>
                           new OpportunityNegaraMitraViewModel
                           {
                               Id = x.Id,
                               NamaNegara = x.NamaNegara,
                               KawasanMitraId = y.Id,
                               NamaKawasan = y.NamaKawasan,
                               CreateDate = x.CreateDate
                           }), x => x.NegaraMitraId, y => y.Id, (x, y) =>
                           new OpportunityNegaraMitraViewModel
                           {
                               Id = x.Id,
                               NegaraMitraId = y.Id,
                               NamaNegara = y.NamaNegara,
                               KawasanMitraId = y.Id,
                               NamaKawasan = y.NamaKawasan,
                               CreateDate = x.CreateDate
                           })
                .OrderBy(x => x.CreateDate)
                .AsQueryable();

            return result.ToList();
        }
        public string GetNamaProyek(Guid id)
        {
            string result = _context.TbltOpportunities.Where(x => x.DeletedDate == null && x.Id == id).Select(x => x.NamaProyek).FirstOrDefault();

            return result;
        }
        public bool? GetStatusDraft(Guid id)
        {
            bool? result = _context.TbltOpportunities.Where(x => x.DeletedDate == null && x.Id == id).Select(x => x.IsDraft).FirstOrDefault();

            return result;
        }
        public List<OpportunityKesiapanProyekViewModel> GetKesiapanProyek(Guid id)
        {
            IQueryable<OpportunityKesiapanProyekViewModel> result = _context.TbltOpportunityKesiapanProyeks
                .Where(x => x.DeletedDate == null && x.OpportunityId == id)
                .Join(_context.TblmKesiapanProyeks, x => x.KesiapanProyekId, y => y.Id, (x, y) =>
                     new OpportunityKesiapanProyekViewModel
                     {
                         Id = x.Id,
                         QueryKesiapanProyekName = y.Name,
                         CreateDate = x.CreateDate
                     })
                .OrderBy(x => x.CreateDate)
                .AsQueryable();

            return result.ToList();
        }
        public async Task<IQueryable<OpportunityViewModel>> QueryOpportunityWithFilter(OpportunityViewModel decodeModel)
        {
            IQueryable<OpportunityViewModel> result = null;

            var query = _context.TbltOpportunities.Where(x => x.DeletedDate == null && x.IsDraft == false).AsQueryable();

            query = query.Where(x => x.DeletedDate == null && x.IsDraft == false);

            IQueryable<OpportunityViewModel> joinquery = QueryList(query);

            if (decodeModel.IsDraft != null)
            {
                if (decodeModel.IsDraft.Value)
                    joinquery = joinquery.Where(x => x.IsDraft == decodeModel.IsDraft).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.EntitasPertaminaDecode))
            {
                string[] strEntitasPertaminas = decodeModel.EntitasPertaminaDecode.Split('_');

                List<Guid> entitasPertaminas = new List<Guid>();
                foreach (var str in strEntitasPertaminas)
                {
                    Guid newGuid = Guid.Parse(str);
                    entitasPertaminas.Add(newGuid);
                }

                joinquery = joinquery.Where(x => entitasPertaminas.Contains(x.QueryEntitasPertaminaId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.StreamBusinessDecode))
            {
                string[] strStreamBusinesss = decodeModel.StreamBusinessDecode.Split('_');

                List<Guid> streamBusinesss = new List<Guid>();
                foreach (var str in strStreamBusinesss)
                {
                    Guid newGuid = Guid.Parse(str);
                    streamBusinesss.Add(newGuid);
                }

                joinquery = joinquery.Where(x => streamBusinesss.Contains(x.QueryStreamBusinessId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.NegaraMitraDecode))
            {
                string[] strNegaraMitras = decodeModel.NegaraMitraDecode.Split('_');

                List<Guid> negaraMitras = new List<Guid>();
                foreach (var str in strNegaraMitras)
                {
                    Guid newGuid = Guid.Parse(str);
                    negaraMitras.Add(newGuid);
                }

                joinquery = joinquery.Where(x => negaraMitras.Contains(x.QueryNegaraMitraId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.OpportunityHolderDecode))
            {
                string[] strInitiativeHolders = decodeModel.OpportunityHolderDecode.Split('_');

                List<Guid> initiativeHolders = new List<Guid>();
                foreach (var str in strInitiativeHolders)
                {
                    Guid newGuid = Guid.Parse(str);
                    initiativeHolders.Add(newGuid);
                }

                joinquery = joinquery.Where(x => initiativeHolders.Contains(x.QueryHshId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.RangeCreateDateDecode))
            {
                if (decodeModel.RangeCreateDateDecode.Contains("-"))
                {
                    string trimRangeCreateDate = decodeModel.RangeCreateDateDecode.Replace(" ", "").Trim();

                    string[] arrayRangeCreateDate = trimRangeCreateDate.Split("-");

                    DateTime start = DateTime.ParseExact(arrayRangeCreateDate[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime end = DateTime.ParseExact(arrayRangeCreateDate[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    joinquery = joinquery.Where(x => x.CreateDate.Value.Date >= start && x.CreateDate.Value.Date <= end).AsQueryable();
                }
            }

            result = await Task.FromResult(joinquery);

            return result;  
        }
        public async Task<IQueryable<OpportunityNegaraMitraViewModel>> GetRecordsNegaraMitra(OpportunityViewModel model)
        {
            IQueryable<OpportunityNegaraMitraViewModel> result = null;

            IQueryable<OpportunityNegaraMitraViewModel> GetAllRecord = QueryOpportunityWithFilter(model).Result
                .GroupBy(x => x.Id)
                .Select(x => new OpportunityNegaraMitraViewModel
                {
                    Id = x.Key
                })
                .AsQueryable();

            GetAllRecord = GetAllRecord.Join(_context.TbltOpportunityNegaraMitras, a => a.Id, b => b.OpportunityId, (a, b) =>
                    new OpportunityNegaraMitraViewModel()
                    {
                        Id = a.Id,
                        OpportunityId = b.OpportunityId,
                        NegaraMitraId = b.NegaraMitraId
                    }).AsQueryable();

            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId)
                .Select(x =>
                    new OpportunityNegaraMitraViewModel
                    {
                        Id = x.Key.Value
                    })
                .AsQueryable();

            result = await Task.FromResult(GetAllRecord);

            return result;
        }

        public async Task<IQueryable<OpportunityNegaraMitraViewModel>> GetRecordsNegaraMitraWithSearch(OpportunityViewModel model, string strSearch)
        {
            IQueryable<OpportunityNegaraMitraViewModel> result = null;

            IQueryable<OpportunityNegaraMitraViewModel> GetAllRecord = QueryOpportunityWithFilter(model).Result
                .Where(x => x.QueryNamaProyek.Contains(strSearch)||
                    x.Deliverables.Contains(strSearch)||
                    x.NilaiProyek.Contains(strSearch)||
                    x.Timeline.Contains(strSearch)||
                    x.ProjectProfile.Contains(strSearch)||
                    x.Progress.Contains(strSearch)||
                    x.CatatanTambahan.Contains(strSearch))
                .GroupBy(x => x.Id)
                .Select(x => new OpportunityNegaraMitraViewModel
                {
                    Id = x.Key
                })
                .AsQueryable();

            GetAllRecord = GetAllRecord.Join(_context.TbltOpportunityNegaraMitras, a => a.Id, b => b.OpportunityId, (a, b) =>
                    new OpportunityNegaraMitraViewModel()
                    {
                        Id = a.Id,
                        OpportunityId = b.OpportunityId,
                        NegaraMitraId = b.NegaraMitraId
                    }).AsQueryable();

            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId)
                .Select(x =>
                    new OpportunityNegaraMitraViewModel
                    {
                        Id = x.Key.Value
                    })
                .AsQueryable();

            result = await Task.FromResult(GetAllRecord);

            return result;
        }
        #endregion

        #region Read
        public List<string> GetReadPartners(Guid guid)
        {
            List<string> result = _context.TbltOpportunityPartners.Where(x => x.OpportunityId == guid).Select(x => x.PartnerName).ToList();

            return result;
        }
        public List<PicFungsiViewModel> GetReadPicFungsiById(Guid guid)
        {
            var levelId = _context.TblmPicLevels.Where(x => x.IsLead == false).FirstOrDefault();

            List<PicFungsiViewModel> result = _context.TbltOpportunityPicFungsis
                .Where(x => x.OpportunityId == guid && x.PicLevelId == levelId.Id)
                .Join(_context.TblmPicFungsis
                .Join(_context.TblmFungsis, x => x.FungsiId, y => y.Id, (x, y) =>
                    new PicFungsiViewModel
                    {
                        Id = x.Id,
                        PicName = x.PicName,
                        Phone = x.Phone,
                        Email = x.Email,
                        FungsiId = x.FungsiId,
                        NameFungsi = y.FungsiName
                    }), x => x.PicFungsiId, y => y.Id, (x, y) =>
                    new PicFungsiViewModel
                    {
                        Id = y.Id,
                        PicName = y.PicName,
                        Phone = y.Phone,
                        Email = y.Email,
                        FungsiId = y.FungsiId,
                        NameFungsi = y.NameFungsi,
                        
                    })
                .ToList();
            return result;
        } 
        public PicFungsiViewModel GetReadPicFungsiByIdLead(Guid guid)
        {
            var levelId = _context.TblmPicLevels.Where(x => x.IsLead == true).FirstOrDefault();

            PicFungsiViewModel result = _context.TbltOpportunityPicFungsis
                .Where(x => x.OpportunityId == guid && x.PicLevelId == levelId.Id)
                .Join(_context.TblmPicFungsis
                .Join(_context.TblmFungsis, x => x.FungsiId, y => y.Id, (x, y) =>
                    new PicFungsiViewModel
                    {
                        Id = x.Id,
                        PicName = x.PicName,
                        Phone = x.Phone,
                        Email = x.Email,
                        FungsiId = x.FungsiId,
                        NameFungsi = y.FungsiName
                    }), x => x.PicFungsiId, y => y.Id, (x, y) =>
                    new PicFungsiViewModel
                    {
                        Id = y.Id,
                        PicName = y.PicName,
                        Phone = y.Phone,
                        Email = y.Email,
                        FungsiId = y.FungsiId,
                        NameFungsi = y.NameFungsi,
                        
                    })
                .FirstOrDefault();
            return result;
        }
        public List<EntitasPertaminaViewModel> GetReadEntitasPertamina(Guid guid)
        {
            List<EntitasPertaminaViewModel> result = _context.TbltOpportunityEntitasPertaminas
                .Where(x => x.OpportunityId == guid)
                .Join(_context.TblmEntitasPertaminas
                .Join(_context.TblmHshes, x => x.HshId, y => y.Id, (x, y) =>
                new EntitasPertaminaViewModel
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    HshId = x.HshId,
                    NameHsh = y.Name
                }), x => x.EntitasPertaminaId, y => y.Id, (x, y) =>
                new EntitasPertaminaViewModel
                {
                    Id = x.Id,
                    CompanyName = y.CompanyName,
                    HshId = y.HshId,
                    NameHsh = y.NameHsh
                }).ToList();
            return result;
        }
        public List<NegaraMitraViewModel> GetNegaraMitraById(Guid guid)
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
                        NamaKawasan = y.NamaKawasan
                    }), x => x.NegaraMitraId, y => y.Id, (x, y) =>
                    new NegaraMitraViewModel
                    {
                        Id = x.Id,
                        NamaNegara = y.NamaNegara,
                        KawasanMitraId = y.KawasanMitraId,
                        NamaKawasan = y.NamaKawasan
                    })
                .ToList();
            return result;
        }
        public List<string> GetLokasiProyekProvinsi(Guid guid)
        {
            List<string> result = _context.TbltOpportunityLokasiProyeks.Where(x => x.OpportunityId == guid).Select(x => x.LokasiProyek).ToList();

            return result;
        }
        public List<StreamBusinessViewModel> GetReadStreamBusinessById(Guid guid)
        {
            List<StreamBusinessViewModel> result = _context.TbltOpportunityStreamBusinesses
                .Where(x => x.OpportunityId == guid)
                .Join(_context.TblmStreamBusinesses, x => x.StreamBusinessId, y => y.Id, (x, y) =>
                    new StreamBusinessViewModel
                    {
                        Id = x.StreamBusinessId.Value,
                        Name = y.Name
                    })
                .ToList();
            return result;
        }
        public List<KesiapanProyekViewModel> GetReadKesiapanProyek(Guid guid)
        {
            List<KesiapanProyekViewModel> result = _context.TbltOpportunityKesiapanProyeks.Where(x => x.OpportunityId == guid)
                .Join(_context.TblmKesiapanProyeks, x => x.KesiapanProyekId, y => y.Id, (x, y) =>
                new KesiapanProyekViewModel
                {
                    Id = x.KesiapanProyekId.Value,
                    Name = y.Name
                })
                .ToList();

            return result;
        }
        public List<TargetMitraViewModel> GetReadTargetMitra(Guid guid)
        {
            List<TargetMitraViewModel> result = _context.TbltOpportunityTargetMitras.Where(x => x.OpportunityId == guid)
                .Join(_context.TblmTargetMitras, x => x.TargetMitraId, y => y.Id, (x, y) =>
                new TargetMitraViewModel
                {
                    Id = x.TargetMitraId.Value,
                    Name = y.Name
                })
                .ToList();

            return result;
        }
        #endregion
    }
}

