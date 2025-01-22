using OfficeOpenXml;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IApplicationFileDownloadService
    {
        Task<byte[]> DownloadZip(Guid idIBI, string feature);
        Task<FileInfoViewModel> DownloadSingleFile(Guid guid, string feature);
    }
}