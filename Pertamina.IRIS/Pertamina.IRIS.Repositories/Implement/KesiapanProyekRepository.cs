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
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class KesiapanProyekRepository : IKesiapanProyekRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;
        public KesiapanProyekRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ResponseDataTableBaseModel<List<KesiapanProyekViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmKesiapanProyeks.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmKesiapanProyek>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.Name.Contains(filter.Value))).Or(x => (x.Description.Contains(filter.Value))).Or(x => (x.OrderSeq.ToString().Contains(filter.Value))).Or(x => (x.UpdateDate.ToString().Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Select(x =>
                                 new KesiapanProyekViewModel()
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

                var list = await PaginatedList<KesiapanProyekViewModel, KesiapanProyekViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, query.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<KesiapanProyekViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<KesiapanProyekViewModel>>(false, ex.Message);
            }
        }
        public void Save(KesiapanProyekViewModel model)
        {
            _context.Set<TblmKesiapanProyek>().Add(_mapper.Map<TblmKesiapanProyek>(model));
            _context.SaveChanges();
        }

        public void Delete(Guid guid, string username)
        {
            
            DateTime now = DateTime.Now;

            TblmKesiapanProyek record = HasRecordById(guid);
            record.UpdateDate = now;
            record.UpdateBy = username;
            record.DeletedDate = now;

            _context.Set<TblmKesiapanProyek>().Update(record);
            _context.SaveChanges();
        }

        public void Update(KesiapanProyekViewModel model, string username)
        {
            DateTime now = DateTime.Now;

            TblmKesiapanProyek record = HasRecordById(model.Id);
            record.Name = model.Name;
            record.Description = model.Description;
            record.OrderSeq = model.OrderSeq;

            record.UpdateDate = now;
            record.UpdateBy = username;

            _context.Set<TblmKesiapanProyek>().Update(record);
            _context.SaveChanges();

        }
        public int GetLastSeqNumber()
        {
            int result = 0;

            IQueryable<TblmKesiapanProyek> GetAllRecord = _context.TblmKesiapanProyeks.Where(x => x.DeletedDate == null);

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
            IQueryable<TblmKesiapanProyek> GetAllRecord = _context.TblmKesiapanProyeks.Where(x => x.DeletedDate == null && x.Name == name);

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
            IQueryable<TblmKesiapanProyek> GetAllRecord = _context.TblmKesiapanProyeks.Where(x => x.DeletedDate == null && x.Name == name && x.Id != guid);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasRecordByOrderSeq(int? order)
        {
            IQueryable<TblmKesiapanProyek> GetAllRecord = _context.TblmKesiapanProyeks.Where(x => x.DeletedDate == null && x.OrderSeq == order);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasRecordByOrderSeqUpdate(int? order, Guid guid)
        {
            IQueryable<TblmKesiapanProyek> GetAllRecord = _context.TblmKesiapanProyeks.Where(x => x.DeletedDate == null && x.OrderSeq == order && x.Id != guid);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsExist(Guid guid)
        {
            IQueryable<TbltOpportunityKesiapanProyek> GetAllRecord = _context.TbltOpportunityKesiapanProyeks.Where(x => x.DeletedDate == null && x.KesiapanProyekId == guid);
            if(GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public KesiapanProyekViewModel GetKesiapanProyekById(Guid guid)
        {
            KesiapanProyekViewModel result = _mapper.Map<KesiapanProyekViewModel>(_context.TblmKesiapanProyeks.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }
        public TblmKesiapanProyek HasRecordById(Guid guid)
        {
            TblmKesiapanProyek result = _context.TblmKesiapanProyeks.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }        
    }
}
