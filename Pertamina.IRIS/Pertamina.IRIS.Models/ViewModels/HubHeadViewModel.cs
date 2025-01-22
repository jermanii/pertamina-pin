using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class HubHeadViewModel
    {
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
    }
}
