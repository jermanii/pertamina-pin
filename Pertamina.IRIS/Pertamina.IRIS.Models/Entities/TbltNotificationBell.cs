using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltNotificationBell
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? TransactionId { get; set; }
        public Guid? FormTransactionId { get; set; }
        public Guid? StatusTypeId { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string SentTo { get; set; }
        public bool? IsRead { get; set; }
    }
}
