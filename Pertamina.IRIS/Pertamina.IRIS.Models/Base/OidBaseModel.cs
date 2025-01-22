using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.Base
{
    public class OidBaseModel
    {
        public static string Sub { get; set; } = "sub";
        public static string Idp { get; set; } = "idp";
        public static string Email { get; set; } = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    }
}
