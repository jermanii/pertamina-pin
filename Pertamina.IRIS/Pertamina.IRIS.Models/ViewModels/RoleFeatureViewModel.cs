using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class RoleFeatureViewModel
    {
        public string Id { get; set; }
        public string FeatureId { get; set; }
        public string RoleId { get; set; }
        public bool IsAddView { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsRead { get; set; }
        public FeatureViewModel Feature { get; set; }
    }
}
