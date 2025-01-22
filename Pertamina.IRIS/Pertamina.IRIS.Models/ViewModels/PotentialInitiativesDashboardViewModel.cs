using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class PotentialInitiativesDashboardViewModel
    {
        public Guid? StreamBusinessId { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public List<PotentialInitiativeHighPriorityViewModel> HighPriorityInitiatives { get; set; }
        public List<MetricViewModel> Metrics { get; set; }
    }
}
