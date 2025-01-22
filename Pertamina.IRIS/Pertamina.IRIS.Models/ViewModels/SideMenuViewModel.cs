using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SideMenuViewModel
    {
        public Guid Feature { get; set; }
        public List<FeatureViewModel> Features { get; set; }
    }
}
