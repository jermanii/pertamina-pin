using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class FormTransactionViewModel
    {
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
    }
}
