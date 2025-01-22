using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltExistingFootprintsPartner
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid ExistingFootPrintsId { get; set; }
        public string Partners { get; set; }
        public string Location { get; set; }

        public virtual TbltExistingFootprint ExistingFootPrints { get; set; }
    }
}
