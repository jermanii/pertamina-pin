using Microsoft.Extensions.DependencyInjection;
using Pertamina.IRIS.Utility.Filtering.Implement;
using Pertamina.IRIS.Utility.Filtering.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.DependencyInjection
{
    public static class FilteringInjection
    {
        public static IServiceCollection ConfigureFiltering(this IServiceCollection services)
        {
            services.AddTransient<IHighPriorityFilteringUtility, HighPriorityFilteringUtility>();

            return services;
        }
    }
}
