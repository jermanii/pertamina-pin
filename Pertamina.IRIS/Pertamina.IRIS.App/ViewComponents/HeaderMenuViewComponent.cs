using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Pertamina.IRIS.App.ViewComponents
{
    [SessionTimeout]
    public class HeaderMenuViewComponent : WebBaseViewComponent
    {
        private readonly IHeaderMenuService _headerMenuService;
        public HeaderMenuViewComponent(IIdamanService idamanService, IHeaderMenuService headerMenuService) : base(idamanService)
        {
            _headerMenuService = headerMenuService ?? throw new ArgumentNullException(nameof(headerMenuService));
        }

        public IViewComponentResult Invoke()
        {
            HeaderMenuViewModel notif = new HeaderMenuViewModel();

            notif = _headerMenuService.GetListNotifs(UserName);
            return View(notif);
        }
    }
}
