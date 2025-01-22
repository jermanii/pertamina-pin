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
    public class ComingSoonController : WebControllerBase
    {
        private readonly ISideMenuService _sideMenuService;

        public ComingSoonController(IIdamanService idamanService, ISideMenuService sideMenuService) : base(idamanService)
        {
            _sideMenuService = sideMenuService ?? throw new ArgumentNullException(nameof(sideMenuService));
        }

        public IActionResult Index(bool isEp, bool isPo, bool isPi, bool isPd, bool isSa, bool isUd, bool isMd, bool isSt)
        {
            try
            {
                ComingSoonViewModel model = new ComingSoonViewModel();
                if (isEp)
                    model.ExistingFootprintsActive = "active";

                if (isPo)
                    model.ProjectToOfferActive = "active";

                if (isPi)
                    model.PotentialInitiativesActive = "active";

                if (isPd)
                    model.ProgramDevelopmentActive = "active";

                if (isSa)
                    model.SignedAgreementActive = "active";

                if (isUd)
                {
                    model.UserDashboardHere = "here badge badge-light-danger badge-lg";
                    model.HomeSubMenuHidden = "hidden";
                }

                if (isMd)
                {
                    model.MeetingDashboardHere = "here badge badge-light-danger badge-lg";
                    model.HomeSubMenuHidden = "hidden";
                }

                if (isSt)
                {
                    model.SupportTrackerHere = "here badge badge-light-danger badge-lg";
                    model.HomeSubMenuHidden = "hidden";
                }

                return View(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }
    }
}
