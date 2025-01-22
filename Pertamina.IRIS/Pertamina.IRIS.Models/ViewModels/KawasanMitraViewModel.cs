using System;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class KawasanMitraViewModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? ContinentId { get; set; }
        public string NamaKawasan { get; set; }
        public string Description { get; set; }
        public int? OrderSeq { get; set; }
    }
}