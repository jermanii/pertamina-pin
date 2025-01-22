using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.ViewComponents
{
    [SessionTimeout]
    public class SideMenuViewComponent : WebBaseViewComponent
    {
        private readonly ISideMenuService _sideMenuService;
        public SideMenuViewComponent(IIdamanService idamanService, ISideMenuService sideMenuService) : base(idamanService)
        {
            _sideMenuService = sideMenuService ?? throw new ArgumentNullException(nameof(sideMenuService));
        }

        public IViewComponentResult Invoke()
        {
            SideMenuViewModel feature = new SideMenuViewModel();
            List<string> roles = RolesAsync().Result;
            feature = _sideMenuService.GetListFeature(roles);
            return View(feature);
        }
    }
}
