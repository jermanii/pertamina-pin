using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class LandingPageService : ILandingPageService
    {
        private readonly ILandingPageRepository _repository;

        public LandingPageService(ILandingPageRepository repository)
        {
            _repository = repository;
        }

        public async Task<LinkPowerBiViewModel> GetLinkPowerBI()
        {

            LinkPowerBiViewModel model = new LinkPowerBiViewModel();

            try
            {
                model = await _repository.GetLinkPowerBI();
                model.IsError = true;

            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
            return model;
        }
        public async Task<string> GetDownloadUserManual()
        {
            string result = string.Empty;

            string featureName = EnumBaseModel.UserManual;

            result = await _repository.GetDownloadUserManual(featureName);

            return result;
        }

    }
}
