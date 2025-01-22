using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class OpportunityEntitasPertaminaViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? OpportunityId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public bool? DeletedStatus { get; set; }

        public string CompanyName { get; set; }
        public Guid? HshId { get; set; }
        public string HshName { get; set; }
        public EntitasPertaminaViewModel EntitasPertamina { get; set; }
    }
}
