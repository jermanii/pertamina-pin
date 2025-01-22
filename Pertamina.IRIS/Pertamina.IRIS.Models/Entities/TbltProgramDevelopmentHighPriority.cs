using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltProgramDevelopmentHighPriority
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid ProgramDevelopmentId { get; set; }
        public int? Sequence { get; set; }

        public virtual TbltProgramDevelopment ProgramDevelopment { get; set; }
    }
}
