using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmProvinsi
    {
        public TblmProvinsi()
        {
            TbltOpportunityLokasiProyeks = new HashSet<TbltOpportunityLokasiProyek>();
        }

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
        public string ProvinceAcronyms { get; set; }

        public virtual TblmNegaraMitra NegaraMitra { get; set; }
        public virtual ICollection<TbltOpportunityLokasiProyek> TbltOpportunityLokasiProyeks { get; set; }
    }
}
