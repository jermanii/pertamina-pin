using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmHubHead
    {
        public TblmHubHead()
        {
            TbltAgreementHubHeads = new HashSet<TbltAgreementHubHead>();
            TbltExistingFootprintsHubHeads = new HashSet<TbltExistingFootprintsHubHead>();
            TbltInitiativeHubHeads = new HashSet<TbltInitiativeHubHead>();
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid HubId { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string ContactNumber { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblmHub Hub { get; set; }
        public virtual ICollection<TbltAgreementHubHead> TbltAgreementHubHeads { get; set; }
        public virtual ICollection<TbltExistingFootprintsHubHead> TbltExistingFootprintsHubHeads { get; set; }
        public virtual ICollection<TbltInitiativeHubHead> TbltInitiativeHubHeads { get; set; }
    }
}
