using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltProgramDevelopmentEntitasPertamina
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ProgramDevelopmentId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual TblmEntitasPertamina EntitasPertamina { get; set; }
        public virtual TbltProgramDevelopment ProgramDevelopment { get; set; }
    }
}
