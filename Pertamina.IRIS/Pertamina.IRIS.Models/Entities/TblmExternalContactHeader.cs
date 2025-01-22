using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmExternalContactHeader
    {
        public TblmExternalContactHeader()
        {
            TblmExternalContactDetails = new HashSet<TblmExternalContactDetail>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string NamaCompany { get; set; }
        public string KoneksiPertamina { get; set; }
        public string AlamatCompany { get; set; }
        public string EmailCompany { get; set; }
        public string NoTelpCompany { get; set; }
        public string NoFax { get; set; }

        public virtual ICollection<TblmExternalContactDetail> TblmExternalContactDetails { get; set; }
    }
}
