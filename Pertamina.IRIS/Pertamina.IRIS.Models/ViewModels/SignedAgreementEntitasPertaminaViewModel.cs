using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementEntitasPertaminaViewModel
    {
        public Guid Id { get; set; }
        public Guid? AgreementId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public bool? DeletedStatus { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public int? OrderSeq { get; set; }
        public bool? ActiveStatus { get; set; }
        public int? Level { get; set; }
    }
}
