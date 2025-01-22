using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltExistingFootprintsPic
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid ExistingFootprintsId { get; set; }
        public Guid? PicFungsiId { get; set; }
        public Guid? PicLevelId { get; set; }

        public virtual TbltExistingFootprint ExistingFootprints { get; set; }
        public virtual TblmPicFungsi PicFungsi { get; set; }
        public virtual TblmPicLevel PicLevel { get; set; }
    }
}
