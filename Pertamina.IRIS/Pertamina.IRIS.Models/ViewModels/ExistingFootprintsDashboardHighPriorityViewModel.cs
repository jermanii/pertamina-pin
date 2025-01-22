using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class ExistingFootprintsDashboardHighPriorityViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid ExistingFootprintsId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public string CompanyName { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string NamaNegara { get; set; }
        public string CountryAcronyms { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? TotalAsset { get; set; }
        public decimal? Ebitda { get; set; }
        public int? Year { get; set; }
        public int? Sequence { get; set; }
        public string Flag { get; set; }
        public bool ExistsFlag { get; set; }
        public string PathFlag { get; set; }
        public Guid? HubId { get; set; }
        public Guid? HubHeadId { get; set; }
        public string HubHeadName { get; set; }
        public string HubName { get; set; }
        public Guid? PicFungsiId { get; set; }
        public string PicFungsiName { get; set; }
        public string PicFungsiPhone { get; set; }
        public string PicFungsiEmail { get; set; }
        public string UpdatedWording { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public string StreamBusinessName { get; set; }
        public string HubHeadEmail { get; set; }
        public string HubHeadContactNumber { get; set; }
        public List<ExistingFootprintsPartnersViewModel> Partners { get; set; }
    }
}