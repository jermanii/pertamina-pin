using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Utility.Sorting.Interfaces
{
    public interface ISortingFuncUtility
    {
        IQueryable<T> GetSortList<T>(IQueryable<T> list, string categorySort, string sortOrder);
    }
}
