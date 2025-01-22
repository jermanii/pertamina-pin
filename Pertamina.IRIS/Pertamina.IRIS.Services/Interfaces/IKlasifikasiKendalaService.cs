using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IKlasifikasiKendalaService
    {
        Task<PaginationBaseModel<KlasifikasiKendalaViewModel>> GetListPaging(RequestFormDtBaseModel request);
        KlasifikasiKendalaViewModel Save(KlasifikasiKendalaViewModel model, string userName);
        KlasifikasiKendalaViewModel Update(KlasifikasiKendalaViewModel model, string userName);
        KlasifikasiKendalaViewModel Delete(Guid guid, string userName);
        KlasifikasiKendalaViewModel GetKlasifikasiKendalaById(Guid guid);
    }
}
