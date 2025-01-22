using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwPicFungsi
    {
        public Guid? EntitasPertaminaId { get; set; }
        public string CompanyName { get; set; }
        public Guid? FungsiId { get; set; }
        public string FungsiName { get; set; }
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string PicName { get; set; }
        public string Phone { get; set; }
    }
}
