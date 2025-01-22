using Microsoft.Extensions.DependencyInjection;
using Pertamina.IRIS.Services.Implement;
using Pertamina.IRIS.Services.Interfaces;
using Pertamina.IRIS.Utility.Wording.Implement;
using Pertamina.IRIS.Utility.Wording.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.DependencyInjection
{
    public static class WordingInjection
    {
        public static IServiceCollection ConfigureWording(this IServiceCollection services)
        {
            services.AddTransient<IUpdatedWordingUtility, UpdatedWordingUtility>();

            return services;
        }
    }
}