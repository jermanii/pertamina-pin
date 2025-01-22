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
    public class InterestRepository : IInterestRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public InterestRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public int GetLastSeqNumber()
        {
            int result = 0;

            IQueryable<TblmInterest> GetAllRecord = _context.TblmInterests.Where(x => x.DeletedDate == null);

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
            IQueryable<TblmInterest> GetAllRecord = _context.TblmInterests.Where(x => x.DeletedDate == null && x.Name == name);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Save(InterestViewModel model)
        {
            _context.Set<TblmInterest>().Add(_mapper.Map<TblmInterest>(model));
            _context.SaveChanges();
        }
        public async Task<ResponseDataTableBaseModel<List<InterestViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmInterests.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmInterest>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.Name.Contains(filter.Value))).Or(x => (x.OrderSeq.ToString().Contains(filter.Value))).Or(x => (x.Description.Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Select(x =>
                                 new InterestViewModel()
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

                var list = await PaginatedList<InterestViewModel, InterestViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, query.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<InterestViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<InterestViewModel>>(false, ex.Message);
            }
        }
        public bool HasRecordByOrder(int? order)
        {
            IQueryable<TblmInterest> GetAllRecord = _context.TblmInterests.Where(x => x.DeletedDate == null && x.OrderSeq == order);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Update(InterestViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TblmInterest record = HasRecordById(model.Id.Value);
            record.Name = model.Name;
            record.Description = model.Description;
            record.OrderSeq = model.OrderSeq;
            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TblmInterest>().Update(record);
            _context.SaveChanges();
        }
        public bool HasRecordByOrderUpdate(int? order, Guid guid)
        {
            IQueryable<TblmInterest> GetAllRecord = _context.TblmInterests.Where(x => x.DeletedDate == null && x.OrderSeq == order && x.Id != guid);

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
            IQueryable<TblmInterest> GetAllRecord = _context.TblmInterests.Where(x => x.DeletedDate == null && x.Name == name && x.Id != guid);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExistInInitiative(Guid guid)
        {
            IQueryable<TbltInitiative> IsExist = _context.TbltInitiatives.Where(x => x.InterestId == guid);

            if (IsExist.Count() > 0)
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

            TblmInterest record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TblmInterest>().Update(record);
            _context.SaveChanges();
        }
        public InterestViewModel GetInterestById(Guid guid)
        {
            InterestViewModel result = _mapper.Map<InterestViewModel>(_context.TblmInterests.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }

        public TblmInterest HasRecordById(Guid guid)
        {
            TblmInterest result = _context.TblmInterests.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }


    }

}
