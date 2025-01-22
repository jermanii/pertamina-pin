using AutoMapper;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class KlasifikasiKendalaService : IKlasifikasiKendalaService
    {
        private IKlasifikasiKendalaRepository _repository;
        private readonly IMapper _mapper;
        public KlasifikasiKendalaService(IKlasifikasiKendalaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginationBaseModel<KlasifikasiKendalaViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {
            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<KlasifikasiKendalaViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }

        public KlasifikasiKendalaViewModel Save(KlasifikasiKendalaViewModel model, string userName)
        {
            try
            {
                model = ValidationForm(model, true, false, false);
                if (model.IsError)
                {
                    return model;
                }

                DateTime now = DateTime.Now;

                model.Id = Guid.NewGuid();
                model.CreateBy = userName;
                model.CreateDate = now;

                _repository.Save(model);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }

        public KlasifikasiKendalaViewModel Update(KlasifikasiKendalaViewModel model, string userName)
        {
            try
            {
                model = ValidationForm(model, false, true, false);
                if (model.IsError)
                {
                    return model;
                }

                model.IsError = false;
                _repository.Update(model, userName);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }

        public KlasifikasiKendalaViewModel Delete(Guid guid, string userName)
        {
            KlasifikasiKendalaViewModel model = new KlasifikasiKendalaViewModel();
            model.Id = guid;
            try
            {
                model = ValidationForm(model, false, false, true);
                if (model.IsError)
                {
                    return model;
                }
                else
                {
                    model.IsError = false;
                    _repository.Delete(guid, userName);
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
            return model;
        }



        protected KlasifikasiKendalaViewModel ValidationForm(KlasifikasiKendalaViewModel model, bool isCreate, bool isUpdate, bool isDelete)
        {
            if (isCreate)
            {
                if (_repository.HasRecordByName(model.Name))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.Name + " already exist";
                }
                if (_repository.HasRecordByOrderSeq(model.OrderSeq))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.OrderSeq + " already exist";
                }
                if (model.OrderSeq == 0)
                {
                    model.IsError = true;
                    model.ErrorMessage = "Order must be greater than 0";
                }
            }

            if (isUpdate)
            {
                if (_repository.HasRecordByNameUpdate(model.Name, model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.Name + " already exist";
                }
                if (_repository.HasRecordByOrderSeqUpdate(model.OrderSeq, model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.OrderSeq + " already exist";
                }
                if (model.OrderSeq == 0)
                {
                    model.IsError = true;
                    model.ErrorMessage = "Order must be greater than 0";
                }
            }

            if (isDelete)
            {
                if (_repository.ExistInAgreement(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
            }

            return model;
        }


        public KlasifikasiKendalaViewModel GetKlasifikasiKendalaById(Guid guid)
        {
            KlasifikasiKendalaViewModel model = new KlasifikasiKendalaViewModel();
            try
            {
                model = _repository.GetKlasifikasiKendalaById(guid);
                model.IsError = true;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
            return model;
        }
    }
}
