using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwFactOpportunity
    {
        public Guid OpportunityId { get; set; }
        public string NamaProyek { get; set; }
        public string ProjectProfile { get; set; }
        public string Deliverables { get; set; }
        public string NilaiProyek { get; set; }
        public string HSh { get; set; }
        public string CompanyName { get; set; }
        public string StreamBusinessName { get; set; }
        public string Continent { get; set; }
        public string Kawasan { get; set; }
        public string NegaraMitra { get; set; }
        public string TargetMitraName { get; set; }
    }
}
