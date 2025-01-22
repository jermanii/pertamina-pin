using Microsoft.Extensions.DependencyInjection;
using Pertamina.IRIS.Utility.MimeType.Implement;
using Pertamina.IRIS.Utility.MimeType.Interfaces;
using Pertamina.IRIS.Utility.Sorting.Implement;
using Pertamina.IRIS.Utility.Sorting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.DependencyInjection
{
    public static class MimeTypeInjection
    {
        public static IServiceCollection ConfigureMimeType(this IServiceCollection services)
        {
            services.AddTransient<IMimeTypeUtility, MimeTypeUtility>();

            return services;
        }
    }
}
