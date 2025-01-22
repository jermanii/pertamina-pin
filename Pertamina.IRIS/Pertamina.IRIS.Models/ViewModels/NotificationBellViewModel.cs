using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class NotificationBellViewModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? TransactionId { get; set; }
        public string Transaction { get; set; }
        public Guid? FormTransactionId { get; set; }
        public string FormTransaction { get; set; }
        public Guid? StatusTypeId { get; set; }
        public string Status { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string SentTo { get; set; }
        public bool? IsRead { get; set; }
        public string NotifTime { get; set; }
        public string RedirectUrl {  get; set; }
        public string TransactionTitle {  get; set; }

    }
}
