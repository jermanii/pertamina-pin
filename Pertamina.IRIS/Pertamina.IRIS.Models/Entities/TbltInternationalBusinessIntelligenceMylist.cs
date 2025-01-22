using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltInternationalBusinessIntelligenceMylist
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InternationalBusinessIntelligenceId { get; set; }
        public string Email { get; set; }
        public int? Sequence { get; set; }

        public virtual TbltInternationalBusinessIntelligence InternationalBusinessIntelligence { get; set; }
    }
}
