using Microsoft.Extensions.DependencyInjection;
using Pertamina.IRIS.Utility.Pagination.Implement;
using Pertamina.IRIS.Utility.Pagination.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.DependencyInjection
{
    public static class PaginationInjection
    {
        public static IServiceCollection ConfigurePagination(this IServiceCollection services)
        {
            services.AddTransient<ITakeFuncUtility, TakeFuncUtility>();

            return services;
        }
    }
}
