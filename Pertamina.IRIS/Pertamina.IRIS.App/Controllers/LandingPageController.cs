using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using System;

namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class LandingPageController : WebControllerBase
    {
        private readonly ILandingPageService _service;
        private readonly ISideMenuService _sideMenuService;

        public LandingPageController(IIdamanService idamanService, ILandingPageService service, ISideMenuService sideMenuService) : base(idamanService)
        {
            _service = service;
            _sideMenuService = sideMenuService ?? throw new ArgumentNullException(nameof(sideMenuService));
        }
        public IActionResult Index()
        {
            try
            {
                UserName = HttpContext.Session.GetString("UserLogin");
                RolesSession = RolesAsync().Result;
                MappingRolesSession = _sideMenuService.GetRole();
                ProfileSession = ProfileAsync().Result;

                if (RolesSession.Count > 0)
                    return RedirectToAction("Index", "ExistingFootprintsDashboard");
                else
                    return View();
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }

        public IActionResult Logout()
        {
            try
            {
                ResetSession();
                var authenticationProperties = new AuthenticationProperties
                {
                    RedirectUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}",
                };
                return SignOut(new AuthenticationProperties { RedirectUri = authenticationProperties.RedirectUri }, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }
    }
}
