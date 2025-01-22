using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmNegaraMitraInfomation
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string Population { get; set; }
        public string Gdp { get; set; }
        public string GdpPerCapita { get; set; }
        public string OilGasReserves { get; set; }
        public string OilProduction { get; set; }
        public string CountryAcronyms { get; set; }

        public virtual TblmNegaraMitra NegaraMitra { get; set; }
    }
}
