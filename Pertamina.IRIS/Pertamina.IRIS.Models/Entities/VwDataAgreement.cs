using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwDataAgreement
    {
        public Guid AgreementId { get; set; }
        public string Parties { get; set; }
        public string Pertamina { get; set; }
        public string FungsiTeamSebagai { get; set; }
        public string JenisPerjanjian { get; set; }
        public string JudulPerjanjian { get; set; }
        public string NegaraMitra { get; set; }
        public string LokasiProyek { get; set; }
        public string Scope { get; set; }
        public string StreamBusiness { get; set; }
        public DateTime? TanggalTtd { get; set; }
        public DateTime? TanggalBerakhir { get; set; }
        public string DiscussionStatus { get; set; }
        public string Progress { get; set; }
        public string KlasifikasiKendala { get; set; }
        public string FaktorKendala { get; set; }
        public string DeskripsiKendala { get; set; }
        public string SupportPemerintah { get; set; }
        public string KeterlibatanKementrian { get; set; }
        public string NilaiProyek { get; set; }
        public string PotensiEskalasi { get; set; }
        public string CatatanTambahan { get; set; }
        public string KodeAgreement { get; set; }
    }
}
