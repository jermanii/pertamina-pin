using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface ITargetMitraRepository
    {
        int GetLastSeqNumber();
        void Save(TargetMitraViewModel model);
        bool HasRecordByName(string companyName);
        Task<ResponseDataTableBaseModel<List<TargetMitraViewModel>>> GetList(RequestFormDtBaseModel request);
         bool HasRecordByOrder(int? order);
        bool HasRecordByOrderUpdate(int? order, Guid guid);
        bool HasRecordByNameUpdate(string name, Guid guid);
        bool IsExistInOpportunity(Guid guid);
        void Delete(Guid guid, string userName);
        TargetMitraViewModel GetTargetMitraById(Guid guid);
        void Update(TargetMitraViewModel model, string userName);


    }
}
