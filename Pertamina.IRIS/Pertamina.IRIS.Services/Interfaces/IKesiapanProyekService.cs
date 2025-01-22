using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IKesiapanProyekService
    {
        Task<PaginationBaseModel<KesiapanProyekViewModel>> GetListPaging(RequestFormDtBaseModel request);
        KesiapanProyekViewModel Save(KesiapanProyekViewModel model, string userName);
        KesiapanProyekViewModel Update(KesiapanProyekViewModel model, string userName);
        KesiapanProyekViewModel Delete(Guid guid, string userName);
        KesiapanProyekViewModel GetKesiapanProyekById(Guid guid);

    }
}
