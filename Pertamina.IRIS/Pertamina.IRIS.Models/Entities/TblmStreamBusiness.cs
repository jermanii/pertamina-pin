using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmStreamBusiness
    {
        public TblmStreamBusiness()
        {
            TbltAgreementStreamBusinesses = new HashSet<TbltAgreementStreamBusiness>();
            TbltExistingFootprints = new HashSet<TbltExistingFootprint>();
            TbltInitiativeStreamBusinesses = new HashSet<TbltInitiativeStreamBusiness>();
            TbltInternationalBusinessIntelligences = new HashSet<TbltInternationalBusinessIntelligence>();
            TbltOpportunityStreamBusinesses = new HashSet<TbltOpportunityStreamBusiness>();
            TbltProgramDevelopmentStreamBusinesses = new HashSet<TbltProgramDevelopmentStreamBusiness>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OrderSeq { get; set; }
        public string ColorHexa { get; set; }
        public string ColorName { get; set; }
        public bool? IsGreenBusiness { get; set; }

        public virtual ICollection<TbltAgreementStreamBusiness> TbltAgreementStreamBusinesses { get; set; }
        public virtual ICollection<TbltExistingFootprint> TbltExistingFootprints { get; set; }
        public virtual ICollection<TbltInitiativeStreamBusiness> TbltInitiativeStreamBusinesses { get; set; }
        public virtual ICollection<TbltInternationalBusinessIntelligence> TbltInternationalBusinessIntelligences { get; set; }
        public virtual ICollection<TbltOpportunityStreamBusiness> TbltOpportunityStreamBusinesses { get; set; }
        public virtual ICollection<TbltProgramDevelopmentStreamBusiness> TbltProgramDevelopmentStreamBusinesses { get; set; }
    }
}
