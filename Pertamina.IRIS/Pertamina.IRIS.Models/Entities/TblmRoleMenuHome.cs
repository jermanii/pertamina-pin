using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmRoleMenuHome
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid MenuHomeId { get; set; }
        public Guid RoleId { get; set; }
        public bool? IsCreate { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsUpdate { get; set; }
        public bool? IsDelete { get; set; }

        public virtual TblmMenuHome MenuHome { get; set; }
        public virtual TblmRole Role { get; set; }
    }
}
