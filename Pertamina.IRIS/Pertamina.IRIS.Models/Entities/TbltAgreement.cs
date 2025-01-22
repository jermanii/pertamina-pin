using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltAgreement
    {
        public TbltAgreement()
        {
            TbltAgreementAddenda = new HashSet<TbltAgreementAddendum>();
            TbltAgreementEntitasPertaminas = new HashSet<TbltAgreementEntitasPertamina>();
            TbltAgreementHighPriorities = new HashSet<TbltAgreementHighPriority>();
            TbltAgreementHubHeads = new HashSet<TbltAgreementHubHead>();
            TbltAgreementKementrianLembagas = new HashSet<TbltAgreementKementrianLembaga>();
            TbltAgreementLokasiProyeks = new HashSet<TbltAgreementLokasiProyek>();
            TbltAgreementMilestoneTimelines = new HashSet<TbltAgreementMilestoneTimeline>();
            TbltAgreementNegaraMitras = new HashSet<TbltAgreementNegaraMitra>();
            TbltAgreementPartners = new HashSet<TbltAgreementPartner>();
            TbltAgreementPicFungsis = new HashSet<TbltAgreementPicFungsi>();
            TbltAgreementStreamBusinesses = new HashSet<TbltAgreementStreamBusiness>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string JudulPerjanjian { get; set; }
        public Guid? JenisPerjanjianId { get; set; }
        public string Scope { get; set; }
        public string NilaiProyek { get; set; }
        public DateTime? TanggalTtd { get; set; }
        public DateTime? TanggalBerakhir { get; set; }
        public Guid? StatusBerlakuId { get; set; }
        public Guid? DiscussionStatusId { get; set; }
        public string Progress { get; set; }
        public Guid? FaktorKendalaId { get; set; }
        public Guid? KlasifikasiKendalaId { get; set; }
        public string DeskripsiKendala { get; set; }
        public string SupportPemerintah { get; set; }
        public Guid? InitiativeId { get; set; }
        public Guid? OpportunityId { get; set; }
        public string PotensiEskalasi { get; set; }
        public string CatatanTambahan { get; set; }
        public Guid? RelatedAgreementId { get; set; }
        public bool? DeletedStatus { get; set; }
        public bool? IsDraft { get; set; }
        public string CompanyCode { get; set; }
        public string KodeAgreement { get; set; }
        public string TindakLanjut { get; set; }
        public decimal? PotentialRevenuePerYear { get; set; }
        public decimal? Capex { get; set; }
        public decimal? Valuation { get; set; }
        public bool? IsGtg { get; set; }
        public bool? IsAddendum { get; set; }
        public Guid? TrafficLightId { get; set; }

        public virtual ICollection<TbltAgreementAddendum> TbltAgreementAddenda { get; set; }
        public virtual ICollection<TbltAgreementEntitasPertamina> TbltAgreementEntitasPertaminas { get; set; }
        public virtual ICollection<TbltAgreementHighPriority> TbltAgreementHighPriorities { get; set; }
        public virtual ICollection<TbltAgreementHubHead> TbltAgreementHubHeads { get; set; }
        public virtual ICollection<TbltAgreementKementrianLembaga> TbltAgreementKementrianLembagas { get; set; }
        public virtual ICollection<TbltAgreementLokasiProyek> TbltAgreementLokasiProyeks { get; set; }
        public virtual ICollection<TbltAgreementMilestoneTimeline> TbltAgreementMilestoneTimelines { get; set; }
        public virtual ICollection<TbltAgreementNegaraMitra> TbltAgreementNegaraMitras { get; set; }
        public virtual ICollection<TbltAgreementPartner> TbltAgreementPartners { get; set; }
        public virtual ICollection<TbltAgreementPicFungsi> TbltAgreementPicFungsis { get; set; }
        public virtual ICollection<TbltAgreementStreamBusiness> TbltAgreementStreamBusinesses { get; set; }
    }
}
