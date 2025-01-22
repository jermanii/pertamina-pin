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
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class AgreementRepository : IAgreementRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public AgreementRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Private
        private IQueryable<AgreementViewModel> QueryList(IQueryable<TbltAgreement> query)
        {
            return query
                .LeftJoin(_context.TbltAgreementEntitasPertaminas
                .Join(_context.TblmEntitasPertaminas
                .Join(_context.TblmHshes, ad => ad.HshId, sf => sf.Id, (ad, sf) => new AgreementEntitasPertaminaViewModel()
                {
                    Id = ad.Id,
                    EntitasPertaminaId = ad.Id,
                    CompanyName = ad.CompanyName,
                    HshId = sf.Id,
                    HshName = sf.Name
                }), ad => ad.EntitasPertaminaId, sf => sf.Id, (ad, sf) => new AgreementEntitasPertaminaViewModel()
                {
                    Id = ad.Id,
                    AgreementId = ad.AgreementId,
                    HshId = sf.HshId,
                    EntitasPertaminaId = ad.EntitasPertaminaId,
                    CompanyName = sf.CompanyName,
                }), ad => ad.Id, sf => sf.AgreementId, (ad, sf) => new AgreementViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    Valuation = ad.Valuation,
                    Capex = ad.Capex,
                    PotentialRevenuePerYear = ad.PotentialRevenuePerYear,
                    IsGtg = ad.IsGtg,
                    JudulPerjanjian = ad.JudulPerjanjian,
                    JenisPerjanjianId = ad.JenisPerjanjianId,
                    HshId = sf.HshId,
                    EntitasPertaminaId = sf.EntitasPertaminaId,
                    RowEntitasPertamina = sf.CompanyName,
                    StatusBerlakuId = ad.StatusBerlakuId,
                    DiscussionStatusId = ad.DiscussionStatusId,
                    TanggalTtd = ad.TanggalTtd,
                    TanggalBerakhir = ad.TanggalBerakhir,
                    KodeAgreement = ad.KodeAgreement,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    DeskripsiKendala = ad.DeskripsiKendala,
                    SupportPemerintah = ad.SupportPemerintah,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan
                })
                .LeftJoin(_context.TbltAgreementNegaraMitras
                .Join(_context.TblmNegaraMitras
                .Join(_context.TblmKawasanMitras, ad => ad.KawasanMitraId, sf => sf.Id, (ad, sf) => new AgreementNegaraMitraViewModel()
                {
                    Id = ad.Id,
                    NegaraMitraId = ad.Id,
                    NamaNegara = ad.NamaNegara,
                    KawasanMitraId = ad.KawasanMitraId,
                    NamaKawasan = sf.NamaKawasan,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                }), ad => ad.NegaraMitraId, sf => sf.Id, (ad, sf) => new AgreementNegaraMitraViewModel()
                {
                    Id = ad.Id,
                    AgreementId = ad.AgreementId,
                    NegaraMitraId = ad.NegaraMitraId,
                    NamaNegara = sf.NamaNegara,
                    KawasanMitraId = sf.KawasanMitraId,
                    NamaKawasan = sf.NamaKawasan,
                }), ad => ad.Id, sf => sf.AgreementId, (ad, sf) => new AgreementViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    Valuation = ad.Valuation,
                    Capex = ad.Capex,
                    PotentialRevenuePerYear = ad.PotentialRevenuePerYear,
                    IsGtg = ad.IsGtg,
                    JudulPerjanjian = ad.JudulPerjanjian,
                    JenisPerjanjianId = ad.JenisPerjanjianId,
                    HshId = ad.HshId,
                    EntitasPertaminaId = ad.EntitasPertaminaId,
                    RowEntitasPertamina = ad.RowEntitasPertamina,
                    StreamBusinessId = ad.StreamBusinessId,
                    NegaraMitraId = sf.NegaraMitraId,
                    RowNegaraMitra = sf.NamaNegara,
                    KawasanMitraId = sf.KawasanMitraId,
                    RowKawasanMitra = sf.NamaKawasan,
                    StatusBerlakuId = ad.StatusBerlakuId,
                    DiscussionStatusId = ad.DiscussionStatusId,
                    TanggalTtd = ad.TanggalTtd,
                    TanggalBerakhir = ad.TanggalBerakhir,
                    KodeAgreement = ad.KodeAgreement,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    DeskripsiKendala = ad.DeskripsiKendala,
                    SupportPemerintah = ad.SupportPemerintah,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan
                })
                .LeftJoin(_context.TblmStatusBerlakus, ad => ad.StatusBerlakuId, sf => sf.Id, (ad, sf) =>
                new AgreementViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    Valuation = ad.Valuation,
                    Capex = ad.Capex,
                    PotentialRevenuePerYear = ad.PotentialRevenuePerYear,
                    IsGtg = ad.IsGtg,
                    JudulPerjanjian = ad.JudulPerjanjian,
                    HshId = ad.HshId,
                    EntitasPertaminaId = ad.EntitasPertaminaId,
                    RowEntitasPertamina = ad.RowEntitasPertamina,
                    JenisPerjanjianId = ad.JenisPerjanjianId,
                    NegaraMitraId = ad.NegaraMitraId,
                    RowNegaraMitra = ad.RowNegaraMitra,
                    KawasanMitraId = ad.KawasanMitraId,
                    RowKawasanMitra = ad.RowKawasanMitra,
                    StatusBerlakuId = ad.StatusBerlakuId,
                    DiscussionStatusId = ad.DiscussionStatusId,
                    RowStatusBerlaku = sf.StatusName.ToUpper(),
                    RowStatusBerlakuColorName = sf.ColorName,
                    RowStatusBerlakuColorHexa = sf.ColorHexa,
                    TanggalTtd = ad.TanggalTtd,
                    TanggalBerakhir = ad.TanggalBerakhir,
                    KodeAgreement = ad.KodeAgreement,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    DeskripsiKendala = ad.DeskripsiKendala,
                    SupportPemerintah = ad.SupportPemerintah,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan
                })
                .LeftJoin(_context.TblmDiscussionStatuses, ad => ad.DiscussionStatusId, sf => sf.Id, (ad, sf) =>
                new AgreementViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    Valuation = ad.Valuation,
                    Capex = ad.Capex,
                    PotentialRevenuePerYear = ad.PotentialRevenuePerYear,
                    IsGtg = ad.IsGtg,
                    JudulPerjanjian = ad.JudulPerjanjian,
                    JenisPerjanjianId = ad.JenisPerjanjianId,
                    HshId = ad.HshId,
                    EntitasPertaminaId = ad.EntitasPertaminaId,
                    RowEntitasPertamina = ad.RowEntitasPertamina,
                    NegaraMitraId = ad.NegaraMitraId,
                    RowNegaraMitra = ad.RowNegaraMitra,
                    KawasanMitraId = ad.KawasanMitraId,
                    RowKawasanMitra = ad.RowKawasanMitra,
                    StatusBerlakuId = ad.StatusBerlakuId,
                    DiscussionStatusId = ad.DiscussionStatusId,
                    RowStatusBerlaku = ad.RowStatusBerlaku,
                    RowStatusBerlakuColorName = ad.RowStatusBerlakuColorName,
                    RowStatusBerlakuColorHexa = ad.RowStatusBerlakuColorHexa,
                    RowDiscussionStatus = sf.Name.ToUpper(),
                    RowDiscussionStatusColorName = sf.ColorName,
                    RowDiscussionStatusColorHexa = sf.ColorHexa,
                    TanggalTtd = ad.TanggalTtd,
                    TanggalBerakhir = ad.TanggalBerakhir,
                    KodeAgreement = ad.KodeAgreement,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    DeskripsiKendala = ad.DeskripsiKendala,
                    SupportPemerintah = ad.SupportPemerintah,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan
                })
                .LeftJoin(_context.TblmJenisPerjanjians, ad => ad.JenisPerjanjianId, sf => sf.Id, (ad, sf) =>
                new AgreementViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    Valuation = ad.Valuation,
                    Capex = ad.Capex,
                    PotentialRevenuePerYear = ad.PotentialRevenuePerYear,
                    IsGtg = ad.IsGtg,
                    IsStrategicPartnerShipDecode = sf.IsStrategic,
                    JudulPerjanjian = ad.JudulPerjanjian,
                    HshId = ad.HshId,
                    EntitasPertaminaId = ad.EntitasPertaminaId,
                    RowEntitasPertamina = ad.RowEntitasPertamina,
                    JenisPerjanjianId = ad.JenisPerjanjianId,
                    RowJenisPerjanjian = sf.Name,
                    NegaraMitraId = ad.NegaraMitraId,
                    RowNegaraMitra = ad.RowNegaraMitra,
                    KawasanMitraId = ad.KawasanMitraId,
                    RowKawasanMitra = ad.RowKawasanMitra,
                    StatusBerlakuId = ad.StatusBerlakuId,
                    DiscussionStatusId = ad.DiscussionStatusId,
                    RowStatusBerlaku = ad.RowStatusBerlaku,
                    RowStatusBerlakuColorName = ad.RowStatusBerlakuColorName,
                    RowStatusBerlakuColorHexa = ad.RowStatusBerlakuColorHexa,
                    RowDiscussionStatus = ad.RowDiscussionStatus,
                    RowDiscussionStatusColorName = ad.RowDiscussionStatusColorName,
                    RowDiscussionStatusColorHexa = ad.RowDiscussionStatusColorHexa,
                    TanggalTtd = ad.TanggalTtd,
                    TanggalBerakhir = ad.TanggalBerakhir,
                    KodeAgreement = ad.KodeAgreement,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    DeskripsiKendala = ad.DeskripsiKendala,
                    SupportPemerintah = ad.SupportPemerintah,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan
                })
                .LeftJoin(_context.TbltAgreementStreamBusinesses
                .Join(_context.TblmStreamBusinesses, ad => ad.StreamBusinessId, sf => sf.Id, (ad, sf) => new AgreementStreamBusinessViewModel()
                {
                    Id = ad.Id,
                    AgreementId = ad.AgreementId,
                    StreamBusinessId = ad.StreamBusinessId,
                    QueryStreamBusinessName = sf.Name,
                    IsGreenBusiness = sf.IsGreenBusiness,
                }), ad => ad.Id, sf => sf.AgreementId, (ad, sf) =>
                new AgreementViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    Valuation = ad.Valuation,
                    Capex = ad.Capex,
                    PotentialRevenuePerYear = ad.PotentialRevenuePerYear,
                    IsGtg = ad.IsGtg,
                    IsGreenBusinessDecode =sf.IsGreenBusiness,
                    IsStrategicPartnerShipDecode =ad.IsStrategicPartnerShipDecode,
                    JudulPerjanjian = ad.JudulPerjanjian,
                    HshId = ad.HshId,
                    EntitasPertaminaId = ad.EntitasPertaminaId,
                    RowEntitasPertamina = ad.RowEntitasPertamina,
                    StreamBusinessId = sf.StreamBusinessId,
                    RowStreamBusiness = sf.QueryStreamBusinessName,
                    JenisPerjanjianId = ad.JenisPerjanjianId,
                    RowJenisPerjanjian = ad.RowJenisPerjanjian,
                    NegaraMitraId = ad.NegaraMitraId,
                    RowNegaraMitra = ad.RowNegaraMitra,
                    KawasanMitraId = ad.KawasanMitraId,
                    RowKawasanMitra = ad.RowKawasanMitra,
                    StatusBerlakuId = ad.StatusBerlakuId,
                    DiscussionStatusId = ad.DiscussionStatusId,
                    RowStatusBerlaku = ad.RowStatusBerlaku,
                    RowStatusBerlakuColorName = ad.RowStatusBerlakuColorName,
                    RowStatusBerlakuColorHexa = ad.RowStatusBerlakuColorHexa,
                    RowDiscussionStatus = ad.RowDiscussionStatus,
                    RowDiscussionStatusColorName = ad.RowDiscussionStatusColorName,
                    RowDiscussionStatusColorHexa = ad.RowDiscussionStatusColorHexa,
                    TanggalTtd = ad.TanggalTtd,
                    TanggalBerakhir = ad.TanggalBerakhir,
                    KodeAgreement = ad.KodeAgreement,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    DeskripsiKendala = ad.DeskripsiKendala,
                    SupportPemerintah = ad.SupportPemerintah,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan

                })
                .AsQueryable();
        }
        #endregion

        public int GetCountStatusBerlaku(int relation)
        {
            int result = 0;

            IQueryable<AgreementViewModel> GetAllRecord = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TblmStatusBerlakus.Where(x => x.RelationSequence == relation && x.IsExpired == false), a => a.StatusBerlakuId, b => b.Id, (a, b) =>
                  new AgreementViewModel()
                  {
                      Id = a.Id,

                  })
                  .Join(_context.TbltAgreementNegaraMitras, b => b.Id, c => c.AgreementId, (b, c) => new AgreementViewModel()
                  {
                      Id=b.Id,
                      NegaraMitraId = c.NegaraMitraId
                  }).AsQueryable();
            
            var excludedNegaraMitraIds = _context.TblmNegaraMitraExcludeds
            .Select(x => x.NegaraMitraId)
            .ToList();

            if (excludedNegaraMitraIds.Count > 0)
            {
                foreach (var filter in excludedNegaraMitraIds)
                {
                    GetAllRecord= GetAllRecord.Where(x => x.NegaraMitraId != filter);
                }
            }
            result = GetAllRecord.Count();


            return result;
        }
        public int GetCountStatusDiscussion(int relation)
        {
            int result = 0;

            IQueryable<AgreementViewModel> GetAllRecord = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TblmDiscussionStatuses.Where(x => x.RelationSequence == relation), a => a.DiscussionStatusId, b => b.Id, (a, b) =>
                  new AgreementViewModel()
                  {
                      Id = a.Id
                  }).Join(_context.TbltAgreementNegaraMitras, b => b.Id, c => c.AgreementId, (b, c) => new AgreementViewModel()
                  {
                      Id=b.Id,
                      NegaraMitraId = c.NegaraMitraId
                  }).AsQueryable();

            var excludedNegaraMitraIds = _context.TblmNegaraMitraExcludeds
            .Select(x => x.NegaraMitraId)
            .ToList();

            if (excludedNegaraMitraIds.Count > 0)
            {
                foreach (var filter in excludedNegaraMitraIds)
                {
                    GetAllRecord= GetAllRecord.Where(x => x.NegaraMitraId != filter);
                }
            }
            result = GetAllRecord.Count();


            return result;
        }
        public int? GetCountHsh(int relation)
        {
            int result = 0;

            IQueryable<AgreementEntitasPertaminaViewModel> GetAllRecord = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TbltAgreementEntitasPertaminas, a => a.Id, b => b.AgreementId, (a, b) =>
                new AgreementEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    AgreementId = a.Id,
                    EntitasPertaminaId = b.EntitasPertaminaId,
                    StatusBerlakuId = a.StatusBerlakuId,
                })
                .Join(_context.TblmStatusBerlakus.Where(x => x.IsExpired == false), a => a.StatusBerlakuId, b => b.Id, (a, b) =>
                  new AgreementEntitasPertaminaViewModel()
                  {
                      Id = a.Id,
                      AgreementId = a.AgreementId,
                      EntitasPertaminaId = b.Id,
                  })
                .Join(_context.TblmEntitasPertaminas, a => a.EntitasPertaminaId, b => b.Id, (a, b) =>
                new AgreementEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    AgreementId = a.AgreementId,
                    EntitasPertaminaId = b.Id,
                    HshId = b.HshId
                })
                .Join(_context.TblmHshes.Where(x => x.RelationSequence == relation), a => a.HshId, b => b.Id, (a, b) =>
                new AgreementEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    AgreementId = a.AgreementId,
                    EntitasPertaminaId = b.Id,
                    HshId = b.Id
                })
               
               .Join(_context.TbltAgreementNegaraMitras, a => a.Id, b => b.AgreementId, (a, b) => new AgreementEntitasPertaminaViewModel()
               {
                   Id = b.Id,
                   AgreementId = a.AgreementId,
                   EntitasPertaminaId = b.Id,
                   NegaraMitraId = b.NegaraMitraId
               }).AsQueryable();

            var excludedNegaraMitraIds = _context.TblmNegaraMitraExcludeds
            .Select(x => x.NegaraMitraId)
            .ToList();

            if (excludedNegaraMitraIds.Count > 0)
            {
                foreach (var filter in excludedNegaraMitraIds)
                {
                    GetAllRecord= GetAllRecord.Where(x => x.NegaraMitraId != filter);
                }
            }

            result = GetAllRecord.GroupBy(x => x.AgreementId).Count();

            return result;
        }
        public int? GetAllAgreement()
        {
            int result = 0;

            IQueryable<AgreementViewModel> GetAllRecord = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.IsDraft == false).Select(x =>
            new AgreementViewModel()
            {
                Id = x.Id,
                StatusBerlakuId = x.StatusBerlakuId
            }).Join(_context.TbltAgreementNegaraMitras, b => b.Id, c => c.AgreementId, (b, c) => new AgreementViewModel()
            {
                Id=b.Id,
                NegaraMitraId = c.NegaraMitraId,
                StatusBerlakuId = b.StatusBerlakuId
            })
            .Join(_context.TblmStatusBerlakus.Where(x => x.IsExpired == false), a => a.StatusBerlakuId, b => b.Id, (a, b) => new AgreementViewModel()
            {
                Id = a.Id,
                NegaraMitraId = a.NegaraMitraId,

            })
            .AsQueryable();

            var excludedNegaraMitraIds = _context.TblmNegaraMitraExcludeds
            .Select(x => x.NegaraMitraId)
            .ToList();

            if (excludedNegaraMitraIds.Count > 0)
            {
                foreach (var filter in excludedNegaraMitraIds)
                {
                    GetAllRecord= GetAllRecord.Where(x => x.NegaraMitraId != filter);
                }
            }
            result = GetAllRecord.Count();

            return result;
        }
        public int? GetAllAgreementWithFilter(AgreementViewModel decodeModel)
        {
            int result = 0;

            IQueryable<AgreementViewModel> GetAllRecord = GetAgreementGroupByWithFilter(decodeModel).Select(x =>
            new AgreementViewModel()
            {
                Id = x.Id
            }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public int? GetAllAgreementWithFilter(AgreementViewModel decodeModel, string strSearch)
        {
            int result = 0;

            IQueryable<AgreementViewModel> GetAllRecord = GetAgreementGroupByWithFilter(decodeModel)
                .Where(x => x.JudulPerjanjian.Contains(strSearch) ||
                            x.KodeAgreement.Contains(strSearch) ||
                            //x.Valuation.HasValue.ToString().Contains(strSearch) ||
                            x.Scope.Contains(strSearch) ||
                            x.Progress.Contains(strSearch) ||
                            x.DeskripsiKendala.Contains(strSearch) ||
                            x.SupportPemerintah.Contains(strSearch) ||
                            x.PotensiEskalasi.Contains(strSearch) ||
                            x.CatatanTambahan.Contains(strSearch))
                .Select(x => new AgreementViewModel() { Id = x.Id }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public int? GetAllNegaraMitra()
        {
            int result = 0;

            IQueryable<AgreementNegaraMitraViewModel> GetAllRecord = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TbltAgreementNegaraMitras, a => a.Id, b => b.AgreementId, (a, b) =>
                new AgreementNegaraMitraViewModel()
                {
                    Id = b.Id,
                    AgreementId = a.Id,
                    NegaraMitraId = b.NegaraMitraId,
                    StatusBerlakuId = a.StatusBerlakuId
                })
                .Join(_context.TblmStatusBerlakus.Where(x => x.IsExpired == false), a => a.StatusBerlakuId, b => b.Id, (a, b) => new AgreementNegaraMitraViewModel()
                {
                    Id = a.Id,
                    NegaraMitraId = a.NegaraMitraId,
                }).AsQueryable();

            var excludedNegaraMitraIds = _context.TblmNegaraMitraExcludeds
            .Select(x => x.NegaraMitraId)
            .ToList();

            if (excludedNegaraMitraIds.Count > 0)
            {
                foreach (var filter in excludedNegaraMitraIds)
                {
                    GetAllRecord= GetAllRecord.Where(x => x.NegaraMitraId != filter);
                }
            }
            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId).Select(x => new AgreementNegaraMitraViewModel
            {
                Id = x.Key.Value
            }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public int? GetAllNegaraMitraWithFilter(AgreementViewModel decodeModel)
        {
            int result = 0;

            IQueryable<AgreementNegaraMitraViewModel> GetAllRecord = GetAgreementGroupByWithFilter(decodeModel).Where(x => x.IsDraft == false)
                .Join(_context.TbltAgreementNegaraMitras, a => a.Id, b => b.AgreementId, (a, b) =>
                    new AgreementNegaraMitraViewModel()
                    {
                        Id = b.Id,
                        AgreementId = a.Id,
                        NegaraMitraId = b.NegaraMitraId
                    })
                .AsQueryable();

            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId)
                .Select(x =>
                    new AgreementNegaraMitraViewModel
                    {
                        Id = x.Key.Value
                    })
                .AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public int? GetAllNegaraMitraWithFilter(AgreementViewModel decodeModel, string strSearch)
        {
            int result = 0;

            IQueryable<AgreementNegaraMitraViewModel> GetAllRecord = GetAgreementGroupByWithFilter(decodeModel)
                .Where(x => x.JudulPerjanjian.Contains(strSearch) ||
                            x.KodeAgreement.Contains(strSearch) ||
                            //x.Valuation.HasValue.ToString().Contains(strSearch) ||
                            x.Scope.Contains(strSearch) ||
                            x.Progress.Contains(strSearch) ||
                            x.DeskripsiKendala.Contains(strSearch) ||
                            x.SupportPemerintah.Contains(strSearch) ||
                            x.PotensiEskalasi.Contains(strSearch) ||
                            x.CatatanTambahan.Contains(strSearch)
                )
                .Join(_context.TbltAgreementNegaraMitras, a => a.Id, b => b.AgreementId, (a, b) =>
                    new AgreementNegaraMitraViewModel()
                    {
                        Id = b.Id,
                        AgreementId = a.Id,
                        NegaraMitraId = b.NegaraMitraId
                    }).AsQueryable();

            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId)
                .Select(x =>
                    new AgreementNegaraMitraViewModel
                    {
                        Id = x.Key.Value
                    })
                .AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public List<StatusBerlakuViewModel> GetAllDataStatusBerlaku()
        {
            IQueryable<TblmStatusBerlaku> result = _context.TblmStatusBerlakus.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<StatusBerlakuViewModel>>(result);
        }
        public List<StatusDiscussionViewModel> GetAllDataStatusDiscussion()
        {
            IQueryable<TblmDiscussionStatus> result = _context.TblmDiscussionStatuses.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<StatusDiscussionViewModel>>(result);
        }
        public List<HshViewModel> GetAllDataHsh()
        {
            IQueryable<TblmHsh> result = _context.TblmHshes.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<HshViewModel>>(result);
        }
        public async Task<List<StatusBerlakuViewModel>> GetAllDataStatusBerlakuAsync()
        {
            IQueryable<TblmStatusBerlaku> query = _context.TblmStatusBerlakus.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            List<StatusBerlakuViewModel> result = await Task.FromResult(_mapper.Map<List<StatusBerlakuViewModel>>(query));

            return result;
        }
        public async Task<List<StatusDiscussionViewModel>> GetAllDataStatusDiscussionAsync()
        {
            IQueryable<TblmDiscussionStatus> query = _context.TblmDiscussionStatuses.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            List<StatusDiscussionViewModel> result = await Task.FromResult(_mapper.Map<List<StatusDiscussionViewModel>>(query));

            return result;
        }
        public async Task<List<HshViewModel>> GetAllDataHshAsync()
        {
            IQueryable<TblmHsh> query = _context.TblmHshes.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            List<HshViewModel> result = await Task.FromResult(_mapper.Map<List<HshViewModel>>(query));

            return result;
        }

        #region Gridview
        public async Task<ResponseDataTableBaseModel<List<AgreementViewModel>>> GetList(RequestFormDtBaseModel request, AgreementViewModel decodeModel)
        {
            try
            {
                var query = _context.TbltAgreements.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TbltAgreement>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.JudulPerjanjian.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.Scope.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.Valuation.HasValue && x.Valuation.Value.ToString().ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.Progress.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.DeskripsiKendala.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.SupportPemerintah.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.PotensiEskalasi.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.KodeAgreement.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.CatatanTambahan.ToLower().Contains(filter.Value.ToLower()))
                                                  );
                }

                query = query.Where(x => x.DeletedDate == null &&(( x.IsDraft == true && x.CreateBy ==decodeModel.CreateBy) || x.IsDraft == false));
                query = query.Where(predicate);

                IQueryable<AgreementViewModel> joinquery = QueryList(query);

                //filtering excluded
                var filterExcludedNegaraMitra = _context.TblmNegaraMitraExcludeds.Where(x => x.DeletedDate == null).ToList();
                if (filterExcludedNegaraMitra.Count > 0)
                {
                    foreach (var filter in filterExcludedNegaraMitra)
                    {
                        joinquery= joinquery.Where(x => x.NegaraMitraId != filter.NegaraMitraId);
                    }
                }
                var filterExcludedExpired = _context.TblmStatusBerlakus.Where(x => x.DeletedDate == null && x.IsExpired == true).FirstOrDefault();
                if (filterExcludedExpired !=null)
                {
                    joinquery= joinquery.Where(x => x.StatusBerlakuId != filterExcludedExpired.Id);
                }

                if (decodeModel.IsDraft != null)
                {
                    if (decodeModel.IsDraft.Value)
                        joinquery = joinquery.Where(x => x.IsDraft == decodeModel.IsDraft).AsQueryable();

                }
                if (decodeModel.IsGtg != null)
                {
                    if (decodeModel.IsGtg== true)
                        joinquery = joinquery.Where(x => x.IsGtg == decodeModel.IsGtg).AsQueryable();

                }
                if (decodeModel.IsStrategicPartnerShipDecode != null)
                {
                    if (decodeModel.IsStrategicPartnerShipDecode.Value)
                        joinquery = joinquery.Where(x => x.IsStrategicPartnerShipDecode == decodeModel.IsStrategicPartnerShipDecode).AsQueryable();

                }
                if (decodeModel.IsBdCoreBusinessDecode != null)
                {
                    if (decodeModel.IsBdCoreBusinessDecode.Value)
                        joinquery = joinquery.Where(x => (x.Valuation ?? 0) +(x.Capex ?? 0) +(x.PotentialRevenuePerYear ?? 0) >= 500000000).AsQueryable();

                }
                if (decodeModel.IsGreenBusinessDecode != null)
                {
                    if (decodeModel.IsGreenBusinessDecode.Value)
                        joinquery = joinquery.Where(x => x.IsGreenBusinessDecode == decodeModel.IsGreenBusinessDecode).AsQueryable();
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

                    joinquery = joinquery.Where(x => entitasPertaminas.Contains(x.EntitasPertaminaId.Value)).AsQueryable();
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

                    joinquery = joinquery.Where(x => streamBusinesss.Contains(x.StreamBusinessId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.JenisPerjanjianDecode))
                {
                    string[] strJenisPerjanjians = decodeModel.JenisPerjanjianDecode.Split('_');

                    List<Guid> jenisPerjanjians = new List<Guid>();
                    foreach (var str in strJenisPerjanjians)
                    {
                        Guid newGuid = Guid.Parse(str);
                        jenisPerjanjians.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => jenisPerjanjians.Contains(x.JenisPerjanjianId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.NegaraMitraDecode) && string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode))
                {
                    string[] strNegaraMitras = decodeModel.NegaraMitraDecode.Split('_');

                    List<Guid> negaraMitras = new List<Guid>();
                    foreach (var str in strNegaraMitras)
                    {
                        Guid newGuid = Guid.Parse(str);
                        negaraMitras.Add(newGuid);
                    }
                    joinquery = QueryList(query);
                    if (filterExcludedExpired !=null)
                    {
                        joinquery= joinquery.Where(x => x.StatusBerlakuId != filterExcludedExpired.Id);
                    }
                    joinquery = joinquery.Where(x => negaraMitras.Contains(x.NegaraMitraId.Value)).AsQueryable();
                }
                if (!string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode) && string.IsNullOrEmpty(decodeModel.NegaraMitraDecode))
                {
                    string[] strStatusBerlakus = decodeModel.StatusBerlakuDecode.Split('_');

                    List<Guid> statusBerlakus = new List<Guid>();
                    foreach (var str in strStatusBerlakus)
                    {
                        Guid newGuid = Guid.Parse(str);
                        statusBerlakus.Add(newGuid);
                    }
                    joinquery = QueryList(query);
                    if (filterExcludedNegaraMitra.Count > 0)
                    {
                        foreach (var filter in filterExcludedNegaraMitra)
                        {
                            joinquery= joinquery.Where(x => x.NegaraMitraId != filter.NegaraMitraId);
                        }
                    }
                    joinquery = joinquery.Where(x => statusBerlakus.Contains(x.StatusBerlakuId.Value)).AsQueryable();
                }
                if (!string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode) && !string.IsNullOrEmpty(decodeModel.NegaraMitraDecode))
                {
                    joinquery = QueryList(query);
                    //filter status berlaku
                    string[] strStatusBerlakus = decodeModel.StatusBerlakuDecode.Split('_');

                    List<Guid> statusBerlakus = new List<Guid>();
                    foreach (var str in strStatusBerlakus)
                    {
                        Guid newGuid = Guid.Parse(str);
                        statusBerlakus.Add(newGuid);
                    }
                    joinquery = joinquery.Where(x => statusBerlakus.Contains(x.StatusBerlakuId.Value)).AsQueryable();

                    //filter negara mitra
                    string[] strNegaraMitras = decodeModel.NegaraMitraDecode.Split('_');

                    List<Guid> negaraMitras = new List<Guid>();
                    foreach (var str in strNegaraMitras)
                    {
                        Guid newGuid = Guid.Parse(str);
                        negaraMitras.Add(newGuid);
                    }
                    joinquery = joinquery.Where(x => negaraMitras.Contains(x.NegaraMitraId.Value)).AsQueryable();
                }
                if (!string.IsNullOrEmpty(decodeModel.KawasanMitraDecode))
                {
                    string[] strKawasanMitras = decodeModel.KawasanMitraDecode.Split('_');

                    List<Guid> kawasanMitras = new List<Guid>();
                    foreach (var str in strKawasanMitras)
                    {
                        Guid newGuid = Guid.Parse(str);
                        kawasanMitras.Add(newGuid);
                    }
                   
                    joinquery = joinquery.Where(x => kawasanMitras.Contains(x.KawasanMitraId.Value)).AsQueryable();
                }
                if (!string.IsNullOrEmpty(decodeModel.StatusDiscussionDecode))
                {
                    string[] strStatusDiscussions = decodeModel.StatusDiscussionDecode.Split('_');

                    List<Guid> statusDiscussions = new List<Guid>();
                    foreach (var str in strStatusDiscussions)
                    {
                        Guid newGuid = Guid.Parse(str);
                        statusDiscussions.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => statusDiscussions.Contains(x.DiscussionStatusId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.AgreementHolderDecode))
                {
                    string[] strAgreementHolders = decodeModel.AgreementHolderDecode.Split('_');

                    List<Guid> agreementHolders = new List<Guid>();
                    foreach (var str in strAgreementHolders)
                    {
                        Guid newGuid = Guid.Parse(str);
                        agreementHolders.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => agreementHolders.Contains(x.HshId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.RangeTanggalTtdDecode))
                {
                    if (decodeModel.RangeTanggalTtdDecode.Contains("-"))
                    {
                        string trimRangeTanggalTtd = decodeModel.RangeTanggalTtdDecode.Replace(" ", "").Trim();

                        string[] arrayRangeTanggalTtd = trimRangeTanggalTtd.Split("-");

                        DateTime start = DateTime.ParseExact(arrayRangeTanggalTtd[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        DateTime end = DateTime.ParseExact(arrayRangeTanggalTtd[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        joinquery = joinquery.Where(x => x.TanggalTtd.Value.Date >= start && x.TanggalTtd.Value.Date <= end).AsQueryable();
                    }
                }

                if (!string.IsNullOrEmpty(decodeModel.RangeTanggalBerakhirDecode))
                {
                    if (decodeModel.RangeTanggalBerakhirDecode.Contains("-"))
                    {
                        string trimRangeTanggalBerakhir = decodeModel.RangeTanggalBerakhirDecode.Replace(" ", "").Trim();

                        string[] arrayRangeTanggalBerakhir = trimRangeTanggalBerakhir.Split("-");

                        DateTime start = DateTime.ParseExact(arrayRangeTanggalBerakhir[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        DateTime end = DateTime.ParseExact(arrayRangeTanggalBerakhir[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        joinquery = joinquery.Where(x => x.TanggalBerakhir.Value.Date >= start && x.TanggalBerakhir.Value.Date <= end).AsQueryable();
                    }
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

                joinquery = joinquery.GroupBy(x => x.Id, (a, b) => new AgreementViewModel
                {
                    Id = a
                }).Join(_context.TbltAgreements, x => x.Id, y => y.Id, (x, y) => new AgreementViewModel
                {
                    Id = x.Id,
                    IsDraft = y.IsDraft,
                    JudulPerjanjian = y.JudulPerjanjian,
                    KodeAgreement = y.KodeAgreement,
                    CreateDate = y.CreateDate,
                    NilaiProyek = y.NilaiProyek,
                    Scope = y.Scope,
                    Progress = y.Progress,
                    DeskripsiKendala = y.DeskripsiKendala,
                    SupportPemerintah = y.SupportPemerintah,
                    PotensiEskalasi = y.PotensiEskalasi,
                    CatatanTambahan = y.CatatanTambahan
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

                var list = await PaginatedList<AgreementViewModel, AgreementViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<AgreementViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<AgreementViewModel>>(false, ex.Message);
            }
        }
        public IQueryable<AgreementViewModel> GetAgreementWithFilter(AgreementViewModel decodeModel)
        {
            IQueryable<AgreementViewModel> result = null;
            var query = _context.TbltAgreements.AsQueryable();

            query = query.Where(x => x.DeletedDate == null && x.IsDraft == false);

            IQueryable<AgreementViewModel> joinquery = QueryList(query);

            //filtering excluded
            var filterExcludedNegaraMitra = _context.TblmNegaraMitraExcludeds.Where(x => x.DeletedDate == null).ToList();
            if (filterExcludedNegaraMitra.Any())
            {
                foreach (var filter in filterExcludedNegaraMitra)
                {
                    joinquery= joinquery.Where(x => x.NegaraMitraId != filter.NegaraMitraId);
                }
            }
            var filterExcludedExpired = _context.TblmStatusBerlakus.Where(x => x.DeletedDate == null && x.IsExpired == true).FirstOrDefault();
            if (filterExcludedExpired !=null)
            {
                joinquery= joinquery.Where(x => x.StatusBerlakuId != filterExcludedExpired.Id);
            }

            if (decodeModel.IsDraft != null)
            {
                if (decodeModel.IsDraft.Value)
                    joinquery = joinquery.Where(x => x.IsDraft == decodeModel.IsDraft).AsQueryable();
            }
            if (decodeModel.IsGtg == true)
            {
                if (decodeModel.IsGtg.HasValue)
                    joinquery = joinquery.Where(x => x.IsGtg == decodeModel.IsGtg).AsQueryable();

            }
            if (decodeModel.IsStrategicPartnerShipDecode != null)
            {
                if (decodeModel.IsStrategicPartnerShipDecode.Value)
                    joinquery = joinquery.Where(x => x.IsStrategicPartnerShipDecode == decodeModel.IsStrategicPartnerShipDecode).AsQueryable();

            }
            if (decodeModel.IsBdCoreBusinessDecode != null)
            {
                if (decodeModel.IsBdCoreBusinessDecode.Value)
                    joinquery = joinquery.Where(x => (x.Valuation ?? 0) +(x.Capex ?? 0) +(x.PotentialRevenuePerYear ?? 0) >= 500000000).AsQueryable();

            }
            if (decodeModel.IsGreenBusinessDecode != null)
            {
                if (decodeModel.IsGreenBusinessDecode.Value)
                    joinquery = joinquery.Where(x => x.IsGreenBusinessDecode == decodeModel.IsGreenBusinessDecode).AsQueryable();
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

                joinquery = joinquery.Where(x => entitasPertaminas.Contains(x.EntitasPertaminaId.Value)).AsQueryable();
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

                joinquery = joinquery.Where(x => streamBusinesss.Contains(x.StreamBusinessId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.JenisPerjanjianDecode))
            {
                string[] strJenisPerjanjians = decodeModel.JenisPerjanjianDecode.Split('_');

                List<Guid> jenisPerjanjians = new List<Guid>();
                foreach (var str in strJenisPerjanjians)
                {
                    Guid newGuid = Guid.Parse(str);
                    jenisPerjanjians.Add(newGuid);
                }

                joinquery = joinquery.Where(x => jenisPerjanjians.Contains(x.JenisPerjanjianId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.NegaraMitraDecode) && string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode))
            {
                string[] strNegaraMitras = decodeModel.NegaraMitraDecode.Split('_');

                List<Guid> negaraMitras = new List<Guid>();
                foreach (var str in strNegaraMitras)
                {
                    Guid newGuid = Guid.Parse(str);
                    negaraMitras.Add(newGuid);
                }
                joinquery = QueryList(query);
                if (filterExcludedExpired !=null)
                {
                    joinquery= joinquery.Where(x => x.StatusBerlakuId != filterExcludedExpired.Id);
                }
                joinquery = joinquery.Where(x => negaraMitras.Contains(x.NegaraMitraId.Value)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode) && string.IsNullOrEmpty(decodeModel.NegaraMitraDecode))
            {
                string[] strStatusBerlakus = decodeModel.StatusBerlakuDecode.Split('_');

                List<Guid> statusBerlakus = new List<Guid>();
                foreach (var str in strStatusBerlakus)
                {
                    Guid newGuid = Guid.Parse(str);
                    statusBerlakus.Add(newGuid);
                }
                joinquery = QueryList(query);
                if (filterExcludedNegaraMitra.Count > 0)
                {
                    foreach (var filter in filterExcludedNegaraMitra)
                    {
                        joinquery= joinquery.Where(x => x.NegaraMitraId != filter.NegaraMitraId);
                    }
                }
                joinquery = joinquery.Where(x => statusBerlakus.Contains(x.StatusBerlakuId.Value)).AsQueryable();
            } 
            if (!string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode) && !string.IsNullOrEmpty(decodeModel.NegaraMitraDecode))
            {
                joinquery = QueryList(query);

                string[] strStatusBerlakus = decodeModel.StatusBerlakuDecode.Split('_');
                List<Guid> statusBerlakus = new List<Guid>();
                foreach (var str in strStatusBerlakus)
                {
                    Guid newGuid = Guid.Parse(str);
                    statusBerlakus.Add(newGuid);
                }
                joinquery = joinquery.Where(x => statusBerlakus.Contains(x.StatusBerlakuId.Value)).AsQueryable();

                string[] strNegaraMitras = decodeModel.NegaraMitraDecode.Split('_');
                List<Guid> negaraMitras = new List<Guid>();
                foreach (var str in strNegaraMitras)
                {
                    Guid newGuid = Guid.Parse(str);
                    negaraMitras.Add(newGuid);
                }
                joinquery = joinquery.Where(x => negaraMitras.Contains(x.NegaraMitraId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.KawasanMitraDecode))
            {
                string[] strKawasanMitras = decodeModel.KawasanMitraDecode.Split('_');

                List<Guid> kawasanMitras = new List<Guid>();
                foreach (var str in strKawasanMitras)
                {
                    Guid newGuid = Guid.Parse(str);
                    kawasanMitras.Add(newGuid);
                }

                joinquery = joinquery.Where(x => kawasanMitras.Contains(x.KawasanMitraId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.StatusDiscussionDecode))
            {
                string[] strStatusDiscussions = decodeModel.StatusDiscussionDecode.Split('_');

                List<Guid> statusDiscussions = new List<Guid>();
                foreach (var str in strStatusDiscussions)
                {
                    Guid newGuid = Guid.Parse(str);
                    statusDiscussions.Add(newGuid);
                }

                joinquery = joinquery.Where(x => statusDiscussions.Contains(x.DiscussionStatusId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.AgreementHolderDecode))
            {
                string[] strAgreementHolders = decodeModel.AgreementHolderDecode.Split('_');

                List<Guid> agreementHolders = new List<Guid>();
                foreach (var str in strAgreementHolders)
                {
                    Guid newGuid = Guid.Parse(str);
                    agreementHolders.Add(newGuid);
                }

                joinquery = joinquery.Where(x => agreementHolders.Contains(x.HshId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.RangeTanggalTtdDecode))
            {
                if (decodeModel.RangeTanggalTtdDecode.Contains("-"))
                {
                    string trimRangeTanggalTtd = decodeModel.RangeTanggalTtdDecode.Replace(" ", "").Trim();

                    string[] arrayRangeTanggalTtd = trimRangeTanggalTtd.Split("-");

                    DateTime start = DateTime.ParseExact(arrayRangeTanggalTtd[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime end = DateTime.ParseExact(arrayRangeTanggalTtd[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    joinquery = joinquery.Where(x => x.TanggalTtd.Value.Date >= start && x.TanggalTtd.Value.Date <= end).AsQueryable();
                }
            }

            if (!string.IsNullOrEmpty(decodeModel.RangeTanggalBerakhirDecode))
            {
                if (decodeModel.RangeTanggalBerakhirDecode.Contains("-"))
                {
                    string trimRangeTanggalBerakhir = decodeModel.RangeTanggalBerakhirDecode.Replace(" ", "").Trim();

                    string[] arrayRangeTanggalBerakhir = trimRangeTanggalBerakhir.Split("-");

                    DateTime start = DateTime.ParseExact(arrayRangeTanggalBerakhir[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime end = DateTime.ParseExact(arrayRangeTanggalBerakhir[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    joinquery = joinquery.Where(x => x.TanggalBerakhir.Value.Date >= start && x.TanggalBerakhir.Value.Date <= end).AsQueryable();
                }
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

            result = joinquery;

            return result;
        }
        public IQueryable<AgreementViewModel> GetAgreementGroupByWithFilter(AgreementViewModel decodeModel)
        {
            IQueryable<AgreementViewModel> result = null;
            var query = _context.TbltAgreements.AsQueryable();

            query = query.Where(x => x.DeletedDate == null && x.IsDraft == false);

            IQueryable<AgreementViewModel> joinquery = QueryList(query);

            //filtering excluded
            var filterExcludedNegaraMitra = _context.TblmNegaraMitraExcludeds.Where(x => x.DeletedDate == null).ToList();
            if (filterExcludedNegaraMitra.Any())
            {
                foreach (var filter in filterExcludedNegaraMitra)
                {
                    joinquery= joinquery.Where(x => x.NegaraMitraId != filter.NegaraMitraId);
                }
            }

            var filterExcludedExpired = _context.TblmStatusBerlakus.Where(x => x.DeletedDate == null && x.IsExpired == true).FirstOrDefault();
            if (filterExcludedExpired !=null)
            {
                    joinquery= joinquery.Where(x => x.StatusBerlakuId != filterExcludedExpired.Id);
            }

            if (decodeModel.IsDraft != null)
            {
                if (decodeModel.IsDraft.Value)
                    joinquery = joinquery.Where(x => x.IsDraft == decodeModel.IsDraft).AsQueryable();
            }
            if (decodeModel.IsGtg == true)
            {
                if (decodeModel.IsGtg.HasValue)
                    joinquery = joinquery.Where(x => x.IsGtg == decodeModel.IsGtg).AsQueryable();

            }
            if (decodeModel.IsStrategicPartnerShipDecode != null)
            {
                if (decodeModel.IsStrategicPartnerShipDecode.Value)
                    joinquery = joinquery.Where(x => x.IsStrategicPartnerShipDecode == decodeModel.IsStrategicPartnerShipDecode).AsQueryable();

            }
            if (decodeModel.IsBdCoreBusinessDecode != null)
            {
                if (decodeModel.IsBdCoreBusinessDecode.Value)
                    joinquery = joinquery.Where(x => (x.Valuation ?? 0) +(x.Capex ?? 0) +(x.PotentialRevenuePerYear ?? 0) >= 500000000).AsQueryable();



            }
            if (decodeModel.IsGreenBusinessDecode != null)
            {
                if (decodeModel.IsGreenBusinessDecode.Value)
                    joinquery = joinquery.Where(x => x.IsGreenBusinessDecode == decodeModel.IsGreenBusinessDecode).AsQueryable();
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

                joinquery = joinquery.Where(x => entitasPertaminas.Contains(x.EntitasPertaminaId.Value)).AsQueryable();
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

                joinquery = joinquery.Where(x => streamBusinesss.Contains(x.StreamBusinessId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.JenisPerjanjianDecode))
            {
                string[] strJenisPerjanjians = decodeModel.JenisPerjanjianDecode.Split('_');

                List<Guid> jenisPerjanjians = new List<Guid>();
                foreach (var str in strJenisPerjanjians)
                {
                    Guid newGuid = Guid.Parse(str);
                    jenisPerjanjians.Add(newGuid);
                }

                joinquery = joinquery.Where(x => jenisPerjanjians.Contains(x.JenisPerjanjianId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.NegaraMitraDecode) &&string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode))
            {
                string[] strNegaraMitras = decodeModel.NegaraMitraDecode.Split('_');

                List<Guid> negaraMitras = new List<Guid>();
                foreach (var str in strNegaraMitras)
                {
                    Guid newGuid = Guid.Parse(str);
                    negaraMitras.Add(newGuid);
                }
                joinquery = QueryList(query);
                if (filterExcludedExpired !=null)
                {
                    joinquery= joinquery.Where(x => x.StatusBerlakuId != filterExcludedExpired.Id);
                }
                joinquery = joinquery.Where(x => negaraMitras.Contains(x.NegaraMitraId.Value)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode) && string.IsNullOrEmpty(decodeModel.NegaraMitraDecode))
            {
                string[] strStatusBerlakus = decodeModel.StatusBerlakuDecode.Split('_');

                List<Guid> statusBerlakus = new List<Guid>();
                foreach (var str in strStatusBerlakus)
                {
                    Guid newGuid = Guid.Parse(str);
                    statusBerlakus.Add(newGuid);
                }
                joinquery = QueryList(query);
                if (filterExcludedNegaraMitra.Count > 0)
                {
                    foreach (var filter in filterExcludedNegaraMitra)
                    {
                        joinquery= joinquery.Where(x => x.NegaraMitraId != filter.NegaraMitraId);
                    }
                }
                joinquery = joinquery.Where(x => statusBerlakus.Contains(x.StatusBerlakuId.Value)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(decodeModel.StatusBerlakuDecode) && !string.IsNullOrEmpty(decodeModel.NegaraMitraDecode))
            {
                joinquery = QueryList(query);

                string[] strStatusBerlakus = decodeModel.StatusBerlakuDecode.Split('_');
                List<Guid> statusBerlakus = new List<Guid>();
                foreach (var str in strStatusBerlakus)
                {
                    Guid newGuid = Guid.Parse(str);
                    statusBerlakus.Add(newGuid);
                }

                joinquery = joinquery.Where(x => statusBerlakus.Contains(x.StatusBerlakuId.Value)).AsQueryable();

                string[] strNegaraMitras = decodeModel.NegaraMitraDecode.Split('_');
                List<Guid> negaraMitras = new List<Guid>();
                foreach (var str in strNegaraMitras)
                {
                    Guid newGuid = Guid.Parse(str);
                    negaraMitras.Add(newGuid);
                }
          
                joinquery = joinquery.Where(x => negaraMitras.Contains(x.NegaraMitraId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.KawasanMitraDecode))
            {
                string[] strKawasanMitras = decodeModel.KawasanMitraDecode.Split('_');

                List<Guid> kawasanMitras = new List<Guid>();
                foreach (var str in strKawasanMitras)
                {
                    Guid newGuid = Guid.Parse(str);
                    kawasanMitras.Add(newGuid);
                }

                joinquery = joinquery.Where(x => kawasanMitras.Contains(x.KawasanMitraId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.StatusDiscussionDecode))
            {
                string[] strStatusDiscussions = decodeModel.StatusDiscussionDecode.Split('_');

                List<Guid> statusDiscussions = new List<Guid>();
                foreach (var str in strStatusDiscussions)
                {
                    Guid newGuid = Guid.Parse(str);
                    statusDiscussions.Add(newGuid);
                }
                joinquery = QueryList(query);
                joinquery = joinquery.Where(x => statusDiscussions.Contains(x.DiscussionStatusId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.AgreementHolderDecode))
            {
                string[] strAgreementHolders = decodeModel.AgreementHolderDecode.Split('_');

                List<Guid> agreementHolders = new List<Guid>();
                foreach (var str in strAgreementHolders)
                {
                    Guid newGuid = Guid.Parse(str);
                    agreementHolders.Add(newGuid);
                }

                joinquery = joinquery.Where(x => agreementHolders.Contains(x.HshId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.RangeTanggalTtdDecode))
            {
                if (decodeModel.RangeTanggalTtdDecode.Contains("-"))
                {
                    string trimRangeTanggalTtd = decodeModel.RangeTanggalTtdDecode.Replace(" ", "").Trim();

                    string[] arrayRangeTanggalTtd = trimRangeTanggalTtd.Split("-");

                    DateTime start = DateTime.ParseExact(arrayRangeTanggalTtd[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime end = DateTime.ParseExact(arrayRangeTanggalTtd[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    joinquery = joinquery.Where(x => x.TanggalTtd.Value.Date >= start && x.TanggalTtd.Value.Date <= end).AsQueryable();
                }
            }

            if (!string.IsNullOrEmpty(decodeModel.RangeTanggalBerakhirDecode))
            {
                if (decodeModel.RangeTanggalBerakhirDecode.Contains("-"))
                {
                    string trimRangeTanggalBerakhir = decodeModel.RangeTanggalBerakhirDecode.Replace(" ", "").Trim();

                    string[] arrayRangeTanggalBerakhir = trimRangeTanggalBerakhir.Split("-");

                    DateTime start = DateTime.ParseExact(arrayRangeTanggalBerakhir[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    DateTime end = DateTime.ParseExact(arrayRangeTanggalBerakhir[1], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    joinquery = joinquery.Where(x => x.TanggalBerakhir.Value.Date >= start && x.TanggalBerakhir.Value.Date <= end).AsQueryable();
                }
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

            joinquery = joinquery.GroupBy(x => x.Id, (a, b) => new AgreementViewModel
            {
                Id = a
            }).Join(_context.TbltAgreements, x => x.Id, y => y.Id, (x, y) => new AgreementViewModel
            {
                Id = x.Id,
                IsDraft = y.IsDraft,
                IsGtg =y.IsGtg,
                JudulPerjanjian = y.JudulPerjanjian,
                KodeAgreement = y.KodeAgreement,
                NilaiProyek = y.NilaiProyek,
                Scope = y.Scope,
                Progress = y.Progress,
                DeskripsiKendala = y.DeskripsiKendala,
                SupportPemerintah = y.SupportPemerintah,
                PotensiEskalasi = y.PotensiEskalasi,
                CatatanTambahan = y.CatatanTambahan,
                CreateDate = y.CreateDate
            });

            result = joinquery;

            return result;
        }
        public bool? GetStatusDraft(Guid id)
        {
            bool? result = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.Id == id).Select(x => x.IsDraft).FirstOrDefault();

            return result;
        }
        public string GetJudulPerjanjian(Guid id)
        {
            string result = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.Id == id).Select(x => x.JudulPerjanjian).FirstOrDefault();

            return result;
        }
        public List<AgreementPartnerViewModel> GetPartners(Guid id)
        {
            IQueryable<TbltAgreementPartner> result = _context.TbltAgreementPartners.Where(x => x.DeletedDate == null && x.AgreementId == id).OrderBy(x => x.PartnerName).AsQueryable();

            return _mapper.Map<List<AgreementPartnerViewModel>>(result);
        }
        public List<AgreementEntitasPertaminaViewModel> GetEntitasPertamina(Guid id)
        {
            IQueryable<AgreementEntitasPertaminaViewModel> result = _context.TbltAgreementEntitasPertaminas
                .Where(x => x.DeletedDate == null && x.AgreementId == id)
                .Join(_context.TblmEntitasPertaminas, x => x.EntitasPertaminaId, y => y.Id, (x, y) =>
                      new AgreementEntitasPertaminaViewModel
                      {
                          Id = x.Id,
                          CompanyName = y.CompanyName,
                          CreateDate = x.CreateDate
                      })
                .OrderBy(x => x.CreateDate).AsQueryable();

            return result.ToList();
        }
        public List<AgreementStreamBusinessViewModel> GetStreamBusiness(Guid id)
        {
            IQueryable<AgreementStreamBusinessViewModel> result = _context.TbltAgreementStreamBusinesses
                .Where(x => x.DeletedDate == null && x.AgreementId == id)
                .Join(_context.TblmStreamBusinesses, x => x.StreamBusinessId, y => y.Id, (x, y) =>
                     new AgreementStreamBusinessViewModel
                     {
                         Id = x.Id,
                         QueryStreamBusinessName = y.Name,
                         CreateDate = x.CreateDate
                     })
                .OrderBy(x => x.CreateDate)
                .AsQueryable();

            return result.ToList();
        }
        public string GetJenisPerjanjian(Guid id)
        {
            string result = _context.TbltAgreements
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmJenisPerjanjians, x => x.JenisPerjanjianId, y => y.Id, (x, y) =>
                    new AgreementJenisPerjanjian
                    {
                        Id = y.Id,
                        AgreementId = x.Id,
                        CreateDate = y.CreateDate,
                        Name = y.Name,
                    })
                .Select(x => x.Name).FirstOrDefault();

            return result;
        }
        public List<AgreementNegaraMitraViewModel> GetNamaNegara(Guid id)
        {
            IQueryable<AgreementNegaraMitraViewModel> result = _context.TbltAgreementNegaraMitras
                .Where(x => x.DeletedDate == null && x.AgreementId == id)
                .Join(_context.TblmNegaraMitras
                .Join(_context.TblmKawasanMitras, x => x.KawasanMitraId, y => y.Id, (x, y) =>
                           new AgreementNegaraMitraViewModel
                           {
                               Id = x.Id,
                               NamaNegara = x.NamaNegara,
                               KawasanMitraId = y.Id,
                               NamaKawasan = y.NamaKawasan,
                               CreateDate = x.CreateDate
                           }), x => x.NegaraMitraId, y => y.Id, (x, y) =>
                           new AgreementNegaraMitraViewModel
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
        public string GetStatusBerlaku(Guid id)
        {
            string result = _context.TbltAgreements
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmStatusBerlakus, x => x.StatusBerlakuId, y => y.Id, (x, y) =>
                    new StatusBerlakuViewModel
                    {
                        Id = y.Id,
                        StatusName = y.StatusName,
                        CreateDate = y.CreateDate,
                    })
                .Select(x => x.StatusName).FirstOrDefault();

            return result;
        }
        public string GetStatusBerlakuColorHexa(Guid id)
        {
            string result = _context.TbltAgreements
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmStatusBerlakus, x => x.StatusBerlakuId, y => y.Id, (x, y) =>
                    new StatusBerlakuViewModel
                    {
                        Id = y.Id,
                        StatusName = y.StatusName,
                        ColorHexa = y.ColorHexa,
                        CreateDate = y.CreateDate,
                    })
                .Select(x => x.ColorHexa).FirstOrDefault();

            return result;
        }
        public string GetStatusBerlakuColorName(Guid id)
        {
            string result = _context.TbltAgreements
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmStatusBerlakus, x => x.StatusBerlakuId, y => y.Id, (x, y) =>
                    new StatusBerlakuViewModel
                    {
                        Id = y.Id,
                        StatusName = y.StatusName,
                        ColorHexa = y.ColorHexa,
                        ColorName = y.ColorName,
                        CreateDate = y.CreateDate,
                    })
                .Select(x => x.ColorName).FirstOrDefault();

            return result;
        }
        public string GetDiscussionStatus(Guid id)
        {
            string result = _context.TbltAgreements
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmDiscussionStatuses, x => x.DiscussionStatusId, y => y.Id, (x, y) =>
                    new StatusDiscussionViewModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                        CreateDate = y.CreateDate,
                    })
                .Select(x => x.Name).FirstOrDefault();

            return result;
        }
        public string GetDiscussionStatusColorHexa(Guid id)
        {
            string result = _context.TbltAgreements
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmDiscussionStatuses, x => x.DiscussionStatusId, y => y.Id, (x, y) =>
                    new StatusDiscussionViewModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                        ColorHexa = y.ColorHexa,
                        CreateDate = y.CreateDate,
                    })
                .Select(x => x.ColorHexa).FirstOrDefault();

            return result;
        }
        public string GetDiscussionStatusColorName(Guid id)
        {
            string result = _context.TbltAgreements
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmDiscussionStatuses, x => x.DiscussionStatusId, y => y.Id, (x, y) =>
                    new StatusDiscussionViewModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                        ColorHexa = y.ColorHexa,
                        ColorName = y.ColorName,
                        CreateDate = y.CreateDate,
                    })
                .Select(x => x.ColorName).FirstOrDefault();

            return result;
        }
        #endregion

        public void Save(AgreementViewModel model)
        {
            var newEntity = _mapper.Map<TbltAgreement>(model);

            _context.Set<TbltAgreement>().Add(newEntity);
            _context.SaveChanges();
        }
        public void SavePic(AgreementPicFungsiViewModel model)
        {
            var newEntity = _mapper.Map<TbltAgreementPicFungsi>(model);
            _context.Set<TbltAgreementPicFungsi>().Add(newEntity);
            _context.SaveChanges();
        }
        public void SaveAddendum(AgreementAddendumViewModel model)
        {
            var newEntity = _mapper.Map<TbltAgreementAddendum>(model);
            _context.Set<TbltAgreementAddendum>().Add(newEntity);
            _context.SaveChanges();
        }
        public void SaveEntitasPertamina(AgreementEntitasPertaminaViewModel model)
        {
            _context.Set<TbltAgreementEntitasPertamina>().Add(_mapper.Map<TbltAgreementEntitasPertamina>(model));
            _context.SaveChanges();
        }
        public void SaveNegaraMitra(AgreementNegaraMitraViewModel model)
        {
            _context.Set<TbltAgreementNegaraMitra>().Add(_mapper.Map<TbltAgreementNegaraMitra>(model));
            _context.SaveChanges();
        }
        public void SaveHubHead(AgreementHubHeadViewModel model)
        {
            _context.Set<TbltAgreementHubHead>().Add(_mapper.Map<TbltAgreementHubHead>(model));
            _context.SaveChanges();

        }

        public void SaveLembaga(AgreementKementrianLembagaViewModel model)
        {
            _context.Set<TbltAgreementKementrianLembaga>().Add(_mapper.Map<TbltAgreementKementrianLembaga>(model));
            _context.SaveChanges();
        }
        public void SaveStreamBusiness(AgreementStreamBusinessViewModel model)
        {
            _context.Set<TbltAgreementStreamBusiness>().Add(_mapper.Map<TbltAgreementStreamBusiness>(model));
            _context.SaveChanges();
        }
        public void SaveToPartner(AgreementPartnerViewModel model)
        {
            _context.Set<TbltAgreementPartner>().Add(_mapper.Map<TbltAgreementPartner>(model));
            _context.SaveChanges();
        }
        public void SaveToLokasiProyek(AgreementLokasiProyekViewModel model)
        {
            _context.Set<TbltAgreementLokasiProyek>().Add(_mapper.Map<TbltAgreementLokasiProyek>(model));
            _context.SaveChanges();
        }

        public async Task<AgreementViewModel> GetAgreementAsyncById(Guid guid)
        {
            AgreementViewModel result = await Task.FromResult(_mapper.Map<AgreementViewModel>(_context.TbltAgreements.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }

        public decimal ParseRevenue(string revenueToParse)
        {
            string cleanedValue = revenueToParse.Replace(".", "");

            if (decimal.TryParse(cleanedValue, out decimal parsedRevenue))
            {
                return parsedRevenue;
            }

            throw new ArgumentException("Invalid revenue format");
        }
        public AgreementViewModel GetAgreementById(Guid guid)
        {
            AgreementViewModel result = _mapper.Map<AgreementViewModel>(_context.TbltAgreements.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            result.PotentialRevenuePerYear = ((int?)result.PotentialRevenuePerYear);
            //result.NilaiProyek =((int?)result.NilaiProyek);
            result.Capex =((int?)result.Capex);
            return result;
        }
        public AgreementViewModel GetAgreementByIdCrud(Guid guid)
        {
            AgreementViewModel result = _mapper.Map<AgreementViewModel>(_context.TbltAgreements.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            
            return result;
        }
        public async Task<StatusBerlakuViewModel> StatusBerlakuCounting(int? sequence)
        {
            StatusBerlakuViewModel result = await Task.FromResult(_mapper.Map<StatusBerlakuViewModel>(_context.TblmStatusBerlakus.Where(x => x.RelationSequence == sequence && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }
        public StatusBerlakuViewModel GetStatusBerlakuName(Guid? guid)
        {
            StatusBerlakuViewModel result = _mapper.Map<StatusBerlakuViewModel>(_context.TblmStatusBerlakus.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public StreamBusinessViewModel GetStreamBusinessById(Guid guid)
        {
            StreamBusinessViewModel result = _mapper.Map<StreamBusinessViewModel>(_context.TblmStreamBusinesses.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public AgreementEntitasPertaminaViewModel GetAgreementEntitasById(Guid guid)
        {
            AgreementEntitasPertaminaViewModel result = _mapper.Map<AgreementEntitasPertaminaViewModel>(_context.TbltAgreementEntitasPertaminas.Where(x => x.AgreementId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public AgreementStreamBusinessViewModel GetAgreementStreamBusinessById(Guid guid)
        {
            AgreementStreamBusinessViewModel result = _mapper.Map<AgreementStreamBusinessViewModel>(_context.TbltAgreementStreamBusinesses.Where(x => x.AgreementId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public AgreementNegaraMitraViewModel GetAgreementNegaraMitraById(Guid guid)
        {
            AgreementNegaraMitraViewModel result = _mapper.Map<AgreementNegaraMitraViewModel>(_context.TbltAgreementNegaraMitras.Where(x => x.AgreementId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public AgreementKementrianLembagaViewModel GetAgreementLembagaById(Guid guid)
        {
            AgreementKementrianLembagaViewModel result = _mapper.Map<AgreementKementrianLembagaViewModel>(_context.TbltAgreementKementrianLembagas.Where(x => x.AgreementId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public AgreementHubHeadViewModel GetAgreementHubHeadById(Guid guid)
        {
            AgreementHubHeadViewModel result = _mapper.Map<AgreementHubHeadViewModel>(_context.TbltAgreementHubHeads.Where(x => x.AgreementId == guid && x.DeletedDate == null).FirstOrDefault());
            HubHeadViewModel recordHubHead = _mapper.Map<HubHeadViewModel>(_context.TblmHubHeads.Where(x =>x.Id == result.HubHeadId && x.DeletedDate == null).FirstOrDefault());
            result.HubHeadName = recordHubHead.Name;
            return result;
        }  
        public AgreementPicFungsiViewModel GetAgreementPicFungsiById(Guid guid)
        {
            AgreementPicFungsiViewModel result = _mapper.Map<AgreementPicFungsiViewModel>(_context.TbltAgreementPicFungsis.Where(x => x.AgreementId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        } 
        public AgreementPicFungsiViewModel GetAgreementPicFungsiLeadById(Guid guid, Guid?  picLeadId)
        {
            AgreementPicFungsiViewModel result = _mapper.Map<AgreementPicFungsiViewModel>(_context.TbltAgreementPicFungsis.Where(x => x.AgreementId == guid && x.DeletedDate == null && x.PicLevelId == picLeadId).FirstOrDefault());
            return result;
        }
        public List<AgreementPicFungsiViewModel> GetAgreementPicFungsisById(Guid guid, Guid? picMemberId)
        {
            List<AgreementPicFungsiViewModel> result = _mapper.Map<List<AgreementPicFungsiViewModel>>(_context.TbltAgreementPicFungsis.Where(x => x.AgreementId == guid && x.DeletedDate == null && x.PicLevelId == picMemberId).ToList());
            return result;
        }
        public List<AgreementAddendumViewModel> GetAgreementAddendumById(Guid guid)
        {
            var addendums = _mapper.Map<List<AgreementAddendumViewModel>>(_context.TbltAgreementAddenda.Where(x => x.AgreementId == guid && x.DeletedDate == null).OrderBy(x => x.Sequence).ToList());
            return addendums;
        }
        public AgreementAddendumViewModel GetAddendumLastById(Guid guid)
        {
            var addendum = _mapper.Map<AgreementAddendumViewModel>(_context.TbltAgreementAddenda.Where(x => x.AgreementId == guid && x.DeletedDate == null).OrderByDescending(x => x.Sequence).FirstOrDefault());
            return addendum;
        }
        public int? GetSequenceAgreementAddendumById(Guid guid)
        {
            AgreementAddendumViewModel result = _mapper.Map<AgreementAddendumViewModel>(_context.TbltAgreementAddenda.Where(x=>x.AgreementId == guid && x.DeletedDate == null).OrderByDescending(x=>x.Sequence).FirstOrDefault());
            if (result == null)
            {
                int? sequenceResult = 0;
                return sequenceResult;
            }
            else
            {
                int? sequenceResult = result.Sequence;
                return sequenceResult;
            }
           
            
        }
        public List<AgreementLokasiProyekViewModel> GetAgreementLokasiById(Guid guid)
        {
            List<AgreementLokasiProyekViewModel> result = _mapper.Map<List<AgreementLokasiProyekViewModel>>(_context.TbltAgreementLokasiProyeks.Where(x => x.AgreementId == guid && x.DeletedDate == null).ToList().OrderBy(x => x.LokasiProyek)); ;
            return result;
        }
        public List<AgreementPartnerViewModel> GetAgreementPartnerById(Guid guid)
        {
            List<AgreementPartnerViewModel> result = _mapper.Map<List<AgreementPartnerViewModel>>(_context.TbltAgreementPartners.Where(x => x.AgreementId == guid && x.DeletedDate == null).ToList().OrderBy(x => x.PartnerName));
            return result;
        }
        public List<AgreementKementrianLembagaViewModel> GetAgreementKementrianLembagaById(Guid guid)
        {
            List<AgreementKementrianLembagaViewModel> result = _mapper.Map<List<AgreementKementrianLembagaViewModel>>(_context.TbltAgreementKementrianLembagas.Where(x => x.AgreementId == guid && x.DeletedDate == null).ToList());
            return result;
        }
        public async Task<InitiativeViewModel> GetInitiativeById(Guid guid)
        {
            InitiativeViewModel result = await Task.FromResult(_mapper.Map<InitiativeViewModel>(_context.TbltInitiatives.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }
        public async Task<OpportunityViewModel> GetOpportunityById(Guid guid)
        {
            OpportunityViewModel result = await Task.FromResult(_mapper.Map<OpportunityViewModel>(_context.TbltOpportunities.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }

        public void Update(AgreementViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TbltAgreement record = HasRecordById(model.Id);
            record.JudulPerjanjian = model.JudulPerjanjian;
            record.JenisPerjanjianId = model.JenisPerjanjianId;
            record.Valuation = model.Valuation;
            record.TanggalTtd = model.TanggalTtd;
            record.TanggalBerakhir = model.TanggalBerakhir;
            record.StatusBerlakuId = model.StatusBerlakuId;
            record.Scope = model.Scope;
            record.Progress = model.Progress;
            record.FaktorKendalaId = model.FaktorKendalaId;
            record.KlasifikasiKendalaId = model.KlasifikasiKendalaId;
            record.DeskripsiKendala = model.DeskripsiKendala;
            record.TindakLanjut = model.TindakLanjut;
            record.SupportPemerintah = model.SupportPemerintah;
            record.PotensiEskalasi = model.PotensiEskalasi;
            record.CatatanTambahan = model.CatatanTambahan;
            record.RelatedAgreementId = model.RelatedAgreementId;
            record.DiscussionStatusId = model.DiscussionStatusId;
            record.IsDraft = false;
            record.UpdateBy = userName;
            record.UpdateDate = now;
            
            if(model.AgreementAddendums != null)
            {
                if (model.AgreementAddendums.Count <0)
                {
                    record.IsAddendum = false;
                }
                else
                {
                    record.IsAddendum = true;
                }
            }
            
            record.IsGtg = model.IsGtg;
            record.Capex = model.Capex;
            record.PotentialRevenuePerYear = model.PotentialRevenuePerYear;
            record.KodeAgreement = model.KodeAgreement;

            if (model.TrafficLightId.HasValue)
            {
                var findTrafficLight = _context.TblmTrafficLights.Any(x=>x.Id == model.TrafficLightId);
                if (!findTrafficLight)
                {
                    model.IsError = true;
                    model.ErrorMessage += "Traffic Light Not Provide in Database";
                }
                record.TrafficLightId = model.TrafficLightId;
            }
            else
            {
                record.TrafficLightId = null;
            }
            //var getTraffic = GetTrafficLightById(model.TrafficLightId);
            
            _context.Set<TbltAgreement>().Update(record);
            _context.SaveChanges();

        }
        public TbltAgreement HasRecordById(Guid guid)
        {
            TbltAgreement result = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.Id == guid).FirstOrDefault();
            return result;
        } 
        public TbltAgreement GetTrafficLightById(Guid guid)
        {
            TbltAgreement result = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.Id == guid).FirstOrDefault();
            return result;
        }
        public void DeleteExistingStreamBusiness(Guid guid)
        {
            var entitas = _context.TbltAgreementStreamBusinesses.Where(x => x.AgreementId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingFungsiPic(Guid guid)
        {
            var entitas = _context.TbltAgreementPicFungsis.Where(x => x.AgreementId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public bool CheckedFungsiPicIdByAgreementId(Guid guid, Guid? fungsiPicId)
        {
            bool result = _context.TbltAgreementPicFungsis.Any(x => x.PicFungsiId == fungsiPicId && x.AgreementId == guid && x.DeletedDate == null);
            return result;
        }
        public void DeleteExistingEntitasPertamina(Guid guid)
        {
            var entitas = _context.TbltAgreementEntitasPertaminas.Where(x => x.AgreementId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingNegaraMitra(Guid guid)
        {
            var entitas = _context.TbltAgreementNegaraMitras.Where(x => x.AgreementId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingLembaga(Guid guid)
        {
            var entitas = _context.TbltAgreementKementrianLembagas.Where(x => x.AgreementId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingPartner(Guid guid)
        {
            var entitas = _context.TbltAgreementPartners.Where(x => x.AgreementId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingLokasiProyek(Guid guid)
        {
            var entitas = _context.TbltAgreementLokasiProyeks.Where(x => x.AgreementId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }

        #region Read Method
        public AgreementJenisPerjanjian GetJenisPerjanjianById(Guid guid)
        {
            AgreementJenisPerjanjian result = _mapper.Map<AgreementJenisPerjanjian>(_context.TbltAgreements.Where(x => x.Id == guid)
                .Join(_context.TblmJenisPerjanjians, x => x.JenisPerjanjianId, y => y.Id,
                (x, y) => new AgreementJenisPerjanjian { Id = x.JenisPerjanjianId, Name = y.Name, })
                .FirstOrDefault());
            return result;
        }
        public IEnumerable<PicFungsiViewModel> GetPicFungsiById(Guid guid,Guid? levelId)
        {
            IEnumerable<PicFungsiViewModel> result = _context.TbltAgreementPicFungsis
                .Where(x => x.AgreementId == guid && x.PicLevelId == levelId)
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
                        NameFungsi = y.NameFungsi
                    });
                
            return result;
        }
        public List<EntitasPertaminaViewModel> GetEntitasPertaminaById(Guid guid)
        {
            List<EntitasPertaminaViewModel> result = _context.TbltAgreementEntitasPertaminas
                .Where(x => x.AgreementId == guid)
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
        public StatusBerlakuViewModel GetStatusBerlakuById(Guid guid)
        {
            StatusBerlakuViewModel result = _context.TbltAgreements.Where(x => x.Id == guid)
                .Join(_context.TblmStatusBerlakus, x => x.StatusBerlakuId, y => y.Id,
                (x, y) =>
                new StatusBerlakuViewModel
                {
                    Id = x.StatusBerlakuId.Value,
                    StatusName = y.StatusName,
                    ColorHexa = y.ColorHexa,
                    ColorName = y.ColorName
                })
                .FirstOrDefault();
            return result;
        }
        public StatusDiscussionViewModel GetStatusDiscussionById(Guid guid)
        {
            StatusDiscussionViewModel result = _context.TbltAgreements.Where(x => x.Id == guid)
                .Join(_context.TblmDiscussionStatuses, x => x.DiscussionStatusId, y => y.Id,
                (x, y) =>
                new StatusDiscussionViewModel
                {
                    Id = x.DiscussionStatusId.Value,
                    Name = y.Name,
                    ColorHexa = y.ColorHexa,
                    ColorName = y.ColorName
                })
                .FirstOrDefault();
            return result;
        }
        public List<string> GetPartnerById(Guid guid)
        {
            List<string> result = _context.TbltAgreementPartners.Where(x => x.AgreementId == guid).Select(x => x.PartnerName).ToList();

            return result;
        }
        public List<NegaraMitraViewModel> GetNegaraMitraById(Guid guid)
        {
            List<NegaraMitraViewModel> result = _context.TbltAgreementNegaraMitras
                .Where(x => x.AgreementId == guid)
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
        public List<string> GetLokasiProyekById(Guid guid)
        {
            List<string> result = _context.TbltAgreementLokasiProyeks.Where(x => x.AgreementId == guid).Select(x => x.LokasiProyek).ToList();

            return result;
        }
        public List<StreamBusinessViewModel> GetAllStreamBusinessById(Guid guid)
        {
            List<StreamBusinessViewModel> result = _context.TbltAgreementStreamBusinesses
                .Where(x => x.AgreementId == guid)
                .Join(_context.TblmStreamBusinesses, x => x.StreamBusinessId, y => y.Id, (x, y) =>
                    new StreamBusinessViewModel
                    {
                        Id = x.StreamBusinessId.Value,
                        Name = y.Name
                    })
                .ToList();
            return result;
        }
        public FaktorKendalaViewModel GetFaktorKendalaById(Guid guid)
        {
            FaktorKendalaViewModel result = _context.TbltAgreements.Where(x => x.Id == guid)
                .Join(_context.TblmFaktorKendalas, x => x.FaktorKendalaId, y => y.Id,
                (x, y) =>
                new FaktorKendalaViewModel
                {
                    Id = x.FaktorKendalaId.Value,
                    Name = y.Name,
                })
                .FirstOrDefault();
            return result;
        }
        public KlasifikasiKendalaViewModel GetKlasifikasiKendalaById(Guid guid)
        {
            KlasifikasiKendalaViewModel result = _context.TbltAgreements.Where(x => x.Id == guid)
                .LeftJoin(_context.TblmKlasifikasiKendalas, x => x.KlasifikasiKendalaId, y => y.Id,
                (x, y) =>
                new KlasifikasiKendalaViewModel
                {
                    Id = x.KlasifikasiKendalaId.Value,
                    Name = y.Name,
                })
                .FirstOrDefault();
            return result;
        }
        public List<string> GetKementrianLembagaById(Guid guid)
        {
            List<string> result = _context.TbltAgreementKementrianLembagas
                .Where(x => x.AgreementId == guid)
                .Join(_context.TblmKementrianLembagas, x => x.KementrianLembagaId, y => y.Id, (x, y) =>
                new AgreementKementrianLembagaViewModel
                {
                    Id = x.Id,
                    KementrianLembagaId = x.KementrianLembagaId,
                    LembagaName = y.LembagaName
                })
                .Select(x => x.LembagaName).ToList();
            return result;
        }
        public AgreementViewModel GetRelatedAgreementById(Guid guid)
        {
            AgreementViewModel result = _context.TbltAgreements.Where(x => x.Id == guid)
                .Join(_context.TbltAgreements, x => x.RelatedAgreementId, y => y.Id,
                (x, y) =>
                new AgreementViewModel
                {
                    Id = y.Id,
                    JudulPerjanjian = y.JudulPerjanjian,
                    KodeAgreement = y.KodeAgreement,
                })
                .FirstOrDefault();
            return result;
        }
        public AgreementViewModel GetTrafficLightReadById(Guid guid)
        {
            AgreementViewModel result = _context.TbltAgreements.Where(x => x.Id == guid)
                .Join(_context.TblmTrafficLights, x => x.TrafficLightId, y => y.Id,
                (x, y) =>
                new AgreementViewModel
                {
                    Id = y.Id,
                    TrafficLightName = y.Name,
                    TrafficLightColor = y.HexColor,
                })
                .FirstOrDefault();
            return result;
        }
        public List<AgreementAddendumViewModel> GetAddendumRomanById(Guid guid)
        {
            List<AgreementAddendumViewModel> result = _context.TbltAgreements.Where(x => x.Id == guid)
                .Join(_context.TbltAgreementAddenda, x => x.Id, y => y.AgreementId,
                (x, y) =>
                new AgreementAddendumViewModel
                {
                    Id = y.Id,
                    AddendumDate = y.AddendumDate,
                    Sequence = y.Sequence,
                }).OrderBy(x=>x.Sequence)
                .ToList();
            return result;
        }
        #endregion

        public IEnumerable<AgreementViewModel> GetAllAgreementExcel()
        {
            IQueryable<AgreementViewModel> items = _context.VwExportAgreements.Select(agreement => new AgreementViewModel
            {
                JudulPerjanjian = agreement.JudulPerjanjian ?? "",
                NilaiProyek = agreement.NilaiProyek.HasValue == true ? agreement.NilaiProyek.ToString() : null,
                TanggalTtd = agreement.TanggalTtd.HasValue == true ? agreement.TanggalTtd : null,
                TanggalBerakhir = agreement.TanggalBerakhir.HasValue == true ? agreement.TanggalBerakhir : null,
                Scope = agreement.Scope ?? "",
                Progress = agreement.Progress,
                IsGtg = agreement.IsGtG ?? null,
                Valuation = agreement.Valuation ?? null,
                Capex = agreement.Capex ?? null,
                PotentialRevenuePerYear = agreement.Revenue ?? null,
                IsGreenBusinessDecode = agreement.IsGreenBusiness ?? null,
                IsStrategicPartnerShipDecode = agreement.IsStrategic ?? null,
                DeskripsiKendala = agreement.DeskripsiKendala ?? "",
                TindakLanjut = agreement.TindakLanjut ?? "",
                SupportPemerintah = agreement.SupportPemerintah ?? "",
                PotensiEskalasi = agreement.PotensiEskalasi ?? "",
                CatatanTambahan = agreement.CatatanTambahan ?? "",
                CellPartner = agreement.Partners ?? "",
                CellEntitasPertamina = agreement.EntitasPertamina ?? "",
                CellStreamBusiness = agreement.StreamBusiness ?? "",
                CellJenisPerjanjian = agreement.JenisPerjanjian ?? "",
                CellNegaraMitra = agreement.NegaraMitra ?? "",
                CellLokasiProyek = agreement.LokasiProyek ?? "",
                CellStatusDiskusi = agreement.DiscussionStatus ?? "",
                CellFaktorKendala = agreement.FaktorKendala ?? "",
                CellKlasifikasiKendala = agreement.KlasifikasiKendala ?? "",
                CellHsh = agreement.HshName ?? "",
                CellFungsiPicLead = agreement.PicLead ?? "",
                CellFungsiPicMember = agreement.PicMember ?? "",
                CellKawasanMitra = agreement.KawasanMitraName ?? "",
                CellStatusBerlaku = agreement.StatusBerlaku ?? "",
                CellLembaga = agreement.KeterlibatanKementrian ?? "",
                CellRelatedAgreement = agreement.RelatedPerjanjian ?? "",
                CellHshId = agreement.HshId ?? "",
                CellEntitasPertaminaId = agreement.EntitasPertaminaId ?? "",
                CellJenisPerjanjianId = agreement.JenisPerjanjianId.HasValue == true ? agreement.JenisPerjanjianId.ToString() : null,
                CellKawasanMitraId = agreement.KawsanMitraId ?? "",
                CellNegaraMitraId = agreement.NegaraMitraId ?? "",
                CellStreamBusinessId = agreement.StreamBusinessId ?? "",
                CellStatusBerlakuId = agreement.StatusBerlakuId.HasValue == true ? agreement.StatusBerlakuId.ToString() : null,
                CellStatusDiskusiId = agreement.DiscussionStatusId.HasValue == true ? agreement.DiscussionStatusId.ToString() : null,
                CellCreateDate = agreement.TanggalDibuat ?? "",
                CellTanggalTTD = agreement.TanggalTtd.HasValue == true ? agreement.TanggalTtd.ToString() : null,
                CellTanggalBerakhir = agreement.TanggalBerakhir.HasValue == true ? agreement.TanggalBerakhir.ToString() : null,
                CellKodeAgreement = agreement.KodeAgreement ?? "",
                CellKodeAgreementRelation = agreement.KodeRelatedPerjanjian ?? "",
                IsDraft = agreement.IsDraft,
                CellCatatanTambahan = agreement.CatatanTambahan,
                //CellProjectCost = agreement.ProjectCost.HasValue == true ? agreement.ProjectCost.ToString() : null,
                CellAmandementDate = agreement.Amandement ?? "",
                CellTrafficLight = agreement.TrafficLight ?? ""

            });
            return items;
        }

        public List<Guid> CheckIsStrategicPartnership(bool isStrategic)
        {
            List<Guid> jenisPerjanjians = null;
            if (isStrategic) 
            {
                jenisPerjanjians = _context.TblmJenisPerjanjians.Where(x => x.IsStrategic == true  && x.IsStrategic == null && x.DeletedDate !=null).Select(x => x.Id).ToList();
            }
            return jenisPerjanjians ;
        }
        public List<Guid> CheckBdCoreBusiness(bool bdCoreBusiness)
        {
            List<Guid> guids = null;
            if (bdCoreBusiness) 
            {
                guids = _context.TbltAgreements.Where(x => x.Valuation + x.PotentialRevenuePerYear + x.Capex >=500000000 && x.DeletedDate !=null).Select(x => x.Id).ToList();
            }
            return guids ;
        }
        public List<Guid> CheckIsGreenBusiness(bool IsGreenBusiness)
        {
            List<Guid> guids = null;
            if (IsGreenBusiness) 
            {
                guids = _context.TblmStreamBusinesses.Where(x => x.IsGreenBusiness == true && x.DeletedDate != null).Select(x => x.Id).ToList();
            }
            return guids ;
        }
        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TbltAgreement record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TbltAgreement>().Update(record);
            _context.SaveChanges();
        }
        public void DeleteEntitas(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltAgreementEntitasPertamina> records = _context.TbltAgreementEntitasPertaminas.Where(x => x.DeletedDate == null && x.AgreementId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltAgreementEntitasPertamina>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeletePicFungsi(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltAgreementPicFungsi> records = _context.TbltAgreementPicFungsis.Where(x => x.DeletedDate == null && x.AgreementId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltAgreementPicFungsi>().Update(rec);
                _context.SaveChanges();
            }

        }
        public void DeletePartner(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltAgreementPartner> records = _context.TbltAgreementPartners.Where(x => x.DeletedDate == null && x.AgreementId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltAgreementPartner>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteNegaraMitra(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltAgreementNegaraMitra> records = _context.TbltAgreementNegaraMitras.Where(x => x.DeletedDate == null && x.AgreementId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltAgreementNegaraMitra>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteLokasiProyek(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltAgreementLokasiProyek> records = _context.TbltAgreementLokasiProyeks.Where(x => x.DeletedDate == null && x.AgreementId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltAgreementLokasiProyek>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteStreamBusiness(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltAgreementStreamBusiness> records = _context.TbltAgreementStreamBusinesses.Where(x => x.DeletedDate == null && x.AgreementId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltAgreementStreamBusiness>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteKementrianLembaga(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltAgreementKementrianLembaga> records = _context.TbltAgreementKementrianLembagas.Where(x => x.DeletedDate == null && x.AgreementId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltAgreementKementrianLembaga>().Update(rec);
                _context.SaveChanges();
            }
        }
        public bool IsExistInAgreement(Guid guid)
        {
            IQueryable<TbltAgreement> GetAllRecord = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.RelatedAgreementId == guid);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsExistInInitiative(Guid guid)
        {
            IQueryable<TbltInitiative> GetAllRecord = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.AgreementId == guid);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<AgreementViewModel> GetHubHeadByHubId(Guid hubId)
        {
            
            var query = await _context.TblmHubHeads
             .Where(x => x.HubId == hubId && x.DeletedDate == null)
             .Select(select => new AgreementViewModel
             {
                 HubHeadId = select.Id,
                 HubId = hubId,
                 HubHeadName = select.Name ?? "-",
             })
             .FirstOrDefaultAsync();

            return query;
        }
        public int? GetSequenceAmandement(Guid guid)
        {
            
            return _context.TbltAgreementAddenda
             .Where(x => x.AgreementId == guid && x.DeletedDate == null).OrderByDescending(x=>x.Sequence)
             .Select(x=>x.Sequence)
             .FirstOrDefault();
        } 
        public List<NegaraMitraExcludedViewModel> GetAllNegareMitraExcluded()
        {

            List<NegaraMitraExcludedViewModel> result = _mapper.Map<List<NegaraMitraExcludedViewModel>>(_context.TblmNegaraMitraExcludeds.Where(x => x.DeletedDate == null).ToList()); 
            return result;

        }   
        public StatusBerlakuViewModel GetStatusExpired()
        {

            StatusBerlakuViewModel result = _mapper.Map<StatusBerlakuViewModel>(_context.TblmStatusBerlakus.Where(x => x.DeletedDate == null && x.IsExpired == true).FirstOrDefault());
            return result;

        } 
        public async Task<AgreementViewModel> GetAdendumSequence(Guid guid,int sequence, bool addRepeater)
        {
            var result = new AgreementViewModel();
            var romanSequences = new List<string>();
            int? getTotalAddendum = GetSequenceAgreementAddendumById(guid);
            if (sequence > 0 && addRepeater == true)
            {
                int currentSequence = sequence + 1;
                for (int i = (getTotalAddendum+1).Value;  i<=currentSequence ; i++)
                {
                    romanSequences.Add($"Amandement {ToRoman(i)}");
                    result.SequenceAmandement = romanSequences;
                }
            }
            else {
                int currentSequence = sequence + 1;
                for (int i = 1; i<= currentSequence; i++)
                {
                    romanSequences.Add($"Amandement {ToRoman(i)}");
                    result.SequenceAmandement = romanSequences;
                }
            }
            return result;
        }
        public string ToRoman(int number)
        {
            if (number < 1 || number > 3999)
                throw new ArgumentOutOfRangeException("Angka harus berada dalam rentang 1 - 3999.");

            string[] romanNumerals =
            {
            "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"
        };
            int[] values =
            {
            1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1
        };

            var roman = string.Empty;

            for (int i = 0; i < values.Length; i++)
            {
                while (number >= values[i])
                {
                    roman += romanNumerals[i];
                    number -= values[i];
                }
            }

            return roman;
        }
        //public async Task<AgreementViewModel> GetAddendumById(Guid hubId)
        //{
        //    var query = await _context.TblmHubHeads
        //     .Where(x => x.HubId == hubId && x.DeletedDate == null)
        //     .Select(select => new AgreementViewModel
        //     {
        //         HubHeadId = select.Id,
        //         HubId = hubId,
        //         HubHeadName = select.Name ?? "-",
        //     })
        //     .FirstOrDefaultAsync();

        //    return query;
        //}
        public Guid GetPicLevelLeadId()
        {
            var query = _context.TblmPicLevels
             .Where(x => x.DeletedDate == null&& x.IsLead== true).FirstOrDefault();

            return query.Id;

        }
        public Guid GetPicLevelMemberId()
        {
            var query = _context.TblmPicLevels
             .Where(x => x.DeletedDate == null&& x.IsLead== false).FirstOrDefault();

            return query.Id;

        }
    }
}