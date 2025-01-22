using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class AgreementAddendumViewModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? AgreementId { get; set; }
        public DateTime AddendumDate { get; set; }
        public int? Sequence { get; set; }

        public string RomanSequence{ get; set; }
        public string AmandementBySequence { get; set; }
        public string AddendumDateToString { get; set; }

    }
}
