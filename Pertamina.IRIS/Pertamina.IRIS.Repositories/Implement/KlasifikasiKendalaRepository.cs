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
    public class KlasifikasiKendalaRepository : IKlasifikasiKendalaRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;
        public KlasifikasiKendalaRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ResponseDataTableBaseModel<List<KlasifikasiKendalaViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmKlasifikasiKendalas.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmKlasifikasiKendala>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.Name.Contains(filter.Value))).Or(x => (x.Description.Contains(filter.Value))).Or(x => (x.OrderSeq.ToString().Contains(filter.Value))).Or(x => (x.UpdateDate.ToString().Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Select(x =>
                                 new KlasifikasiKendalaViewModel()
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

                var list = await PaginatedList<KlasifikasiKendalaViewModel, KlasifikasiKendalaViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, query.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<KlasifikasiKendalaViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<KlasifikasiKendalaViewModel>>(false, ex.Message);
            }
        }

        public void Save(KlasifikasiKendalaViewModel model)
        {
            _context.Set<TblmKlasifikasiKendala>().Add(_mapper.Map<TblmKlasifikasiKendala>(model));
            _context.SaveChanges();
        }

        public void Delete(Guid guid, string username)
        {

            DateTime now = DateTime.Now;

            TblmKlasifikasiKendala record = HasRecordById(guid);
            record.UpdateDate = now;
            record.UpdateBy = username;
            record.DeletedDate = now;

            _context.Set<TblmKlasifikasiKendala>().Update(record);
            _context.SaveChanges();
        }

        public void Update(KlasifikasiKendalaViewModel model, string username)
        {
            DateTime now = DateTime.Now;

            TblmKlasifikasiKendala record = HasRecordById(model.Id);
            record.Name = model.Name;
            record.Description = model.Description;
            record.OrderSeq = model.OrderSeq;

            record.UpdateDate = now;
            record.UpdateBy = username;

            _context.Set<TblmKlasifikasiKendala>().Update(record);
            _context.SaveChanges();

        }
        public int GetLastSeqNumber()
        {
            int result = 0;

            IQueryable<TblmKlasifikasiKendala> GetAllRecord = _context.TblmKlasifikasiKendalas.Where(x => x.DeletedDate == null);

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
            IQueryable<TblmKlasifikasiKendala> GetAllRecord = _context.TblmKlasifikasiKendalas.Where(x => x.DeletedDate == null && x.Name == name);

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
            IQueryable<TblmKlasifikasiKendala> GetAllRecord = _context.TblmKlasifikasiKendalas.Where(x => x.DeletedDate == null && x.Name == name && x.Id != guid);

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
            IQueryable<TblmKlasifikasiKendala> GetAllRecord = _context.TblmKlasifikasiKendalas.Where(x => x.DeletedDate == null && x.OrderSeq == order);

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
            IQueryable<TblmKlasifikasiKendala> GetAllRecord = _context.TblmKlasifikasiKendalas.Where(x => x.DeletedDate == null && x.OrderSeq == order && x.Id != guid);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExistInAgreement(Guid guid)
        {
            IQueryable<TbltAgreement> GetAllRecord = _context.TbltAgreements.Where(x => x.DeletedDate == null && x.KlasifikasiKendalaId == guid);
            
            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public KlasifikasiKendalaViewModel GetKlasifikasiKendalaById(Guid guid)
        {
            KlasifikasiKendalaViewModel result = _mapper.Map<KlasifikasiKendalaViewModel>(_context.TblmKlasifikasiKendalas.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }
        public TblmKlasifikasiKendala HasRecordById(Guid guid)
        {
            TblmKlasifikasiKendala result = _context.TblmKlasifikasiKendalas.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }
    }
}
