using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class OpportunityPicFungsiViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? OpportunityId { get; set; }
        public Guid? PicFungsiId { get; set; }
        public bool? DeletedStatus { get; set; }
        public Guid? PicLevelId { get; set; }

        public string PicName { get; set; }
    }
}
