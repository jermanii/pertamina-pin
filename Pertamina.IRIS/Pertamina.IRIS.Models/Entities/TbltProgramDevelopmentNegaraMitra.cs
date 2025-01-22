using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltProgramDevelopmentNegaraMitra
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ProgramDevelopmentId { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual TblmNegaraMitra NegaraMitra { get; set; }
        public virtual TbltProgramDevelopment ProgramDevelopment { get; set; }
    }
}
