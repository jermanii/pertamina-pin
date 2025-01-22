using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementStreamBusinessViewModel
    {
        public Guid Id { get; set; }
        public Guid? AgreementId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public bool? DeletedStatus { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? OrderSeq { get; set; }
        public string ColorHexa { get; set; }
        public string ColorName { get; set; }
        public bool? IsGreenBusiness { get; set; }
    }
}
