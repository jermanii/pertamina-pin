using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface ISideMenuRepository
    {
        SideMenuViewModel GetListFeature();
        SideMenuViewModel GetListFeature(List<RoleFeatureViewModel> roleFeature);
        List<RoleFeatureViewModel> GetRoleFeature(List<string> roleName);
        List<RoleViewModel> GetRole();

    }
}
