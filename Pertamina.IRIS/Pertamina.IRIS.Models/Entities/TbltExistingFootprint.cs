using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltExistingFootprint
    {
        public TbltExistingFootprint()
        {
            TbltExistingFootprintsDescMaps = new HashSet<TbltExistingFootprintsDescMap>();
            TbltExistingFootprintsHighPriorities = new HashSet<TbltExistingFootprintsHighPriority>();
            TbltExistingFootprintsHubHeads = new HashSet<TbltExistingFootprintsHubHead>();
            TbltExistingFootprintsOperatingMetrics = new HashSet<TbltExistingFootprintsOperatingMetric>();
            TbltExistingFootprintsPartners = new HashSet<TbltExistingFootprintsPartner>();
            TbltExistingFootprintsPics = new HashSet<TbltExistingFootprintsPic>();
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid HubId { get; set; }
        public Guid? NegaraMitraId { get; set; }

        public virtual TblmEntitasPertamina EntitasPertamina { get; set; }
        public virtual TblmHub Hub { get; set; }
        public virtual TblmNegaraMitra NegaraMitra { get; set; }
        public virtual TblmStreamBusiness StreamBusiness { get; set; }
        public virtual ICollection<TbltExistingFootprintsDescMap> TbltExistingFootprintsDescMaps { get; set; }
        public virtual ICollection<TbltExistingFootprintsHighPriority> TbltExistingFootprintsHighPriorities { get; set; }
        public virtual ICollection<TbltExistingFootprintsHubHead> TbltExistingFootprintsHubHeads { get; set; }
        public virtual ICollection<TbltExistingFootprintsOperatingMetric> TbltExistingFootprintsOperatingMetrics { get; set; }
        public virtual ICollection<TbltExistingFootprintsPartner> TbltExistingFootprintsPartners { get; set; }
        public virtual ICollection<TbltExistingFootprintsPic> TbltExistingFootprintsPics { get; set; }
    }
}
