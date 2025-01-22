using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementDashboardMapViewModel
    {
        public List<string> CountriesAcronym { get; set; }
        public List<SignedAgreementDashboardMapPointerViewModel> Pointers { get; set; }
    }
    public class SignedAgreementDashboardMapPointerViewModel
    {
        public string CountriesAcronym { get; set; }
        public string SubHolding { get; set; }
    }
}
