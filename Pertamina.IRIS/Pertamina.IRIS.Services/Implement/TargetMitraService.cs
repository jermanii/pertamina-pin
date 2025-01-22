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
    public class TargetMitraService : ITargetMitraService
    {
        private readonly ITargetMitraRepository _repository;

        public TargetMitraService(ITargetMitraRepository repository)
        {
            _repository = repository;
        }

        public TargetMitraViewModel Save(TargetMitraViewModel model, string userName)
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
        public TargetMitraViewModel Update(TargetMitraViewModel model, string userName)
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

        protected TargetMitraViewModel ValidationForm(TargetMitraViewModel model, bool isCreate, bool isUpdate, bool isDelete)
        {
            if (isCreate)
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
                if (_repository.HasRecordByNameUpdate(model.Name, model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.Name + " already exist!";
                }
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
            }
            if(isDelete)
            {
                if (_repository.IsExistInOpportunity(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
            }
            return model;
        }
       

        public async Task<PaginationBaseModel<TargetMitraViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {
            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<TargetMitraViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public TargetMitraViewModel Delete(Guid guid, string userName)
        {
            TargetMitraViewModel model = new TargetMitraViewModel();
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
        public TargetMitraViewModel GetTargetMitraById(Guid guid)
        {
            TargetMitraViewModel model = new TargetMitraViewModel();
            try
            {
                model = _repository.GetTargetMitraById(guid);
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
