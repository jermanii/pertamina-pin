using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IMetricsService
    {
        List<MetricViewModel> GetMetrics(string group, Guid? negara, Guid? stream, Guid? entitas);
        List<MetricViewModel> GetMetrics(string group);
    }
}
