using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public  class InitiativeNegaraMitraViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public bool? DeletedStatus { get; set; }

        public string NamaNegara { get; set; }
        public Guid? KawasanMitraId { get; set; }
        public string NamaKawasan { get; set; }

        public InitiativeViewModel Initiative { get; set; }
        public  NegaraMitraViewModel NegaraMitra { get; set; }
    }
}
