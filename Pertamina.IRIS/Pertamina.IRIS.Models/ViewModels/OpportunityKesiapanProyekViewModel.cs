﻿using System;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class OpportunityKesiapanProyekViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? OpportunityId { get; set; }
        public Guid? KesiapanProyekId { get; set; }
        public bool? DeletedStatus { get; set; }


        public string Name { get; set; }
        public string QueryKesiapanProyekName { get; set; }
    }
}