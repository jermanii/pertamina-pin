using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SortViewModel
    {
        public string SortColumn {  get; set; }
        public string SortOrder { get; set; }
        public SortViewModel()
        {
        }
        public SortViewModel(string sortColumn, string sortOrder) { 
            SortColumn = sortColumn;
            SortOrder = sortOrder;
        }
    }
}
