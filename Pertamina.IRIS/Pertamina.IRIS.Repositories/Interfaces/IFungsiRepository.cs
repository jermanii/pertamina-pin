using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IFungsiRepository
    {
        Task<ResponseDataTableBaseModel<List<FungsiViewModel>>> GetList(RequestFormDtBaseModel request);
        bool HasRecordByName(FungsiViewModel model);
        bool HasRecordByNameWithoutId(FungsiViewModel model);
        bool ExistInPicFungsi(Guid fungsiId);
        void Save(FungsiViewModel model);
        void Update(FungsiViewModel model, string userName);
        void Delete(Guid guid, string userName);
        FungsiViewModel GetFungsiById(Guid guid);
    }
}
