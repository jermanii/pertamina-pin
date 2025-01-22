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
    public class EntitasPertaminaRepository : IEntitasPertaminaRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public EntitasPertaminaRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseDataTableBaseModel<List<EntitasPertaminaViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmEntitasPertaminas.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmEntitasPertamina>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.Code.Contains(filter.Value))).Or(x => (x.CompanyName.Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Join(_context.TblmHshes, ad => ad.HshId, sf => sf.Id, (ad, sf) =>
                                 new EntitasPertaminaViewModel()
                                 {
                                     Id = ad.Id,
                                     Code = ad.Code,
                                     CompanyName = ad.CompanyName,
                                     HshId = ad.HshId,
                                     NameHsh = sf.Name,
                                     ActiveStatus = ad.ActiveStatus,
                                     UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                                     OrderSeq = ad.OrderSeq,
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
                    joinquery = joinquery.OrderBy(x => x.Code).AsQueryable();
                }

                var list = await PaginatedList<EntitasPertaminaViewModel, EntitasPertaminaViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<EntitasPertaminaViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<EntitasPertaminaViewModel>>(false, ex.Message);
            }
        }

        public bool HasRecordByName(EntitasPertaminaViewModel model)
        {
            IQueryable<TblmEntitasPertamina> GetAllRecord = _context.TblmEntitasPertaminas.Where(x => x.DeletedDate == null && x.HshId == model.HshId && x.CompanyName == model.CompanyName);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasRecordByCode(EntitasPertaminaViewModel model)
        {
            IQueryable<TblmEntitasPertamina> GetAllRecord = _context.TblmEntitasPertaminas.Where(x => x.DeletedDate == null && x.HshId == model.HshId && x.Code == model.Code);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasRecordByNameWithoutId(EntitasPertaminaViewModel model)
        {
            IQueryable<TblmEntitasPertamina> GetAllRecord = _context.TblmEntitasPertaminas.Where(x => x.DeletedDate == null && x.Id != model.Id && x.HshId == model.HshId && x.CompanyName == model.CompanyName);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasRecordByCodeWithoutId(EntitasPertaminaViewModel model)
        {
            IQueryable<TblmEntitasPertamina> GetAllRecord = _context.TblmEntitasPertaminas.Where(x => x.DeletedDate == null && x.Id != model.Id && x.HshId == model.HshId && x.Code == model.Code);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetLastSeqNumber()
        {
            int result = 0;

            IQueryable<TblmEntitasPertamina> GetAllRecord = _context.TblmEntitasPertaminas.Where(x => x.DeletedDate == null);

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

        public void Save(EntitasPertaminaViewModel model)
        {
            _context.Set<TblmEntitasPertamina>().Add(_mapper.Map<TblmEntitasPertamina>(model));
            _context.SaveChanges();
        }

        public void Update(EntitasPertaminaViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TblmEntitasPertamina record = HasRecordById(model.Id);
            record.HshId = model.HshId;
            record.Code = model.Code;
            record.CompanyName = model.CompanyName;
            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TblmEntitasPertamina>().Update(record);
            _context.SaveChanges();
        }

        public void ActiveInActive(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TblmEntitasPertamina record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.ActiveStatus = record.ActiveStatus == true ? false : true;

            _context.Set<TblmEntitasPertamina>().Update(record);
            _context.SaveChanges();
        }

        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TblmEntitasPertamina record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TblmEntitasPertamina>().Update(record);
            _context.SaveChanges();
        }

        public TblmEntitasPertamina HasRecordById(Guid guid)
        {
            TblmEntitasPertamina result = _context.TblmEntitasPertaminas.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }

        public bool IsExistInFungsi(Guid guid)
        {
            IQueryable<TblmFungsi> GetAllRecord = _context.TblmFungsis.Where(x => x.DeletedDate == null && x.EntitasPertaminaId == guid);

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
            IQueryable<TbltOpportunityEntitasPertamina> GetAllRecord = _context.TbltOpportunityEntitasPertaminas.Where(x => x.DeletedDate == null && x.EntitasPertaminaId == guid);

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
        public bool ExistInAgreement(Guid guid)
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
        public EntitasPertaminaViewModel GetEntitasPertaminaById(Guid guid)
        {
            EntitasPertaminaViewModel result = _mapper.Map<EntitasPertaminaViewModel>(_context.TblmEntitasPertaminas.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }
    }
}