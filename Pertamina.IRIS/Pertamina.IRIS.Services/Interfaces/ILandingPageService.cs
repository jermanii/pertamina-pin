using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface ILandingPageService
    {
        Task<LinkPowerBiViewModel> GetLinkPowerBI();
        Task<string> GetDownloadUserManual();
    }
}
