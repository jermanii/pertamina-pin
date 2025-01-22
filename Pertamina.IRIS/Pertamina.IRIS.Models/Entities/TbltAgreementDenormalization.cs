using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltAgreementDenormalization
    {
        public Guid? Id { get; set; }
        public string JudulPerjanjian { get; set; }
        public string KodeAgreement { get; set; }
        public string JenisPerjanjian { get; set; }
        public string DiscussionStatus { get; set; }
        public string KlasifikasiKendala { get; set; }
        public string DeskripsiKendala { get; set; }
        public DateTime? TanggalTtd { get; set; }
    }
}
