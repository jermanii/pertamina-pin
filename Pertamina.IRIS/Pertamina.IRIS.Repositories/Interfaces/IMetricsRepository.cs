using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IMetricsRepository
    {
        MetricViewModel GetMetricBySp(StoredProcedureViewModel sp);
        MetricViewModel GetMetricBySpWithParam(StoredProcedureViewModel sp, Guid? negara, Guid? stream, Guid? entitas);
        List<StoredProcedureViewModel> GetStoreProcedureListByGroup(string group);
    }
}
