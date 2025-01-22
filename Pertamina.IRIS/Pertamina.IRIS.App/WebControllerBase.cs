using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pertamina.IRIS.App.Helpers;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;

namespace Pertamina.IRIS.App
{
    public class WebControllerBase : Controller
    {
        protected readonly IIdamanService _idamanService;

        private const string User_Name = "UserName";
        private const string roles = "RoleList";
        private const string mappingroles = "Mappingroles";
        private const string profile = "Profile";

        private string _userName;
        private List<string> _roles;
        private List<RoleViewModel> _mappingroles;
        private ProfileViewModel _profile;

        public WebControllerBase(IIdamanService idamanService)
        {
            _idamanService = idamanService ?? throw new ArgumentNullException(nameof(idamanService));
        }

        public async Task<List<string>> RolesAsync()
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            List<string> result = await _idamanService.GetRoleAsync(UserName, token);

            return result;
        }
        public async Task<ProfileViewModel> ProfileAsync()
        {
            string token = await HttpContext.GetTokenAsync("access_token");

            var user = await _idamanService.GetUserAsync(UserName, token);

            ProfileViewModel result = new ProfileViewModel();

            result.DisplayName = user.displayName;
            result.companyName = user.companyName;
            result.companyCode = user.companyCode;
            return result;
        }

        #region Session
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
        public List<string> RolesSession
        {
            get
            {
                if (_roles == null && SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, roles) != null)
                {
                    _roles = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, roles);
                }

                return _roles;
            }
            set
            {
                if (value == null)
                {
                    HttpContext.Session.Clear();
                    _roles = null;
                    return;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, roles, value);
                _roles = value;
            }
        }
        public List<RoleViewModel> MappingRolesSession
        {
            get
            {
                if (_mappingroles == null && SessionHelper.GetObjectFromJson<List<RoleViewModel>>(HttpContext.Session, mappingroles) != null)
                {
                    _mappingroles = SessionHelper.GetObjectFromJson<List<RoleViewModel>>(HttpContext.Session, mappingroles);
                }

                return _mappingroles;
            }
            set
            {
                if (value == null)
                {
                    HttpContext.Session.Clear();
                    _mappingroles = null;
                    return;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, mappingroles, value);
                _mappingroles = value;
            }
        }
        public ProfileViewModel ProfileSession
        {
            get
            {
                if (_profile == null && SessionHelper.GetObjectFromJson<ProfileViewModel>(HttpContext.Session, profile) != null)
                {
                    _profile = SessionHelper.GetObjectFromJson<ProfileViewModel>(HttpContext.Session, profile);
                }

                return _profile;
            }
            set
            {
                if (value == null)
                {
                    HttpContext.Session.Clear();
                    _profile = null;
                    return;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, profile, value);
                _profile = value;
            }
        }
        public void ResetSession()
        {
            UserName = null;
            ProfileSession = null;
            MappingRolesSession = null;
            RolesSession = null;
            Response.Clear();
        }
        #endregion
    }
}
