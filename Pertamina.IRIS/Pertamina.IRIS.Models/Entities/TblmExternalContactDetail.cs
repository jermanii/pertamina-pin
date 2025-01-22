using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmExternalContactDetail
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ExternalContactHeaderId { get; set; }
        public string NamaPersonal { get; set; }
        public string JabatanPersonal { get; set; }

        public virtual TblmExternalContactHeader ExternalContactHeader { get; set; }
    }
}
