using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IKlasifikasiKendalaRepository
    {
        Task<ResponseDataTableBaseModel<List<KlasifikasiKendalaViewModel>>> GetList(RequestFormDtBaseModel request);
        int GetLastSeqNumber();
        void Save(KlasifikasiKendalaViewModel model);
        void Delete(Guid guid, string username);
        void Update(KlasifikasiKendalaViewModel model, string username);
        bool HasRecordByName(string name);
        bool HasRecordByNameUpdate(string name, Guid guid);
        bool HasRecordByOrderSeq(int? order);
        bool HasRecordByOrderSeqUpdate(int? order, Guid guid);
        bool ExistInAgreement(Guid guid);
        TblmKlasifikasiKendala HasRecordById(Guid guid);
        KlasifikasiKendalaViewModel GetKlasifikasiKendalaById(Guid guid);
    }
}
