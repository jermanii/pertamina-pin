using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IKesiapanProyekRepository
    {
        Task<ResponseDataTableBaseModel<List<KesiapanProyekViewModel>>> GetList(RequestFormDtBaseModel request);
        int GetLastSeqNumber();
        void Save(KesiapanProyekViewModel model);
        void Delete(Guid guid, string username);
        void Update(KesiapanProyekViewModel model, string username);
        bool HasRecordByName(string name);
        bool HasRecordByNameUpdate(string name, Guid guid);
        bool HasRecordByOrderSeq(int? order);
        bool HasRecordByOrderSeqUpdate(int? order, Guid guid);
        bool IsExist(Guid guid);
        TblmKesiapanProyek HasRecordById(Guid guid);
        KesiapanProyekViewModel GetKesiapanProyekById(Guid guid);
    }
}
