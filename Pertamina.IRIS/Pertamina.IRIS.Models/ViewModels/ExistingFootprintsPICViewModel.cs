using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ExistingFootprintsPICViewModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid ExistingFootprintsId { get; set; }
        public Guid? PicFungsiId { get; set; }
        public Guid? PicLevelId { get; set; }

        public string PicFungsiName { get; set; }
    }
}
