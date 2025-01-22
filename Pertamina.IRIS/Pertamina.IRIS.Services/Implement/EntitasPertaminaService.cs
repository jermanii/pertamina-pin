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
    public class EntitasPertaminaService : IEntitasPertaminaService
    {
        private readonly IEntitasPertaminaRepository _repository;

        public EntitasPertaminaService(IEntitasPertaminaRepository repository)
        {
            _repository = repository;
        }

        protected EntitasPertaminaViewModel ValidationForm(EntitasPertaminaViewModel model, bool isCreate, bool isUpdate, bool isDelete)
        {
            if (isCreate)
            {
                if (_repository.HasRecordByCode(model))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.Code + " already exist!";
                }

                if (_repository.HasRecordByName(model))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.CompanyName + " already exist!";
                }
            }

            if (isUpdate)
            {
                if (_repository.HasRecordByCodeWithoutId(model))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.Code + " already exist!";
                }

                if (_repository.HasRecordByNameWithoutId(model))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.CompanyName + " already exist!";
                }
            }

            if (isDelete)
            {
                if (_repository.IsExistInFungsi(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
                if (_repository.IsExistInOpportunity(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
                if (_repository.ExistInInitiative(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
                if (_repository.ExistInAgreement(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
            }

            return model;
        }

        public async Task<PaginationBaseModel<EntitasPertaminaViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {
            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<EntitasPertaminaViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }

        public EntitasPertaminaViewModel GetEntitasPertaminaById(Guid guid)
        {
            EntitasPertaminaViewModel model = new EntitasPertaminaViewModel();
            try
            {
                model = _repository.GetEntitasPertaminaById(guid);
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

        public EntitasPertaminaViewModel Save(EntitasPertaminaViewModel model, string userName)
        {
            try
            {
                model = ValidationForm(model, true, false, false);
                if (model.IsError)
                {
                    return model;
                }

                DateTime now = DateTime.Now;
                int OrderSeq = _repository.GetLastSeqNumber();

                model.Id = Guid.NewGuid();
                model.CreateBy = userName;
                model.CreateDate = now;
                model.ActiveStatus = true;
                model.OrderSeq = OrderSeq;

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

        public EntitasPertaminaViewModel Update(EntitasPertaminaViewModel model, string userName)
        {
            try
            {
                model = ValidationForm(model, false, true, false);
                if (model.IsError)
                {
                    return model;
                }
                else
                {
                    model.IsError = false;
                    _repository.Update(model, userName);
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

        public EntitasPertaminaViewModel Delete(Guid guid, string userName)
        {
            EntitasPertaminaViewModel model = new EntitasPertaminaViewModel();
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

        public EntitasPertaminaViewModel ActiveInActive(Guid guid, string userName)
        {
            EntitasPertaminaViewModel model = new EntitasPertaminaViewModel();
            try
            {
                model.IsError = false;
                _repository.ActiveInActive(guid, userName);
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