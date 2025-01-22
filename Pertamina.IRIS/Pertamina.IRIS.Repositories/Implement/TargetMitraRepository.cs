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
    public class TargetMitraRepository : ITargetMitraRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public TargetMitraRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public int GetLastSeqNumber()
        {
            int result = 0;

            IQueryable<TblmTargetMitra> GetAllRecord = _context.TblmTargetMitras.Where(x => x.DeletedDate == null);

            if (GetAllRecord.Count() > 0)
            {
                result = GetAllRecord.OrderByDescending(x => x.OrderSeq).FirstOrDefault().OrderSeq.Value + 1;
            }
            else
            {
                result = 1;
            }

            return result;
        }

        public bool HasRecordByName(string name)
        {
            IQueryable<TblmTargetMitra> GetAllRecord = _context.TblmTargetMitras.Where(x => x.DeletedDate == null && x.Name == name);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Save(TargetMitraViewModel model)
        {
            _context.Set<TblmTargetMitra>().Add(_mapper.Map<TblmTargetMitra>(model));
            _context.SaveChanges();
        }
        public async Task<ResponseDataTableBaseModel<List<TargetMitraViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmTargetMitras.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmTargetMitra>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.Name.Contains(filter.Value))).Or(x => (x.OrderSeq.ToString().Contains(filter.Value))).Or(x => (x.Description.Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Select(x =>
                                 new TargetMitraViewModel()
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Description = x.Description,
                                     OrderSeq = x.OrderSeq,
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
                    joinquery = joinquery.OrderBy(x => x.OrderSeq).AsQueryable();
                }

                var list = await PaginatedList<TargetMitraViewModel, TargetMitraViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, query.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<TargetMitraViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<TargetMitraViewModel>>(false, ex.Message);
            }
        }
        public bool HasRecordByOrder(int? order)
        {
            IQueryable<TblmTargetMitra> GetAllRecord = _context.TblmTargetMitras.Where(x => x.DeletedDate == null && x.OrderSeq == order);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasRecordByOrderUpdate(int? order, Guid guid)
        {
            IQueryable<TblmTargetMitra> GetAllRecord = _context.TblmTargetMitras.Where(x => x.DeletedDate == null && x.OrderSeq == order && x.Id!=guid);

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
            IQueryable<TblmTargetMitra> GetAllRecord = _context.TblmTargetMitras.Where(x => x.DeletedDate == null && x.Name == name && x.Id != guid);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsExistInOpportunity(Guid guid)
        {
            IQueryable<TbltOpportunityTargetMitra> GetAllRecord = _context.TbltOpportunityTargetMitras.Where(x => x.DeletedDate == null && x.TargetMitraId == guid);
            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TblmTargetMitra record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TblmTargetMitra>().Update(record);
            _context.SaveChanges();
        }
        public TblmTargetMitra HasRecordById(Guid guid)
        {
            TblmTargetMitra result = _context.TblmTargetMitras.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }

        public TargetMitraViewModel GetTargetMitraById(Guid guid)
        {
            TargetMitraViewModel result = _mapper.Map<TargetMitraViewModel>(_context.TblmTargetMitras.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }
        public void Update(TargetMitraViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TblmTargetMitra record = HasRecordById(model.Id);
            record.Name = model.Name;
            record.Description = model.Description;
            record.OrderSeq = model.OrderSeq;
            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TblmTargetMitra>().Update(record);
            _context.SaveChanges();
        }

    }
}
