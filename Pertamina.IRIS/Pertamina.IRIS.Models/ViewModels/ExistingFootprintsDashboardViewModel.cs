using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ExistingFootprintsDashboardViewModel : ErrorBaseModel
    {
        public Guid? NegaraMitraId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public Guid? CategoryId { get; set; }
        public int? PageCountSort { get; set; }
        public int? RecordHighPriorityCountSort { get; set; }
        public int? AllRecordHighPriorityCountSort { get; set; }
        public string CountryAcronym { get; set; }
        public List<ExistingFootprintsDashboardHighPriorityViewModel> HighPriority { get; set; }
        public List<MetricViewModel> Metrics { get; set; }
        public List<ExistingFootprintsDashboardLegendViewModel> Legends { get; set; }
    }
}