using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementDashboardViewModel : ErrorBaseModel
    {
        public Guid? StreamBusinessId { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid? CategoryId { get; set; }
        public string CapexSort { get; set; }
        public string PotentialSort { get; set; }
        public string LembagaSort { get; set; }
        public int? PageCountSort { get; set; }
        public int? RecordHighPriorityCountSort { get; set; }
        public int? AllRecordHighPriorityCountSort { get; set; }
        public string CountryAcronym { get; set; }
        public List<SignedAgreementDashboardHighPriorityViewModel> HighPriority { get; set; }
        public List<MetricViewModel> Metrics { get; set; }
        public List<SignedAgreementKementriaanLembagaViewModel> KementrianLembaga { get; set; }
    }
}
