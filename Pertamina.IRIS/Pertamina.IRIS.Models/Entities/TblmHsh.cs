﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmHsh
    {
        public TblmHsh()
        {
            TblmEntitasPertaminas = new HashSet<TblmEntitasPertamina>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OrderSeq { get; set; }
        public int? RelationSequence { get; set; }

        public virtual ICollection<TblmEntitasPertamina> TblmEntitasPertaminas { get; set; }
    }
}
