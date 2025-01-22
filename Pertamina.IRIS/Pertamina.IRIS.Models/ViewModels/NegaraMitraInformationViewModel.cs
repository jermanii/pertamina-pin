using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class NegaraMitraInformationViewModel: ErrorBaseModel
    {
        public string Id { get; set; }   
        public string CountryAcronyms { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string NegaraMitraName { get; set; }
        public string Population { get; set; }
        public string Gdp { get; set; }
        public string GdpPerCapita { get; set; }
        public string OilGasReserves { get; set; }
        public string OilProduction { get; set; } 
        public string Flag { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? DocumentCount { get; set; }
        public List<Guid?> InternationalBusinessIntelligenceId { get; set; }

    }
}
