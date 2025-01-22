using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IEntitasPertaminaRepository
    {
        Task<ResponseDataTableBaseModel<List<EntitasPertaminaViewModel>>> GetList(RequestFormDtBaseModel request);
        bool HasRecordByName(EntitasPertaminaViewModel model);
        bool HasRecordByCode(EntitasPertaminaViewModel model);
        bool HasRecordByNameWithoutId(EntitasPertaminaViewModel model);
        bool HasRecordByCodeWithoutId(EntitasPertaminaViewModel model);
        int GetLastSeqNumber();
        void Save(EntitasPertaminaViewModel model);
        void Update(EntitasPertaminaViewModel model, string userName);
        void Delete(Guid guid, string userName);
        void ActiveInActive(Guid guid, string userName);
        bool IsExistInFungsi(Guid guid);
        bool IsExistInOpportunity(Guid guid);
        EntitasPertaminaViewModel GetEntitasPertaminaById(Guid guid);
        bool ExistInInitiative(Guid id);
        bool ExistInAgreement(Guid id);
    }
}
