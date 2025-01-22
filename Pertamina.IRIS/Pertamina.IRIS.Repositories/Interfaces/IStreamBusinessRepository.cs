using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IStreamBusinessRepository
    {
        int GetLastSeqNumber();
        void Save(StreamBusinessViewModel model);
        bool HasRecordByName(string companyName);
        bool HasRecordByOrder(int? order);
        bool IsExistInOpportunity(Guid guid);
        bool IsExistInInitiative(Guid guid);
        bool IsExistInAgreement(Guid guid);
        void Delete(Guid guid, string userName);
        void Update(StreamBusinessViewModel model, string userName);
        StreamBusinessViewModel GetStreamBusinessById(Guid guid);
        bool HasRecordByOrderUpdate(int? order, Guid guid);
        bool HasRecordByNameUpdate(string name, Guid guid);
        Task<ResponseDataTableBaseModel<List<StreamBusinessViewModel>>> GetList(RequestFormDtBaseModel request);

    }
}
