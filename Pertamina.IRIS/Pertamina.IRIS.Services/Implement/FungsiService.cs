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
    public class FungsiService : IFungsiService
    {
        private readonly IFungsiRepository _repository;

        public FungsiService(IFungsiRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginationBaseModel<FungsiViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {
            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<FungsiViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }

        protected FungsiViewModel ValidationForm(FungsiViewModel model, bool isCreate, bool isUpdate, bool isDelete)
        {
            if (isCreate)
            {
                if (_repository.HasRecordByName(model))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.FungsiName + " already exist!";
                }
            }

            if (isUpdate)
            {
                if (_repository.HasRecordByNameWithoutId(model))
                {
                    model.IsError = true;
                    model.ErrorMessage = model.FungsiName + " already exist!";
                }
            }

            if (isDelete)
            {
                if (_repository.ExistInPicFungsi(model.Id))
                {
                    model.IsError = true;
                    model.ErrorMessage = "The Record Could Not be Deleted Because It is Associated With Another Records. ";
                }
            }

            return model;
        }

        public FungsiViewModel Save(FungsiViewModel model, string userName)
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

        public FungsiViewModel Update(FungsiViewModel model, string userName)
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

        public FungsiViewModel Delete(Guid guid, string userName)
        {
            FungsiViewModel model = new FungsiViewModel();
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

        public FungsiViewModel GetFungsiById(Guid guid)
        {
            FungsiViewModel model = new FungsiViewModel();
            try
            {
                model = _repository.GetFungsiById(guid);
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
