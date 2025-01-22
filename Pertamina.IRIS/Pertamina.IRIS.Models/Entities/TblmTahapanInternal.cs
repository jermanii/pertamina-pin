﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmTahapanInternal
    {
        public TblmTahapanInternal()
        {
            TbltOpportunityTahapanInternals = new HashSet<TbltOpportunityTahapanInternal>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? OrderSeq { get; set; }

        public virtual ICollection<TbltOpportunityTahapanInternal> TbltOpportunityTahapanInternals { get; set; }
    }
}
