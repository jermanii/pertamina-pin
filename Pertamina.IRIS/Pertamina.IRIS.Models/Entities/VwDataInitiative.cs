using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwDataInitiative
    {
        public Guid InitiativeId { get; set; }
        public string TanggalDibuat { get; set; }
        public string CreateBy { get; set; }
        public string JudulInisiasi { get; set; }
        public string StreamBusiness { get; set; }
        public string Pertamina { get; set; }
        public string Pic { get; set; }
        public string KawasanMitra { get; set; }
        public string LokasiProyek { get; set; }
        public string NilaiProyek { get; set; }
        public string InitiativeType { get; set; }
        public string IntiativeStage { get; set; }
        public string InitiativeStatus { get; set; }
        public string PerusahaanMitra { get; set; }
        public string InterestName { get; set; }
        public string TargetWaktuProyek { get; set; }
        public string Scope { get; set; }
        public string ValueForIndonesia { get; set; }
        public string Progress { get; set; }
        public string IsuKendala { get; set; }
        public string SupportPemerintah { get; set; }
    }
}
