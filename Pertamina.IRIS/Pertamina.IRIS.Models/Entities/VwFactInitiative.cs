using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class VwFactInitiative
    {
        public Guid Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string JudulInisiasi { get; set; }
        public string HshName { get; set; }
        public string CompanyName { get; set; }
        public string NegaraMitra { get; set; }
        public string Kawasan { get; set; }
        public string Continent { get; set; }
        public string StreamBusinessName { get; set; }
        public string InterestName { get; set; }
        public string InitiativeType { get; set; }
        public string InitativeStatus { get; set; }
    }
}
