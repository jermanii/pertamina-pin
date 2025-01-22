using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IEntitasPertaminaService
    {
        Task<PaginationBaseModel<EntitasPertaminaViewModel>> GetListPaging(RequestFormDtBaseModel request);
        EntitasPertaminaViewModel GetEntitasPertaminaById(Guid guid);
        EntitasPertaminaViewModel Save(EntitasPertaminaViewModel model, string userName);
        EntitasPertaminaViewModel Update(EntitasPertaminaViewModel model, string userName);
        EntitasPertaminaViewModel Delete(Guid guid, string userName);
        EntitasPertaminaViewModel ActiveInActive(Guid guid, string userName);
    }
}
