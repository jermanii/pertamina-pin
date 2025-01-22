using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ExistingFootprintViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid HubId { get; set; }
        public Guid? NegaraMitraId { get; set; }

        [NotMapped]
        public Guid HubHeadId { get; set; }
        public int? Year { get; set; }

        public List<string> Partners{ get; set; }
        public List<string> Locations { get; set; }
        public string HubName { get; set; }
        public string HubHeadName { get; set; }
        public string PartnerName { get; set; }
        #region pic
        public Guid? PicFungsiId { get; set; }
        public Guid? PicFungsiLeadName { get; set; }
        public Guid? PicFungsiMemberNe { get; set; }
        public Guid? PicLevelId { get; set; }
        public Guid? PicLevelMemberId { get; set; }
        public Guid? PicLevelName { get; set; }
        public Guid? PicLevelNameMember { get; set; }
        public Guid? CreatePicLevelNameMember { get; set; }

        #endregion

        #region operatingMetrics
        public decimal? Revenue { get; set; }
        public decimal? TotalAsset { get; set; }
        public decimal? Ebitda { get; set; }
        #endregion

        #region Grid
        public Guid GridId { get; set; }
        public decimal? GridRevenue { get; set; }
        public string GridRevenueToString { get; set; }
        public decimal? GridTotalAsset { get; set; }
        public string GridTotalAssetToString { get; set; }
        public decimal? GridEbitda { get; set; }
        public string GridEbitdaToString { get; set; }
        public int? GridYear { get; set; }
        public string GridYearToString { get; set; }
        public string GridHubName { get; set; }
        public string GridHubHeadName { get; set; }
        public string GridEntitas { get; set; }
        public string GridNegaraMitra { get; set; }
        public string GridPicFungsiName { get; set; }
        public string GridPicLevelName { get; set; }
        public string GridProjectType { get; set; }
        public string GridProjectTypeDescription { get; set; }

        #endregion

        #region Cells
        public decimal? CellRevenue { get; set; }
        public decimal? CellTotalAsset { get; set; }
        public decimal? CellEbitda { get; set; }
        public int? CellYear { get; set; }
        public string CellHubName { get; set; }
        public string CellHubHeadName { get; set; }
        public string CellHubHeadId { get; set; }
        public string CellEntitas { get; set; }
        public string CellEntitasId { get; set; }
        public string CellNegaraMitra { get; set; }
        public string CellNegaraMitraId { get; set; }
        public string CellPicFungsiName { get; set; }
        public string CellPicFungsiId { get; set; }
        public string CellPicLevelName { get; set; }
        public string CellProjectType { get; set; }
        public string CellProjectTypeId { get; set; }
        public string CellProjectTypeDescription { get; set; }

        #endregion 
        
        #region Decode
        public string RevenueMinDecode { get; set; }
        public string RevenueMaxDecode { get; set; }
        public string TotalAssetMinDecode { get; set; }
        public string TotalAssetMaxDecode { get; set; }
        public string EbitdaMinDecode { get; set; }
        public string EbitdaMaxDecode { get; set; }
        public decimal? RevenueMin { get; set; }
        public decimal? RevenueMax { get; set; }
        public decimal? TotalAssetMin { get; set; }
        public decimal? TotalAssetMax { get; set; }
        public decimal? EbitdaMin { get; set; }
        public decimal? EbitdaMax { get; set; }
        public string YearDecode { get; set; }
        public string HubHeadIdDecode { get; set; }
        public string EntitasIdDecode { get; set; }
        public string NegaraMitraIdDecode { get; set; }
        public string ProjectTypeIdDecode { get; set; }
        public string PartnerCountryIdDecode { get; set; }
        public string DecodedString { get; set; }

        #endregion

        public ExistingFootprintsPICViewModel CreatePicFungsi { get; set; }
        public ExistingFootprintsPICViewModel PicFungsi { get; set; }
        public List<ExistingFootprintsPICViewModel> PicFungsis { get; set; }
        public ExistingFootprintsHubHeadViewModel Head { get; set; }
        public List<ExistingFootprintsHubHeadViewModel> Heads { get; set; }
        public ExistingFootprintsOperatingMetricViewModel OperatingMetrics { get; set; }
        public List<ExistingFootprintsOperatingMetricViewModel> OperatingMetricss { get; set; }
        public ExistingFootprintsPartnersViewModel Partner { get; set; }
        public ExistingFootprintsPartnersViewModel CreatePartner { get; set; }
        public List<ExistingFootprintsPartnersViewModel> Partnerss { get; set; }
    }
}
