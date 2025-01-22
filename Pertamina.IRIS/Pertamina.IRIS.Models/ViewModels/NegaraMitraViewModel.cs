using System;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class NegaraMitraViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? KawasanMitraId { get; set; }
        public string NamaNegara { get; set; }
        public string Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Flag { get; set; }
        public Guid? HubId { get; set; }

        public string NamaKawasan { get; set; }


        public virtual KawasanMitraViewModel KawasanMitra { get; set; }
    }
}