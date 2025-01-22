using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.Base
{
    public class PageInfoBaseModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
    }
}
