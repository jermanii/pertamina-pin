using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Services.Implement
{
    public class SideMenuService : ISideMenuService
    {
        private readonly ISideMenuRepository _sideMenuRepository;

        public SideMenuService(ISideMenuRepository fileTransferRepository)
        {
            _sideMenuRepository = fileTransferRepository;
        }

        public SideMenuViewModel GetListFeature()
        {
            SideMenuViewModel result = new SideMenuViewModel();
            result = _sideMenuRepository.GetListFeature();
            return result;
        }

        public SideMenuViewModel GetListFeature(List<string> roleName)
        {
            SideMenuViewModel result = new SideMenuViewModel();

            List<RoleFeatureViewModel> roleFeature = new List<RoleFeatureViewModel>();
            roleFeature = _sideMenuRepository.GetRoleFeature(roleName);

            result = _sideMenuRepository.GetListFeature(roleFeature);

            return result;
        }

        public List<RoleViewModel> GetRole()
        {
            List<RoleViewModel> result = new List<RoleViewModel>();

            result = _sideMenuRepository.GetRole();

            return result;
        }
    }
}