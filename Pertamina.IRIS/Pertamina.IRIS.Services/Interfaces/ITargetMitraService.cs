using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface ITargetMitraService
    {
        TargetMitraViewModel Save(TargetMitraViewModel model, string userName);
        Task<PaginationBaseModel<TargetMitraViewModel>> GetListPaging(RequestFormDtBaseModel request);
        TargetMitraViewModel Delete(Guid guid, string userName);
        TargetMitraViewModel GetTargetMitraById(Guid guid);
        TargetMitraViewModel Update(TargetMitraViewModel model, string userName);

    }
}
