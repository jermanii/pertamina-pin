using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App.Helpers
{
    public static class HtmlHelper
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController);

            return returnActive ? "active" : "";
        }
        public static string IsOpen(this IHtmlHelper htmlHelper, FeatureViewModel model)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = model.ChildFeatures.Any(x => x.ControllerName == routeController && x.ParentFeatureCode == model.Code);

            return returnActive ? "show" : "";
        }
        public static string IsAdd(ISession session)
        {
            string result = string.Empty;
            bool Is_Add = false;

            List<RoleViewModel> mappingRoles = new List<RoleViewModel>();
            mappingRoles = SessionHelper.GetObjectFromJson<List<RoleViewModel>>(session, "Mappingroles");
            List<string> roleList = new List<string>();
            roleList = SessionHelper.GetObjectFromJson<List<string>>(session, "RoleList");

            foreach (var rec in roleList)
            {
                RoleViewModel role = mappingRoles.Where(x => x.Name == rec).FirstOrDefault();

                if (Is_Add == false)
                {
                    if (role.IsAdd == true)
                    {
                        Is_Add = true;
                        result = "";
                    }
                    else
                    {
                        result = "hidden";
                    }
                }
            }

            return result;
        }
        public static string IsEdit(ISession session)
        {
            string result = string.Empty;
            bool Is_Edit = false;

            List<RoleViewModel> mappingRoles = new List<RoleViewModel>();
            mappingRoles = SessionHelper.GetObjectFromJson<List<RoleViewModel>>(session, "Mappingroles");
            List<string> roleList = new List<string>();
            roleList = SessionHelper.GetObjectFromJson<List<string>>(session, "RoleList");

            foreach (var rec in roleList)
            {
                RoleViewModel role = mappingRoles.Where(x => x.Name == rec).FirstOrDefault();

                if (Is_Edit == false)
                {
                    if (role.IsEdit == true)
                    {
                        Is_Edit = true;
                        result = "";
                    }
                    else
                    {
                        result = "hidden";
                    }
                }
            }

            return result;
        }
        public static string IsDelete(ISession session)
        {
            string result = string.Empty;
            bool Is_Delete = false;

            List<RoleViewModel> mappingRoles = new List<RoleViewModel>();
            mappingRoles = SessionHelper.GetObjectFromJson<List<RoleViewModel>>(session, "Mappingroles");
            List<string> roleList = new List<string>();
            roleList = SessionHelper.GetObjectFromJson<List<string>>(session, "RoleList");

            foreach (var rec in roleList)
            {
                RoleViewModel role = mappingRoles.Where(x => x.Name == rec).FirstOrDefault();

                if (Is_Delete == false)
                {
                    if (role.IsDelete == true)
                    {
                        Is_Delete = true;
                        result = "";
                    }
                    else
                    {
                        result = "hidden";
                    }
                }
            }

            return result;
        }
        public static List<string> AllRoles(ISession session)
        {
            List<string> result = new List<string>();

            result = SessionHelper.GetObjectFromJson<List<string>>(session, "RoleList");

            return result;
        }
        public static ProfileViewModel Profile(ISession session)
        {
            ProfileViewModel result = new ProfileViewModel();
            result = SessionHelper.GetObjectFromJson<ProfileViewModel>(session, "Profile");

            return result;
        }
    }
}
