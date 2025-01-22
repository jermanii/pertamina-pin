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
    public class PicFungsiService : IPicFungsiService
    {
        private readonly IPicFungsiRepository _repository;

        public PicFungsiService(IPicFungsiRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginationBaseModel<PicFungsiViewModel>> GetListPaging(RequestFormDtBaseModel request)
        {
            var resultData = await _repository.GetList(request);

            var result = new PaginationBaseModel<PicFungsiViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }

        protected PicFungsiViewModel ValidationForm(PicFungsiViewModel model, bool isCreate, bool isUpdate, bool isDelete)
        {
            if (isCreate)
            {
            }

            if (isUpdate)
            {
            }

            if (isDelete)
            {
                if (_repository.ExistInOpportunity(model.Id))
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

        public PicFungsiViewModel Save(PicFungsiViewModel model, string userName)
        {
            try
            {
                DateTime now = DateTime.Now;

                model.Id = Guid.NewGuid();
                model.CreateBy = userName;
                model.CreateDate = now;
                model.ActiveStatus = true;

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

        public PicFungsiViewModel Update(PicFungsiViewModel model, string userName)
        {
            try
            {
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

        public PicFungsiViewModel Delete(Guid guid, string userName)
        {
            PicFungsiViewModel model = new PicFungsiViewModel();
            model.Id = guid;
            try
            {
                model = ValidationForm(model, false, false, true);
                if (model.IsError)
                {
                    return model;
                }

                model.IsError = false;
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

        public PicFungsiViewModel GetPicFungsiById(Guid guid)
        {
            PicFungsiViewModel model = new PicFungsiViewModel();
            try
            {
                model = _repository.GetPicFungsiById(guid);
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

        public PicFungsiViewModel ActiveInActive(Guid guid, string userName)
        {
            PicFungsiViewModel model = new PicFungsiViewModel();
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
