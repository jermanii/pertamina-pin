using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InitiativeMilestoneTimelineViewModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public string MilestoneName { get; set; }
        public int? Sequence { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public DateTime? TargetDefinitive { get; set; }
    }
}
