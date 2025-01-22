using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwFactAgreement
    {
        public Guid Id { get; set; }
        public string CreateDate { get; set; }
        public DateTime? TanggalTtd { get; set; }
        public DateTime? TanggalBerakhir { get; set; }
        public string JudulPerjanjian { get; set; }
        public string JenisPerjanjian { get; set; }
        public string EntitasPertamina { get; set; }
        public string HshName { get; set; }
        public string NegaraMitra { get; set; }
        public string Kawasan { get; set; }
        public string Continent { get; set; }
        public string StatusBerlaku { get; set; }
        public string StatusDiskusi { get; set; }
        public string FaktorKendala { get; set; }
        public string KlasifikasiKendala { get; set; }
    }
}
