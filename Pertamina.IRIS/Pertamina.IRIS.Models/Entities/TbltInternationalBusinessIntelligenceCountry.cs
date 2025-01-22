using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltInternationalBusinessIntelligenceCountry
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InternationalBusinessIntelligenceId { get; set; }
        public Guid? NegaraMitraId { get; set; }

        public virtual TbltInternationalBusinessIntelligence InternationalBusinessIntelligence { get; set; }
        public virtual TblmNegaraMitra NegaraMitra { get; set; }
    }
}
