using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class PotentialInitiativeHighPriorityViewModel: ErrorBaseModel
    {
        public Guid Id { get; set; }
        public Guid InitiativeId { get; set; }
        public string JudulInisiasi { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? KementrianLembagaId { get; set; }
        public string KementrianLembagaName { get; set; }
        public string KementrianLembagaDescription { get; set; }
        public DateTime? MilestoneTargetDate { get; set; }
        public DateTime? MilestoneTargetDefinitive { get; set; }
        public decimal? RevenuePerYear { get; set; }
        public decimal? Capex { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string NegaraMitraName { get; set; }
        public string Flag { get; set; }
        public bool ExistsFlag { get; set; }
        public string PathFlag { get; set; }
        public Guid? HubId { get; set; }
        public string HubName { get; set; }
        public Guid? HubHeadId { get; set; }
        public string HubHeadName { get; set; }
        public Guid? PicFungsiId { get; set; }
        public Guid? PicFungsiLeadId { get; set; }
        public string PicFungsiName { get; set; }
        public string PicFungsiLeadName { get; set; }
        public string PicFungsiLeadEmail { get; set; }
        public string PicFungsiLeadPhone { get; set; }
        public Guid? InitiativeStatusId { get; set; }
        public string InitiativeStatus { get; set; }
        public string InitiativeStatusColorName { get; set; }
        public string UpdatedWording { get; set; }
        public string PartnerName { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public string EntitasPertaminaName { get; set; }
        public string CatatanTambahan { get; set; }
        [NotMapped]
        public List<PicFungsiViewModel> PicFungsiMembers { get; set; }

    }
}
