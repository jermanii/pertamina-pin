﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmKlasifikasiJenisPerjanjian
    {
        public TblmKlasifikasiJenisPerjanjian()
        {
            TblmJenisPerjanjians = new HashSet<TblmJenisPerjanjian>();
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

        public virtual ICollection<TblmJenisPerjanjian> TblmJenisPerjanjians { get; set; }
    }
}
