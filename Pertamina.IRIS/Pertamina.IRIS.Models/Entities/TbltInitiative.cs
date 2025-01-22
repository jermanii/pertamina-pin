using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltInitiative
    {
        public TbltInitiative()
        {
            TbltInitiativeContactPeople = new HashSet<TbltInitiativeContactPerson>();
            TbltInitiativeEntitasPertaminas = new HashSet<TbltInitiativeEntitasPertamina>();
            TbltInitiativeHighPriorities = new HashSet<TbltInitiativeHighPriority>();
            TbltInitiativeHubHeads = new HashSet<TbltInitiativeHubHead>();
            TbltInitiativeKementrianLembagas = new HashSet<TbltInitiativeKementrianLembaga>();
            TbltInitiativeLokasiProyeks = new HashSet<TbltInitiativeLokasiProyek>();
            TbltInitiativeMilestoneTimelines = new HashSet<TbltInitiativeMilestoneTimeline>();
            TbltInitiativeNegaraMitras = new HashSet<TbltInitiativeNegaraMitra>();
            TbltInitiativePartners = new HashSet<TbltInitiativePartner>();
            TbltInitiativePicFungsis = new HashSet<TbltInitiativePicFungsi>();
            TbltInitiativeStreamBusinesses = new HashSet<TbltInitiativeStreamBusiness>();
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
        public Guid? InitiativeTypesId { get; set; }
        public Guid? InitiativeStageId { get; set; }
        public Guid? InitiativeStatusId { get; set; }
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
        public decimal? PotentialRevenuePerYear { get; set; }
        public decimal? Capex { get; set; }
        public string Comments { get; set; }
        public DateTime? TargetDefinitiveAgreement { get; set; }

        public virtual ICollection<TbltInitiativeContactPerson> TbltInitiativeContactPeople { get; set; }
        public virtual ICollection<TbltInitiativeEntitasPertamina> TbltInitiativeEntitasPertaminas { get; set; }
        public virtual ICollection<TbltInitiativeHighPriority> TbltInitiativeHighPriorities { get; set; }
        public virtual ICollection<TbltInitiativeHubHead> TbltInitiativeHubHeads { get; set; }
        public virtual ICollection<TbltInitiativeKementrianLembaga> TbltInitiativeKementrianLembagas { get; set; }
        public virtual ICollection<TbltInitiativeLokasiProyek> TbltInitiativeLokasiProyeks { get; set; }
        public virtual ICollection<TbltInitiativeMilestoneTimeline> TbltInitiativeMilestoneTimelines { get; set; }
        public virtual ICollection<TbltInitiativeNegaraMitra> TbltInitiativeNegaraMitras { get; set; }
        public virtual ICollection<TbltInitiativePartner> TbltInitiativePartners { get; set; }
        public virtual ICollection<TbltInitiativePicFungsi> TbltInitiativePicFungsis { get; set; }
        public virtual ICollection<TbltInitiativeStreamBusiness> TbltInitiativeStreamBusinesses { get; set; }
    }
}
