using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltExistingFootprintsOperatingMetric
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid ExistingFootprintsId { get; set; }
        public int? Year { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? TotalAsset { get; set; }
        public decimal? Ebitda { get; set; }

        public virtual TbltExistingFootprint ExistingFootprints { get; set; }
    }
}
