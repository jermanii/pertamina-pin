using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltProgramDevelopmentMilestoneTimeline
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ProgramDevelopmentId { get; set; }
        public Guid? MilestoneId { get; set; }
        public int? Sequence { get; set; }
        public DateTime? TargetDate { get; set; }
        public DateTime? ActualDate { get; set; }

        public virtual TblmProgramDevelopmentMilestone Milestone { get; set; }
        public virtual TbltProgramDevelopment ProgramDevelopment { get; set; }
    }
}
