using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltExistingFootprintsDescMap
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ExistingFootprintsId { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string Description { get; set; }
        public int? OrderSeq { get; set; }

        public virtual TbltExistingFootprint ExistingFootprints { get; set; }
    }
}
