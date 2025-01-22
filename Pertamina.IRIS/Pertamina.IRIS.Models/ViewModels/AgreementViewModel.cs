using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class AgreementViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string JudulPerjanjian { get; set; }
        public string Scope { get; set; }
        public string NilaiProyek { get; set; }
        public DateTime? TanggalTtd { get; set; }
        public DateTime? TanggalBerakhir { get; set; }
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
        public Guid? JenisPerjanjianId { get; set; }
        public Guid? StatusBerlakuId { get; set; }
        public Guid? DiscussionStatusId { get; set; }
        public string CompanyCode { get; set; }
        public string KodeAgreement { get; set; }
        public string TindakLanjut { get; set; }
        public decimal? PotentialRevenuePerYear { get; set; }
        public string PotentialRevenuePerYearToString { get; set; }
        public string CapexToString { get; set; }
        public decimal? Capex { get; set; }
        public bool? IsGtg { get; set; }
        public bool? IsAddendum { get; set; }
        public decimal? Valuation { get; set; }
        public string ValuationToString { get; set; }
        public DateTime? AmandementDate { get; set; }
        public List<string> SequenceAmandement { get; set; }
        public int? Sequence { get; set; }
        public Guid? TrafficLightId { get; set; }


        public string TrafficLightName { get; set; }
        public string TrafficLightColor { get; set; }

        public Guid? PicFungsiId { get; set; }
        public Guid? PicLevelId { get; set; }
        public Guid? PicLevelMemberId { get; set; }
        public string PicLevelName { get; set; }
        public string PicLevelNameMember { get; set; }


        public Guid? JudulId { get; set; }
        public Guid? HshId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid? HubId{ get; set; }
        public Guid HubHeadId { get; set; }
        public string HubHeadName { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public Guid? KawasanMitraId { get; set; }
        public Guid? KementrianLembaga { get; set; }
        public Guid? StatusBerlakuValue { get; set; }
        public bool? RowStatus { get; set; }
        public string RowEntitasPertamina { get; set; }
        
        public string RowPartner { get; set; }
        public string RowStreamBusiness { get; set; }
        public string RowJenisPerjanjian { get; set; }
        public string RowNegaraMitra { get; set; }
        public string RowKawasanMitra { get; set; }

        public string SelectedFormType { get; set; }
        public string JudulInisiasi { get; set; }
        public List<StatusBerlakuViewModel> StatusBerlaku { get; set; }
        public string StatusBerlakuName { get; set; }

        public string RowStatusBerlaku { get; set; }
        public string RowStatusBerlakuColorName { get; set; }
        public string RowStatusBerlakuColorHexa { get; set; }

        public List<StatusDiscussionViewModel> DiscussionStatus { get; set; }
        public string RowDiscussionStatus { get; set; }
        public string RowDiscussionStatusColorName { get; set; }
        public string RowDiscussionStatusColorHexa { get; set; }

        public List<HshViewModel> AgreementHolder { get; set; }
        public int? Perjanjian { get; set; }
        public int? NegaraMitra { get; set; }
        public Guid? AgreementHolderId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public string RangeTanggalTtd { get; set; }
        public string RangeTanggalBerakhir { get; set; }
        public string RangeCreateDate { get; set; }

        public List<Guid> StreamBusinessIds { get; set; }
        public List<Guid> KementrianLembagaIds { get; set; }
        public List<Guid> FungsiPicId { get; set; }
        public AgreementPartnerViewModel AgreementPartner { get; set; }
        public List<AgreementPartnerViewModel> AgreementPartners { get; set; }
        public AgreementLokasiProyekViewModel AgreementLokasiProyek { get; set; }
        public List<AgreementLokasiProyekViewModel> AgreementLokasiProyeks { get; set; }

        public string EntitasPertaminaEncode { get; set; }
        public string StreamBusinessEncode { get; set; }
        public string JenisPerjanjianEncode { get; set; }
        public string NegaraMitraEncode { get; set; }
        public string KawasanMitraEncode { get; set; }
        public string StatusBerlakuEncode { get; set; }
        public string StatusDiscussionEncode { get; set; }
        public string AgreementHolderEncode { get; set; }
        public string RangeTanggalTtdEncode { get; set; }
        public string RangeTanggalBerakhirEncode { get; set; }
        public string RangeCreateDateEncode { get; set; }

        public string EntitasPertaminaDecode { get; set; }
        public string StreamBusinessDecode { get; set; }
        public string JenisPerjanjianDecode { get; set; }
        public string KawasanMitraDecode { get; set; }
        public string NegaraMitraDecode { get; set; }
        public string StatusBerlakuDecode { get; set; }
        public string StatusDiscussionDecode { get; set; }
        public string AgreementHolderDecode { get; set; }
        public string RangeTanggalTtdDecode { get; set; }
        public string RangeTanggalBerakhirDecode { get; set; }
        public string RangeCreateDateDecode { get; set; }
        public bool? IsG2GDecode { get; set; }
        public bool? IsStrategicPartnerShipDecode { get; set; }
        public bool? IsBdCoreBusinessDecode { get; set; }
        public bool? IsGreenBusinessDecode { get; set; }

        public string StrSearch { get; set; }

        public List<Guid> EntitasPertaminaEvent { get; set; }
        public AgreementEntitasPertaminaViewModel AgreementEntitasPertamina { get; set; }
        public AgreementInitiativeViewModel AgreementInitiative { get; set; }
        public AgreementKementrianLembagaViewModel AgreementKementrianLembaga { get; set; }
        public AgreementNegaraMitraViewModel AgreementNegaraMitra { get; set; }
        public AgreementOpportunityViewModel AgreementOpportunity { get; set; }
        public AgreementHubHeadViewModel AgreementHubHead { get; set; }
        public AgreementPicFungsiViewModel AgreementPicFungsi { get; set; }
        public List<AgreementPicFungsiViewModel> AgreementPicFungsis { get; set; }
        public List<AgreementAddendumViewModel> AgreementAddendums { get; set; }
        public AgreementAddendumViewModel AgreementAddendum { get; set; }
        public AgreementStreamBusinessViewModel AgreementStreamBusiness { get; set; }

        //Read
        public AgreementJenisPerjanjian JenisPerjanjianRead { get; set; }
        public PicFungsiViewModel PicFungsiLeadRead { get; set; }
        public List<PicFungsiViewModel> PicFungsiRead { get; set; }
        public List<EntitasPertaminaViewModel> EntitasPertaminaRead { get; set; }
        public List<AgreementAddendumViewModel> AddendumRead { get; set; }
        public StatusBerlakuViewModel StatusBerlakuRead { get; set; }
        public StatusDiscussionViewModel StatusDiscussionRead { get; set; }
        public List<string> PartnerRead { get; set; }
        public List<NegaraMitraViewModel> NegaraMitraRead { get; set; }
        public List<string> LokasiProyekRead { get; set; }
        public List<StreamBusinessViewModel> StreamBusinessRead { get; set; }
        public FaktorKendalaViewModel FaktorKendalaRead { get; set; }
        public KlasifikasiKendalaViewModel KlasifikasiKendalaRead { get; set; }
        public List<string> KementrianLembagaRead { get; set; }
        public AgreementViewModel RelatedAgreementRead { get; set; }
        public AgreementViewModel TrafficLightRead { get; set; }
        public List<AgreementKementrianLembagaViewModel> AgreementKementrianLembagas { get; set; }

        #region Export
        public string CellNumber{ get; set; }
        public string CellNamaProyek { get; set; }
        public string CellStreamBusiness{ get; set; }
        public string CellEntitasPertamina { get; set; }
        public string CellHsh { get; set; }
        public string CellFungsiPicLead { get; set; }
        public string CellFungsiPicMember { get; set; }
        public string CellKesiapanProyek { get; set; }
        public string CellCatatanTambahan { get; set; }
        public string CellPartner { get; set; }
        public string CellLokasiProyek { get; set; }
        public string CellJenisPerjanjian { get; set; }
        public string CellKawasanMitra { get; set; }
        public string CellNegaraMitra { get; set; }
        public string CellProjectCost { get; set; }
        public string CellRelatedAgreement { get; set; }
        public string CellStatusDiskusi { get; set; }
        public string CellFaktorKendala { get; set; }
        public string CellKlasifikasiKendala { get; set; }
        public string CellLembaga { get; set; }
        public string CellStatusBerlaku { get; set; }
        public string CellHshId { get; set; }
        public string CellEntitasPertaminaId { get; set; }
        public string CellJenisPerjanjianId { get; set; }
        public string CellKawasanMitraId { get; set; }
        public string CellNegaraMitraId { get; set; }
        public string CellStreamBusinessId { get; set; }
        public string CellStatusBerlakuId { get; set; }
        public string CellStatusDiskusiId { get; set; }
        public string CellCreateDate { get; set; }
        public string CellTanggalTTD { get; set; }
        public string CellTanggalBerakhir { get; set; }
        public string CellKodeAgreement { get; set; }
        public string CellKodeAgreementRelation { get; set; }
        public string CellAmandementDate{ get; set; }
        public string CellTrafficLight { get; set; }
        #endregion
    }
}
