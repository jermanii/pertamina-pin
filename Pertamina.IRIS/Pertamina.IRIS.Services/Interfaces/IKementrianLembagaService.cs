using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IKementrianLembagaService
    {
        Task<PaginationBaseModel<KementrianLembagaViewModel>> GetListPaging(RequestFormDtBaseModel request);
        KementrianLembagaViewModel Save(KementrianLembagaViewModel model, string userName);
        KementrianLembagaViewModel Update(KementrianLembagaViewModel model, string userName);
        KementrianLembagaViewModel Delete(Guid guid, string userName);
        KementrianLembagaViewModel GetKementrianById(Guid guid);
    }
}
