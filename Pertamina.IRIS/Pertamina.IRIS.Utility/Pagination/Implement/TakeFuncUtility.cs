using Pertamina.IRIS.Utility.Pagination.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Utility.Pagination.Implement
{
    public class TakeFuncUtility : ITakeFuncUtility
    {
        public IQueryable<T> GetTakeList<T>(IQueryable<T> list, int pageCountSort)
        {
            return list.Take(pageCountSort * 5);
        }
    }
}