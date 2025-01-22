using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IStreamBusinessService
    {
        StreamBusinessViewModel Save(StreamBusinessViewModel model, string userName);
        Task<PaginationBaseModel<StreamBusinessViewModel>> GetListPaging(RequestFormDtBaseModel request);
        StreamBusinessViewModel Delete(Guid guid, string userName);
        StreamBusinessViewModel Update(StreamBusinessViewModel model, string userName);
        StreamBusinessViewModel GetStreamBusinessById(Guid guid);


    }
}
