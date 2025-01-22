using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InitiativeOpportunityViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public Guid? OpportunityId { get; set; }
        public bool? DeletedStatus { get; set; }

        public  InitiativeViewModel Initiative { get; set; }
        public  OpportunityViewModel Opportunity { get; set; }
    }
}
