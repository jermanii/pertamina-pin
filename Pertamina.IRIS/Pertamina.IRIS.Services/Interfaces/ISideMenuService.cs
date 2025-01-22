using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface ISideMenuService
    {
        SideMenuViewModel GetListFeature();
        SideMenuViewModel GetListFeature(List<string> roleName);
        List<RoleViewModel> GetRole();
    }
}
