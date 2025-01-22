using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmStoredProcedure
    {
        public TblmStoredProcedure()
        {
            TblmStoredProcedureParams = new HashSet<TblmStoredProcedureParam>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Group { get; set; }
        public string SpName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int? OrderSeq { get; set; }

        public virtual ICollection<TblmStoredProcedureParam> TblmStoredProcedureParams { get; set; }
    }
}
