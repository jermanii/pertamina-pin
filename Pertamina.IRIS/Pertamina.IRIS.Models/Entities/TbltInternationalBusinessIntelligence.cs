using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltInternationalBusinessIntelligence
    {
        public TbltInternationalBusinessIntelligence()
        {
            TbltInternationalBusinessIntelligenceAuthors = new HashSet<TbltInternationalBusinessIntelligenceAuthor>();
            TbltInternationalBusinessIntelligenceCountries = new HashSet<TbltInternationalBusinessIntelligenceCountry>();
            TbltInternationalBusinessIntelligenceDocuments = new HashSet<TbltInternationalBusinessIntelligenceDocument>();
            TbltInternationalBusinessIntelligenceMylists = new HashSet<TbltInternationalBusinessIntelligenceMylist>();
        }

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? TypeStudyId { get; set; }
        public DateTime? ResearchDate { get; set; }
        public string Title { get; set; }
        public Guid? ConfidentialityId { get; set; }
        public string Description { get; set; }
        public bool? IsDraft { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid? StreamBusinessId { get; set; }

        public virtual TblmConfidentiality Confidentiality { get; set; }
        public virtual TblmEntitasPertamina EntitasPertamina { get; set; }
        public virtual TblmStreamBusiness StreamBusiness { get; set; }
        public virtual TblmTypeStudy TypeStudy { get; set; }
        public virtual ICollection<TbltInternationalBusinessIntelligenceAuthor> TbltInternationalBusinessIntelligenceAuthors { get; set; }
        public virtual ICollection<TbltInternationalBusinessIntelligenceCountry> TbltInternationalBusinessIntelligenceCountries { get; set; }
        public virtual ICollection<TbltInternationalBusinessIntelligenceDocument> TbltInternationalBusinessIntelligenceDocuments { get; set; }
        public virtual ICollection<TbltInternationalBusinessIntelligenceMylist> TbltInternationalBusinessIntelligenceMylists { get; set; }
    }
}
