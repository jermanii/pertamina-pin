using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltInitiativeContactPerson
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual TbltInitiative Initiative { get; set; }
    }
}
