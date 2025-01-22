using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.Base
{
    public class AppSettingsBaseModel
    {
        public string ApplicationCookiesName { get; set; }
        public string ApplicationFolderApp { get; set; }
        public string IdamanUrlLogin { get; set; }
        public string IdamanUrlApi { get; set; }
        public string IdamanClientId { get; set; }
        public string IdamanObjectId { get; set; }
        public string IdamanClientSecret { get; set; }
        public string IdamanScopes { get; set; }
        public string DigitalSignatureApi { get; set; }
    }
}
