using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwDimEntitasPertamina
    {
        public Guid HshId { get; set; }
        public string HSh { get; set; }
        public Guid EntitasPertaminaId { get; set; }
        public string CompanyName { get; set; }
    }
}
