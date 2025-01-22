using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmRoleFeature
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string FeatureId { get; set; }
        public Guid? RoleId { get; set; }
        public bool? IsCreate { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsDelete { get; set; }

        public virtual TblmFeature Feature { get; set; }
        public virtual TblmRole Role { get; set; }
    }
}
