using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwDimNegaraMitra
    {
        public Guid ContinentId { get; set; }
        public string Continent { get; set; }
        public Guid? KawasanMitraId { get; set; }
        public string Kawasan { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string NegaraMitra { get; set; }
    }
}
