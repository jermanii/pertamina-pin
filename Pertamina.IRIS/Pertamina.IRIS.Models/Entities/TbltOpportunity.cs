using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltOpportunity
    {
        public TbltOpportunity()
        {
            TbltOpportunityEntitasPertaminas = new HashSet<TbltOpportunityEntitasPertamina>();
            TbltOpportunityHighPriorities = new HashSet<TbltOpportunityHighPriority>();
            TbltOpportunityKesiapanProyeks = new HashSet<TbltOpportunityKesiapanProyek>();
            TbltOpportunityLokasiProyeks = new HashSet<TbltOpportunityLokasiProyek>();
            TbltOpportunityNegaraMitras = new HashSet<TbltOpportunityNegaraMitra>();
            TbltOpportunityPartners = new HashSet<TbltOpportunityPartner>();
            TbltOpportunityPicFungsis = new HashSet<TbltOpportunityPicFungsi>();
            TbltOpportunityStreamBusinesses = new HashSet<TbltOpportunityStreamBusiness>();
            TbltOpportunityTargetMitras = new HashSet<TbltOpportunityTargetMitra>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string NamaProyek { get; set; }
        public string Deliverables { get; set; }
        public string ProjectProfile { get; set; }
        public string NilaiProyek { get; set; }
        public string Timeline { get; set; }
        public string Progress { get; set; }
        public string CatatanTambahan { get; set; }
        public bool? DeletedStatus { get; set; }
        public bool? IsDraft { get; set; }
        public string CompanyCode { get; set; }
        public string TindakLanjut { get; set; }
        public string TypeOfPartnerNeeded { get; set; }
        public decimal? PotentialRevenuePerYear { get; set; }
        public decimal? Capex { get; set; }

        public virtual ICollection<TbltOpportunityEntitasPertamina> TbltOpportunityEntitasPertaminas { get; set; }
        public virtual ICollection<TbltOpportunityHighPriority> TbltOpportunityHighPriorities { get; set; }
        public virtual ICollection<TbltOpportunityKesiapanProyek> TbltOpportunityKesiapanProyeks { get; set; }
        public virtual ICollection<TbltOpportunityLokasiProyek> TbltOpportunityLokasiProyeks { get; set; }
        public virtual ICollection<TbltOpportunityNegaraMitra> TbltOpportunityNegaraMitras { get; set; }
        public virtual ICollection<TbltOpportunityPartner> TbltOpportunityPartners { get; set; }
        public virtual ICollection<TbltOpportunityPicFungsi> TbltOpportunityPicFungsis { get; set; }
        public virtual ICollection<TbltOpportunityStreamBusiness> TbltOpportunityStreamBusinesses { get; set; }
        public virtual ICollection<TbltOpportunityTargetMitra> TbltOpportunityTargetMitras { get; set; }
    }
}
