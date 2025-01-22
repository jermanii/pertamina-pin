using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmPicLevel
    {
        public TblmPicLevel()
        {
            TbltAgreementPicFungsis = new HashSet<TbltAgreementPicFungsi>();
            TbltExistingFootprintsPics = new HashSet<TbltExistingFootprintsPic>();
            TbltInitiativePicFungsis = new HashSet<TbltInitiativePicFungsi>();
            TbltOpportunityPicFungsis = new HashSet<TbltOpportunityPicFungsi>();
            TbltProgramDevelopmentPicFungsis = new HashSet<TbltProgramDevelopmentPicFungsi>();
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsLead { get; set; }

        public virtual ICollection<TbltAgreementPicFungsi> TbltAgreementPicFungsis { get; set; }
        public virtual ICollection<TbltExistingFootprintsPic> TbltExistingFootprintsPics { get; set; }
        public virtual ICollection<TbltInitiativePicFungsi> TbltInitiativePicFungsis { get; set; }
        public virtual ICollection<TbltOpportunityPicFungsi> TbltOpportunityPicFungsis { get; set; }
        public virtual ICollection<TbltProgramDevelopmentPicFungsi> TbltProgramDevelopmentPicFungsis { get; set; }
    }
}
