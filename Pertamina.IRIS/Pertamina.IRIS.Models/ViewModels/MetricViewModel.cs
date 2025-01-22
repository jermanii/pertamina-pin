using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class MetricViewModel
    {
        public int? Year { get; set; }
        public decimal? Point { get; set; }
        public int? Percent { get; set; }
        public Guid? IconId { get; set; }
        [NotMapped]
        public string Title { get; set; }
        [NotMapped]
        public string Src { get; set; }
        [NotMapped]
        public string ColorCode { get; set; }
        public int? OrderSeq { get; set; }
    }
}
