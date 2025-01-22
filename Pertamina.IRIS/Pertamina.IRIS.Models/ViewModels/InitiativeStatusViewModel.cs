using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InitiativeStatusViewModel
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
        public int? RelationSequence { get; set; }
        public string ColorHexa { get; set; }
        public string ColorName { get; set; }

        public int? Count { get; set; }
        IQueryable<InitiativeViewModel> Initiatives { get; set; }

    }
}
