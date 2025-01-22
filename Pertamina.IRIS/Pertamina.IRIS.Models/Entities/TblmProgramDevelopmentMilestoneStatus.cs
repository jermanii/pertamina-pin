using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmProgramDevelopmentMilestoneStatus
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public string ColorHexa { get; set; }
        public string ColorName { get; set; }
        public int? OrderSeq { get; set; }
    }
}
