using System;
using System.Collections.Generic;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class HeaderMenuViewModel
    {
        public Guid? StreamBusinessId { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string ContentItem { get; set; }
        public List<NotificationBellViewModel> NotificationBell { get; set; }

        public Guid? StatusBerlakuId { get; set; }
        public Guid? DiscussionStatusId { get; set; }
        public Guid? AgreementHolderId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid? KawasanMitraId { get; set; }
        public Guid? JenisPerjanjianId { get; set; }
        public string RangeTanggalTtd { get; set; }
        public string RangeTanggalBerakhir { get; set; }
        public string RangeCreateDate { get; set; }
    }
}
