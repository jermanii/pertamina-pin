using Microsoft.Extensions.DependencyInjection;
using Pertamina.IRIS.Utility.Sorting.Implement;
using Pertamina.IRIS.Utility.Sorting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.DependencyInjection
{
    public static class SortingInjection
    {
        public static IServiceCollection ConfigureSorting(this IServiceCollection services)
        {
            services.AddTransient<ISortingFuncUtility, SortingFuncUtility>();

            return services;
        }
    }
}
