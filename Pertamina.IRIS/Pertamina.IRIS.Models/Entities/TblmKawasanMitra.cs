using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmKawasanMitra
    {
        public TblmKawasanMitra()
        {
            TblmNegaraMitras = new HashSet<TblmNegaraMitra>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ContinentId { get; set; }
        public string NamaKawasan { get; set; }
        public string Description { get; set; }
        public int? OrderSeq { get; set; }

        public virtual TblmContinent Continent { get; set; }
        public virtual ICollection<TblmNegaraMitra> TblmNegaraMitras { get; set; }
    }
}
