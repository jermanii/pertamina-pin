using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class FeatureViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string ParentFeatureCode { get; set; }
        public string FeatureName { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string MenuIcon { get; set; }
        public bool? IsMenu { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public int SequenceChild { get; set; }
        public string SubMenuIcon { get; set; }
        public string SubMenuDesc { get; set; }
        public string MenuLink { get; set; }
        public List<FeatureViewModel> ChildFeatures { get; set; }
    }
}
