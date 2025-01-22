using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltProgramDevelopment
    {
        public TbltProgramDevelopment()
        {
            TbltProgramDevelopmentEntitasPertaminas = new HashSet<TbltProgramDevelopmentEntitasPertamina>();
            TbltProgramDevelopmentHighPriorities = new HashSet<TbltProgramDevelopmentHighPriority>();
            TbltProgramDevelopmentKementrianLembagas = new HashSet<TbltProgramDevelopmentKementrianLembaga>();
            TbltProgramDevelopmentLokasiProyeks = new HashSet<TbltProgramDevelopmentLokasiProyek>();
            TbltProgramDevelopmentMilestoneTimelines = new HashSet<TbltProgramDevelopmentMilestoneTimeline>();
            TbltProgramDevelopmentNegaraMitras = new HashSet<TbltProgramDevelopmentNegaraMitra>();
            TbltProgramDevelopmentPartners = new HashSet<TbltProgramDevelopmentPartner>();
            TbltProgramDevelopmentPicFungsis = new HashSet<TbltProgramDevelopmentPicFungsi>();
            TbltProgramDevelopmentStreamBusinesses = new HashSet<TbltProgramDevelopmentStreamBusiness>();
        }

        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string JudulInisiasi { get; set; }
        public Guid? InterestId { get; set; }
        public string Scope { get; set; }
        public string NilaiProyek { get; set; }
        public string TargetWaktuProyek { get; set; }
        public Guid? ProgramDevelopmentStatusId { get; set; }
        public string Progress { get; set; }
        public string IsuKendala { get; set; }
        public string Referal { get; set; }
        public string CatatanTambahan { get; set; }
        public string SupportPemerintah { get; set; }
        public string PotensiEskalasi { get; set; }
        public string ValueForIndonesia { get; set; }
        public Guid? OpportunityId { get; set; }
        public Guid? AgreementId { get; set; }
        public bool? DeletedStatus { get; set; }
        public bool? IsDraft { get; set; }
        public string CompanyCode { get; set; }
        public string TindakLanjut { get; set; }
        public decimal? PotentialRevenue { get; set; }
        public decimal? Capex { get; set; }
        public Guid? InitiativeId { get; set; }

        public virtual ICollection<TbltProgramDevelopmentEntitasPertamina> TbltProgramDevelopmentEntitasPertaminas { get; set; }
        public virtual ICollection<TbltProgramDevelopmentHighPriority> TbltProgramDevelopmentHighPriorities { get; set; }
        public virtual ICollection<TbltProgramDevelopmentKementrianLembaga> TbltProgramDevelopmentKementrianLembagas { get; set; }
        public virtual ICollection<TbltProgramDevelopmentLokasiProyek> TbltProgramDevelopmentLokasiProyeks { get; set; }
        public virtual ICollection<TbltProgramDevelopmentMilestoneTimeline> TbltProgramDevelopmentMilestoneTimelines { get; set; }
        public virtual ICollection<TbltProgramDevelopmentNegaraMitra> TbltProgramDevelopmentNegaraMitras { get; set; }
        public virtual ICollection<TbltProgramDevelopmentPartner> TbltProgramDevelopmentPartners { get; set; }
        public virtual ICollection<TbltProgramDevelopmentPicFungsi> TbltProgramDevelopmentPicFungsis { get; set; }
        public virtual ICollection<TbltProgramDevelopmentStreamBusiness> TbltProgramDevelopmentStreamBusinesses { get; set; }
    }
}
