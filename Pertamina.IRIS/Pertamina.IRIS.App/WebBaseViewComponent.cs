using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;

namespace Pertamina.IRIS.App
{
    public abstract class WebBaseViewComponent : ViewComponent
    {
        protected readonly IIdamanService _idamanService;
        private const string User_Name = "UserName";

        private string _userName;
        public WebBaseViewComponent(IIdamanService idamanService)
        {
            _idamanService = idamanService ?? throw new ArgumentNullException(nameof(idamanService));
        }

        public async Task<List<string>> RolesAsync()
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            List<string> result = await _idamanService.GetRoleAsync(UserName, token);

            return result;
        }

        public string UserName
        {
            get
            {
                if (_userName == null && HttpContext.Session.GetString(User_Name) != null)
                {
                    _userName = HttpContext.Session.GetString(User_Name);
                }

                return _userName;
            }
            set
            {
                if (value == null)
                {
                    HttpContext.Session.Remove(User_Name);
                    _userName = null;
                    return;
                }
                HttpContext.Session.SetString(User_Name, value);
                _userName = value;
            }
        }
    }
}
