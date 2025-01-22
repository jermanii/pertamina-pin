using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class OpportunityViewModel : ErrorBaseModel
    {
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

        
        public Guid? StreamBusinessId { get; set; }
        public string EntitasPertaminaName { get; set; }
        public string StrSearch { get; set; }
        public string HubHeadName { get; set; }

        public string PotentialRevenuePerYearToString { get; set; }
        public string CapexToString { get; set; }
        public bool ExistsFlag { get; set; }
        public string UpdatedWording { get; set; }
        public string LastUpdate { get; set; }
        public string PICLead { get; set; }
        public Guid? PICLevelLeadId { get; set; }
        public Guid? PICLevelMemberId { get; set; }
        public List<string> PICName { get; set; }

        public List<Guid?> NegaraMitraId { get; set; }
        public List<Guid?> EntitasPertaminaId { get; set; }
        public List<Guid?> PicFungsiId { get; set; }
        public List<Guid?> KesiapanProyekId { get; set; }
        public List<Guid?> TargetMitraId { get; set; }
        public string PartnerName { get; set; }
        public List<string> Partners { get; set; }
        public List<string> LokasiProyek { get; set; }

        public  OpportunityEntitasPertaminaViewModel OpEntitasPertamina { get; set; }
        public List<OpportunityEntitasPertaminaViewModel> OpEntitasPertaminas { get; set; }
        public  OpportunityNegaraMitraViewModel OpNegaraMitra { get; set; }
        public  NegaraMitraViewModel NegaraMitra { get; set; }
        public  OpportunityTargetMitraViewModel OpTargetMitra { get; set; }
        public OpportunityPicFungsiViewModel OpPicFungsi { get; set; }
        public List<OpportunityPicFungsiViewModel> OpPicFungsis { get; set; }
        public List<PicFungsiViewModel> PicFungsis { get; set; }
        public PicFungsiViewModel PicFungsi { get; set; }
        public OpportunityKesiapanProyekViewModel OpKesiapanProyek { get; set; }
        public OpportunityStreamBusinessViewModel OpStreamBusiness { get; set; }
        public KawasanMitraViewModel KawasanMitra { get; set;}
        public List<OpportunityPartnerViewModel> OpPartners { get; set; }
        public OpportunityPartnerViewModel OpPartner { get; set; }
        public List<OpportunityLokasiProyekViewModel> OpLokasiProyeks { get; set; }
        public OpportunityLokasiProyekViewModel OpLokasiProyek { get; set; }
        public HubHeadViewModel HubHead { get; set; }

        #region Export
        public string CellNumber { get; set; }
        public string CellNamaProyek { get; set; }
        public string CellStreamBusiness { get; set; }
        public string CellEntitasPertamina { get; set; }
        public string CellHsh { get; set; }
        public string CellFungsiPic { get; set; }
        public string CellFungsiPicMember { get; set; }
        public string CellExistingPartner { get; set; }
        public string CellTargetMitra { get; set; }
        public string CellKawasanProyek { get; set; }
        public string CellNegaraMitra { get; set; }
        public string CellLokasiProyekNegara { get; set; }
        public string CellLokasiProyekProvinsi { get; set; }
        public string CellProjectProfile { get; set; }
        public string CellDeliverables { get; set; }
        public string CellNilaiProyek { get; set; }
        public string CellTimeline { get; set; }
        public string CellProgress { get; set; }
        public string CellKesiapanProyek { get; set; }
        public string CellCatatanTambahan { get; set; }
        public string CellTindakLanjut { get; set; }
        public string CellHshId { get; set; }
        public string CellCapex { get; set; }
        public string CellPotentialRevenue { get; set; }
        public string CellTypeOfPartnerNeeded { get; set; }
        public string CellNegaraMitraId { get; set; }
        public string CellStreamBusinessId { get; set; }
        public string CellEntitasPertaminaId { get; set; }
        public string CellCreateDate { get; set; }
        #endregion

        #region Grid
        // Count
        public List<HshViewModel> CountOpportunityHolder { get; set; }
        public int? CountOpportunity { get; set; }
        public int? CountNegaraMitra { get; set; }

        // DDL
        public string DdlOpportunityHolderId { get; set; }
        public string DdlNegaraMitraId { get; set; }
        public string DdlStreamBusinessId { get; set; }
        public string DdlEntitasPertaminaId { get; set; }

        // Date Range
        public string RangeCreateDate { get; set; }

        // Row
        public bool? RowStatus { get; set; }
        public string RowNamaProyek { get; set; }
        public string RowPartner { get; set; }
        public string RowEntitasPertamina { get; set; }
        public string RowStreamBusiness { get; set; }
        public string RowNegaraMitra { get; set; }
        public string RowKawasanMitra { get; set; }
        public string RowKesiapanProyek { get; set; }

        // Decode
        public string EntitasPertaminaDecode { get; set; }
        public string StreamBusinessDecode { get; set; }
        public string NegaraMitraDecode { get; set; }
        public string OpportunityHolderDecode { get; set; }
        public string RangeCreateDateDecode { get; set; }

        // Encode
        public string EntitasPertaminaEncode { get; set; }
        public string StreamBusinessEncode { get; set; }
        public string NegaraMitraEncode { get; set; }
        public string OpportunityHolderEncode { get; set; }
        public string RangeCreateDateEncode { get; set; }
        public string IsDraftEncode { get; set; }   

        // Query
        public string QueryNamaProyek { get; set; }
        public Guid? QueryHshId { get; set; }
        public Guid? QueryEntitasPertaminaId { get; set; }
        public string QueryEntitasPertaminaName { get; set; }
        public Guid? QueryNegaraMitraId { get; set; }
        public string QueryNamaNegara { get; set; }
        public Guid? QueryKawasanMitraId { get; set; }
        public string QueryNamaKawasan { get; set; }
        public Guid? QueryStreamBusinessId { get; set; }
        public string QueryStreamBusinessName { get; set; }

        #endregion

        #region Read
        public string ReadNamaProyek { get; set; }
        public List<string> ReadPartner { get; set; }
        public List<PicFungsiViewModel> ReadPicFungsi { get; set; }
        public PicFungsiViewModel ReadPicFungsiLead { get; set; }
        public List<EntitasPertaminaViewModel> ReadEntitasPertamina { get; set; }
        public List<NegaraMitraViewModel> ReadNegaraMitra { get; set; }
        public List<string> ReadLokasiProyekProvinsi { get; set; }
        public string ReadNilaiProyek { get; set; }
        public string ReadTimeline { get; set; }
        public List<StreamBusinessViewModel> ReadStreamBusiness { get; set; }
        public string ReadProjectProfile { get; set; }
        public string ReadDeliverables { get; set; }
        public string ReadProgress { get; set; }
        public List<KesiapanProyekViewModel> ReadKesiapanProyek { get; set; }
        public List<TargetMitraViewModel> ReadTargetMitra { get; set; }
        public string ReadCatatanTambahan { get; set; }
        public string ReadTindakLanjut { get; set; }
        #endregion
    }
}
