using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmMenuHome
    {
        public TblmMenuHome()
        {
            TblmRoleMenuHomes = new HashSet<TblmRoleMenuHome>();
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Code { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
        public int? Sequence { get; set; }

        public virtual ICollection<TblmRoleMenuHome> TblmRoleMenuHomes { get; set; }
    }
}
