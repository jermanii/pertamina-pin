using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Core;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Utility.Wording.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class PotentialInitiativeDashboardRepository : IPotentialInitiativeDashboardRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;
        protected readonly IUpdatedWordingUtility _updatedWordingUtility;

        public PotentialInitiativeDashboardRepository(DB_PINMContext context,
                                                      IMapper mapper,
                                                      IUpdatedWordingUtility updatedWordingUtility)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _updatedWordingUtility = updatedWordingUtility;
        }
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
                .Join(_context.TbltInitiativePicFungsis.Join(_context.TblmPicLevels.Where(x => x.IsLead == true), p => p.PicLevelId, l => l.Id, (p, l) => new
                {
                    picTran = p,
                    level = l
                }).Join(_context.TblmPicFungsis, f => f.picTran.PicFungsiId, y => y.Id, (f, y) => new
                {
                    trx = f,
                    masterFungsi = y
                }), x => x.Id, o => o.trx.picTran.InitiativeId, (x, o) => new InitiativeViewModel
                {
                    Id = x.Id,
                    IsDraft = x.IsDraft,
                    JudulInisiasi = x.JudulInisiasi,
                    QueryHshId = x.QueryHshId,
                    QueryEntitasPertaminaId = x.QueryEntitasPertaminaId,
                    QueryEntitasPertaminaName = x.QueryEntitasPertaminaName,
                    QueryStreamBusinessId = x.QueryStreamBusinessId,
                    QueryStreamBusinessName = x.QueryStreamBusinessName,
                    QueryNegaraMitraId = x.QueryNegaraMitraId,
                    QueryNamaNegara = x.QueryNamaNegara,
                    QueryKawasanMitraId = x.QueryKawasanMitraId,
                    QueryNamaKawasan = x.QueryNamaKawasan,
                    QueryInitiativeStageId = x.QueryInitiativeStageId,
                    QueryInitiativeStageName = x.QueryInitiativeStageName,
                    QueryInitiativeStatusId = x.QueryInitiativeStatusId,
                    QueryInitiativeStatusName = x.QueryInitiativeStatusName,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate == null ? x.CreateDate : x.UpdateDate,
                    NilaiProyek = x.NilaiProyek,
                    TargetWaktuProyek = x.TargetWaktuProyek,
                    Scope = x.Scope,
                    Progress = x.Progress,
                    IsuKendala = x.IsuKendala,
                    Referal = x.Referal,
                    PotensiEskalasi = x.PotensiEskalasi,
                    CatatanTambahan = x.CatatanTambahan,
                    SupportPemerintah = x.SupportPemerintah,
                    ValueForIndonesia = x.ValueForIndonesia
                })
                .OrderByDescending(x => x.IsDraft)
                .AsQueryable();
        }
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
                    CreateDate = y.CreateDate,
                    PotentialRevenuePerYear = y.PotentialRevenuePerYear,
                    Capex = y.Capex
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

        public HubHeadViewModel GetHubHead(Guid guid)
        {
            var query = _context.TbltInitiativeNegaraMitras.Where(x => x.InitiativeId == guid);

            var joinQuery = query
                .Join(_context.TblmNegaraMitras, inm => inm.NegaraMitraId, mn => mn.Id, (inm, mn) => new
                {
                    tblmMitra = mn,
                    tbltInitiative = inm
                })
                .Join(_context.TblmHubHeads.Join(_context.TblmHubs, mhh => mhh.HubId, mh => mh.Id, (mhh, mh) => new HubHeadViewModel
                {
                    Id = mhh.Id,
                    HubId = mh.Id,
                    ContactNumber = mhh.ContactNumber,
                    IsActive = mhh.IsActive,
                    Name = mhh.Name,
                    UserEmail = mhh.UserEmail
                }), aa => aa.tblmMitra.HubId, bb => bb.HubId, (aa, bb) => new HubHeadViewModel
                {
                    Id = bb.Id,
                    HubId = bb.HubId,
                    ContactNumber = bb.ContactNumber,
                    IsActive = bb.IsActive,
                    Name = bb.Name,
                    UserEmail = bb.UserEmail
                });

            var result = joinQuery.FirstOrDefault();

            return result;
        }

        public PicFungsiViewModel GetPicLead(Guid guid)
        {
            var query = _context.TbltInitiativePicFungsis.Where(x => x.InitiativeId == guid && x.DeletedDate == null)
                .Join(_context.TblmPicFungsis, ipf => ipf.PicFungsiId, mf => mf.Id, (ipf, mf) => new
                {
                    initiatveFungsi = ipf,
                    mstPicFungsi = mf
                })
                .Join(_context.TblmPicLevels.Where(x => x.DeletedDate == null && x.IsLead == true), i => i.initiatveFungsi.PicLevelId, j => j.Id, (i, j) => new PicFungsiViewModel
                {
                    Id = i.mstPicFungsi.Id,
                    ActiveStatus = i.mstPicFungsi.ActiveStatus,
                    Email = i.mstPicFungsi.Email,
                    FungsiId = i.mstPicFungsi.FungsiId,
                    PicName = i.mstPicFungsi.PicName,
                    Phone = i.mstPicFungsi.Phone
                });

            var result = query.FirstOrDefault();

            return result;
        }

        public KementrianLembagaViewModel GetKementrianLembaga(Guid guid)
        {
            var query = _context.TbltInitiativeKementrianLembagas.Where(x => x.InitiativeId == guid && x.DeletedDate == null)
                .Join(_context.TblmKementrianLembagas, ik => ik.KementrianLembagaId, mk => mk.Id, (ik, mk) => new KementrianLembagaViewModel
                {
                    Id = mk.Id,
                    Description = mk.Description,
                    LembagaName = mk.LembagaName,
                    CreateBy = mk.CreateBy,
                    CreateDate = mk.CreateDate,
                    DeletedDate = mk.DeletedDate
                });

            var result = query.FirstOrDefault();

            return result;
        }

        public async Task<IQueryable<InitiativeViewModel>> QueryInitiativeWithFilter(InitiativeViewModel decodeModel)
        {
            IQueryable<InitiativeViewModel> result = null;

            var query = _context.TbltInitiatives.Where(x => x.DeletedDate == null && x.IsDraft == false).AsQueryable();

            query = query.Where(x => x.DeletedDate == null && x.IsDraft == false);

            IQueryable<InitiativeViewModel> joinquery = QueryList(query);

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
        public List<InitiativeStageViewModel> GetRecordsInitiativeStage()
        {
            IQueryable<TblmInitiativeStage> result = _context.TblmInitiativeStages.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<InitiativeStageViewModel>>(result);
        }
        public List<HshViewModel> GetRecordsHsh()
        {
            IQueryable<TblmHsh> result = _context.TblmHshes.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<HshViewModel>>(result);
        }
        public List<InitiativeStatusViewModel> GetRecordsInitiativeStatus()
        {
            IQueryable<TblmInitiativeStatus> result = _context.TblmInitiativeStatuses.Where(x => x.DeletedDate == null).OrderBy(x => x.OrderSeq).AsQueryable();

            return _mapper.Map<List<InitiativeStatusViewModel>>(result);
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

        public List<PotentialInitiativeHighPriorityViewModel> GetHighPriority(string wwwroot, SortViewModel sort)
        {
            List<PotentialInitiativeHighPriorityViewModel> result = new List<PotentialInitiativeHighPriorityViewModel>();

            var query = _context.TbltInitiativeHighPriorities.Where(x => x.DeletedDate == null).Take(5).OrderBy(x => x.Sequence).AsQueryable();

            var initiative = query
                .LeftJoin(_context.TbltInitiatives
                .LeftJoin(_context.TblmInitiativeStatuses, c => c.InitiativeStatusId, d => d.Id, (c, d) => new PotentialInitiativeHighPriorityViewModel
                {
                    InitiativeId = c.Id,
                    JudulInisiasi = c.JudulInisiasi,
                    Capex = c.Capex,
                    RevenuePerYear = c.PotentialRevenuePerYear,
                    InitiativeStatusId = d.Id,
                    InitiativeStatus = d.StatusName,
                    InitiativeStatusColorName = d.ColorName
                })
                , a => a.InitiativeId, b => b.InitiativeId, (a, b) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = a.Id,
                    InitiativeId = b.InitiativeId,
                    JudulInisiasi = b.JudulInisiasi,
                    Capex = b.Capex,
                    RevenuePerYear = b.RevenuePerYear,
                    InitiativeStatusId = b.InitiativeStatusId,
                    InitiativeStatus = b.InitiativeStatus,
                    InitiativeStatusColorName = b.InitiativeStatusColorName,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate

                }).AsQueryable();

            var initiativeNegaraMitra = initiative.LeftJoin(_context.TbltInitiativeNegaraMitras, a => a.InitiativeId, b => b.InitiativeId, (a, b) => new PotentialInitiativeHighPriorityViewModel
            {
                Id = a.Id,
                InitiativeId = a.InitiativeId,
                JudulInisiasi = a.JudulInisiasi,
                Capex = a.Capex,
                RevenuePerYear = a.RevenuePerYear,
                InitiativeStatusId = a.InitiativeStatusId,
                InitiativeStatus = a.InitiativeStatus,
                InitiativeStatusColorName = a.InitiativeStatusColorName,
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,
                NegaraMitraId = b.NegaraMitraId
            }).AsQueryable();

            var hubMitra = initiativeNegaraMitra
                .LeftJoin(_context.TblmNegaraMitras, a => a.NegaraMitraId, b => b.Id, (a, b) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = a.Id,
                    InitiativeId = a.InitiativeId,
                    JudulInisiasi = a.JudulInisiasi,
                    Capex = a.Capex,
                    RevenuePerYear = a.RevenuePerYear,
                    InitiativeStatusId = a.InitiativeStatusId,
                    InitiativeStatus = a.InitiativeStatus,
                    InitiativeStatusColorName = a.InitiativeStatusColorName,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    NegaraMitraId = a.NegaraMitraId,
                    NegaraMitraName = b.NamaNegara,
                    Flag = b.Flag,
                    ExistsFlag = File.Exists(wwwroot + b.Flag),
                    PathFlag = wwwroot + b.Flag,
                    HubId = b.HubId
                })
                .LeftJoin(_context.TblmHubs, a => a.HubId, b => b.Id, (a, b) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = a.Id,
                    InitiativeId = a.InitiativeId,
                    JudulInisiasi = a.JudulInisiasi,
                    Capex = a.Capex,
                    RevenuePerYear = a.RevenuePerYear,
                    InitiativeStatusId = a.InitiativeStatusId,
                    InitiativeStatus = a.InitiativeStatus,
                    InitiativeStatusColorName = a.InitiativeStatusColorName,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    NegaraMitraId = a.NegaraMitraId,
                    NegaraMitraName = a.NegaraMitraName,
                    Flag = a.Flag,
                    ExistsFlag = File.Exists(wwwroot + a.Flag),
                    PathFlag = wwwroot + a.Flag,
                    HubId = b.Id,
                    HubName = b.Name
                }).AsQueryable();

            var hubHead = hubMitra.LeftJoin(_context.TblmHubHeads, a => a.HubId, b => b.HubId, (a, b) => new PotentialInitiativeHighPriorityViewModel
            {
                Id = a.Id,
                InitiativeId = a.InitiativeId,
                JudulInisiasi = a.JudulInisiasi,
                Capex = a.Capex,
                RevenuePerYear = a.RevenuePerYear,
                InitiativeStatusId = a.InitiativeStatusId,
                InitiativeStatus = a.InitiativeStatus,
                InitiativeStatusColorName = a.InitiativeStatusColorName,
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,
                NegaraMitraId = a.NegaraMitraId,
                NegaraMitraName = a.NegaraMitraName,
                Flag = a.Flag,
                ExistsFlag = File.Exists(wwwroot + a.Flag),
                PathFlag = wwwroot + a.Flag,
                HubId = a.HubId,
                HubName = a.HubName,
                HubHeadId = b.Id,
                HubHeadName = b.Name
            }).AsQueryable();

            var picFungsi = hubHead
                .LeftJoin(_context.TbltInitiativePicFungsis.Where(x => x.DeletedDate == null)
                .Join(_context.TblmPicLevels.Where(x => x.DeletedDate == null && x.IsLead == true), m => m.PicLevelId, n => n.Id, (m, n) => new
                {
                    tranLevel = m,
                    masterLevel = n
                }).Join(_context.TblmPicFungsis.Where(x => x.DeletedDate == null), o => o.tranLevel.PicFungsiId, p => p.Id, (o, p) => new
                {
                    tranLevelFungsi = o,
                    masterLevelFungsi = p
                }), q => q.InitiativeId, r => r.tranLevelFungsi.tranLevel.InitiativeId, (o, p) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = o.Id,
                    InitiativeId = o.InitiativeId,
                    JudulInisiasi = o.JudulInisiasi,
                    Capex = o.Capex,
                    RevenuePerYear = o.RevenuePerYear,
                    InitiativeStatusId = o.InitiativeStatusId,
                    InitiativeStatus = o.InitiativeStatus,
                    InitiativeStatusColorName = o.InitiativeStatusColorName,
                    CreatedDate = o.CreatedDate,
                    UpdatedDate = o.UpdatedDate,
                    NegaraMitraId = o.NegaraMitraId,
                    NegaraMitraName = o.NegaraMitraName,
                    Flag = o.Flag,
                    ExistsFlag = File.Exists(wwwroot + o.Flag),
                    PathFlag = wwwroot + o.Flag,
                    HubId = o.HubId,
                    HubName = o.HubName,
                    HubHeadId = o.HubHeadId,
                    HubHeadName = o.HubHeadName,
                    PicFungsiId = p.tranLevelFungsi.tranLevel.PicFungsiId,
                    PicFungsiName = p.masterLevelFungsi.PicName,
                    UpdatedWording = _updatedWordingUtility.GetUpdatedWording(o.CreatedDate, o.UpdatedDate)
                }).AsQueryable();

            var initiativeKementrianLembaga = picFungsi.LeftJoin(_context.TbltInitiativeKementrianLembagas, a => a.InitiativeId, b => b.InitiativeId, (a, b) => new PotentialInitiativeHighPriorityViewModel
            {
                Id = a.Id,
                InitiativeId = a.InitiativeId,
                JudulInisiasi = a.JudulInisiasi,
                Capex = a.Capex,
                RevenuePerYear = a.RevenuePerYear,
                InitiativeStatusId = a.InitiativeStatusId,
                InitiativeStatus = a.InitiativeStatus,
                InitiativeStatusColorName = a.InitiativeStatusColorName,
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,
                NegaraMitraId = a.NegaraMitraId,
                NegaraMitraName = a.NegaraMitraName,
                Flag = a.Flag,
                ExistsFlag = File.Exists(wwwroot + a.Flag),
                PathFlag = wwwroot + a.Flag,
                HubId = a.HubId,
                HubName = a.HubName,
                HubHeadId = a.HubHeadId,
                HubHeadName = a.HubHeadName,
                PicFungsiId = a.PicFungsiId,
                PicFungsiName = a.PicFungsiName,
                UpdatedWording = a.UpdatedWording,
                KementrianLembagaId = b.KementrianLembagaId
            }).LeftJoin(_context.TblmKementrianLembagas, c => c.KementrianLembagaId, d => d.Id, (c, d) => new PotentialInitiativeHighPriorityViewModel
            {
                Id = c.Id,
                InitiativeId = c.InitiativeId,
                JudulInisiasi = c.JudulInisiasi,
                Capex = c.Capex,
                RevenuePerYear = c.RevenuePerYear,
                InitiativeStatusId = c.InitiativeStatusId,
                InitiativeStatus = c.InitiativeStatus,
                InitiativeStatusColorName = c.InitiativeStatusColorName,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                NegaraMitraId = c.NegaraMitraId,
                NegaraMitraName = c.NegaraMitraName,
                Flag = c.Flag,
                ExistsFlag = File.Exists(wwwroot + c.Flag),
                PathFlag = wwwroot + c.Flag,
                HubId = c.HubId,
                HubName = c.HubName,
                HubHeadId = c.HubHeadId,
                HubHeadName = c.HubHeadName,
                PicFungsiId = c.PicFungsiId,
                PicFungsiName = c.PicFungsiName,
                UpdatedWording = c.UpdatedWording,
                KementrianLembagaId = d.Id,
                KementrianLembagaName = d.LembagaName,
                KementrianLembagaDescription = d.Description
            }).AsQueryable();

            if (!(string.IsNullOrEmpty(sort.SortColumn) && string.IsNullOrEmpty(sort.SortOrder)))
            {
                if (sort.SortOrder == "asc")
                    initiativeKementrianLembaga = initiativeKementrianLembaga.OrderBy(c => EF.Property<string>(c, sort.SortColumn));
                else
                    initiativeKementrianLembaga = initiativeKementrianLembaga.OrderByDescending(c => EF.Property<string>(c, sort.SortColumn));
            }

            result = initiativeKementrianLembaga.ToList();

            return result;

        }

        public List<PotentialInitiativeHighPriorityViewModel> GetHighPriority()
        {
            List<PotentialInitiativeHighPriorityViewModel> result = new List<PotentialInitiativeHighPriorityViewModel>();

            var query = _context.TbltInitiativeHighPriorities.Where(x => x.DeletedDate == null).Take(5).OrderBy(x => x.Sequence).AsQueryable();

            var initiative = query
                .Join(_context.TbltInitiatives
                .Join(_context.TblmInitiativeStatuses, c => c.InitiativeStatusId, d => d.Id, (c, d) => new PotentialInitiativeHighPriorityViewModel
                {
                    InitiativeId = c.Id,
                    JudulInisiasi = c.JudulInisiasi,
                    Capex = c.Capex,
                    RevenuePerYear = c.PotentialRevenuePerYear,
                    InitiativeStatusId = d.Id,
                    InitiativeStatus = d.StatusName,
                    InitiativeStatusColorName = d.ColorName
                })
                , a => a.InitiativeId, b => b.InitiativeId, (a, b) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = a.Id,
                    InitiativeId = b.InitiativeId,
                    JudulInisiasi = b.JudulInisiasi,
                    Capex = b.Capex,
                    RevenuePerYear = b.RevenuePerYear,
                    InitiativeStatusId = b.InitiativeStatusId,
                    InitiativeStatus = b.InitiativeStatus,
                    InitiativeStatusColorName = b.InitiativeStatusColorName,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate

                }).AsQueryable();

            var initiativeNegaraMitra = initiative.Join(_context.TbltInitiativeNegaraMitras, a => a.InitiativeId, b => b.InitiativeId, (a, b) => new PotentialInitiativeHighPriorityViewModel
            {
                Id = a.Id,
                InitiativeId = a.InitiativeId,
                JudulInisiasi = a.JudulInisiasi,
                Capex = a.Capex,
                RevenuePerYear = a.RevenuePerYear,
                InitiativeStatusId = a.InitiativeStatusId,
                InitiativeStatus = a.InitiativeStatus,
                InitiativeStatusColorName = a.InitiativeStatusColorName,
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,
                NegaraMitraId = b.NegaraMitraId
            }).AsQueryable();

            var hubMitra = initiativeNegaraMitra
                .Join(_context.TblmNegaraMitras, a => a.NegaraMitraId, b => b.Id, (a, b) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = a.Id,
                    InitiativeId = a.InitiativeId,
                    JudulInisiasi = a.JudulInisiasi,
                    Capex = a.Capex,
                    RevenuePerYear = a.RevenuePerYear,
                    InitiativeStatusId = a.InitiativeStatusId,
                    InitiativeStatus = a.InitiativeStatus,
                    InitiativeStatusColorName = a.InitiativeStatusColorName,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    NegaraMitraId = a.NegaraMitraId,
                    NegaraMitraName = b.NamaNegara,
                    Flag = b.Flag,
                    HubId = b.HubId
                })
                .Join(_context.TblmHubs, a => a.HubId, b => b.Id, (a, b) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = a.Id,
                    InitiativeId = a.InitiativeId,
                    JudulInisiasi = a.JudulInisiasi,
                    Capex = a.Capex,
                    RevenuePerYear = a.RevenuePerYear,
                    InitiativeStatusId = a.InitiativeStatusId,
                    InitiativeStatus = a.InitiativeStatus,
                    InitiativeStatusColorName = a.InitiativeStatusColorName,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    NegaraMitraId = a.NegaraMitraId,
                    NegaraMitraName = a.NegaraMitraName,
                    Flag = a.Flag,
                    HubId = b.Id,
                    HubName = b.Name
                }).AsQueryable();

            var hubHead = hubMitra.Join(_context.TblmHubHeads, a => a.HubId, b => b.HubId, (a, b) => new PotentialInitiativeHighPriorityViewModel
            {
                Id = a.Id,
                InitiativeId = a.InitiativeId,
                JudulInisiasi = a.JudulInisiasi,
                Capex = a.Capex,
                RevenuePerYear = a.RevenuePerYear,
                InitiativeStatusId = a.InitiativeStatusId,
                InitiativeStatus = a.InitiativeStatus,
                InitiativeStatusColorName = a.InitiativeStatusColorName,
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,
                NegaraMitraId = a.NegaraMitraId,
                NegaraMitraName = a.NegaraMitraName,
                Flag = a.Flag,
                HubId = a.HubId,
                HubName = a.HubName,
                HubHeadId = b.Id,
                HubHeadName = b.Name
            }).AsQueryable();

            var picFungsi = hubHead
                .Join(_context.TbltInitiativePicFungsis.Where(x => x.DeletedDate == null)
                .Join(_context.TblmPicLevels.Where(x => x.DeletedDate == null && x.IsLead == true), m => m.PicLevelId, n => n.Id, (m, n) => new
                {
                    tranLevel = m,
                    masterLevel = n
                }).Join(_context.TblmPicFungsis.Where(x => x.DeletedDate == null), o => o.tranLevel.PicFungsiId, p => p.Id, (o, p) => new
                {
                    tranLevelFungsi = o,
                    masterLevelFungsi = p
                }), q => q.InitiativeId, r => r.tranLevelFungsi.tranLevel.InitiativeId, (o, p) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = o.Id,
                    InitiativeId = o.InitiativeId,
                    JudulInisiasi = o.JudulInisiasi,
                    Capex = o.Capex,
                    RevenuePerYear = o.RevenuePerYear,
                    InitiativeStatusId = o.InitiativeStatusId,
                    InitiativeStatus = o.InitiativeStatus,
                    InitiativeStatusColorName = o.InitiativeStatusColorName,
                    CreatedDate = o.CreatedDate,
                    UpdatedDate = o.UpdatedDate,
                    NegaraMitraId = o.NegaraMitraId,
                    NegaraMitraName = o.NegaraMitraName,
                    Flag = o.Flag,
                    HubId = o.HubId,
                    HubName = o.HubName,
                    HubHeadId = o.HubHeadId,
                    HubHeadName = o.HubHeadName,
                    PicFungsiId = p.tranLevelFungsi.tranLevel.PicFungsiId,
                    PicFungsiName = p.masterLevelFungsi.PicName,
                    UpdatedWording = _updatedWordingUtility.GetUpdatedWording(o.CreatedDate, o.UpdatedDate)
                }).AsQueryable();

            var initiativeKementrianLembaga = picFungsi.Join(_context.TbltInitiativeKementrianLembagas, a => a.InitiativeId, b => b.InitiativeId, (a, b) => new PotentialInitiativeHighPriorityViewModel
            {
                Id = a.Id,
                InitiativeId = a.InitiativeId,
                JudulInisiasi = a.JudulInisiasi,
                Capex = a.Capex,
                RevenuePerYear = a.RevenuePerYear,
                InitiativeStatusId = a.InitiativeStatusId,
                InitiativeStatus = a.InitiativeStatus,
                InitiativeStatusColorName = a.InitiativeStatusColorName,
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,
                NegaraMitraId = a.NegaraMitraId,
                NegaraMitraName = a.NegaraMitraName,
                Flag = a.Flag,
                HubId = a.HubId,
                HubName = a.HubName,
                HubHeadId = a.HubHeadId,
                HubHeadName = a.HubHeadName,
                PicFungsiId = a.PicFungsiId,
                PicFungsiName = a.PicFungsiName,
                UpdatedWording = a.UpdatedWording,
                KementrianLembagaId = b.KementrianLembagaId
            }).Join(_context.TblmKementrianLembagas, c => c.KementrianLembagaId, d => d.Id, (c, d) => new PotentialInitiativeHighPriorityViewModel
            {
                Id = c.Id,
                InitiativeId = c.InitiativeId,
                JudulInisiasi = c.JudulInisiasi,
                Capex = c.Capex,
                RevenuePerYear = c.RevenuePerYear,
                InitiativeStatusId = c.InitiativeStatusId,
                InitiativeStatus = c.InitiativeStatus,
                InitiativeStatusColorName = c.InitiativeStatusColorName,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                NegaraMitraId = c.NegaraMitraId,
                NegaraMitraName = c.NegaraMitraName,
                Flag = c.Flag,
                HubId = c.HubId,
                HubName = c.HubName,
                HubHeadId = c.HubHeadId,
                HubHeadName = c.HubHeadName,
                PicFungsiId = c.PicFungsiId,
                PicFungsiName = c.PicFungsiName,
                UpdatedWording = c.UpdatedWording,
                KementrianLembagaId = d.Id,
                KementrianLembagaName = d.LembagaName,
                KementrianLembagaDescription = d.Description
            }).AsQueryable();

            result = initiativeKementrianLembaga.ToList();

            return result;

        }
        public async Task<List<PotentialInitiativesDashboardHubGroupViewModel>> GetHubGrouppingCountries()
        {
            List<PotentialInitiativesDashboardHubGroupViewModel> result = new List<PotentialInitiativesDashboardHubGroupViewModel>();

            var query = await _context.TbltInitiatives.Where(x => x.DeletedDate == null)
                         .Join(_context.TbltInitiativeNegaraMitras, a => a.Id, b => b.InitiativeId, (a, b) => new
                         {
                             initiative = a,
                             initiativeMitra = b
                         })
                         .Join(_context.TblmHubs
                               .Join(_context.TblmNegaraMitras, c => c.Id, d => d.HubId, (c, d) => new
                               {
                                   mstHub = c,
                                   mstMitra = d
                               }),
                             e => e.initiativeMitra.NegaraMitraId, f => f.mstMitra.Id, (e, f) => new
                             {
                                 tranInitiative = e,
                                 joinHub = f
                             })
                         .ToListAsync(); // Fetch the data first

            result = query
                .GroupBy(h => new
                {
                    HubId = h.joinHub.mstHub.Id,
                    HubName = h.joinHub.mstHub.Name,
                    HubColor = h.joinHub.mstHub.ColorHexa,
                    HubLatitude = h.joinHub.mstHub.Latitude,
                    HubLongitude = h.joinHub.mstHub.Longitude
                })
                .Select(g => new PotentialInitiativesDashboardHubGroupViewModel
                {
                    Hub = new HubViewModel
                    {
                        Name = g.Key.HubName,
                        ColorHexa = g.Key.HubColor,
                        Latitude = g.Key.HubLatitude,
                        Longitude = g.Key.HubLongitude
                    },
                    GrouppedCountriesAcronym = g.SelectMany(h => _context.TblmNegaraMitraInfomations
                        .Where(i => i.NegaraMitraId == h.joinHub.mstMitra.Id)
                        .Select(i => i.CountryAcronyms)).Distinct().ToList()
                }).ToList();

            return result;
        }

        public async Task<List<string>> GetCountriesMap()
        {
            List<string> result = new List<string>();

            var query = _context.TbltInitiatives.Where(x => x.DeletedDate == null)
                .Join(_context.TbltInitiativeNegaraMitras
                    .Join(_context.TblmNegaraMitras, a => a.NegaraMitraId, b => b.Id, (a, b) => new
                    {
                        initiativeMitra = a,
                        mstMitra = b
                    })
                    .Join(_context.TblmNegaraMitraInfomations, c => c.mstMitra.Id, d => d.NegaraMitraId, (c, d) => new
                    {
                        joinedMitra = c,
                        mitraInfo = d
                    }), e => e.Id, f => f.joinedMitra.initiativeMitra.InitiativeId, (e, f) => new
                    {
                        Countries = f.mitraInfo.CountryAcronyms,
                    }).AsQueryable();

            return result = await query.Select(x => x.Countries).Distinct().ToListAsync();
        }
        public async Task<List<PotentialInitiativesDashboardMapPointerViewModel>> GetSubHoldingMap()
        {
            List<PotentialInitiativesDashboardMapPointerViewModel> result = new List<PotentialInitiativesDashboardMapPointerViewModel>();

            var query = _context.TbltInitiatives.Where(x => x.DeletedDate == null)
                .Join(_context.TbltInitiativeNegaraMitras, a => a.Id, b => b.InitiativeId, (a, b) => new
                {
                    header = a,
                    headerMitra = b
                })
                .Join(_context.TbltInitiativeEntitasPertaminas, c => c.header.Id, d => d.InitiativeId, (c, d) => new
                {
                    headerMitra = c.headerMitra,
                    headerEntitas = d
                })
                .Join(_context.TblmNegaraMitraInfomations.Where(x => x.DeletedDate == null), e => e.headerMitra.NegaraMitraId, f => f.NegaraMitraId, (e, f) => new
                {
                    CountryAcronyms = f.CountryAcronyms,
                    EntitasPertaminaId = e.headerEntitas.EntitasPertaminaId
                })
                .Join(_context.TblmEntitasPertaminas.Where(x => x.DeletedDate == null), g => g.EntitasPertaminaId, h => h.Id, (g, h) => new
                {
                    header = g,
                    detail = h,
                })
                .Join(_context.TblmHshes.Where(x => x.DeletedDate == null), e => e.detail.HshId, f => f.Id, (e, f) => new
                PotentialInitiativesDashboardMapPointerViewModel
                {
                    CountriesAcronym = e.header.CountryAcronyms,
                    SubHolding = f.Name,
                })
                .AsQueryable();

            return result = await query.ToListAsync();
        }

        public async Task<List<PicFungsiViewModel>> GetPicMembers(Guid guid)
        {
            var query = _context.TbltInitiativePicFungsis.Where(x => x.InitiativeId == guid && x.DeletedDate == null)
                .Join(_context.TblmPicFungsis, ipf => ipf.PicFungsiId, mf => mf.Id, (ipf, mf) => new
                {
                    initiatveFungsi = ipf,
                    mstPicFungsi = mf
                })
                .Join(_context.TblmPicLevels.Where(x => x.DeletedDate == null && x.IsLead == false), i => i.initiatveFungsi.PicLevelId, j => j.Id, (i, j) => new PicFungsiViewModel
                {
                    Id = i.mstPicFungsi.Id,
                    ActiveStatus = i.mstPicFungsi.ActiveStatus,
                    Email = i.mstPicFungsi.Email,
                    FungsiId = i.mstPicFungsi.FungsiId,
                    PicName = i.mstPicFungsi.PicName,
                    Phone = i.mstPicFungsi.Phone
                }).AsQueryable();

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<PotentialInitiativeHighPriorityViewModel> GetDetailHighPriorityById(Guid guid)
        {
            PotentialInitiativeHighPriorityViewModel result = new PotentialInitiativeHighPriorityViewModel();

            var query = _context.TbltInitiativeHighPriorities.Where(x => x.Id == guid && x.DeletedDate == null).AsQueryable();

            var joinQuery = query.
                Join(_context.TbltInitiatives
                         .Join(_context.TbltInitiativePartners, c => c.Id, d => d.InitiativeId, (c, d) => new PotentialInitiativeHighPriorityViewModel
                         {
                             InitiativeId = c.Id,
                             JudulInisiasi = c.JudulInisiasi,
                             Capex = c.Capex,
                             RevenuePerYear = c.PotentialRevenuePerYear,
                             CatatanTambahan = c.CatatanTambahan,
                             CreatedDate = c.CreateDate,
                             UpdatedDate = c.UpdateDate,
                             PartnerName = d.PartnerName,
                             InitiativeStatusId = c.InitiativeStatusId
                         }),                        
                    a => a.InitiativeId, b => b.InitiativeId, (a, b) => new PotentialInitiativeHighPriorityViewModel
                    {
                        Id = a.Id,
                        InitiativeId = b.InitiativeId,
                        JudulInisiasi = b.JudulInisiasi,
                        Capex = b.Capex,
                        RevenuePerYear = b.RevenuePerYear,
                        CreatedDate = b.CreatedDate,
                        UpdatedDate = b.UpdatedDate,
                        CatatanTambahan = b.CatatanTambahan,
                        PartnerName = b.PartnerName,
                        InitiativeStatusId = b.InitiativeStatusId,
                    })
                 .Join(_context.TbltInitiativeNegaraMitras, g => g.InitiativeId, h => h.InitiativeId, (g, h) => new PotentialInitiativeHighPriorityViewModel
                 {
                     Id = g.Id,
                     InitiativeId = g.InitiativeId,
                     JudulInisiasi = g.JudulInisiasi,
                     Capex = g.Capex,
                     RevenuePerYear = g.RevenuePerYear,
                     CatatanTambahan = g.CatatanTambahan,
                     CreatedDate = g.CreatedDate,
                     UpdatedDate = g.UpdatedDate,
                     PartnerName = g.PartnerName,
                     InitiativeStatusId = g.InitiativeStatusId,
                     NegaraMitraId = h.NegaraMitraId
                 })
                .Join(_context.TblmInitiativeStatuses, e => e.InitiativeStatusId, f => f.Id, (e, f) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = e.Id,
                    InitiativeId = e.InitiativeId,
                    JudulInisiasi = e.JudulInisiasi,
                    Capex = e.Capex,
                    RevenuePerYear = e.RevenuePerYear,
                    CatatanTambahan = e.CatatanTambahan,
                    CreatedDate = e.CreatedDate,
                    UpdatedDate = e.UpdatedDate,
                    PartnerName = e.PartnerName,
                    InitiativeStatusId = e.InitiativeStatusId,
                    NegaraMitraId = e.NegaraMitraId,
                    InitiativeStatus = f.StatusName,
                    InitiativeStatusColorName = f.ColorName
                })
                .Join(_context.TblmNegaraMitraInfomations
                     .Join(_context.TblmNegaraMitras, i => i.NegaraMitraId, j => j.Id, (i, j) => new
                     {
                         masterMitra = j
                     }),
                k => k.NegaraMitraId, l => l.masterMitra.Id, (k, l) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = k.Id,
                    InitiativeId = k.InitiativeId,
                    JudulInisiasi = k.JudulInisiasi,
                    Capex = k.Capex,
                    RevenuePerYear = k.RevenuePerYear,
                    CatatanTambahan = k.CatatanTambahan,
                    CreatedDate = k.CreatedDate,
                    UpdatedDate = k.UpdatedDate,
                    PartnerName = k.PartnerName,
                    InitiativeStatusId = k.InitiativeStatusId,
                    NegaraMitraId = k.NegaraMitraId,
                    InitiativeStatus = k.InitiativeStatus,
                    InitiativeStatusColorName = k.InitiativeStatusColorName,
                    NegaraMitraName = l.masterMitra.NamaNegara,
                    HubId = l.masterMitra.HubId
                })
                .Join(_context.TblmHubs
                      .Join(_context.TblmHubHeads, m => m.Id, n => n.HubId, (m, n) => new
                      {
                          HubId = m.Id,
                          HubHeadName = n.Name,
                          HubLocation = m.Name
                      }),
                o => o.HubId, p => p.HubId, (o, p) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = o.Id,
                    InitiativeId = o.InitiativeId,
                    JudulInisiasi = o.JudulInisiasi,
                    Capex = o.Capex,
                    RevenuePerYear = o.RevenuePerYear,
                    CatatanTambahan = o.CatatanTambahan,
                    CreatedDate = o.CreatedDate,
                    UpdatedDate = o.UpdatedDate,
                    PartnerName = o.PartnerName,
                    InitiativeStatusId = o.InitiativeStatusId,
                    NegaraMitraId = o.NegaraMitraId,
                    InitiativeStatus = o.InitiativeStatus,
                    InitiativeStatusColorName = o.InitiativeStatusColorName,
                    NegaraMitraName = o.NegaraMitraName,
                    HubId = o.HubId,
                    HubHeadName = p.HubHeadName,
                    HubName = p.HubLocation
                })
                .AsQueryable();

            var picFungsiLead = joinQuery
                .LeftJoin(_context.TbltInitiativePicFungsis
                      .Join(_context.TblmPicLevels.Where(x => x.DeletedDate == null && x.IsLead == true), c => c.PicLevelId, d => d.Id, (c, d) => new
                      {
                          InitiativeId = c.InitiativeId,
                          PicLeadFungsiId = c.PicFungsiId,
                          level = d
                      })
                      .Join(_context.TblmPicFungsis.Where(x => x.DeletedDate == null && x.ActiveStatus == true), e => e.PicLeadFungsiId, f => f.Id, (e, f) => new
                      {
                          InitiativeId = e.InitiativeId,
                          PicLeadFungsiId = e.PicLeadFungsiId,
                          PicLeadName = f.PicName,
                          PicLeadEmail = f.Email,
                          PicLeadPhone = f.Phone,
                      }),
                a => a.InitiativeId, b => b.InitiativeId, (a, b) => new PotentialInitiativeHighPriorityViewModel
                {
                    Id = a.Id,
                    InitiativeId = a.InitiativeId,
                    JudulInisiasi = a.JudulInisiasi,
                    Capex = a.Capex,
                    RevenuePerYear = a.RevenuePerYear,
                    CatatanTambahan = a.CatatanTambahan,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    UpdatedWording = _updatedWordingUtility.GetUpdatedWording(a.CreatedDate, a.UpdatedDate),
                    PartnerName = a.PartnerName,
                    InitiativeStatusId = a.InitiativeStatusId,
                    NegaraMitraId = a.NegaraMitraId,
                    InitiativeStatus = a.InitiativeStatus,
                    InitiativeStatusColorName = a.InitiativeStatusColorName,
                    NegaraMitraName = a.NegaraMitraName,
                    HubId = a.HubId,
                    HubHeadName = a.HubHeadName,
                    HubName = a.HubName,
                    PicFungsiLeadId = b.PicLeadFungsiId,
                    PicFungsiLeadName = b.PicLeadName,
                    PicFungsiLeadEmail = b.PicLeadEmail,
                    PicFungsiLeadPhone = b.PicLeadPhone
                }).AsQueryable();


            //var picMember = joinQuery.
            result = await picFungsiLead.FirstOrDefaultAsync();

            return result;
        }
    }
}
