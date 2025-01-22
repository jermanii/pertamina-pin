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
    public class StreamBusinessRepository : IStreamBusinessRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public StreamBusinessRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public int GetLastSeqNumber()
        {
            int result = 0;

            IQueryable<TblmStreamBusiness> GetAllRecord = _context.TblmStreamBusinesses.Where(x => x.DeletedDate == null);

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
            IQueryable<TblmStreamBusiness> GetAllRecord = _context.TblmStreamBusinesses.Where(x => x.DeletedDate == null && x.Name == name);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Save(StreamBusinessViewModel model)
        {
            _context.Set<TblmStreamBusiness>().Add(_mapper.Map<TblmStreamBusiness>(model));
            _context.SaveChanges();
        }
        public async Task<ResponseDataTableBaseModel<List<StreamBusinessViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmStreamBusinesses.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmStreamBusiness>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.Name.Contains(filter.Value))).Or(x => (x.OrderSeq.ToString().Contains(filter.Value))).Or(x => (x.Description.Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Select(x =>
                                 new StreamBusinessViewModel()
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

                var list = await PaginatedList<StreamBusinessViewModel, StreamBusinessViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, query.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<StreamBusinessViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<StreamBusinessViewModel>>(false, ex.Message);
            }
        }
        public bool HasRecordByOrder(int? order)
        {
            IQueryable<TblmStreamBusiness> GetAllRecord = _context.TblmStreamBusinesses.Where(x => x.DeletedDate == null && x.OrderSeq == order);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Update(StreamBusinessViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TblmStreamBusiness record = HasRecordById(model.Id);
            record.Name = model.Name;
            record.Description = model.Description;
            record.OrderSeq = model.OrderSeq;
            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TblmStreamBusiness>().Update(record);
            _context.SaveChanges();
        }
        public bool HasRecordByOrderUpdate(int? order, Guid guid)
        {
            IQueryable<TblmStreamBusiness> GetAllRecord = _context.TblmStreamBusinesses.Where(x => x.DeletedDate == null && x.OrderSeq == order && x.Id != guid);

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
            IQueryable<TblmStreamBusiness> GetAllRecord = _context.TblmStreamBusinesses.Where(x => x.DeletedDate == null && x.Name == name && x.Id != guid);

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
            IQueryable<TbltOpportunityStreamBusiness> GetAllRecord = _context.TbltOpportunityStreamBusinesses.Where(x => x.DeletedDate == null && x.StreamBusinessId == guid);
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
            IQueryable<TbltInitiativePicFungsi> IsExist = _context.TbltInitiativePicFungsis.Where(x => x.PicFungsiId == guid);

            if (IsExist.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsExistInAgreement(Guid guid)
        {
            IQueryable<TbltAgreementPicFungsi> IsExist = _context.TbltAgreementPicFungsis.Where(x => x.PicFungsiId == guid);

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

            TblmStreamBusiness record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TblmStreamBusiness>().Update(record);
            _context.SaveChanges();
        }
        public StreamBusinessViewModel GetStreamBusinessById(Guid guid)
        {
            StreamBusinessViewModel result = _mapper.Map<StreamBusinessViewModel>(_context.TblmStreamBusinesses.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }

        public TblmStreamBusiness HasRecordById(Guid guid)
        {
            TblmStreamBusiness result = _context.TblmStreamBusinesses.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }


    }
}
