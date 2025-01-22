using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltProgramDevelopmentPicFungsi
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ProgramDevelopmentId { get; set; }
        public Guid? PicFungsiId { get; set; }
        public Guid? PicLevelId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual TblmPicFungsi PicFungsi { get; set; }
        public virtual TblmPicLevel PicLevel { get; set; }
        public virtual TbltProgramDevelopment ProgramDevelopment { get; set; }
    }
}
