using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementPartnerViewModel
    {
        public Guid Id { get; set; }
        public Guid? AgreementId { get; set; }
        public string PartnerName { get; set; }
    }
}
