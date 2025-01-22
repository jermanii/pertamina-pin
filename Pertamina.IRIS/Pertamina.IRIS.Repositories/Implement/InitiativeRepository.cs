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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class InitiativeRepository : IInitiativeRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public InitiativeRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Private
        private IQueryable<InitiativeViewModel> QueryList(IQueryable<TbltInitiative> query)
        {
            return query
                .LeftJoin(_context.TbltInitiativeEntitasPertaminas
                .Join(_context.TblmEntitasPertaminas
                .Join(_context.TblmHshes, ad => ad.HshId, sf => sf.Id, (ad, sf) => new InitiativeEntitasPertaminaViewModel()
                {
                    Id = ad.Id,
                    EntitasPertaminaId = ad.Id,
                    CompanyName = ad.CompanyName,
                    HshId = sf.Id,
                    HshName = sf.Name
                }), ad => ad.EntitasPertaminaId, sf => sf.Id, (ad, sf) => new InitiativeEntitasPertaminaViewModel()
                {
                    Id = ad.Id,
                    InitiativeId = ad.InitiativeId,
                    HshId = sf.HshId,
                    EntitasPertaminaId = ad.EntitasPertaminaId,
                    CompanyName = sf.CompanyName,
                }), ad => ad.Id, sf => sf.InitiativeId, (ad, sf) => new InitiativeViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    JudulInisiasi = ad.JudulInisiasi,
                    QueryHshId = sf.HshId,
                    QueryEntitasPertaminaId = sf.EntitasPertaminaId,
                    QueryEntitasPertaminaName = sf.CompanyName,
                    InitiativeStageId = ad.InitiativeStageId,
                    InitiativeStatusId = ad.InitiativeStatusId,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    TargetWaktuProyek = ad.TargetWaktuProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress, 
                    IsuKendala = ad.IsuKendala,
                    Referal = ad.Referal,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan,
                    SupportPemerintah = ad.SupportPemerintah,
                    ValueForIndonesia = ad.ValueForIndonesia
                    
                })
                .LeftJoin(_context.TbltInitiativeNegaraMitras
                .Join(_context.TblmNegaraMitras
                .Join(_context.TblmKawasanMitras, ad => ad.KawasanMitraId, sf => sf.Id, (ad, sf) => new InitiativeNegaraMitraViewModel()
                {
                    Id = ad.Id,
                    NegaraMitraId = ad.Id,
                    NamaNegara = ad.NamaNegara,
                    KawasanMitraId = ad.KawasanMitraId,
                    NamaKawasan = sf.NamaKawasan,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                }), ad => ad.NegaraMitraId, sf => sf.Id, (ad, sf) => new InitiativeNegaraMitraViewModel()
                {
                    Id = ad.Id,
                    InitiativeId = ad.InitiativeId,
                    NegaraMitraId = ad.NegaraMitraId,
                    NamaNegara = sf.NamaNegara,
                    KawasanMitraId = sf.KawasanMitraId,
                    NamaKawasan = sf.NamaKawasan,
                }), ad => ad.Id, sf => sf.InitiativeId, (ad, sf) => new InitiativeViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    JudulInisiasi = ad.JudulInisiasi,
                    QueryHshId = ad.QueryHshId,
                    QueryEntitasPertaminaId = ad.QueryEntitasPertaminaId,
                    QueryEntitasPertaminaName = ad.QueryEntitasPertaminaName,
                    QueryNegaraMitraId = sf.NegaraMitraId,
                    QueryNamaNegara = sf.NamaNegara,
                    QueryKawasanMitraId = sf.KawasanMitraId,
                    QueryNamaKawasan = sf.NamaKawasan,
                    InitiativeStageId = ad.InitiativeStageId,
                    InitiativeStatusId = ad.InitiativeStatusId,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    TargetWaktuProyek = ad.TargetWaktuProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    IsuKendala = ad.IsuKendala,
                    Referal = ad.Referal,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan,
                    SupportPemerintah = ad.SupportPemerintah,
                    ValueForIndonesia = ad.ValueForIndonesia

                })
                .LeftJoin(_context.TbltInitiativeStreamBusinesses
                .Join(_context.TblmStreamBusinesses, ad => ad.StreamBusinessId, sf => sf.Id, (ad, sf) => new InitiativeStreamBusinessViewModel()
                {
                    Id = ad.Id,
                    InitiativeId = ad.InitiativeId,
                    StreamBusinessId = ad.StreamBusinessId,
                    QueryStreamBusinessName = sf.Name,
                }), ad => ad.Id, sf => sf.InitiativeId, (ad, sf) =>
                new InitiativeViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    JudulInisiasi = ad.JudulInisiasi,
                    QueryHshId = ad.QueryHshId,
                    QueryEntitasPertaminaId = ad.QueryEntitasPertaminaId,
                    QueryEntitasPertaminaName = ad.QueryEntitasPertaminaName,
                    QueryStreamBusinessId = sf.StreamBusinessId,
                    QueryStreamBusinessName = sf.QueryStreamBusinessName,
                    QueryNegaraMitraId = ad.QueryNegaraMitraId,
                    QueryNamaNegara = ad.QueryNamaNegara,
                    QueryKawasanMitraId = ad.QueryKawasanMitraId,
                    QueryNamaKawasan = ad.QueryNamaKawasan,
                    InitiativeStageId = ad.InitiativeStageId,
                    InitiativeStatusId = ad.InitiativeStatusId,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    TargetWaktuProyek = ad.TargetWaktuProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    IsuKendala = ad.IsuKendala,
                    Referal = ad.Referal,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan,
                    SupportPemerintah = ad.SupportPemerintah,
                    ValueForIndonesia = ad.ValueForIndonesia

                })
                .LeftJoin(_context.TblmInitiativeStages, ad => ad.InitiativeStageId, sf => sf.Id, (ad, sf) =>
                new InitiativeViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    JudulInisiasi = ad.JudulInisiasi,
                    QueryHshId = ad.QueryHshId,
                    QueryEntitasPertaminaId = ad.QueryEntitasPertaminaId,
                    QueryEntitasPertaminaName = ad.QueryEntitasPertaminaName,
                    QueryStreamBusinessId = ad.QueryStreamBusinessId,
                    QueryStreamBusinessName = ad.QueryStreamBusinessName,
                    QueryNegaraMitraId = ad.QueryNegaraMitraId,
                    QueryNamaNegara = ad.QueryNamaNegara,
                    QueryKawasanMitraId = ad.QueryKawasanMitraId,
                    QueryNamaKawasan = ad.QueryNamaKawasan,
                    QueryInitiativeStageId = ad.InitiativeStageId,
                    QueryInitiativeStageName = sf.Name,
                    InitiativeStatusId = ad.InitiativeStatusId,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    TargetWaktuProyek = ad.TargetWaktuProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    IsuKendala = ad.IsuKendala,
                    Referal = ad.Referal,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan,
                    SupportPemerintah = ad.SupportPemerintah,
                    ValueForIndonesia = ad.ValueForIndonesia

                })
                .LeftJoin(_context.TblmInitiativeStatuses, ad => ad.InitiativeStatusId, sf => sf.Id, (ad, sf) =>
                new InitiativeViewModel()
                {
                    Id = ad.Id,
                    IsDraft = ad.IsDraft,
                    JudulInisiasi = ad.JudulInisiasi,
                    QueryHshId = ad.QueryHshId,
                    QueryEntitasPertaminaId = ad.QueryEntitasPertaminaId,
                    QueryEntitasPertaminaName = ad.QueryEntitasPertaminaName,
                    QueryStreamBusinessId = ad.QueryStreamBusinessId,
                    QueryStreamBusinessName = ad.QueryStreamBusinessName,
                    QueryNegaraMitraId = ad.QueryNegaraMitraId,
                    QueryNamaNegara = ad.QueryNamaNegara,
                    QueryKawasanMitraId = ad.QueryKawasanMitraId,
                    QueryNamaKawasan = ad.QueryNamaKawasan,
                    QueryInitiativeStageId = ad.QueryInitiativeStageId,
                    QueryInitiativeStageName = ad.QueryInitiativeStageName,
                    QueryInitiativeStatusId = ad.InitiativeStatusId,
                    QueryInitiativeStatusName = sf.StatusName,
                    CreateDate = ad.CreateDate,
                    UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                    NilaiProyek = ad.NilaiProyek,
                    TargetWaktuProyek = ad.TargetWaktuProyek,
                    Scope = ad.Scope,
                    Progress = ad.Progress,
                    IsuKendala = ad.IsuKendala,
                    Referal = ad.Referal,
                    PotensiEskalasi = ad.PotensiEskalasi,
                    CatatanTambahan = ad.CatatanTambahan,
                    SupportPemerintah = ad.SupportPemerintah,
                    ValueForIndonesia = ad.ValueForIndonesia

                })
                .OrderByDescending(x => x.IsDraft)
                .AsQueryable();
        }
        #endregion

        #region Grid
        public async Task<ResponseDataTableBaseModel<List<InitiativeViewModel>>> GetList(RequestFormDtBaseModel request, InitiativeViewModel decodeModel)
        {
            try
            {
                var query = _context.TbltInitiatives.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TbltInitiative>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.JudulInisiasi.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.Scope.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.NilaiProyek.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.TargetWaktuProyek.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.Progress.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.IsuKendala.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.Referal.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.CatatanTambahan.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.SupportPemerintah.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.PotensiEskalasi.ToLower().Contains(filter.Value.ToLower())) ||
                                                  (x.ValueForIndonesia.ToLower().Contains(filter.Value.ToLower()))

                    );
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);
                IQueryable<InitiativeViewModel> joinquery = QueryList(query);

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

                if (!string.IsNullOrEmpty(decodeModel.InitiativeStageDecode))
                {
                    string[] strInitiativeStage = decodeModel.InitiativeStageDecode.Split('_');

                    List<Guid> initiativeStages = new List<Guid>();
                    foreach (var str in strInitiativeStage)
                    {
                        Guid newGuid = Guid.Parse(str);
                        initiativeStages.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => initiativeStages.Contains(x.QueryInitiativeStageId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.InitiativeStatusDecode))
                {
                    string[] strInitiativeStatus = decodeModel.InitiativeStatusDecode.Split('_');

                    List<Guid> initiativeStatus = new List<Guid>();
                    foreach (var str in strInitiativeStatus)
                    {
                        Guid newGuid = Guid.Parse(str);
                        initiativeStatus.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => initiativeStatus.Contains(x.QueryInitiativeStatusId.Value)).AsQueryable();
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

                if (!string.IsNullOrEmpty(decodeModel.KawasanMitraDecode))
                {
                    string[] strKawasanMitras = decodeModel.KawasanMitraDecode.Split('_');

                    List<Guid> kawasanMitras = new List<Guid>();
                    foreach (var str in strKawasanMitras)
                    {
                        Guid newGuid = Guid.Parse(str);
                        kawasanMitras.Add(newGuid);
                    }

                    joinquery = joinquery.Where(x => kawasanMitras.Contains(x.QueryKawasanMitraId.Value)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(decodeModel.InitiativeHolderDecode))
                {
                    string[] strInitiativeHolders = decodeModel.InitiativeHolderDecode.Split('_');

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

                joinquery = joinquery.GroupBy(x => x.Id, (a, b) => new InitiativeViewModel
                {
                    Id = a
                }).Join(_context.TbltInitiatives, x => x.Id, y => y.Id, (x, y) => new InitiativeViewModel
                {
                    Id = x.Id,
                    IsDraft = y.IsDraft,
                    RowJudulInitiative = y.JudulInisiasi,
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

                var list = await PaginatedList<InitiativeViewModel, InitiativeViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<InitiativeViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<InitiativeViewModel>>(false, ex.Message);
            }
        }
        public List<InitiativeStageViewModel> GetRecordsInitiativeStage()
        {
            IQueryable<TblmInitiativeStage> result = _context.TblmInitiativeStages.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<InitiativeStageViewModel>>(result);
        }
        public int? GetCountInitiativeStage(int relation)
        {
            int result = 0;

            IQueryable<InitiativeViewModel> GetAllRecord = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TblmInitiativeStages.Where(x => x.RelationSequence == relation), a => a.InitiativeStageId, b => b.Id, (a, b) =>
                  new InitiativeViewModel()
                  {
                      Id = a.Id
                  }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public List<InitiativeStatusViewModel> GetRecordsInitiativeStatus()
        {
            IQueryable<TblmInitiativeStatus> result = _context.TblmInitiativeStatuses.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<InitiativeStatusViewModel>>(result);
        }
        public int? GetCountInitiativeStatus(int relation)
        {
            int result = 0;

            IQueryable<InitiativeViewModel> GetAllRecord = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TblmInitiativeStatuses.Where(x => x.RelationSequence == relation), a => a.InitiativeStatusId, b => b.Id, (a, b) =>
                  new InitiativeViewModel()
                  {
                      Id = a.Id
                  }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public List<HshViewModel> GetRecordsHsh()
        {
            IQueryable<TblmHsh> result = _context.TblmHshes.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<HshViewModel>>(result);
        }
        public int? GetCountHsh(int relation)
        {
            int result = 0;

            IQueryable<InitiativeEntitasPertaminaViewModel> GetAllRecord = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TbltInitiativeEntitasPertaminas, a => a.Id, b => b.InitiativeId, (a, b) =>
                new InitiativeEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    InitiativeId = a.Id,
                    EntitasPertaminaId = b.EntitasPertaminaId
                })
                .Join(_context.TblmEntitasPertaminas, a => a.EntitasPertaminaId, b => b.Id, (a, b) =>
                new InitiativeEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    InitiativeId = a.InitiativeId,
                    EntitasPertaminaId = b.Id,
                    HshId = b.HshId
                })
                .Join(_context.TblmHshes.Where(x => x.RelationSequence == relation), a => a.HshId, b => b.Id, (a, b) =>
                new InitiativeEntitasPertaminaViewModel()
                {
                    Id = b.Id,
                    InitiativeId = a.InitiativeId,
                    EntitasPertaminaId = b.Id,
                    HshId = b.Id
                })
                .AsQueryable();

            result = GetAllRecord.GroupBy(x => x.InitiativeId).Count();

            return result;
        }
        public int? GetCountRecordsInitiative()
        {
            int result = 0;

            IQueryable<InitiativeViewModel> GetAllRecord = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.IsDraft == false).Select(x =>
            new InitiativeViewModel()
            {
                Id = x.Id
            }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public int? GetCountRecordsNegaraMitra()
        {
            int result = 0;

            IQueryable<InitiativeNegaraMitraViewModel> GetAllRecord = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.IsDraft == false)
                .Join(_context.TbltInitiativeNegaraMitras, a => a.Id, b => b.InitiativeId, (a, b) =>
                new InitiativeNegaraMitraViewModel()
                {
                    Id = b.Id,
                    InitiativeId = a.Id,
                    NegaraMitraId = b.NegaraMitraId
                }).AsQueryable();

            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId).Select(x => new InitiativeNegaraMitraViewModel
            {
                Id = x.Key.Value
            }).AsQueryable();

            result = GetAllRecord.Count();

            return result;
        }
        public List<InitiativePartnerViewModel> GetPartners(Guid guid)
        {
            IQueryable<TbltInitiativePartner> result = _context.TbltInitiativePartners.Where(x => x.DeletedDate == null && x.InitiativeId == guid).OrderBy(x => x.PartnerName).AsQueryable();

            return _mapper.Map<List<InitiativePartnerViewModel>>(result);
        }
        public List<InitiativeEntitasPertaminaViewModel> GetEntitasPertamina(Guid id)
        {
            IQueryable<InitiativeEntitasPertaminaViewModel> result = _context.TbltInitiativeEntitasPertaminas
                .Where(x => x.DeletedDate == null && x.InitiativeId == id)
                .Join(_context.TblmEntitasPertaminas, x => x.EntitasPertaminaId, y => y.Id, (x, y) =>
                      new InitiativeEntitasPertaminaViewModel
                      {
                          Id = x.Id,
                          CompanyName = y.CompanyName,
                          CreateDate = x.CreateDate
                      })
                .OrderBy(x => x.CreateDate).AsQueryable();

            return result.ToList();
        }
        public List<InitiativeStreamBusinessViewModel> GetStreamBusiness(Guid id)
        {
            IQueryable<InitiativeStreamBusinessViewModel> result = _context.TbltInitiativeStreamBusinesses
                .Where(x => x.DeletedDate == null && x.InitiativeId == id)
                .Join(_context.TblmStreamBusinesses, x => x.StreamBusinessId, y => y.Id, (x, y) =>
                     new InitiativeStreamBusinessViewModel
                     {
                         Id = x.Id,
                         QueryStreamBusinessName = y.Name,
                         CreateDate = x.CreateDate
                     })
                .OrderBy(x => x.CreateDate)
                .AsQueryable();

            return result.ToList();
        }
        public List<InitiativeNegaraMitraViewModel> GetNamaNegara(Guid id)
        {
            IQueryable<InitiativeNegaraMitraViewModel> result = _context.TbltInitiativeNegaraMitras
                .Where(x => x.DeletedDate == null && x.InitiativeId == id)
                .Join(_context.TblmNegaraMitras
                .Join(_context.TblmKawasanMitras, x => x.KawasanMitraId, y => y.Id, (x, y) =>
                           new InitiativeNegaraMitraViewModel
                           {
                               Id = x.Id,
                               NamaNegara = x.NamaNegara,
                               KawasanMitraId = y.Id,
                               NamaKawasan = y.NamaKawasan,
                               CreateDate = x.CreateDate
                           }), x => x.NegaraMitraId, y => y.Id, (x, y) =>
                           new InitiativeNegaraMitraViewModel
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
        public bool? GetStatusDraft(Guid id)
        {
            bool? result = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.Id == id).Select(x => x.IsDraft).FirstOrDefault();

            return result;
        }
        public string GetJudulInitiative(Guid id)
        {
            string result = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.Id == id).Select(x => x.JudulInisiasi).FirstOrDefault();

            return result;
        }
        public string GetInitiativeStage(Guid id)
        {
            string result = _context.TbltInitiatives
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmInitiativeStages, x => x.InitiativeStageId, y => y.Id, (x, y) =>
                    new InitiativeStageViewModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                        CreateDate = y.CreateDate,
                    })
                .Select(x => x.Name).FirstOrDefault();

            return result;
        }
        public string GetInitiativeStatus(Guid id)
        {
            string result = _context.TbltInitiatives
                .Where(x => x.DeletedDate == null && x.Id == id)
                .Join(_context.TblmInitiativeStatuses, x => x.InitiativeStatusId, y => y.Id, (x, y) =>
                    new InitiativeStatusViewModel
                    {
                        Id = y.Id,
                        StatusName = y.StatusName,
                        CreateDate = y.CreateDate,
                    })
                .Select(x => x.StatusName).FirstOrDefault();

            return result;
        }
        public async Task<IQueryable<InitiativeViewModel>> QueryInitiativeWithFilter(InitiativeViewModel decodeModel)
        {
            IQueryable<InitiativeViewModel> result = null;

            var query = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.IsDraft == false).AsQueryable();

            query = query.Where(x => x.DeletedDate == null && x.IsDraft == false);

            IQueryable<InitiativeViewModel> joinquery = QueryList(query);

            if(decodeModel.IsDraft != null)
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

            if (!string.IsNullOrEmpty(decodeModel.InitiativeStageDecode))
            {
                string[] strInitiativeStage = decodeModel.InitiativeStageDecode.Split('_');

                List<Guid> initiativeStages = new List<Guid>();
                foreach (var str in strInitiativeStage)
                {
                    Guid newGuid = Guid.Parse(str);
                    initiativeStages.Add(newGuid);
                }

                joinquery = joinquery.Where(x => initiativeStages.Contains(x.QueryInitiativeStageId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.InitiativeStatusDecode))
            {
                string[] strInitiativeStatus = decodeModel.InitiativeStatusDecode.Split('_');

                List<Guid> initiativeStatus = new List<Guid>();
                foreach (var str in strInitiativeStatus)
                {
                    Guid newGuid = Guid.Parse(str);
                    initiativeStatus.Add(newGuid);
                }

                joinquery = joinquery.Where(x => initiativeStatus.Contains(x.QueryInitiativeStatusId.Value)).AsQueryable();
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

            if (!string.IsNullOrEmpty(decodeModel.KawasanMitraDecode))
            {
                string[] strKawasanMitras = decodeModel.KawasanMitraDecode.Split('_');

                List<Guid> kawasanMitras = new List<Guid>();
                foreach (var str in strKawasanMitras)
                {
                    Guid newGuid = Guid.Parse(str);
                    kawasanMitras.Add(newGuid);
                }

                joinquery = joinquery.Where(x => kawasanMitras.Contains(x.QueryKawasanMitraId.Value)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(decodeModel.InitiativeHolderDecode))
            {
                string[] strInitiativeHolders = decodeModel.InitiativeHolderDecode.Split('_');

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
        public IEnumerable<InitiativeViewModel> GetAllInitiativeExcel()
        {
            IQueryable<InitiativeViewModel> items = _context.VwExportInitiatives.Select(initiative => new InitiativeViewModel
            {
                //CellPartner = initiative.PerusahaanMitra ?? "",
                CellEntitasPertamina = initiative.Pertamina ?? "",
                CellStreamBusiness = initiative.StreamBusiness ?? "",
                CellHsh = initiative.HshName ?? "",
                //CellFungsiPic = initiative.Pic ?? "",
                CellJudulInisiasi = initiative.JudulInisiasi ?? "",
                CellInterest = initiative.InterestName ?? "",
                CellInitiativeStatus = initiative.InitiativeStatus ?? "",
                CellInitiativeStage = initiative.IntiativeStage ?? "",
                CellInitiativeType = initiative.InitiativeType ?? "",
                CellKawasanMitra = initiative.KawasanMitra ?? "",
                CellNegaraMitra = initiative.NegaraMitra ?? "",
                CellLokasiProyek = initiative.LokasiProyek ?? "",
                CellNilaiProyek = initiative.NilaiProyek ?? "",
                CellTargetProyek = initiative.TargetWaktuProyek ?? "",
                CellScope = initiative.Scope ?? "",
                CellProgress = initiative.Progress ?? "",
                CellValue = initiative.ValueForIndonesia ?? "",
                CellIsuKendala = initiative.IsuKendala ?? "",
                CellTindakLanjut = initiative.TindakLanjut ?? "",
                CellSupportPemerintah = initiative.SupportPemerintah ?? "",
                CellReferral = initiative.Referal ?? "",
                CellPotensiEkskalasi = initiative.PotensiEskalasi ?? "",
                CellRelatedAgreement = initiative.RelatedAgreement ?? "",
                CellCatatanTambahan = initiative.CatatanTambahan ?? "",
                CellInitiativeStageId = initiative.InitiativeStageId.HasValue == true ? initiative.InitiativeStageId.ToString() : null,
                CellInitiativeStatusId = initiative.InititativeStatusId.HasValue == true ? initiative.InititativeStatusId.ToString() :null,
                CellInitiativeHolderId = initiative.HshId ?? "",
                CellKawasanMitraId = initiative.KawasanMitraId ?? "",
                CellNegaraMitraId = initiative.NegaraMitraId ?? "",
                CellStreamBusinessId = initiative.StreamBusinessId ?? "",
                CellEntitasPertaminaId = initiative.EntitasPertaminaId ?? "",
                CellCreateDate = initiative.TanggalDibuat ?? "",
                CellKodeAgreement = initiative.KodeAgreement ?? "",
                IsDraft = initiative.IsDraft
            });
            return items;
        }

        public async Task<IQueryable<InitiativeNegaraMitraViewModel>> GetRecordsNegaraMitra(InitiativeViewModel model)
        {
            IQueryable<InitiativeNegaraMitraViewModel> result = null;

            IQueryable<InitiativeNegaraMitraViewModel> GetAllRecord = QueryInitiativeWithFilter(model).Result
                .GroupBy(x => x.Id)
                .Select(x => new InitiativeNegaraMitraViewModel
                {
                    Id = x.Key
                }).AsQueryable();

            GetAllRecord = GetAllRecord.Join(_context.TbltInitiativeNegaraMitras, a => a.Id, b => b.InitiativeId, (a, b) =>
                    new InitiativeNegaraMitraViewModel()
                    {
                        Id = a.Id,
                        InitiativeId = b.InitiativeId,
                        NegaraMitraId = b.NegaraMitraId
                    }).AsQueryable();

            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId)
                .Select(x =>
                    new InitiativeNegaraMitraViewModel
                    {
                        Id = x.Key.Value
                    })
                .AsQueryable();

            result = await Task.FromResult(GetAllRecord);

            return result;
        }
        public async Task<IQueryable<InitiativeNegaraMitraViewModel>> GetRecordsNegaraMitraWithSearch(InitiativeViewModel model, string strSearch)
        {
            IQueryable<InitiativeNegaraMitraViewModel> result = null;

            IQueryable<InitiativeNegaraMitraViewModel> GetAllRecord = QueryInitiativeWithFilter(model).Result
                .Where(x => x.JudulInisiasi.ToLower().Contains(strSearch.ToLower()) ||
                            x.NilaiProyek.ToLower().Contains(strSearch.ToLower()) ||
                            x.TargetWaktuProyek.ToLower().Contains(strSearch.ToLower()) ||
                            x.Scope.ToLower().Contains(strSearch.ToLower()) ||
                            x.Progress.ToLower().Contains(strSearch.ToLower()) ||
                            x.IsuKendala.ToLower().Contains(strSearch.ToLower()) ||
                            x.Referal.ToLower().Contains(strSearch.ToLower()) ||
                            x.PotensiEskalasi.ToLower().Contains(strSearch.ToLower()) ||
                            x.CatatanTambahan.ToLower().Contains(strSearch.ToLower()) ||
                            x.SupportPemerintah.ToLower().Contains(strSearch.ToLower()) ||
                            x.ValueForIndonesia.ToLower().Contains(strSearch.ToLower())
                            )
                            
                .GroupBy(x => x.Id)
                .Select(x => new InitiativeNegaraMitraViewModel
                {
                    Id = x.Key
                }).AsQueryable();

            GetAllRecord = GetAllRecord.Join(_context.TbltInitiativeNegaraMitras, a => a.Id, b => b.InitiativeId, (a, b) =>
                    new InitiativeNegaraMitraViewModel()
                    {
                        Id = a.Id,
                        InitiativeId = b.InitiativeId,
                        NegaraMitraId = b.NegaraMitraId
                    }).AsQueryable();

            GetAllRecord = GetAllRecord.GroupBy(x => x.NegaraMitraId)
                .Select(x =>
                    new InitiativeNegaraMitraViewModel
                    {
                        Id = x.Key.Value
                    })
                .AsQueryable();

            result = await Task.FromResult(GetAllRecord);

            return result;
        }
        #endregion

        public void Save(InitiativeViewModel model)
        {
            _context.Set<TbltInitiative>().Add(_mapper.Map<TbltInitiative>(model));
            _context.SaveChanges();
        }
        public void SavePic(InitiativePicFungsiViewModel model)
        {
            _context.Set<TbltInitiativePicFungsi>().Add(_mapper.Map<TbltInitiativePicFungsi>(model));
            _context.SaveChanges();
        }
        public void SaveEntitasPertamina(InitiativeEntitasPertaminaViewModel model)
        {
            _context.Set<TbltInitiativeEntitasPertamina>().Add(_mapper.Map<TbltInitiativeEntitasPertamina>(model));
            _context.SaveChanges();
        }
        public async Task<StatusBerlakuViewModel> StatusBerlakuCounting(int? sequence)
        {
            StatusBerlakuViewModel result = await Task.FromResult(_mapper.Map<StatusBerlakuViewModel>(_context.TblmStatusBerlakus.Where(x => x.RelationSequence == sequence && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }
        public void SaveNegaraMitra(InitiativeNegaraMitraViewModel model)
        {
            _context.Set<TbltInitiativeNegaraMitra>().Add(_mapper.Map<TbltInitiativeNegaraMitra>(model));
            _context.SaveChanges();
        }
        public void SaveStreamBusiness(InitiativeStreamBusinessViewModel model)
        {
            _context.Set<TbltInitiativeStreamBusiness>().Add(_mapper.Map<TbltInitiativeStreamBusiness>(model));
            _context.SaveChanges();
        }
        public void SaveToPartner(InitiativePartnerViewModel model)
        {
            _context.Set<TbltInitiativePartner>().Add(_mapper.Map<TbltInitiativePartner>(model));
            _context.SaveChanges();
        }
        public void SaveToLokasiProyek(InitiativeLokasiProyekViewModel model)
        {
            _context.Set<TbltInitiativeLokasiProyek>().Add(_mapper.Map<TbltInitiativeLokasiProyek>(model));
            _context.SaveChanges();
        }
        public void SaveKementrianLembaga(InitiativeKementrianLembagaViewModel model)
        {
            _context.Set<TbltInitiativeKementrianLembaga>().Add(_mapper.Map<TbltInitiativeKementrianLembaga>(model));
            _context.SaveChanges();
        }
        public InitiativeViewModel GetInitiativeById(Guid guid)
        {
            InitiativeViewModel result = _mapper.Map<InitiativeViewModel>(_context.TbltInitiatives.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public async Task<InitiativeViewModel> GetInitiativeAsyncById(Guid guid)
        {
            InitiativeViewModel result = await Task.FromResult(_mapper.Map<InitiativeViewModel>(_context.TbltInitiatives.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault()));
            return result;
        }
        public InitiativeEntitasPertaminaViewModel GetInitiativeEntitasById(Guid guid)
        {
            InitiativeEntitasPertaminaViewModel result = _mapper.Map<InitiativeEntitasPertaminaViewModel>(_context.TbltInitiativeEntitasPertaminas.Where(x => x.InitiativeId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public InitiativeStreamBusinessViewModel GetInitiativeStreamBusinessById(Guid guid)
        {
            InitiativeStreamBusinessViewModel result = _mapper.Map<InitiativeStreamBusinessViewModel>(_context.TbltInitiativeStreamBusinesses.Where(x => x.InitiativeId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public InitiativeNegaraMitraViewModel GetInitiativeNegaraMitraById(Guid guid)
        {
            InitiativeNegaraMitraViewModel result = _mapper.Map<InitiativeNegaraMitraViewModel>(_context.TbltInitiativeNegaraMitras.Where(x => x.InitiativeId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public InitiativePicFungsiViewModel GetInitiativePicFungsiById(Guid guid)
        {
            InitiativePicFungsiViewModel result = _mapper.Map<InitiativePicFungsiViewModel>(_context.TbltInitiativePicFungsis.Where(x => x.InitiativeId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public List<InitiativeLokasiProyekViewModel> GetInitiativeLokasiById(Guid guid)
        {
            List<InitiativeLokasiProyekViewModel> result = _mapper.Map<List<InitiativeLokasiProyekViewModel>>(_context.TbltInitiativeLokasiProyeks.Where(x => x.InitiativeId == guid && x.DeletedDate == null).ToList().OrderBy(x => x.LokasiProyek)); ;
            return result;
        }
        public List<InitiativePartnerViewModel> GetInitiativePartnerById(Guid guid)
        {
            List<InitiativePartnerViewModel> result = _mapper.Map<List<InitiativePartnerViewModel>>(_context.TbltInitiativePartners.Where(x => x.InitiativeId == guid && x.DeletedDate == null).ToList().OrderBy(x => x.PartnerName));
            return result;
        }
        public InitiativeKementrianLembagaViewModel GetInitiativeKementrianLembagaById(Guid guid)
        {
            InitiativeKementrianLembagaViewModel result = _mapper.Map<InitiativeKementrianLembagaViewModel>(_context.TbltInitiativeKementrianLembagas.Where(x => x.InitiativeId == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public void Update(InitiativeViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TbltInitiative record = HasRecordById(model.Id);
            record.OpportunityId = model.OpportunityId;
            record.JudulInisiasi = model.JudulInisiasi;
            record.InterestId = model.InterestId;
            record.InitiativeStageId = model.InitiativeStageId;
            record.InitiativeStatusId = model.InitiativeStatusId;
            record.InitiativeTypesId = model.InitiativeTypesId;
            record.NilaiProyek = model.NilaiProyek;
            record.TargetWaktuProyek = model.TargetWaktuProyek;
            record.Scope = model.Scope;
            record.Progress = model.Progress;
            record.IsuKendala = model.IsuKendala;
            record.TindakLanjut = model.TindakLanjut;
            record.Referal = model.Referal;
            record.PotensiEskalasi = model.PotensiEskalasi;
            record.CatatanTambahan = model.CatatanTambahan;
            record.SupportPemerintah = model.SupportPemerintah;
            record.ValueForIndonesia = model.ValueForIndonesia;
            record.AgreementId = model.AgreementId;
            record.IsDraft = false;
            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TbltInitiative>().Update(record);
            _context.SaveChanges();

        }
        public TbltInitiative HasRecordById(Guid guid)
        {
            TbltInitiative result = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.Id == guid).FirstOrDefault();
            return result;
        }

        #region Delete
        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TbltInitiative record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TbltInitiative>().Update(record);
            _context.SaveChanges();
        }
        public void DeletePicFungsi(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltInitiativePicFungsi> records = _context.TbltInitiativePicFungsis.Where(x => x.DeletedDate == null && x.InitiativeId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltInitiativePicFungsi>().Update(rec);
                _context.SaveChanges();
            }

        }
        public void DeleteEntitas(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltInitiativeEntitasPertamina> records = _context.TbltInitiativeEntitasPertaminas.Where(x => x.DeletedDate == null && x.InitiativeId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltInitiativeEntitasPertamina>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeletePartner(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltInitiativePartner> records = _context.TbltInitiativePartners.Where(x => x.DeletedDate == null && x.InitiativeId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltInitiativePartner>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteNegaraMitra(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltInitiativeNegaraMitra> records = _context.TbltInitiativeNegaraMitras.Where(x => x.DeletedDate == null && x.InitiativeId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltInitiativeNegaraMitra>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteLokasiProyek(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltInitiativeLokasiProyek> records = _context.TbltInitiativeLokasiProyeks.Where(x => x.DeletedDate == null && x.InitiativeId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltInitiativeLokasiProyek>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteStreamBusiness(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            List<TbltInitiativeStreamBusiness> records = _context.TbltInitiativeStreamBusinesses.Where(x => x.DeletedDate == null && x.InitiativeId == guid).ToList();
            foreach (var rec in records)
            {
                rec.UpdateBy = userName;
                rec.UpdateDate = now;
                rec.DeletedDate = now;
                _context.Set<TbltInitiativeStreamBusiness>().Update(rec);
                _context.SaveChanges();
            }
        }
        public void DeleteExistingStreamBusiness(Guid guid)
        {
            var entitas = _context.TbltInitiativeStreamBusinesses.Where(x => x.InitiativeId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingFungsiPic(Guid guid)
        {
            var entitas = _context.TbltInitiativePicFungsis.Where(x => x.InitiativeId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingEntitasPertamina(Guid guid)
        {
            var entitas = _context.TbltInitiativeEntitasPertaminas.Where(x => x.InitiativeId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingNegaraMitra(Guid guid)
        {
            var entitas = _context.TbltInitiativeNegaraMitras.Where(x => x.InitiativeId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingPartner(Guid guid)
        {
            var entitas = _context.TbltInitiativePartners.Where(x => x.InitiativeId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingLokasiProyek(Guid guid)
        {
            var entitas = _context.TbltInitiativeLokasiProyeks.Where(x => x.InitiativeId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }
        public void DeleteExistingKementrianLembaga(Guid guid)
        {
            var entitas = _context.TbltInitiativeKementrianLembagas.Where(x => x.InitiativeId == guid).ToList();
            _context.RemoveRange(entitas);
            _context.SaveChanges();
        }

        #endregion

        #region Read
        public InterestViewModel GetReadInitiativeInterestById(Guid guid)
        {
            InterestViewModel result = _context.TbltInitiatives.Where(x => x.Id == guid && x.DeletedDate == null)
                .LeftJoin(_context.TblmInterests, x => x.InterestId, y => y.Id, (x, y) =>
                new InterestViewModel
                {
                    Id = x.InterestId.HasValue ? x.InterestId.Value : Guid.Empty,
                    Name = y.Name
                }).FirstOrDefault();
            return result;
        }
        public InitiativeStatusViewModel GetReadInitiativeStatusById(Guid guid)
        {
            InitiativeStatusViewModel result = _context.TbltInitiatives.Where(x => x.Id == guid && x.DeletedDate == null)
                .Join(_context.TblmInitiativeStatuses, x => x.InitiativeStatusId, y => y.Id, (x, y) => new
                InitiativeStatusViewModel
                {
                    Id = x.InitiativeStatusId.Value,
                    StatusName = y.StatusName,
                    ColorName = y.ColorName
                }).FirstOrDefault();
            return result;
        }
        public InitiativeTypesViewModel GetReadInitiativeTypesById(Guid guid)
        {
            InitiativeTypesViewModel result = _context.TbltInitiatives.Where(x => x.Id == guid && x.DeletedDate == null)
                .Join(_context.TblmInitiativeTypes, x => x.InitiativeTypesId, y => y.Id, (x, y) => new
                InitiativeTypesViewModel
                {
                    Id = x.InitiativeTypesId.Value,
                    StatusName = y.StatusName
                }).FirstOrDefault();
            return result;
        }
        public InitiativeStageViewModel GetReadInitiativeStageById(Guid guid)
        {
            InitiativeStageViewModel result = _context.TbltInitiatives.Where(x => x.Id == guid && x.DeletedDate == null)
                .Join(_context.TblmInitiativeStages, x => x.InitiativeStageId, y => y.Id, (x, y) => new
                InitiativeStageViewModel
                {
                    Id = x.InitiativeStageId.Value,
                    Name = y.Name
                }).FirstOrDefault();
            return result;
        }
        public List<PicFungsiViewModel> GetReadPicFungsiById(Guid guid)
        {
            List<PicFungsiViewModel> result = _context.TbltInitiativePicFungsis
                .Where(x => x.InitiativeId == guid)
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
                    })
                .ToList();
            return result;
        }
        public List<EntitasPertaminaViewModel> GetReadEntitasPertamina(Guid guid)
        {
            List<EntitasPertaminaViewModel> result = _context.TbltInitiativeEntitasPertaminas
                .Where(x => x.InitiativeId == guid)
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
        public List<string> GetReadPartners(Guid guid)
        {
            List<string> result = _context.TbltInitiativePartners.Where(x => x.InitiativeId == guid).Select(x => x.PartnerName).ToList();

            return result;
        }
        public List<NegaraMitraViewModel> GetNegaraMitraById(Guid guid)
        {
            List<NegaraMitraViewModel> result = _context.TbltInitiativeNegaraMitras
                .Where(x => x.InitiativeId == guid)
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
        public List<string> GetReadLokasiProyekById(Guid guid)
        {
            List<string> result = _context.TbltInitiativeLokasiProyeks.Where(x => x.InitiativeId == guid).Select(x => x.LokasiProyek).ToList();

            return result;
        }
        public AgreementViewModel GetReadRelatedAgreementById(Guid guid)
        {
            AgreementViewModel result = _context.TbltInitiatives.Where(x => x.Id == guid)
                .Join(_context.TbltAgreements, x => x.AgreementId, y => y.Id,
                (x, y) =>
                new AgreementViewModel
                {
                    Id = y.Id,
                    JudulPerjanjian = y.JudulPerjanjian,
                    KodeAgreement = y.KodeAgreement
                }).FirstOrDefault();
            return result;
        }
        public List<StreamBusinessViewModel> GetReadStreamBusinessById(Guid guid)
        {
            List<StreamBusinessViewModel> result = _context.TbltInitiativeStreamBusinesses
                .Where(x => x.InitiativeId == guid)
                .Join(_context.TblmStreamBusinesses, x => x.StreamBusinessId, y => y.Id, (x, y) =>
                    new StreamBusinessViewModel
                    {
                        Id = x.StreamBusinessId.Value,
                        Name = y.Name
                    })
                .ToList();
            return result;
        }
        #endregion
        public List<InitiativeHighPriorityViewModel> GetIntitativeHighPriority()
        {
            List<InitiativeHighPriorityViewModel> result = new List<InitiativeHighPriorityViewModel>();

            result = _mapper.Map<List<InitiativeHighPriorityViewModel>>(_context.TbltInitiativeHighPriorities.OrderByDescending(x => x.Sequence).Take(5).OrderBy(x => x.Sequence).ToList());

            return result;
        }

        public List<InitiativeMilestoneTimelineViewModel> GetIntitativeMilestoneTimelineById(Guid guid)
        {
            List<InitiativeMilestoneTimelineViewModel> result = new List<InitiativeMilestoneTimelineViewModel>();
            result = _mapper.Map<List<InitiativeMilestoneTimelineViewModel>>(_context.TbltInitiativeMilestoneTimelines.Where(x => x.InitiativeId == guid).ToList());

            return result;
        }
    }
}
