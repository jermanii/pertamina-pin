using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmEntitasPertamina
    {
        public TblmEntitasPertamina()
        {
            TblmFungsis = new HashSet<TblmFungsi>();
            TbltAgreementEntitasPertaminas = new HashSet<TbltAgreementEntitasPertamina>();
            TbltExistingFootprints = new HashSet<TbltExistingFootprint>();
            TbltInitiativeEntitasPertaminas = new HashSet<TbltInitiativeEntitasPertamina>();
            TbltInternationalBusinessIntelligences = new HashSet<TbltInternationalBusinessIntelligence>();
            TbltOpportunityEntitasPertaminas = new HashSet<TbltOpportunityEntitasPertamina>();
            TbltProgramDevelopmentEntitasPertaminas = new HashSet<TbltProgramDevelopmentEntitasPertamina>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? HshId { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public int? OrderSeq { get; set; }
        public bool? ActiveStatus { get; set; }
        public int? Level { get; set; }

        public virtual TblmHsh Hsh { get; set; }
        public virtual ICollection<TblmFungsi> TblmFungsis { get; set; }
        public virtual ICollection<TbltAgreementEntitasPertamina> TbltAgreementEntitasPertaminas { get; set; }
        public virtual ICollection<TbltExistingFootprint> TbltExistingFootprints { get; set; }
        public virtual ICollection<TbltInitiativeEntitasPertamina> TbltInitiativeEntitasPertaminas { get; set; }
        public virtual ICollection<TbltInternationalBusinessIntelligence> TbltInternationalBusinessIntelligences { get; set; }
        public virtual ICollection<TbltOpportunityEntitasPertamina> TbltOpportunityEntitasPertaminas { get; set; }
        public virtual ICollection<TbltProgramDevelopmentEntitasPertamina> TbltProgramDevelopmentEntitasPertaminas { get; set; }
    }
}
