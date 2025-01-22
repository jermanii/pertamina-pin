using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IFungsiService
    {
        Task<PaginationBaseModel<FungsiViewModel>> GetListPaging(RequestFormDtBaseModel request);
        FungsiViewModel Save(FungsiViewModel model, string userName);
        FungsiViewModel Update(FungsiViewModel model, string userName);
        FungsiViewModel Delete(Guid guid, string userName);
        FungsiViewModel GetFungsiById(Guid guid);
    }
}
