using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class MeetingDashboardViewModel : ErrorBaseModel
    { 
        public List<PotentialInitiativeHighPriorityViewModel> HighPriority { get; set; }
        public List<MetricViewModel> Metrics { get; set; }


    }
}

