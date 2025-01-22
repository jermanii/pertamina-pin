using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IPicFungsiService
    {
        Task<PaginationBaseModel<PicFungsiViewModel>> GetListPaging(RequestFormDtBaseModel request);
        PicFungsiViewModel Save(PicFungsiViewModel model, string userName);
        PicFungsiViewModel Update(PicFungsiViewModel model, string userName);
        PicFungsiViewModel Delete(Guid guid, string userName);
        PicFungsiViewModel GetPicFungsiById(Guid guid);
        PicFungsiViewModel ActiveInActive(Guid guid, string v);
    }
}
