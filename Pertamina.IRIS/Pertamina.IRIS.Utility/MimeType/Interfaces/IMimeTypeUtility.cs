using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Utility.MimeType.Interfaces
{
    public interface IMimeTypeUtility
    {
        string GetMimeType(string fileExtension);
    }
}
