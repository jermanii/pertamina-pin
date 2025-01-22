using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class FungsiViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public string FungsiName { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public string NameEntitasPertamina { get; set; }
    }
}
