using OfficeOpenXml;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IExternalContactService
    {
        Task<PaginationBaseModel<ExternalContactViewModel>> GetListPaging(RequestFormDtBaseModel request);
        List<ExternalContactViewModel> Save(ExternalContactViewModel model, string userName);
        ExternalContactViewModel Delete(Guid guid, string userName);
        ExternalContactViewModel Update(ExternalContactViewModel model, string userName);
        ExternalContactViewModel GetExternalContactById(Guid guid);
        ExcelPackage ExportXLS(bool withData, string searchKeyword);
        ExcelPackage ExportXLSThisPage(bool withData, string searchQuery, int pageNumber, int pageSize);
        Task<ExternalContactViewModel> readExcelPackage(Stream fileInfo, string fileName);
    }
}
