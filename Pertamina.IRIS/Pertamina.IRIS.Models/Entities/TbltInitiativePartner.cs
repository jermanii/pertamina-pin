using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltInitiativePartner
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public string PartnerName { get; set; }
        public string Position { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public virtual TbltInitiative Initiative { get; set; }
    }
}
