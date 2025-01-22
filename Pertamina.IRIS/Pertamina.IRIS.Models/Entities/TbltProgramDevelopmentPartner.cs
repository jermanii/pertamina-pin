using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltProgramDevelopmentPartner
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ProgramDevelopmentId { get; set; }
        public string PartnerName { get; set; }

        public virtual TbltProgramDevelopment ProgramDevelopment { get; set; }
    }
}
