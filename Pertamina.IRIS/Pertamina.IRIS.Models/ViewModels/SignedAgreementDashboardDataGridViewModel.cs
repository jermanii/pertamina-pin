using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementDashboardDataGridViewModel : ErrorBaseModel
    {
        public DateTime? CreatedDate { get; set; }
        public Guid AgreementId { get; set; }
        public string JudulPerjanjian { get; set; }
        public decimal? PotentialRevenuePerYear { get; set; }
        public string PotentialRevenuePerYearFormat { get; set; }
        public decimal? Capex { get; set; }
        public string CapexFormat { get; set; }
        public string EntitasPertamina { get; set; }
        public string NamaNegara { get; set; }
        public string HubHeadName { get; set; }
        public string KodeAgreement { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public string PICName { get; set; }
        public string MoUSigningDate { get; set; }
        public Guid? StreamBusinessFilter { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid? JenisPerjanjianId { get; set; }





    }
}
