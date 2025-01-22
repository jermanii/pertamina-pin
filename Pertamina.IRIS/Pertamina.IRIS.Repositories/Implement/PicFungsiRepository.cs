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
    public class PicFungsiRepository : IPicFungsiRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;

        public PicFungsiRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseDataTableBaseModel<List<PicFungsiViewModel>>> GetList(RequestFormDtBaseModel request)
        {
            try
            {
                var query = _context.TblmPicFungsis.AsQueryable();

                // Filtering data`
                var predicate = PredicateBuilder.New<TblmPicFungsi>(true);
                foreach (var filter in request.Filters)
                {
                    predicate = predicate.Or(x => (x.PicName.Contains(filter.Value))).Or(x => (x.Phone.Contains(filter.Value)));
                }

                query = query.Where(predicate);
                query = query.Where(x => x.DeletedDate == null);

                var joinquery = query.Join(_context.TblmFungsis, ad => ad.FungsiId, sf => sf.Id, (ad, sf) =>
                                 new PicFungsiViewModel()
                                 {
                                     Id = ad.Id,
                                     FungsiId = ad.FungsiId,
                                     NameFungsi = sf.FungsiName,
                                     IdEntitasPertamina = sf.EntitasPertaminaId,
                                     PicName = ad.PicName,
                                     Phone = ad.Phone,
                                     Email = ad.Email,
                                     ActiveStatus = ad.ActiveStatus,
                                     UpdateDate = ad.UpdateDate == null ? ad.CreateDate : ad.UpdateDate,
                                 }).Join(_context.TblmEntitasPertaminas, ad => ad.IdEntitasPertamina, sf => sf.Id, (ad, sf) =>
                                 new PicFungsiViewModel()
                                 {
                                     Id = ad.Id,
                                     IdEntitasPertamina = sf.Id,
                                     NameEntitasPertamina = sf.CompanyName,
                                     FungsiId = ad.FungsiId,
                                     NameFungsi = ad.NameFungsi,
                                     PicName = ad.PicName,
                                     Phone = ad.Phone,
                                     Email = ad.Email,
                                     ActiveStatus = ad.ActiveStatus,
                                     UpdateDate = ad.UpdateDate,
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
                    joinquery = joinquery.OrderBy(x => x.PicName).AsQueryable();
                }

                var list = await PaginatedList<PicFungsiViewModel, PicFungsiViewModel>.CreateAsync(joinquery, request.PageValue, request.PageSize, joinquery.Count(), _mapper);

                return new ResponseDataTableBaseModel<List<PicFungsiViewModel>>(list, list.ListInfo);
            }
            catch (Exception ex)
            {
                return new ResponseDataTableBaseModel<List<PicFungsiViewModel>>(false, ex.Message);
            }
        }

        public bool HasRecordByName(PicFungsiViewModel model)
        {
            IQueryable<TblmPicFungsi> GetAllRecord = _context.TblmPicFungsis.Where(x => x.DeletedDate == null && x.FungsiId == model.FungsiId && x.PicName == model.PicName);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasRecordByNameWithoutId(PicFungsiViewModel model)
        {
            IQueryable<TblmPicFungsi> GetAllRecord = _context.TblmPicFungsis.Where(x => x.DeletedDate == null && x.Id != model.Id && x.FungsiId == model.FungsiId && x.PicName == model.PicName);

            if (GetAllRecord.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistInOpportunity(Guid guid)
        {
            IQueryable<TbltOpportunityPicFungsi> IsExist = _context.TbltOpportunityPicFungsis.Where(x => x.PicFungsiId == guid);

            if (IsExist.Count() > 0)
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
        public void Save(PicFungsiViewModel model)
        {
            _context.Set<TblmPicFungsi>().Add(_mapper.Map<TblmPicFungsi>(model));
            _context.SaveChanges();
        }

        public void Update(PicFungsiViewModel model, string userName)
        {
            DateTime now = DateTime.Now;

            TblmPicFungsi record = HasRecordById(model.Id);
            record.FungsiId = model.FungsiId;
            record.PicName = model.PicName;
            record.Phone = model.Phone;
            record.Email = model.Email;
            record.UpdateBy = userName;
            record.UpdateDate = now;

            _context.Set<TblmPicFungsi>().Update(record);
            _context.SaveChanges();
        }

        public void Delete(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TblmPicFungsi record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.DeletedDate = now;

            _context.Set<TblmPicFungsi>().Update(record);
            _context.SaveChanges();
        }

        public void ActiveInActive(Guid guid, string userName)
        {
            DateTime now = DateTime.Now;

            TblmPicFungsi record = HasRecordById(guid);
            record.UpdateBy = userName;
            record.UpdateDate = now;
            record.ActiveStatus = record.ActiveStatus == true ? false : true;

            _context.Set<TblmPicFungsi>().Update(record);
            _context.SaveChanges();
        }

        public PicFungsiViewModel GetPicFungsiById(Guid guid)
        {
            PicFungsiViewModel result = _mapper.Map<PicFungsiViewModel>(_context.TblmPicFungsis.Where(x => x.Id == guid).FirstOrDefault());
            return result;
        }

        private TblmPicFungsi HasRecordById(Guid guid)
        {
            TblmPicFungsi result = _context.TblmPicFungsis.Where(x => x.Id == guid).FirstOrDefault();
            return result;
        }
    }
}
