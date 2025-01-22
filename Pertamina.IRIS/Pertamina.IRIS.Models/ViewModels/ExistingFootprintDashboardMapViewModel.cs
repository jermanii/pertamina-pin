using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ExistingFootprintDashboardMapViewModel
    {
        public List<string> CountriesAcronym { get; set; }
        public List<ExistingFootprintDashboardMapPointerViewModel> Pointers {  get; set; }
    }

    public class ExistingFootprintDashboardMapPointerViewModel
    {
        public string CountriesAcronym { get; set; }
        public string SubHolding { get; set; }
    }
}
