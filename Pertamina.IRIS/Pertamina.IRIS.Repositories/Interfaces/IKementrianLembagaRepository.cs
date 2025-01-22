using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IKementrianLembagaRepository
    {
        Task<ResponseDataTableBaseModel<List<KementrianLembagaViewModel>>> GetList(RequestFormDtBaseModel request);
        void Save(KementrianLembagaViewModel model);
        void Delete(Guid guid, string userName);
        void Update(KementrianLembagaViewModel model, string username);
        bool HasRecordByName(string name);
        bool HasRecordByNameUpdate(string name, Guid guid);
        KementrianLembagaViewModel GetKementrianById(Guid guid);
        TblmKementrianLembaga HasRecordById(Guid guid);
        bool ExistInAgreement(Guid kementrianId);
    }
}
