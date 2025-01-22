using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwExportInitiativeOld
    {
        public Guid InitiativeId { get; set; }
        public string TanggalDibuat { get; set; }
        public string CreateBy { get; set; }
        public string JudulInisiasi { get; set; }
        public string StreamBusinessId { get; set; }
        public string StreamBusiness { get; set; }
        public string EntitasPertaminaId { get; set; }
        public string Pertamina { get; set; }
        public string HshId { get; set; }
        public string HshName { get; set; }
        public string Pic { get; set; }
        public string NegaraMitraId { get; set; }
        public string NegaraMitra { get; set; }
        public string KawasanMitraId { get; set; }
        public string KawasanMitra { get; set; }
        public string LokasiProyek { get; set; }
        public string NilaiProyek { get; set; }
        public Guid? InitiativeTypeId { get; set; }
        public string InitiativeType { get; set; }
        public Guid? InitiativeStageId { get; set; }
        public string IntiativeStage { get; set; }
        public Guid? InititativeStatusId { get; set; }
        public string InitiativeStatus { get; set; }
        public string PerusahaanMitra { get; set; }
        public string InterestName { get; set; }
        public string TargetWaktuProyek { get; set; }
        public string Scope { get; set; }
        public string ValueForIndonesia { get; set; }
        public string Progress { get; set; }
        public string IsuKendala { get; set; }
        public string SupportPemerintah { get; set; }
        public string Referal { get; set; }
        public string PotensiEskalasi { get; set; }
        public string RelatedAgreement { get; set; }
        public string CatatanTambahan { get; set; }
        public string KodeAgreement { get; set; }
    }
}
