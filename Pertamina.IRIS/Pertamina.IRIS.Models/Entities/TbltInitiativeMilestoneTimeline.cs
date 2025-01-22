using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltInitiativeMilestoneTimeline
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public Guid? MilestoneStepId { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public bool? IsDone { get; set; }
        public Guid? MilestoneStatusId { get; set; }

        public virtual TbltInitiative Initiative { get; set; }
        public virtual TblmInitiativeMilestoneStatus MilestoneStatus { get; set; }
        public virtual TblmInitiativeMilestoneStep MilestoneStep { get; set; }
    }
}
