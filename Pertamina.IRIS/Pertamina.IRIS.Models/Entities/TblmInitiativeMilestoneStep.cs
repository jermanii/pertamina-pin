using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmInitiativeMilestoneStep
    {
        public TblmInitiativeMilestoneStep()
        {
            TbltInitiativeMilestoneTimelines = new HashSet<TbltInitiativeMilestoneTimeline>();
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StepSequence { get; set; }
        public int? RelationSequence { get; set; }
        public string ColorHexa { get; set; }
        public string ColorName { get; set; }

        public virtual ICollection<TbltInitiativeMilestoneTimeline> TbltInitiativeMilestoneTimelines { get; set; }
    }
}
