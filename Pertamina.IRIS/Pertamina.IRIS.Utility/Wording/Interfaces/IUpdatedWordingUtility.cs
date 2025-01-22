using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Utility.Wording.Interfaces
{
    public interface IUpdatedWordingUtility
    {
        string GetUpdatedWording(DateTime? CreatedDate, DateTime? UpdatedDate);
    }
}
