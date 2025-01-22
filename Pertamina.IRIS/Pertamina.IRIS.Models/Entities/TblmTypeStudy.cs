using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmTypeStudy
    {
        public TblmTypeStudy()
        {
            TbltInternationalBusinessIntelligences = new HashSet<TbltInternationalBusinessIntelligence>();
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public int? Sequence { get; set; }

        public virtual ICollection<TbltInternationalBusinessIntelligence> TbltInternationalBusinessIntelligences { get; set; }
    }
}
