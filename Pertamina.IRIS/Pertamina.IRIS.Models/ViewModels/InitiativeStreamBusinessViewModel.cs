using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InitiativeStreamBusinessViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public bool? DeletedStatus { get; set; }
        [NotMapped]
        public string QueryStreamBusinessName { get; set; }
        public virtual InitiativeViewModel Initiative { get; set; }
        public virtual StreamBusinessViewModel StreamBusiness { get; set; }
    }
}
