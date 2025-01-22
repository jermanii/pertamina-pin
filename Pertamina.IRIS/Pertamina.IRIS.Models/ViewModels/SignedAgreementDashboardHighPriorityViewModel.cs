using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class SignedAgreementDashboardHighPriorityViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid AgreementId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public string CountryAcronyms { get; set; }
        public string JudulPerjanjian { get; set; }
        public string NamaPerjanjian { get; set; }
        public string JenisPerjanjian { get; set; }
        public bool HasLembaga { get; set; }
        public string LembagaName { get; set; }
        public string LembagaDesc { get; set; }
        public decimal? PotentialRevenuePerYear { get; set; }
        public string PotentialRevenuePerYearFormat { get; set; }
        public decimal? Valuation { get; set; }
        public string ValuationFormat { get; set; }
        public decimal? Capex { get; set; }
        public string CapexFormat { get; set; }
        public string CountryFlag { get; set; }
        public string NamaNegara { get; set; }
        public string CountryRegion { get; set; }
        public string PartnerName { get; set; }
        public string LokasiProyek { get; set; }
        public string EntitasPertamina { get; set; }
        public string HubHeadName { get; set; }
        public string StatusBerlakuName { get; set; }
        public Guid? StatusBerlakuId { get; set; }
        public string ValidStatusColorName { get; set; }
        public string ValidStatusColorHexa { get; set; }
        public string DiscussionStatus { get; set; }
        public string DiscussionStatusColorName { get; set; }
        public string DiscussionStatusColorHexa { get; set; }
        public string KlasifikasiKendala { get; set; }
        public string DeskripsiKendala { get; set; }
        public string KodeAgreement { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string StreamBusinessName { get; set; }
        public string StreamBusinessCollected { get; set; }
        public string PicFungsiName { get; set; }
        public string PicFungsiEmail { get; set; }
        public string PicFungsiPhone { get; set; }
        public string TargetDateAwal { get; set; }
        public string TargetDateAkhir { get; set; }
        public string UpdatedWording { get; set; }
        public int? Sequence { get; set; }
        public bool ExistsFlag { get; set; }
        public bool? IsGtg { get; set; }
        public bool? IsStrategic { get; set; }
        public bool? IsGreenBusiness { get; set; }
        public DateTime? TanggalTtd { get; set; }
        public DateTime? TanggalBerakhir { get; set; }
        public List<SignedAgreementPartnerViewModel> Partners { get; set; }
        public List<SignedAgreementStreamBusinessViewModel> Streams { get; set; }
        public List<SignedAgreementEntitasPertaminaViewModel> Entitas { get; set; }
        public List<SignedAgreementLokasiProyekViewModel> ProjectLocations { get; set; }
    }
}