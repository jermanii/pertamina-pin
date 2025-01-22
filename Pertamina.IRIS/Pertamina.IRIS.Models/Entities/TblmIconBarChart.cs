using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmIconBarChart
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Src { get; set; }
        public string Year1 { get; set; }
        public string Year2 { get; set; }
        public string Year3 { get; set; }
    }
}
