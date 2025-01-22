using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TblmNegaraMitra
    {
        public TblmNegaraMitra()
        {
            TblmNegaraMitraExcludeds = new HashSet<TblmNegaraMitraExcluded>();
            TblmNegaraMitraInfomations = new HashSet<TblmNegaraMitraInfomation>();
            TblmProvinsis = new HashSet<TblmProvinsi>();
            TbltAgreementNegaraMitras = new HashSet<TbltAgreementNegaraMitra>();
            TbltExistingFootprints = new HashSet<TbltExistingFootprint>();
            TbltInitiativeNegaraMitras = new HashSet<TbltInitiativeNegaraMitra>();
            TbltInternationalBusinessIntelligenceCountries = new HashSet<TbltInternationalBusinessIntelligenceCountry>();
            TbltOpportunityNegaraMitras = new HashSet<TbltOpportunityNegaraMitra>();
            TbltProgramDevelopmentNegaraMitras = new HashSet<TbltProgramDevelopmentNegaraMitra>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? KawasanMitraId { get; set; }
        public string NamaNegara { get; set; }
        public string Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Flag { get; set; }
        public Guid? HubId { get; set; }

        public virtual TblmHub Hub { get; set; }
        public virtual TblmKawasanMitra KawasanMitra { get; set; }
        public virtual ICollection<TblmNegaraMitraExcluded> TblmNegaraMitraExcludeds { get; set; }
        public virtual ICollection<TblmNegaraMitraInfomation> TblmNegaraMitraInfomations { get; set; }
        public virtual ICollection<TblmProvinsi> TblmProvinsis { get; set; }
        public virtual ICollection<TbltAgreementNegaraMitra> TbltAgreementNegaraMitras { get; set; }
        public virtual ICollection<TbltExistingFootprint> TbltExistingFootprints { get; set; }
        public virtual ICollection<TbltInitiativeNegaraMitra> TbltInitiativeNegaraMitras { get; set; }
        public virtual ICollection<TbltInternationalBusinessIntelligenceCountry> TbltInternationalBusinessIntelligenceCountries { get; set; }
        public virtual ICollection<TbltOpportunityNegaraMitra> TbltOpportunityNegaraMitras { get; set; }
        public virtual ICollection<TbltProgramDevelopmentNegaraMitra> TbltProgramDevelopmentNegaraMitras { get; set; }
    }
}
