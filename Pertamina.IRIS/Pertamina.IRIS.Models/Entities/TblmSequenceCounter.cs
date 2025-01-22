using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmSequenceCounter
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public int? Year { get; set; }
    }
}
