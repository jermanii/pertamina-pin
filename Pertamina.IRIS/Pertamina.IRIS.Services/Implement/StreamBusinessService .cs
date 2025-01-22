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
    public class StreamBusinessService : IStreamBusinessService
    {
        private readonly IStreamBusinessRepository _repository;

        public StreamBusinessService(IStreamBusinessRepository repository)
        {
            _repository = repository;
        }

        public StreamBusinessViewModel Save(StreamBusinessViewModel model, string userName)
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

        protected StreamBusinessViewModel ValidationForm(StreamBusinessViewModel model, bool isCreate, bool isUpdate, bool isDelete)
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
            
            if(isUpdate)
            {
                if (model.OrderSeq == 0)
                {
                    model.IsError = true;
                    model.ErrorMessage = "Order must be greater than 0";
                }
                if (_repository.HasRecordByOrderUpdate(model.OrderSeq, model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.OrderSeq + " already exist!";
                }
                if (_repository.HasRecordByNameUpdate(model.Name, model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.Name + " already exist!";
                }
            }

            if (isDelete)
            {
                if (_repository.IsExistInOpportunity(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
                if (_repository.IsExistInInitiative(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
                if (_repository.IsExistInAgreement(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
            }
            return model;
        }

        public async Task<PaginationBaseModel<StreamBusinessViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {
            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<StreamBusinessViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public StreamBusinessViewModel Update(StreamBusinessViewModel model, string userName)
        {
            try
            {
                model = ValidationForm(model, false, true, false); ;
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

        public StreamBusinessViewModel Delete(Guid guid, string userName)
        {
            StreamBusinessViewModel model = new StreamBusinessViewModel();
            model.Id = guid;
            try
            {
                model = ValidationForm(model, false, false, true);
                if(model.IsError)
                {
                    return model;
                } else
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

        public StreamBusinessViewModel GetStreamBusinessById(Guid guid)
        {
            StreamBusinessViewModel model = new StreamBusinessViewModel();
            try
            {
                model = _repository.GetStreamBusinessById(guid);
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
