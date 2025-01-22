using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltAgreementNegaraMitra
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? AgreementId { get; set; }
        public Guid? NegaraMitraId { get; set; }

        public virtual TbltAgreement Agreement { get; set; }
        public virtual TblmNegaraMitra NegaraMitra { get; set; }
    }
}
