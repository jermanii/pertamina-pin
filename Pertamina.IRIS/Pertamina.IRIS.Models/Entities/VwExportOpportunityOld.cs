using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwExportOpportunityOld
    {
        public Guid OpportunityId { get; set; }
        public string DibuatOleh { get; set; }
        public string TanggalBuat { get; set; }
        public string NamaProyek { get; set; }
        public string StreamBusinessId { get; set; }
        public string StreamBusiness { get; set; }
        public string EntitasPertaminaId { get; set; }
        public string EntitasPertamina { get; set; }
        public string HshId { get; set; }
        public string HshName { get; set; }
        public string PicName { get; set; }
        public string NegaraMitraId { get; set; }
        public string LokasiNegaraProyek { get; set; }
        public string KawasanMitraId { get; set; }
        public string KawasanMitra { get; set; }
        public string LokasiProyek { get; set; }
        public string KesiapanProyek { get; set; }
        public string ProjectProfile { get; set; }
        public string NilaiProyek { get; set; }
        public string Deliverables { get; set; }
        public string Timeline { get; set; }
        public string Progress { get; set; }
        public string TargetMitra { get; set; }
        public string ExistingParties { get; set; }
        public string CatatanTambahan { get; set; }
    }
}
