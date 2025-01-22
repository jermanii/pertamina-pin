using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.ViewModels
{
    public partial class InitiativePartnerViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public string PartnerName { get; set; }

        public virtual InitiativeViewModel Initiative { get; set; }
    }
}
