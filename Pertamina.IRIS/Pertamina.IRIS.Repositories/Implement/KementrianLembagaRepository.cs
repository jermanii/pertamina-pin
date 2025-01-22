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
    public class KementrianLembagaRepository : IKementrianLembagaRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;
        public KementrianLembagaRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ResponseDataTableBaseModel<List<KementrianLembagaViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmKementrianLembagas.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmKementrianLembaga>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.LembagaName.Contains(filter.Value))).Or(x => (x.Description.Contains(filter.Value))).Or(x => (x.UpdateDate.ToString().Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Select(x =>
                                 new KementrianLembagaViewModel()
                                 {
                                     Id = x.Id,
                                     LembagaName = x.LembagaName,
                                     Description = x.Description,
                                     UpdateDate = x.UpdateDate == null ? x.CreateDate : x.UpdateDate
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
                    joinquery = joinquery.OrderBy(x => x.LembagaName).AsQueryable();
                }

                var list = await PaginatedList<KementrianLembagaViewModel, KementrianLembagaViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<KementrianLembagaViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<KementrianLembagaViewModel>>(false, ex.Message);
            }
        }
        public void Save(KementrianLembagaViewModel model)
        {
            _context.Set<TblmKementrianLembaga>().Add(_mapper.Map<TblmKementrianLembaga>(model));
            _context.SaveChanges();
        }
        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TblmKementrianLembaga record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TblmKementrianLembaga>().Update(record);
            _context.SaveChanges();
        }

        public void Update(KementrianLembagaViewModel model, string username)
        {
            DateTime now = DateTime.Now;

            TblmKementrianLembaga record = HasRecordById(model.Id);
            record.LembagaName = model.LembagaName;
            record.Description = model.Description;

            record.UpdateDate = now;
            record.UpdateBy = username;

            _context.Set<TblmKementrianLembaga>().Update(record);
            _context.SaveChanges();

        }
        public bool ExistInAgreement(Guid kementrianId)
        {
            IQueryable<TbltAgreementKementrianLembaga> IsExist = _context.TbltAgreementKementrianLembagas.Where(x => x.KementrianLembagaId == kementrianId);

            if (IsExist.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasRecordByName(string name)
        {
            IQueryable<TblmKementrianLembaga> GetAllRecord = _context.TblmKementrianLembagas.Where(x => x.DeletedDate == null && x.LembagaName == name);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasRecordByNameUpdate(string name, Guid guid)
        {
            IQueryable<TblmKementrianLembaga> GetAllRecord = _context.TblmKementrianLembagas.Where(x => x.DeletedDate == null && x.LembagaName == name && x.Id != guid);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public KementrianLembagaViewModel GetKementrianById(Guid guid)
        {
            KementrianLembagaViewModel result = _mapper.Map<KementrianLembagaViewModel>(_context.TblmKementrianLembagas.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }
        public TblmKementrianLembaga HasRecordById(Guid guid)
        {
            TblmKementrianLembaga result = _context.TblmKementrianLembagas.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }
    }
}
