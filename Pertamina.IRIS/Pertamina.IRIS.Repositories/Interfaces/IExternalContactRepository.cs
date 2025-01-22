using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IExternalContactRepository
    {
        Task<ResponseDataTableBaseModel<List<ExternalContactViewModel>>> GetList(RequestFormDtBaseModel request);
        void Save(ExternalContactViewModel model);
        void Delete(Guid guid, string userName);
        TbltExternalContact HasRecordById(Guid guid);
        ExternalContactViewModel GetExternalContactById(Guid guid);
        void Update(ExternalContactViewModel model, string userName);
        IEnumerable<TbltExternalContact> GetAllExternalContact();
        Task<ExternalContactViewModel> ImportXLSData(List<List<string>> xlsData, string fileName, long fileSize);

    }
}
