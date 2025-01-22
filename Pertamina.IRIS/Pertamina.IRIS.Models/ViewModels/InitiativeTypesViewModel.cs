using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InitiativeTypesViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public int? OrderSeq { get; set; }

        ICollection<InitiativeViewModel> Initiatives { get; set; }

    }
}
