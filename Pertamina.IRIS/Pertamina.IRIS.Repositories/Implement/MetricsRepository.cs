using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pertamina.IRIS.Models.Entities;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Repositories.Implement
{
    public class MetricsRepository : IMetricsRepository
    {
        protected readonly DB_PINMContext _context;
        protected readonly IMapper _mapper;
        public MetricsRepository(DB_PINMContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public MetricViewModel GetMetricBySp(StoredProcedureViewModel sp)
        {
            MetricViewModel result = new MetricViewModel();

            result = _mapper.Map<MetricViewModel>(_context.Set<TbltMetric>().FromSqlInterpolated($"EXEC {sp.SpName}").AsEnumerable().FirstOrDefault());
            TblmIconArrowUpdownChart icon = _context.TblmIconArrowUpdownCharts.Where(x => x.DeletedDate == null && x.Id == result.IconId).FirstOrDefault();

            result.Src = icon.Src;
            result.ColorCode = icon.ColorCode;

            return result;
        }

        public MetricViewModel GetMetricBySpWithParam(StoredProcedureViewModel sp, Guid? negara, Guid? stream, Guid? entitas)
        {
            MetricViewModel result = new MetricViewModel();

            var parameters = new Dictionary<string, string>
            {
                { "Negara_Mitra_Id", negara != null ? negara.ToString() : "" },
                { "Stream_Business_Id", stream != null ? stream.ToString() : "" },
                { "Entitas_Pertamina_Id", entitas != null ? entitas.ToString() : "" }
            };

            // Bangun query dinamis
            var sqlQuery = new StringBuilder($"EXEC {sp.SpName}");
            foreach (var param in parameters)
            {
                sqlQuery.Append($" @{param.Key} = '{(param.Value == null ? "NULL" : param.Value)}', ");
            }

            var query = sqlQuery.ToString().TrimEnd(',', ' ');

            result = _mapper.Map<MetricViewModel>(_context.Set<TbltMetric>().FromSqlRaw(query).AsEnumerable().FirstOrDefault());
            if(result != null)
            {
                TblmIconArrowUpdownChart icon = _context.TblmIconArrowUpdownCharts.Where(x => x.DeletedDate == null && x.Id == result.IconId).FirstOrDefault();

                result.Src = icon.Src;
                result.ColorCode = icon.ColorCode;
            }

            return result;
        }

        public List<StoredProcedureViewModel> GetStoreProcedureListByGroup(string group)
        {
            List<StoredProcedureViewModel> result = new List<StoredProcedureViewModel>();

            result = _mapper.Map<List<StoredProcedureViewModel>>(_context.TblmStoredProcedures.Where(x => x.DeletedDate == null && x.Group == group).OrderBy(x => x.OrderSeq));

            return result;
        }
    }
}
