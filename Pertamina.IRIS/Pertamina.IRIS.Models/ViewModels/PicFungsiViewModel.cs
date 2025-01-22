using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class PicFungsiViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? FungsiId { get; set; }
        public string PicName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? ActiveStatus { get; set; }

        [NotMapped]
        public string NameFungsi { get; set; }
        [NotMapped]
        public string NameEntitasPertamina { get; set; }
        [NotMapped]
        public Guid? IdEntitasPertamina { get; set; }
    }
}
