using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IInterestService
    {
        InterestViewModel Save(InterestViewModel model, string userName);
        Task<PaginationBaseModel<InterestViewModel>> GetListPaging(RequestFormDtBaseModel request);
        InterestViewModel Delete(Guid guid, string userName);
        InterestViewModel Update(InterestViewModel model, string userName);
        InterestViewModel GetInterestById(Guid guid);

    }
}
