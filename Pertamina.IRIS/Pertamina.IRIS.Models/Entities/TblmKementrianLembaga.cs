using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmKementrianLembaga
    {
        public TblmKementrianLembaga()
        {
            TbltAgreementKementrianLembagas = new HashSet<TbltAgreementKementrianLembaga>();
            TbltInitiativeKementrianLembagas = new HashSet<TbltInitiativeKementrianLembaga>();
            TbltProgramDevelopmentKementrianLembagas = new HashSet<TbltProgramDevelopmentKementrianLembaga>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string LembagaName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TbltAgreementKementrianLembaga> TbltAgreementKementrianLembagas { get; set; }
        public virtual ICollection<TbltInitiativeKementrianLembaga> TbltInitiativeKementrianLembagas { get; set; }
        public virtual ICollection<TbltProgramDevelopmentKementrianLembaga> TbltProgramDevelopmentKementrianLembagas { get; set; }
    }
}
