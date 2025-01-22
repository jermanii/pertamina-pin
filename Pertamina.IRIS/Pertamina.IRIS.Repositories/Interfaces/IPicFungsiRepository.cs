using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IPicFungsiRepository
    {
        Task<ResponseDataTableBaseModel<List<PicFungsiViewModel>>> GetList(RequestFormDtBaseModel request);
        bool HasRecordByName(PicFungsiViewModel model);
        bool HasRecordByNameWithoutId(PicFungsiViewModel model);
        void Save(PicFungsiViewModel model);
        void Update(PicFungsiViewModel model, string userName);
        void Delete(Guid guid, string userName);
        void ActiveInActive(Guid guid, string userName);
        bool ExistInOpportunity(Guid guid);
        bool ExistInInitiative(Guid guid);
        bool ExistInAgreement(Guid guid);
        PicFungsiViewModel GetPicFungsiById(Guid guid);
    }
}
