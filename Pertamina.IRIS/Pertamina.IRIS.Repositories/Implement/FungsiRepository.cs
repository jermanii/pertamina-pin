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
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class FungsiRepository : IFungsiRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public FungsiRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseDataTableBaseModel<List<FungsiViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmFungsis.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmFungsi>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.FungsiName.Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Join(_context.TblmEntitasPertaminas, ad => ad.EntitasPertaminaId, sf => sf.Id, (ad, sf) =>
                                 new FungsiViewModel()
                                 {
                                     Id = ad.Id,
                                     EntitasPertaminaId = ad.EntitasPertaminaId,
                                     NameEntitasPertamina = sf.CompanyName,
                                     FungsiName = ad.FungsiName,
                                     Description = ad.Description,
                                     UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                                 }).AsQueryable();

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
                    joinquery = joinquery.OrderBy(x => x.NameEntitasPertamina).AsQueryable();
                }

                var list = await PaginatedList<FungsiViewModel, FungsiViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<FungsiViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<FungsiViewModel>>(false, ex.Message);
            }
        }

        public bool HasRecordByName(FungsiViewModel model)
        {
            IQueryable<TblmFungsi> GetAllRecord = _context.TblmFungsis.Where(x => x.DeletedDate == null && x.EntitasPertaminaId == model.EntitasPertaminaId && x.FungsiName == model.FungsiName);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasRecordByNameWithoutId(FungsiViewModel model)
        {
            IQueryable<TblmFungsi> GetAllRecord = _context.TblmFungsis.Where(x => x.DeletedDate == null && x.EntitasPertaminaId == model.EntitasPertaminaId &&  x.Id != model.Id && x.FungsiName == model.FungsiName);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistInPicFungsi(Guid fungsiId)
        {
            IQueryable<TblmPicFungsi> IsExist = _context.TblmPicFungsis.Where(x => x.FungsiId == fungsiId);

            if (IsExist.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private TblmFungsi HasRecordById(Guid guid)
        {
            TblmFungsi result = _context.TblmFungsis.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }

        public void Save(FungsiViewModel model)
        {
            _context.Set<TblmFungsi>().Add(_mapper.Map<TblmFungsi>(model));
            _context.SaveChanges();
        }

        public void Update(FungsiViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TblmFungsi record = HasRecordById(model.Id);
            record.EntitasPertaminaId = model.EntitasPertaminaId;
            record.FungsiName = model.FungsiName;
            record.Description = model.Description;
            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TblmFungsi>().Update(record);
            _context.SaveChanges();
        }

        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TblmFungsi record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TblmFungsi>().Update(record);
            _context.SaveChanges();
        }

        public FungsiViewModel GetFungsiById(Guid guid)
        {
            FungsiViewModel result = _mapper.Map<FungsiViewModel>(_context.TblmFungsis.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }
    }
}
