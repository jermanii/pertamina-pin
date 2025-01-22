using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementLokasiProyekViewModel
    {
        public Guid Id { get; set; }
        public Guid? AgreementId { get; set; }
        public string LokasiProyek { get; set; }
    }
}
