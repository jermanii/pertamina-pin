using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmHub
    {
        public TblmHub()
        {
            TblmHubHeads = new HashSet<TblmHubHead>();
            TblmNegaraMitras = new HashSet<TblmNegaraMitra>();
            TbltExistingFootprints = new HashSet<TbltExistingFootprint>();
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ColorHexa { get; set; }
        public string ColorName { get; set; }

        public virtual ICollection<TblmHubHead> TblmHubHeads { get; set; }
        public virtual ICollection<TblmNegaraMitra> TblmNegaraMitras { get; set; }
        public virtual ICollection<TbltExistingFootprint> TbltExistingFootprints { get; set; }
    }
}
