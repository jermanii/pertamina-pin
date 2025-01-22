using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltOpportunityTargetMitra
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? OpportunityId { get; set; }
        public Guid? TargetMitraId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual TbltOpportunity Opportunity { get; set; }
        public virtual TblmTargetMitra TargetMitra { get; set; }
    }
}
