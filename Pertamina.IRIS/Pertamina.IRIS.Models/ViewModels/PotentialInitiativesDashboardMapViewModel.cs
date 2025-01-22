using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class PotentialInitiativesDashboardMapViewModel
    {        
        public List<PotentialInitiativesDashboardHubGroupViewModel> HubGrouppedCountries { get; set; }
        public List<string> CountriesAcronym { get; set; }
        public List<PotentialInitiativesDashboardMapPointerViewModel> Pointers { get; set; }
    }

    public class PotentialInitiativesDashboardMapPointerViewModel
    {
        public string CountriesAcronym { get; set; }
        public string SubHolding { get; set; }
    }

    public class PotentialInitiativesDashboardHubGroupViewModel
    {
        public HubViewModel Hub { get; set; }
        public List<string> GrouppedCountriesAcronym { get; set; }
    }
}
