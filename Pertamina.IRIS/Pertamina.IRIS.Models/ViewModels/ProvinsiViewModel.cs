using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ProvinsiViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string NamaProvinsi { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? OrderSeq { get; set; }
    }
}
