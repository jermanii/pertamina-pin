using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface ILandingPageRepository
    {
        Task<LinkPowerBiViewModel> GetLinkPowerBI();
        Task<string> GetDownloadUserManual(string featureName);
    }
}
