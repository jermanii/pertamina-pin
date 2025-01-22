using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Services.Implement
{
    public class MetricsService : IMetricsService
    {
        private readonly IMetricsRepository _repository;
        public MetricsService(IMetricsRepository repository)
        {
            _repository = repository;
        }

        public List<MetricViewModel> GetMetrics(string group)
        {
            List<MetricViewModel> result = new List<MetricViewModel>();

            List<StoredProcedureViewModel> storedProcedure = _repository.GetStoreProcedureListByGroup(group);

            foreach (var sp in storedProcedure)
            {
                MetricViewModel metric = new MetricViewModel();
                metric = _repository.GetMetricBySp(sp);
                metric.Title = sp.Title;
                metric.OrderSeq = sp.OrderSeq;

                result.Add(metric);
            }

            return result;
        }

        public List<MetricViewModel> GetMetrics(string group, Guid? negara, Guid? stream, Guid? entitas)
        {
            List<MetricViewModel> result = new List<MetricViewModel>();

            List<StoredProcedureViewModel> storedProcedure = _repository.GetStoreProcedureListByGroup(group);

            foreach (var sp in storedProcedure)
            {
                MetricViewModel metric = new MetricViewModel();

                if ((negara != null) || (stream != null) || (entitas != null))
                    metric = _repository.GetMetricBySpWithParam(sp, negara, stream, entitas);
                else
                    metric = _repository.GetMetricBySp(sp);

                if(metric == null)
                {
                    metric = new MetricViewModel();
                }

                metric.Title = sp.Title;
                metric.OrderSeq = sp.OrderSeq;

                result.Add(metric);
            }

            return result;
        }
    }
}
