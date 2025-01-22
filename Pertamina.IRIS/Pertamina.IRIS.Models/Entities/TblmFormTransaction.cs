using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmFormTransaction
    {
        public TblmFormTransaction()
        {
            TblmStatusTypes = new HashSet<TblmStatusType>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string TransactionForm { get; set; }
        public int RelationSequence { get; set; }
        public int? OrderSeq { get; set; }
        public string FeatureId { get; set; }

        public virtual TblmFeature Feature { get; set; }
        public virtual ICollection<TblmStatusType> TblmStatusTypes { get; set; }
    }
}
