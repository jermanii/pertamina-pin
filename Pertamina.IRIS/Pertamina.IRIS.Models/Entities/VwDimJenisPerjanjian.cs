using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwDimJenisPerjanjian
    {
        public Guid JenisPerjanjianId { get; set; }
        public string JenisPerjanjian { get; set; }
        public Guid KlasifikasiPerjanjianId { get; set; }
        public string KlasifikasiPerjanjian { get; set; }
    }
}
