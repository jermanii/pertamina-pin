using Pertamina.IRIS.Utility.Sorting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Utility.Sorting.Implement
{
    public class SortingFuncUtility : ISortingFuncUtility
    {
        public IQueryable<T> GetSortList<T>(IQueryable<T> list, string categorySort, string sortOrder)
        {
            Func<T, object> orderByFunc = h =>
            {
                var propertyInfo = typeof(T).GetProperty(categorySort);
                return propertyInfo != null ? propertyInfo.GetValue(h, null) : null;
            };

            var result = sortOrder == "asc" ? list.OrderBy(orderByFunc).AsQueryable() : list.OrderByDescending(orderByFunc).AsQueryable();

            return result;
        }
    }
}
