using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class AgreementHubHeadViewModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid AgreementId { get; set; }
        public Guid HubHeadId { get; set; }

        public string HubHeadName { get; set; }
        //public Guid? HubIdd { get; set; }
    }
}
