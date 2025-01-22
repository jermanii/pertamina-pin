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
    public class InterestService : IInterestService
    {
        private readonly IInterestRepository _repository;

        public InterestService(IInterestRepository repository)
        {
            _repository = repository;
        }

        public InterestViewModel Save(InterestViewModel model, string userName)
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

        protected InterestViewModel ValidationForm(InterestViewModel model, bool isCreate, bool isUpdate, bool isDelete)
        {
            if(isCreate)
            {
                if (_repository.HasRecordByName(model.Name))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.Name + " already exist!";
                }
                if (_repository.HasRecordByOrder(model.OrderSeq))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.OrderSeq + " already exist!";
                }
                if (model.OrderSeq == 0)
                {
                    model.IsError = true;
                    model.ErrorMessage = "Order must be greater than 0";
                }
            }
            if (isUpdate)
            {
                if (_repository.HasRecordByNameUpdate(model.Name, model.Id.Value))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.Name + " already exist!";
                }
                if (model.OrderSeq == 0)
                {
                    model.IsError = true;
                    model.ErrorMessage = "Order must be greater than 0";
                }
                if (_repository.HasRecordByOrderUpdate(model.OrderSeq, model.Id.Value))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.OrderSeq + " already exist!";
                }
            }
            if (isDelete)
            {
                if (_repository.ExistInInitiative(model.Id.Value))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
            }
            
            return model;
        }

        public async Task<PaginationBaseModel<InterestViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {
            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<InterestViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public InterestViewModel Update(InterestViewModel model, string userName)
        {
            try
            {
                model = ValidationForm(model, false, true, false);
                if (model.IsError)
                {
                    return model;
                }

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

        public InterestViewModel Delete(Guid guid, string userName)
        {
            InterestViewModel model = new InterestViewModel();
            model.Id = guid;
            try
            {
                model = ValidationForm(model, false, false, true);
                if (model.IsError)
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

        public InterestViewModel GetInterestById(Guid guid)
        {
            InterestViewModel model = new InterestViewModel();
            try
            {
                model = _repository.GetInterestById(guid);
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
