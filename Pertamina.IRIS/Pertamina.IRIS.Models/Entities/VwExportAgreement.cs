using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwExportAgreement
    {
        public Guid AgreementId { get; set; }
        public string Partners { get; set; }
        public string HshId { get; set; }
        public string HshName { get; set; }
        public string EntitasPertaminaId { get; set; }
        public string EntitasPertamina { get; set; }
        public Guid? JenisPerjanjianId { get; set; }
        public string JenisPerjanjian { get; set; }
        public string JudulPerjanjian { get; set; }
        public string KawsanMitraId { get; set; }
        public string KawasanMitraName { get; set; }
        public string NegaraMitraId { get; set; }
        public string NegaraMitra { get; set; }
        public string LokasiProyek { get; set; }
        public string Scope { get; set; }
        public string StreamBusinessId { get; set; }
        public string StreamBusiness { get; set; }
        public DateTime? TanggalTtd { get; set; }
        public DateTime? TanggalBerakhir { get; set; }
        public Guid? StatusBerlakuId { get; set; }
        public string StatusBerlaku { get; set; }
        public Guid? DiscussionStatusId { get; set; }
        public string DiscussionStatus { get; set; }
        public string Progress { get; set; }
        public string KlasifikasiKendala { get; set; }
        public string FaktorKendala { get; set; }
        public string DeskripsiKendala { get; set; }
        public string SupportPemerintah { get; set; }
        public string KeterlibatanKementrian { get; set; }
        public decimal? NilaiProyek { get; set; }
        public string PotensiEskalasi { get; set; }
        public string RelatedPerjanjian { get; set; }
        public string CatatanTambahan { get; set; }
        public string TanggalDibuat { get; set; }
        public string KodeAgreement { get; set; }
        public string KodeRelatedPerjanjian { get; set; }
        public string TindakLanjut { get; set; }
        public bool IsDraft { get; set; }
        public string TrafficLight { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Capex { get; set; }
        public decimal? Valuation { get; set; }
        public string PicLead { get; set; }
        public string PicMember { get; set; }
        public string Amandement { get; set; }
        public bool? IsGtG { get; set; }
        public bool? IsStrategic { get; set; }
        public bool? IsGreenBusiness { get; set; }
        public Guid? InitiativeId { get; set; }
        public Guid? OpportunityId { get; set; }
    }
}
