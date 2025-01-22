using Pertamina.IRIS.Models.ViewModels; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IApplicationFileDownloadRepository
    {
        Task<string> GetPath(string featureName); 
    }
}