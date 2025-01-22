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
    public class KementrianLembagaService : IKementrianLembagaService
    {
        private IKementrianLembagaRepository _repository;
        private readonly IMapper _mapper;
        public KementrianLembagaService(IKementrianLembagaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginationBaseModel<KementrianLembagaViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {

            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<KementrianLembagaViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public KementrianLembagaViewModel Save(KementrianLembagaViewModel model, string userName)
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
        public KementrianLembagaViewModel Update(KementrianLembagaViewModel model, string userName)
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
        public KementrianLembagaViewModel Delete(Guid guid, string userName)
        {
            KementrianLembagaViewModel model = new KementrianLembagaViewModel();
            model.Id = guid;
            try
            {
                model = ValidationForm(model, false, false, true);
                if(model.IsError)
                {
                    return model;
                }
                _repository.Delete(guid, userName);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        protected KementrianLembagaViewModel ValidationForm(KementrianLembagaViewModel model, bool isCreate, bool isUpdate, bool isDelete)
        {
            if(isCreate)
            {
                if (_repository.HasRecordByName(model.LembagaName))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.LembagaName + " already exist";
                }
            }
            if(isUpdate)
            {
                if (_repository.HasRecordByNameUpdate(model.LembagaName, model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.LembagaName + " already exist";
                }
            }
            if(isDelete)
            {
                if (_repository.ExistInAgreement(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
            }
            return model;
        }
        

        public KementrianLembagaViewModel GetKementrianById(Guid guid)
        {
            KementrianLembagaViewModel model = new KementrianLembagaViewModel();
            try
            {
                model = _repository.GetKementrianById(guid);
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
