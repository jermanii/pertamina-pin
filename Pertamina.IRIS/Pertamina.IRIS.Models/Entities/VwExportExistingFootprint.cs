using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwExportExistingFootprint
    {
        public Guid ExistingFootPrintId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public string StreamBusinessName { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public string EntitasPertaminaName { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string PartnerCountry { get; set; }
        public Guid HubId { get; set; }
        public string HubName { get; set; }
        public string HubHead { get; set; }
        public string Partners { get; set; }
        public string Pic { get; set; }
        public int? LastYearDataMetrics { get; set; }
        public decimal? TotalAsset { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Ebitda { get; set; }
    }
}
