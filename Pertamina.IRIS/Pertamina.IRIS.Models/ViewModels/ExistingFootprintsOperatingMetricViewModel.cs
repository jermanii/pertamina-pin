using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ExistingFootprintsOperatingMetricViewModel
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


        public string RevenueToString { get; set; }
        public string TotalAssetToString { get; set; }
        public string EbitdaToString { get; set; }
    }
}
