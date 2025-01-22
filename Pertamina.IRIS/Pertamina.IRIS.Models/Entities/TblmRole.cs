using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmRole
    {
        public TblmRole()
        {
            TblmRoleFeatures = new HashSet<TblmRoleFeature>();
            TblmRoleMenuHomes = new HashSet<TblmRoleMenuHome>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public bool? IsAdd { get; set; }
        public bool? IsEdit { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsAddExternalContact { get; set; }
        public bool? IsEditExternalContact { get; set; }
        public bool? IsDuplicateExternalContact { get; set; }
        public bool? IsDeleteExternalContact { get; set; }

        public virtual ICollection<TblmRoleFeature> TblmRoleFeatures { get; set; }
        public virtual ICollection<TblmRoleMenuHome> TblmRoleMenuHomes { get; set; }
    }
}
