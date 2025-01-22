using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class InternationalBusinessIntellegenceRepository : IInternationalBusinessIntellegenceRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public InternationalBusinessIntellegenceRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public void Save(InternationalBusinessIntelligenceViewModel model)
        {
            _context.Set<TbltInternationalBusinessIntelligence>().Add(_mapper.Map<TbltInternationalBusinessIntelligence>(model));
            _context.SaveChanges();
        }
        public void SaveCountryIBI(InternationalBusinessIntelligenceCountryViewModel model)
        {
            _context.Set<TbltInternationalBusinessIntelligenceCountry>().Add(_mapper.Map<TbltInternationalBusinessIntelligenceCountry>(model));
            _context.SaveChanges();
        }
        public void SaveAuthors(InternationalBusinessIntelligenceAuthorViewModel model)
        {
            _context.Set<TbltInternationalBusinessIntelligenceAuthor>().Add(_mapper.Map<TbltInternationalBusinessIntelligenceAuthor>(model));
            _context.SaveChanges();
        }
        public void SaveDocuments(InternationalBusinessIntelligenceDocumentViewModel model)
        {
            _context.Set<TbltInternationalBusinessIntelligenceDocument>().Add(_mapper.Map<TbltInternationalBusinessIntelligenceDocument>(model));
            _context.SaveChanges();
        }
        public List<InternationalBusinessIntelligenceAuthorViewModel> GetAuthors(Guid id)
        {
            IQueryable<TbltInternationalBusinessIntelligenceAuthor> result = _context.TbltInternationalBusinessIntelligenceAuthors.Where(x => x.DeletedDate == null && x.InternationalBusinessIntelligenceId == id).OrderBy(x => x.Name).AsQueryable();

            return _mapper.Map<List<InternationalBusinessIntelligenceAuthorViewModel>>(result);
        }
        public List<InternationalBusinessIntelligenceAuthorViewModel> SearchAuthors(string SearchBar)
        {
            IQueryable<TbltInternationalBusinessIntelligenceAuthor> result = _context.TbltInternationalBusinessIntelligenceAuthors
                .Where(x => x.DeletedDate == null)
                .AsQueryable();


            if (!string.IsNullOrEmpty(SearchBar))
            {
                result = result.Where(x => x.Name.Contains(SearchBar));
            }

            return _mapper.Map<List<InternationalBusinessIntelligenceAuthorViewModel>>(result);
        }
        public List<InternationalBusinessIntelligenceCountryViewModel> GetRelatedCountry(Guid id)
        {
            IQueryable<InternationalBusinessIntelligenceCountryViewModel> result = _context.TbltInternationalBusinessIntelligenceCountries
                .Where(x => x.DeletedDate == null && x.InternationalBusinessIntelligenceId == id)
                .Join(_context.TblmNegaraMitras, x => x.NegaraMitraId, y => y.Id, (x, y) =>
                new InternationalBusinessIntelligenceCountryViewModel
                {
                    Id = x.Id,
                    InternationalBusinessIntelligenceId = x.InternationalBusinessIntelligenceId,
                    NegaraMitra = y,
                    CreatedDate = x.CreatedDate,
                    NegaraMitraId = y.Id
                })
                .OrderBy(x => x.CreatedDate)
                .AsQueryable();

            return result.ToList();
        }
        public List<InternationalBusinessIntelligenceCountryViewModel> GetIBIByRelatedCountry(List<Guid?> countrySelected)
        {
            IQueryable<InternationalBusinessIntelligenceCountryViewModel> result = _context.TbltInternationalBusinessIntelligenceCountries
                .Where(x => x.DeletedDate == null && countrySelected.Contains(x.NegaraMitraId))
                .Join(_context.TblmNegaraMitras, x => x.NegaraMitraId, y => y.Id, (x, y) =>
                new InternationalBusinessIntelligenceCountryViewModel
                {
                    Id = x.Id,
                    InternationalBusinessIntelligenceId = x.InternationalBusinessIntelligenceId,
                    NegaraMitra = y,
                    CreatedDate = x.CreatedDate
                })
                .OrderBy(x => x.CreatedDate)
                .AsQueryable();

            return result.ToList();
        }
        public List<InternationalBusinessIntelligenceCountryViewModel> SearchIBIRelatedCountry(string SearchBar)
        {
            IQueryable<InternationalBusinessIntelligenceCountryViewModel> result = _context.TbltInternationalBusinessIntelligenceCountries
               .Where(x => x.DeletedDate == null)
               .Join(_context.TblmNegaraMitras, x => x.NegaraMitraId, y => y.Id, (x, y) =>
               new InternationalBusinessIntelligenceCountryViewModel
               {
                   Id = x.Id,
                   InternationalBusinessIntelligenceId = x.InternationalBusinessIntelligenceId,
                   NegaraMitra = y,
                   CreatedDate = x.CreatedDate
               })
               .OrderBy(x => x.CreatedDate)
               .AsQueryable();

            if (!string.IsNullOrEmpty(SearchBar))
            {
                result = result.Where(x => x.NegaraMitra.NamaNegara.Contains(SearchBar));
            }

            return result.ToList();
        }
        public List<InternationalBusinessIntelligenceDocumentViewModel> GetDocuments(Guid id)
        {
            IQueryable<TbltInternationalBusinessIntelligenceDocument> result = _context.TbltInternationalBusinessIntelligenceDocuments.Where(x => x.DeletedDate == null && x.InternationalBusinessIntelligenceId == id).OrderBy(x => x.FileNameSystem).AsQueryable();

            return _mapper.Map<List<InternationalBusinessIntelligenceDocumentViewModel>>(result);
        }
        public List<InternationalBusinessIntelligenceViewModel> GetIbIByCategoryOwn(string userName)
        {
            IQueryable<TbltInternationalBusinessIntelligence> result = _context.TbltInternationalBusinessIntelligences.Where(x => x.DeletedDate == null && x.CreatedBy == userName).OrderByDescending(x => x.CreatedDate).AsQueryable().Take(10);

            return _mapper.Map<List<InternationalBusinessIntelligenceViewModel>>(result);
        }
        public List<InternationalBusinessIntelligenceViewModel> GetIbIByCategoryRecenly()
        {
            IQueryable<TbltInternationalBusinessIntelligence> result = _context.TbltInternationalBusinessIntelligences.Where(x => x.DeletedDate == null).OrderByDescending(x => x.CreatedDate).AsQueryable().Take(10);

            return _mapper.Map<List<InternationalBusinessIntelligenceViewModel>>(result);
        }
        public List<InternationalBusinessIntelligenceViewModel> GetIbIFilterBy(InternationalBusinessIntelligenceViewModel model, List<Guid?> idIBI)
        {
            IQueryable<TbltInternationalBusinessIntelligence> result = _context.TbltInternationalBusinessIntelligences
              .Where(x => x.DeletedDate == null)
              .AsQueryable();

            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.SearchBar))
                {
                    result = result.Where(x => x.Title.Contains(model.SearchBar) || x.Description.Contains(model.SearchBar));
                }

                if (model.TypeStudyId != null)
                {
                    result = result.Where(x => x.TypeStudyId == model.TypeStudyId);
                }

                if (model.StreamBusinessId != null)
                {
                    result = result.Where(x => x.StreamBusinessId == model.StreamBusinessId);
                }

                if (model.EntitasPertaminaId != null)
                {
                    result = result.Where(x => x.EntitasPertaminaId == model.EntitasPertaminaId);
                }
            }

            if (idIBI != null && idIBI.Any())
            {
                result = result.Where(x => idIBI.Contains(x.Id));
            }

            return _mapper.Map<List<InternationalBusinessIntelligenceViewModel>>(result.ToList());

        }
        public List<InternationalBusinessIntelligenceViewModel> GetIbiAll()
        {
            IQueryable<TbltInternationalBusinessIntelligence> result = _context.TbltInternationalBusinessIntelligences
                .Where(x => x.DeletedDate == null)
                .OrderBy(x => x.Title)
                .AsQueryable();

            return _mapper.Map<List<InternationalBusinessIntelligenceViewModel>>(result.ToList());
        }
        public List<InternationalBusinessIntelligenceViewModel> GetIbiSearchBar(InternationalBusinessIntelligenceViewModel model,
            List<Guid?> idIBI, List<Guid?> idIBICountries)
        {
            IQueryable<TbltInternationalBusinessIntelligence> result = _context.TbltInternationalBusinessIntelligences
                .Where(x => x.DeletedDate == null)
                .AsQueryable();

            // Apply Filterin  
            if (idIBICountries != null && idIBICountries.Any())
            {
                result = result.Where(x => idIBICountries.Contains(x.Id));
            }

            if (model != null)
            {
                if (idIBI != null && idIBI.Any())
                {
                    result = result.Where(x => idIBI.Contains(x.Id));
                }
                else
                {
                    result = result.Where(x => x.Title.Contains(model.SearchBar) || x.Description.Contains(model.SearchBar));
                }

                if (model.TypeStudyId != null)
                {
                    result = result.Where(x => x.TypeStudyId == model.TypeStudyId);
                }

                if (model.StreamBusinessId != null)
                {
                    result = result.Where(x => x.StreamBusinessId == model.StreamBusinessId);
                }

                if (model.EntitasPertaminaId != null)
                {
                    result = result.Where(x => x.EntitasPertaminaId == model.EntitasPertaminaId);
                }
            }



            return _mapper.Map<List<InternationalBusinessIntelligenceViewModel>>(result.ToList());
        }
        public List<NegaraMitraInformationViewModel> GetNegaraMitraInformation()
        {
            var distinctNegaraMitraIds = _context.TbltInternationalBusinessIntelligenceCountries
                .Where(x => x.DeletedDate == null)
                .Select(x => x.NegaraMitraId)
                .Distinct();

            var result = _context.TblmNegaraMitraInfomations
                .Where(b => distinctNegaraMitraIds.Contains(b.NegaraMitraId))
                .Join(
                    _context.TblmNegaraMitras,
                    b => b.NegaraMitraId,
                    c => c.Id,
                    (b, c) => new NegaraMitraInformationViewModel
                    {
                        NegaraMitraId = b.NegaraMitraId,
                        Gdp = b.Gdp ?? string.Empty,
                        GdpPerCapita = b.GdpPerCapita ?? string.Empty,
                        OilGasReserves = b.OilGasReserves ?? string.Empty,
                        OilProduction = b.OilProduction ?? string.Empty,
                        Population = !string.IsNullOrEmpty(b.Population) ? string.Format("{0:N0}", int.Parse(b.Population)).Replace(",", ".") : string.Empty,
                        Flag = c.Flag ?? string.Empty,
                        Latitude = c.Latitude,
                        Longitude = c.Longitude,
                        CountryAcronyms = b.CountryAcronyms ?? string.Empty,
                        NegaraMitraName = c.NamaNegara

                    }
                )
                .Distinct()
                .ToList();

            return _mapper.Map<List<NegaraMitraInformationViewModel>>(result);
        }
        public List<InternationalBusinessIntelligenceCountryViewModel> GetCountDocumentIBI(Guid? negaraMitraId)
        {
            var query = _context.TbltInternationalBusinessIntelligenceCountries
                .Where(a => a.NegaraMitraId == negaraMitraId).AsQueryable();
            var result = query.Join(_context.TbltInternationalBusinessIntelligences.Where(x => x.DeletedDate == null), a => a.InternationalBusinessIntelligenceId, b => b.Id, (a, b) => a)
                              .Join(_context.TbltInternationalBusinessIntelligenceDocuments, c => c.InternationalBusinessIntelligenceId, d => d.InternationalBusinessIntelligenceId, (c, d) => new InternationalBusinessIntelligenceCountryViewModel
                              {
                                  InternationalBusinessIntelligenceId = c.InternationalBusinessIntelligenceId,
                                  NegaraMitraId = c.NegaraMitraId
                              }).ToList();

            return _mapper.Map<List<InternationalBusinessIntelligenceCountryViewModel>>(result);
        }
        public async Task<string> GetPath(string featureName)
        {
            string result = string.Empty;

            TblmDirectory GetRecord = await _context.TblmDirectories.Where(x => x.FeatureName == featureName).FirstOrDefaultAsync();

            if (GetRecord != null)
            {
                result = GetRecord.Path;
            }

            return result;
        }
        public async Task<SequenceCounterViewModel> GetSequence(string featureName)
        {
            TblmSequenceCounter GetRecord = await _context.TblmSequenceCounters.Where(x => x.Name == featureName).FirstOrDefaultAsync();

            SequenceCounterViewModel result = new SequenceCounterViewModel();


            int yearNow = DateTime.Now.Year;

            if (GetRecord != null)
            {
                result.Name = GetRecord.Name;
                result.Year = GetRecord.Year;

                if (GetRecord.Year == yearNow)
                {
                    result.Sequence = GetRecord.Sequence + 1;
                    GetRecord.Sequence = result.Sequence;
                    _context.Set<TblmSequenceCounter>().Update(GetRecord);
                    _context.SaveChanges();
                }
                else
                {
                    result.Sequence = 1;
                    result.Year = yearNow;
                    GetRecord.Sequence = result.Sequence;
                    GetRecord.Year = yearNow;
                    _context.Set<TblmSequenceCounter>().Update(GetRecord);
                    _context.SaveChanges();
                }
            }
            else
            {
                result.Sequence = 1;
                result.Year = yearNow;
                TblmSequenceCounter saveRecord = new TblmSequenceCounter();
                saveRecord.Name = featureName;
                saveRecord.Sequence = result.Sequence;
                saveRecord.Year = yearNow;
                _context.Set<TblmSequenceCounter>().Add(saveRecord);
                _context.SaveChanges();
            }

            return result;
        }
        public async Task<InternationalBusinessIntelligenceViewModel> GetIbiDocument(Guid? guid)
        {
            var result = new InternationalBusinessIntelligenceViewModel();

            var query = _context.TbltInternationalBusinessIntelligences.Where(x => x.Id == guid && x.DeletedDate == null).AsQueryable();

            var joinQuery = query
                            .LeftJoin(_context.TblmConfidentialities.Where(x => x.DeletedDate == null), a => a.ConfidentialityId, b => b.Id, (a, b) => new InternationalBusinessIntelligenceViewModel
                            {
                                Id = a.Id,
                                CreatedBy = a.CreatedBy,
                                CreatedDate = a.CreatedDate,
                                UpdatedBy = a.UpdatedBy,
                                UpdatedDate = a.UpdatedDate,
                                TypeStudyId = a.TypeStudyId,
                                ResearchDate = a.ResearchDate,
                                Title = a.Title,
                                EntitasPertaminaId = a.EntitasPertaminaId,
                                StreamBusinessId = a.StreamBusinessId,
                                ConfidentialityId = a.ConfidentialityId,
                                Description = a.Description,
                                ConfidentialityName = b.Name,
                                ConfidentialityColor = b.HexColor
                            })
                            .LeftJoin(_context.TblmEntitasPertaminas, c => c.EntitasPertaminaId, d => d.Id, (c, d) => new InternationalBusinessIntelligenceViewModel
                            {
                                Id = c.Id,
                                CreatedBy = c.CreatedBy,
                                CreatedDate = c.CreatedDate,
                                UpdatedBy = c.UpdatedBy,
                                UpdatedDate = c.UpdatedDate,
                                TypeStudyId = c.TypeStudyId,
                                ResearchDate = c.ResearchDate,
                                Title = c.Title,
                                EntitasPertaminaId = c.EntitasPertaminaId,
                                StreamBusinessId = c.StreamBusinessId,
                                ConfidentialityId = c.ConfidentialityId,
                                Description = c.Description,
                                ConfidentialityName = c.ConfidentialityName,
                                ConfidentialityColor = c.ConfidentialityColor,
                                EntitasPertaminaName = d.CompanyName
                            });

            var authors = await joinQuery.LeftJoin(_context.TbltInternationalBusinessIntelligenceAuthors.Where(x => x.DeletedDate == null), e => e.Id, f => f.InternationalBusinessIntelligenceId, (e, f) => new InternationalBusinessIntelligenceAuthorViewModel
            {
                Id = f.Id,
                Name = f.Name
            }).ToListAsync();

            var docs = await joinQuery.LeftJoin(_context.TbltInternationalBusinessIntelligenceDocuments.Where(x => x.DeletedDate == null), g => g.Id, h => h.InternationalBusinessIntelligenceId, (g, h) => new InternationalBusinessIntelligenceDocumentViewModel
            {
                Id = h.Id,
                FileNameSystem = h.FileNameSystem,
                FileNameUser = h.FileNameUser,
                CreatedDate = h.CreatedDate
            }).ToListAsync();

            var countries = await query.LeftJoin(_context.TbltInternationalBusinessIntelligenceCountries
                                        .Join(_context.TblmNegaraMitras.Where(x => x.DeletedDate == null), i => i.NegaraMitraId, j => j.Id, (i, j) => new
                                        {
                                            ibiCountry = i,
                                            mstNegMitra = j
                                        }), k => k.Id, l => l.ibiCountry.InternationalBusinessIntelligenceId, (k, l) => new InternationalBusinessIntelligenceCountryViewModel
                                        {
                                            Id = l.ibiCountry.Id,
                                            NegaraMitraId = l.mstNegMitra.Id,
                                            NegaraMitraName = l.mstNegMitra.NamaNegara
                                        }).ToListAsync();

            result = await joinQuery.Select(x => new InternationalBusinessIntelligenceViewModel
            {
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                TypeStudyId = x.TypeStudyId,
                ResearchDate = x.ResearchDate,
                Title = x.Title,
                EntitasPertaminaId = x.EntitasPertaminaId,
                StreamBusinessId = x.StreamBusinessId,
                ConfidentialityId = x.ConfidentialityId,
                Description = x.Description,
                ConfidentialityName = x.ConfidentialityName,
                ConfidentialityColor = x.ConfidentialityColor,
                EntitasPertaminaName = x.EntitasPertaminaName,
                Authors = authors,
                Documents = docs,
                RelatedCountry = countries
            }).FirstOrDefaultAsync();
            //result = await joinQuery
            //                  .LeftJoin(_context.TbltInternationalBusinessIntelligenceAuthors, e => e.Id, f => f.InternationalBusinessIntelligenceId, (e, f) => new
            //                  {
            //                      e,
            //                      f
            //                  })
            //                  .LeftJoin(_context.TbltInternationalBusinessIntelligenceDocuments, g => g.e.Id, h => h.InternationalBusinessIntelligenceId, (g, h) => new
            //                  {
            //                      g,
            //                      h
            //                  })
            //                  .LeftJoin(_context.TbltInternationalBusinessIntelligenceCountries
            //                            .Join(_context.TblmNegaraMitras.Where(x => x.DeletedDate == null), i => i.NegaraMitraId, j => j.Id, (i, j) => new
            //                            {
            //                                ibiCountry = i,
            //                                mstNegMitra = j
            //                            }), k => k.g.e.Id, l => l.ibiCountry.InternationalBusinessIntelligenceId, (k, l) => new InternationalBusinessIntelligenceViewModel
            //                            {
            //                                Id = k.g.e.Id,
            //                                CreatedBy = k.g.e.CreatedBy,
            //                                CreatedDate = k.g.e.CreatedDate,
            //                                UpdatedBy = k.g.e.UpdatedBy,
            //                                UpdatedDate = k.g.e.UpdatedDate,
            //                                TypeStudyId = k.g.e.TypeStudyId,
            //                                ResearchDate = k.g.e.ResearchDate,
            //                                Title = k.g.e.Title,
            //                                EntitasPertaminaId = k.g.e.EntitasPertaminaId,
            //                                StreamBusinessId = k.g.e.StreamBusinessId,
            //                                ConfidentialityId = k.g.e.ConfidentialityId,                                            
            //                                Description = k.g.e.Description,
            //                                ConfidentialityName = k.g.e.ConfidentialityName,
            //                                ConfidentialityColor = k.g.e.ConfidentialityColor,
            //                                EntitasPertaminaName = k.g.e.EntitasPertaminaName,
            //                                Authors = new List<InternationalBusinessIntelligenceAuthorViewModel>
            //                                {
            //                                    new InternationalBusinessIntelligenceAuthorViewModel
            //                                    {
            //                                        Id = k.g.f.Id,
            //                                        Name = k.g.f.Name
            //                                    }
            //                                },
            //                                RelatedCountry = new List<InternationalBusinessIntelligenceCountryViewModel>
            //                                {
            //                                    new InternationalBusinessIntelligenceCountryViewModel
            //                                    {
            //                                        Id = l.ibiCountry.Id,
            //                                        NegaraMitraId = l.ibiCountry.NegaraMitraId,
            //                                        NegaraMitraName = l.mstNegMitra.NamaNegara
            //                                    }
            //                                },
            //                                Documents = new List<InternationalBusinessIntelligenceDocumentViewModel>
            //                                {
            //                                    new InternationalBusinessIntelligenceDocumentViewModel
            //                                    {
            //                                        Id = k.h.Id,
            //                                        CreatedBy = k.h.CreatedBy,
            //                                        CreatedDate = k.h.CreatedDate,
            //                                        FileNameSystem = k.h.FileNameSystem,
            //                                        FileNameUser = k.h.FileNameUser,

            //                                    }
            //                                }

            //                            }).FirstOrDefaultAsync();

            return result;
        }
        public List<InternationalBusinessIntelligenceMyListViewModel> GetUserBookmarks(string username)
        {
            var result = _mapper.Map<List<InternationalBusinessIntelligenceMyListViewModel>>(
                         _context.TbltInternationalBusinessIntelligenceMylists
                         .Where(x => x.DeletedDate == null &&
                                x.Email == username)
                         .OrderBy(x => x.Sequence).ToList());


            return result;
        }
        public async Task<InternationalBusinessIntelligenceMyListViewModel> GetUserBookmarkById(Guid? guid, string username)
        {
            var result = _mapper.Map<InternationalBusinessIntelligenceMyListViewModel>(await _context.TbltInternationalBusinessIntelligenceMylists
                                .Where(x =>
                                       x.DeletedDate == null &&
                                       x.Email == username &&
                                       x.InternationalBusinessIntelligenceId == guid).FirstOrDefaultAsync());


            return result;
        }
        public void AddBookmark(InternationalBusinessIntelligenceMyListViewModel model)
        {
            _context.Set<TbltInternationalBusinessIntelligenceMylist>().Add(_mapper.Map<TbltInternationalBusinessIntelligenceMylist>(model));
            _context.SaveChanges();
        }
        public void UpdateBookmark(InternationalBusinessIntelligenceMyListViewModel model)
        {
            TbltInternationalBusinessIntelligenceMylist record = HasRecordById(model.Id);

            record.DeletedDate = DateTime.Now;

            _context.Set<TbltInternationalBusinessIntelligenceMylist>().Update(record);
            _context.SaveChanges();
        }
        private TbltInternationalBusinessIntelligenceMylist HasRecordById(Guid guid)
        {
            TbltInternationalBusinessIntelligenceMylist result = _context.TbltInternationalBusinessIntelligenceMylists
                                                           .Where(x => x.DeletedDate == null && x.Id == guid).FirstOrDefault();
            return result;
        }
        public List<InternationalBusinessIntelligenceViewModel> GetIbIByCategoryMyList(string userName)
        {
            List<InternationalBusinessIntelligenceViewModel> result = new List<InternationalBusinessIntelligenceViewModel>();
            var query = _context.TbltInternationalBusinessIntelligenceMylists.Where(x => x.DeletedDate == null && x.Email == userName).OrderBy(x => x.Sequence).AsQueryable().Take(10);

            var joinQuery = query.Join(
                _context.TbltInternationalBusinessIntelligences.Where(x => x.DeletedDate == null), a => a.InternationalBusinessIntelligenceId, b => b.Id, (a, b) => b).AsQueryable();

            result = _mapper.Map<List<InternationalBusinessIntelligenceViewModel>>(joinQuery);
            return result;
        }
        public async Task<InternationalBusinessIntelligenceViewModel> DeleteIBI(Guid? guid, string userName = null)
        {
            InternationalBusinessIntelligenceViewModel result = new InternationalBusinessIntelligenceViewModel();
            var model = await _context.TbltInternationalBusinessIntelligences.FirstOrDefaultAsync(x => x.Id == guid);
            model.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            result = _mapper.Map<InternationalBusinessIntelligenceViewModel>(model);
            return result;
        }
        public void Update(InternationalBusinessIntelligenceViewModel model)
        {
            TbltInternationalBusinessIntelligence existing = _context.TbltInternationalBusinessIntelligences.Find(model.Id);

            model.CreatedBy = existing.CreatedBy;
            model.CreatedDate = existing.CreatedDate;
            model.IsDraft = false;

            _context.Entry(existing).CurrentValues.SetValues(_mapper.Map<TbltInternationalBusinessIntelligence>(model));
            _context.SaveChanges();
        }
        public void DeleteCountryIBI(InternationalBusinessIntelligenceCountryViewModel model)
        {
            TbltInternationalBusinessIntelligenceCountry existing = _context.TbltInternationalBusinessIntelligenceCountries.Find(model.Id);
            _context.Entry(existing).CurrentValues.SetValues(_mapper.Map<TbltInternationalBusinessIntelligenceCountry>(model));
            _context.SaveChanges();
        }
        public void DeleteAuthorIBI(InternationalBusinessIntelligenceAuthorViewModel model)
        {
            TbltInternationalBusinessIntelligenceAuthor existing = _context.TbltInternationalBusinessIntelligenceAuthors.Find(model.Id);
            _context.Entry(existing).CurrentValues.SetValues(_mapper.Map<TbltInternationalBusinessIntelligenceAuthor>(model));
            _context.SaveChanges();
        }
        public void DeleteDocumentIBI(InternationalBusinessIntelligenceDocumentViewModel model)
        {
            TbltInternationalBusinessIntelligenceDocument existing = _context.TbltInternationalBusinessIntelligenceDocuments.Find(model.Id);
            _context.Entry(existing).CurrentValues.SetValues(_mapper.Map<TbltInternationalBusinessIntelligenceDocument>(model));
            _context.SaveChanges();
        }
        public async Task<InternationalBusinessIntelligenceDocumentViewModel> GetSingleFile(Guid guid)
        {
            TbltInternationalBusinessIntelligenceDocument result = await _context.TbltInternationalBusinessIntelligenceDocuments.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefaultAsync();

            return _mapper.Map<InternationalBusinessIntelligenceDocumentViewModel>(result);
        }
        public async Task<InternationalBusinessIntelligenceViewModel> GetIbiById(Guid guid)
        {
            InternationalBusinessIntelligenceViewModel existing = _mapper.Map<InternationalBusinessIntelligenceViewModel>(await _context.TbltInternationalBusinessIntelligences.FindAsync(guid));
            return existing;
        }
    }
}
