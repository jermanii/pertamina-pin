using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Utility.Pagination.Interfaces
{
    public interface ITakeFuncUtility
    {
        IQueryable<T> GetTakeList<T>(IQueryable<T> list, int pageCountSort);
    }
}
