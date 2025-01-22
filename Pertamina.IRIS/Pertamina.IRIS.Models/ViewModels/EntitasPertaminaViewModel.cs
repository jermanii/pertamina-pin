using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class EntitasPertaminaViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? HshId { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public int? OrderSeq { get; set; }
        public bool? ActiveStatus { get; set; }


        [NotMapped]
        public string NameHsh { get; set; }
    }
}