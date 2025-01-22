using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwDimClassAgreement
    {
        public Guid KlasifikasiJenisPerjanjianId { get; set; }
        public string KlasifikasiPerjanjian { get; set; }
        public string GroupAgreement { get; set; }
    }
}
