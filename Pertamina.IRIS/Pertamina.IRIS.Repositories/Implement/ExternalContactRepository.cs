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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class ExternalContactRepository : IExternalContactRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public ExternalContactRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseDataTableBaseModel<List<ExternalContactViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TbltExternalContacts.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TbltExternalContact>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.NamaCompany.Contains(filter.Value)))
                        .Or(x => (x.AlamatCompany.Contains(filter.Value)))
                        .Or(x => (x.EmailCompany.Contains(filter.Value)))
                        .Or(x => (x.Remark.Contains(filter.Value)))
                        .Or(x => (x.NamaPerson.Contains(filter.Value)))
                        .Or(x => (x.JabatanPerson.Contains(filter.Value)))
                        .Or(x => (x.NoTelpPerson.Contains(filter.Value)))
                        .Or(x => (x.EmailPerson.Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Select(x => 
                                 new ExternalContactViewModel()
                                 {
                                     Id = x.Id,
                                     NamaCompany = x.NamaCompany,
                                     AlamatCompany = x.AlamatCompany,
                                     EmailCompany = x.EmailCompany,
                                     KoneksiCompany = x.KoneksiCompany,
                                     Remark = x.Remark,
                                     NamaPerson = x.NamaPerson,
                                     JabatanPerson = x.JabatanPerson,
                                     NoTelpPerson = x.NoTelpPerson,
                                     EmailPerson = x.EmailPerson,
                                     UpdateDate = x.UpdateDate == null ? x.CreateDate : x.UpdateDate,
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
                    joinquery = joinquery.OrderBy(x => x.NamaCompany).AsQueryable();
                }

                var list = await PaginatedList<ExternalContactViewModel, ExternalContactViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<ExternalContactViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<ExternalContactViewModel>>(false, ex.Message);
            }
        }

        public void Save(ExternalContactViewModel model)
        {
            _context.Set<TbltExternalContact>().Add(_mapper.Map<TbltExternalContact>(model));
            _context.SaveChanges();
        }
        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TbltExternalContact record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TbltExternalContact>().Update(record);
            _context.SaveChanges();
        }
        public TbltExternalContact HasRecordById(Guid guid)
        {
            TbltExternalContact result = _context.TbltExternalContacts.Where(x => x.DeletedDate == null && x.Id == guid).FirstOrDefault();
            return result;
        }
        public ExternalContactViewModel GetExternalContactById(Guid guid)
        {
            ExternalContactViewModel result = _mapper.Map<ExternalContactViewModel>(_context.TbltExternalContacts.Where(x => x.Id == guid && x.DeletedDate == null).FirstOrDefault());
            return result;
        }

        public void Update(ExternalContactViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TbltExternalContact record = HasRecordById(model.Id);
            record.NamaCompany = model.NamaCompany;
            record.AlamatCompany = model.AlamatCompany;
            record.EmailCompany = model.EmailCompany;
            record.KoneksiCompany = model.KoneksiCompany;
            record.Remark = model.Remark;
            record.NamaPerson = model.NamaPerson;
            record.JabatanPerson = model.JabatanPerson;
            record.EmailPerson = model.EmailPerson;
            record.NoTelpPerson = model.NoTelpPerson;

            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TbltExternalContact>().Update(record);
            _context.SaveChanges();
        }

        public IEnumerable<TbltExternalContact> GetAllExternalContact()
        {
            IEnumerable<TbltExternalContact> items = _context.Set<TbltExternalContact>().ToList();
            items = items.Where(x => x.DeletedDate == null);

            return items.ToList();
        }


        public async Task<ExternalContactViewModel> ImportXLSData(List<List<string>> xlsData, string fileName, long fileSize)
        {
            ExternalContactViewModel model = new ExternalContactViewModel();
            
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Row #x: The data
                    for (int i = 0; i < xlsData.Count(); i++)
                    {

                        model.Id = Guid.NewGuid();
                        model.CreateBy = "System";
                        model.CreateDate = DateTime.Now;

                        model.NamaCompany = xlsData[i][0];
                        model.AlamatCompany = xlsData[i][1];
                        model.EmailCompany = xlsData[i][2];
                        model.Remark = xlsData[i][3];
                        model.NamaPerson = xlsData[i][4];
                        model.JabatanPerson = xlsData[i][5];
                        model.NoTelpPerson = xlsData[i][6];
                        model.EmailPerson = xlsData[i][7];

                        _context.Set<TbltExternalContact>().Add(_mapper.Map<TbltExternalContact>(model));
                        await Task.Run(() => _context.SaveChanges());
                    }

                    
                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                    return model;
                }
                catch (Exception ex)
                {
                    model.ErrorMessage = ex.Message;
                    model.IsError = true;
                    return model;
                }
            }
        }
    }
}
