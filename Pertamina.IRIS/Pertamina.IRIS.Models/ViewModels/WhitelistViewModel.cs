using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class WhitelistViewModel
    {
        public int? next { get; set; }
        public int? start { get; set; }
        public int? total { get; set; }
        public List<ValueWhitelist> value { get; set; }
    }

    public class ApplicationWhitelist
    {
        public string id { get; set; }
        public string applicationName { get; set; }
    }

    public class RoleWhitelist
    {
        public string id { get; set; }
        public string roleName { get; set; }
        public ApplicationWhitelist application { get; set; }
    }

    public class UserWhitelist
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
    }

    public class ValueWhitelist
    {
        public string id { get; set; }
        public UserWhitelist user { get; set; }
        public List<RoleWhitelist> role { get; set; }
        public bool isDeleted { get; set; }
    }
}