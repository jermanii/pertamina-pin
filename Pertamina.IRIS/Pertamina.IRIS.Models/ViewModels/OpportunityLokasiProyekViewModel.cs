﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class OpportunityLokasiProyekViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? OpportunityId { get; set; }
        public string LokasiProyek { get; set; }


        public string ProvinceAcronyms { get; set; }
        public string NamaProvinsi { get; set; }
        public string NamaProyeks { get; set; }
        public List<OpportunityLokasiProyekViewModel> NamaProyekss { get; set; }
    }
}
