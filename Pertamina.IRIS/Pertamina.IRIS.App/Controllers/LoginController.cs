using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;

namespace Pertamina.IRIS.App.Controllers
{
    public class LoginController : Controller
    {
        protected readonly IIdamanService _idamanService;
        public LoginController(IIdamanService idamanService)
        {
            _idamanService = idamanService ?? throw new ArgumentNullException(nameof(idamanService));
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Submit()
        {
            try
            {
                string email = User.Claims.Where(c => c.Type.Equals(OidBaseModel.Email)).FirstOrDefault().Value;
                string token = await HttpContext.GetTokenAsync("access_token");
                UserViewModel user = await _idamanService.GetUserAsync(email, token);
                HttpContext.Session.SetString("UserLogin", user.email);

                return RedirectToAction("Index", "LandingPage");
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return View("Error", error);
            }
        }
    }
}
