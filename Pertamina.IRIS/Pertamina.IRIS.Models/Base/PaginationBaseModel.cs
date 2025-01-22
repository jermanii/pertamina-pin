using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.Base
{
    public class PaginationBaseModel<T> : ResponseDataTableBaseModel
    {
        public string Draw { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public List<T> Data { get; set; }
    }
}
