using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class StoredProcedureViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Group { get; set; }
        public string SpName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int? OrderSeq { get; set; }
    }
}
