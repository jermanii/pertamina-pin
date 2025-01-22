using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementKementriaanLembagaViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? AgreementId { get; set; }
        public Guid? KementrianLembagaId { get; set; }
        public string LembagaName { get; set; }
        public string Description { get; set; }
        public bool? DeletedStatus { get; set; }
    }
}
