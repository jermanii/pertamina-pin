using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltExternalContact
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string NamaCompany { get; set; }
        public string KoneksiCompany { get; set; }
        public string AlamatCompany { get; set; }
        public string EmailCompany { get; set; }
        public string NoTelpCompany { get; set; }
        public string NamaPerson { get; set; }
        public string JabatanPerson { get; set; }
        public string EmailPerson { get; set; }
        public string NoTelpPerson { get; set; }
        public string Remark { get; set; }
    }
}
