using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmPicFungsi
    {
        public TblmPicFungsi()
        {
            TbltAgreementPicFungsis = new HashSet<TbltAgreementPicFungsi>();
            TbltExistingFootprintsPics = new HashSet<TbltExistingFootprintsPic>();
            TbltInitiativePicFungsis = new HashSet<TbltInitiativePicFungsi>();
            TbltOpportunityPicFungsis = new HashSet<TbltOpportunityPicFungsi>();
            TbltProgramDevelopmentPicFungsis = new HashSet<TbltProgramDevelopmentPicFungsi>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? FungsiId { get; set; }
        public string PicName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? ActiveStatus { get; set; }

        public virtual TblmFungsi Fungsi { get; set; }
        public virtual ICollection<TbltAgreementPicFungsi> TbltAgreementPicFungsis { get; set; }
        public virtual ICollection<TbltExistingFootprintsPic> TbltExistingFootprintsPics { get; set; }
        public virtual ICollection<TbltInitiativePicFungsi> TbltInitiativePicFungsis { get; set; }
        public virtual ICollection<TbltOpportunityPicFungsi> TbltOpportunityPicFungsis { get; set; }
        public virtual ICollection<TbltProgramDevelopmentPicFungsi> TbltProgramDevelopmentPicFungsis { get; set; }
    }
}
