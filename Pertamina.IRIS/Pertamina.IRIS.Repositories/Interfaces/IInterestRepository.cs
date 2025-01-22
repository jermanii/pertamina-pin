using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IInterestRepository
    {
        int GetLastSeqNumber();
        void Save(InterestViewModel model);
        bool HasRecordByName(string companyName);
        bool HasRecordByOrder(int? order);
        void Delete(Guid guid, string userName);
        void Update(InterestViewModel model, string userName);
        InterestViewModel GetInterestById(Guid guid);
        bool HasRecordByOrderUpdate(int? order, Guid guid);
        bool HasRecordByNameUpdate(string name, Guid guid);
        bool ExistInInitiative(Guid guid);
        Task<ResponseDataTableBaseModel<List<InterestViewModel>>> GetList(RequestFormDtBaseModel request);

    }
}
