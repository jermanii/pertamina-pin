using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InitiativeViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public Guid? InitiativeTypesId { get; set; }
        public Guid? InitiativeStageId { get; set; }
        public Guid? InitiativeStatusId { get; set; }
        public Guid? InterestId { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public Guid? OpportunityId { get; set; }
        public Guid? AgreementId { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string JudulInisiasi { get; set; }
        public string Scope { get; set; }
        public string NilaiProyek { get; set; }
        public string TargetWaktuProyek { get; set; }
        public string Progress { get; set; }
        public string IsuKendala { get; set; }
        public string TindakLanjut { get; set; }
        public string Referal { get; set; }
        public string Research { get; set; }
        public string CatatanTambahan { get; set; }
        public string SupportPemerintah { get; set; }
        public string PotensiEskalasi { get; set; }
        public string ValueForIndonesia { get; set; }
        public string SelectedFormType { get; set; }
        public string PicLeads { get; set; }
        public string DeskripsiKementrianLembaga { get; set; }
        public string ProjectName { get; set; }
        public string InitialLead { get; set; }
        public string Initial { get; set; }
        public string SendToSupport { get; set; }
        public string SendToSubject { get; set; }
        public string SendToDetails { get; set; }

        public bool? DeletedStatus { get; set; }
        public bool? IsDraft { get; set; }

        public int? StepStatus { get; set; }
        public decimal? PotentialRevenuePerYear { get; set; }
        public decimal? Capex { get; set; }

        public List<Guid> FungsiPicId { get; set; }
        public List<Guid?> EntitasPertaminaId { get; set; }
        public List<Guid?> StreamBusinessId { get; set; }
        public List<Guid?> HubId { get; set; }

        public List<string> LokasiProyek { get; set; }
        public List<string> Partners { get; set; }
        public string LastUpdate { get; set; }        
        public List<Guid> KementrianLembagaId { get; set; }

        public  InterestViewModel Interest { get; set; }
        public  NegaraMitraViewModel NegaraMitra { get; set; }
        public  StreamBusinessViewModel StreamBusiness { get; set; }
        public InitiativeEntitasPertaminaViewModel InitiativeEntitasPertamina { get; set; }
        public InitiativeLokasiProyekViewModel InitiativeLokasiProyek { get; set; }
        public List<InitiativeLokasiProyekViewModel> InitiativeLokasiProyeks { get; set; }
        public InitiativeNegaraMitraViewModel InitiativeNegaraMitra { get; set; }
        public InitiativeOpportunityViewModel InitiativeOpportunity { get; set; }
        public InitiativePartnerViewModel InitiativePartner { get; set; }
        public List<InitiativePartnerViewModel> InitiativePartners { get; set; }
        public InitiativePicFungsiViewModel InitiativePicFungsi { get; set; }
        public InitiativeStreamBusinessViewModel InitiativeStreamBusiness { get; set; }
        public InitiativeStageViewModel InitiativeStage { get; set; }
        public InitiativeStatusViewModel InitiativeStatus { get; set; }
        public InitiativeTypesViewModel InitiativeTypes { get; set; }
        public InitiativeHighPriorityViewModel InitiativeHighPriority { get; set; }
        public List<InitiativeMilestoneTimelineViewModel> InitiativeMilestoneTimeline { get; set; }
        public HubHeadViewModel HubHead { get; set; }
        public PicFungsiViewModel PicLead { get; set; }
        public KementrianLembagaViewModel KementrianLembaga { get; set; }
        public InitiativeKementrianLembagaViewModel InitiativeKementrianLembaga { get; set; }

        #region Export
        public string CellNumber { get; set; }
        public string CellPartner { get; set; }
        public string CellEntitasPertamina { get; set; }
        public string CellStreamBusiness { get; set; }
        public string CellHsh { get; set; }
        public string CellFungsiPic { get; set; }
        public string CellJudulInisiasi { get; set; }
        public string CellInterest { get; set; }
        public string CellInitiativeStatus { get; set; }
        public string CellInitiativeType { get; set; }
        public string CellInitiativeStage { get; set; }
        public string CellKawasanMitra { get; set; }
        public string CellNegaraMitra { get; set; }
        public string CellLokasiProyek { get; set; }
        public string CellNilaiProyek { get; set; }
        public string CellTargetProyek { get; set; }
        public string CellScope { get; set; }
        public string CellProgress { get; set; }
        public string CellValue { get; set; }
        public string CellIsuKendala { get; set; }
        public string CellSupportPemerintah { get; set; }
        public string CellReferral { get; set; }
        public string CellPotensiEkskalasi { get; set; }
        public string CellRelatedAgreement { get; set; }
        public string CellCatatanTambahan { get; set; }
        public string CellInitiativeStageId { get; set; }
        public string CellInitiativeStatusId { get; set; }
        public string CellInitiativeHolderId { get; set; }
        public string CellNegaraMitraId { get; set; }
        public string CellKawasanMitraId { get; set; }
        public string CellStreamBusinessId { get; set; }
        public string CellEntitasPertaminaId { get; set; }
        public string CellCreateDate { get; set; }
        public string CellKodeAgreement { get; set; }
        public string CompanyCode { get; set; }
        public string CellTindakLanjut { get; set; }
        #endregion

        #region Index
        // Query
        public Guid? QueryHshId { get; set; }
        public Guid? QueryEntitasPertaminaId { get; set; }
        public string QueryEntitasPertaminaName { get; set; }
        public Guid? QueryStatusBerlakuId { get; set; }
        public Guid? QueryNegaraMitraId { get; set; }
        public string QueryNamaNegara { get; set; }
        public Guid? QueryKawasanMitraId { get; set; }
        public string QueryNamaKawasan { get; set; }
        public Guid? QueryStreamBusinessId { get; set; }
        public string QueryStreamBusinessName { get; set; }
        public Guid? QueryInitiativeStageId { get; set; }
        public string QueryInitiativeStageName { get; set; }
        public Guid? QueryInitiativeStatusId { get; set; }
        public string QueryInitiativeStatusName { get; set; }


        // Count
        public List<InitiativeStageViewModel> CountInitiativeStage { get; set; }
        public List<InitiativeStatusViewModel> CountInitiativeStatus { get; set; }
        public List<HshViewModel> CountInitiativeHolder { get; set; }
        public int? CountInitiative { get; set; }
        public int? CountNegaraMitra { get; set; }

        // Filter By
        public string DdlInitiativeStageId { get; set; }
        public string DdlInitiativeStatusId { get; set; }
        public string DdlInitiativeHolderId { get; set; }
        public string DdlNegaraMitraId { get; set; }
        public string DdlKawasanMitraId { get; set; }
        public string DdlStreamBusinessId { get; set; }
        public string DdlEntitasPertaminaId { get; set; }

        // Date Range
        public string RangeCreateDate { get; set; }

        // Row Table
        public bool? RowStatus { get; set; }
        public string RowJudulInitiative { get; set; }
        public string RowPartner { get; set; }
        public string RowEntitasPertamina { get; set; }
        public string RowStreamBusiness { get; set; }
        public string RowInitiativeStage { get; set; }
        public string RowInitiativeStatus { get; set; }
        public string RowNegaraMitra { get; set; }
        public string RowKawasanMitra { get; set; }
        public string RowTargetMoU { get; set; }
        public string RowTargetDefinitive { get; set; }
        public string RowHubHeadName { get; set; }
        public string RowPicLeadName { get; set; }
        public string RowPotentialRevenue { get; set; }
        public string RowCapex { get; set; }
        public string RowStatusMilestone { get; set; }
        public string RowSupportRequired { get; set; }

        // Encode
        public string EntitasPertaminaEncode { get; set; }
        public string StreamBusinessEncode { get; set; }
        public string NegaraMitraEncode { get; set; }
        public string KawasanMitraEncode { get; set; }
        public string InitiativeHolderEncode { get; set; }
        public string InitiativeStageEncode { get; set; }
        public string InitiativeStatusEncode { get; set; }
        public string RangeCreateDateEncode { get; set; }
        

        // Decode
        public string EntitasPertaminaDecode { get; set; }
        public string StreamBusinessDecode { get; set; }
        public string KawasanMitraDecode { get; set; }
        public string NegaraMitraDecode { get; set; }
        public string InitiativeHolderDecode { get; set; }
        public string InitiativeStageDecode { get; set; }
        public string InitiativeStatusDecode { get; set; }
        public string RangeCreateDateDecode { get; set; }

        // Search
        public string StrSearch { get; set; }
        #endregion

        #region Read
        public string ReadJudulInitiative { get; set; }
        public InterestViewModel ReadInterest { get; set; }
        public InitiativeStatusViewModel ReadInitiativeStatus { get; set; }
        public InitiativeTypesViewModel ReadInitiativeType { get; set; }
        public InitiativeStageViewModel ReadInitiativeStage { get; set; }
        public List<PicFungsiViewModel> ReadPicFungsi { get; set; }
        public List<EntitasPertaminaViewModel> ReadEntitasPertamina { get; set; }
        public List<string> ReadPartner { get; set; }
        public List<NegaraMitraViewModel> ReadNegaraMitra { get; set; }
        public List<string> ReadLokasiProyek { get; set; }
        public string ReadNilaiProyek { get; set; }
        public string ReadTargetWaktuProyek { get; set; }
        public List<StreamBusinessViewModel> ReadStreamBusiness { get; set; }
        public string ReadProgress { get; set; }
        public string ReadIsuKendala { get; set; }
        public string ReadTindakLanjut { get; set; }
        public string ReadSupportPemerintah { get; set; }
        public string ReadValueForIndonesia { get; set; }
        public AgreementViewModel ReadRelatedAgreement { get; set; }
        public string ReadReferal { get; set; }
        public string ReadPotensiEskalasi { get; set; }
        public string ReadCatatanTambahan { get; set; }
        #endregion
    }

}
