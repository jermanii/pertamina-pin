using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmJenisPerjanjian
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OrderSeq { get; set; }
        public Guid? KlasifikasiJenisPerjanjianId { get; set; }
        public string Shortname { get; set; }
        public bool? IsFirst { get; set; }
        public bool? IsSecret { get; set; }
        public bool? IsNext { get; set; }
        public bool? IsValue { get; set; }
        public bool? IsOther { get; set; }
        public bool? IsStrategic { get; set; }

        public virtual TblmKlasifikasiJenisPerjanjian KlasifikasiJenisPerjanjian { get; set; }
    }
}
