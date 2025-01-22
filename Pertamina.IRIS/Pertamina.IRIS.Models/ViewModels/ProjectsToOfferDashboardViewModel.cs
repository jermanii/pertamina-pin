using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ProjectsToOfferDashboardViewModel
    {
        public Guid? ProvinsiIndonesiaId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public List<OpportunityViewModel> HighPrioritys { get; set; }
        public OpportunityViewModel Opportunity { get; set; }
        public List<MetricViewModel> Metrics { get; set; }
    }
}
