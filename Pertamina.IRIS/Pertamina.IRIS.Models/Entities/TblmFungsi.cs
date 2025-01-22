using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmFungsi
    {
        public TblmFungsi()
        {
            TblmPicFungsis = new HashSet<TblmPicFungsi>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public string FungsiName { get; set; }
        public string Description { get; set; }

        public virtual TblmEntitasPertamina EntitasPertamina { get; set; }
        public virtual ICollection<TblmPicFungsi> TblmPicFungsis { get; set; }
    }
}
