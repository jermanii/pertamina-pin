using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class AgreementEntitasPertaminaViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? AgreementId { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public bool? DeletedStatus { get; set; }
        public virtual AgreementViewModel Agreement { get; set; }
        public virtual EntitasPertaminaViewModel EntitasPertamina { get; set; }
        [NotMapped]
        public Guid? HshId { get; set; }
        public string HshName { get; set; }
        public string CompanyName { get; set; }

        public Guid? NegaraMitraId { get; set; }
        public Guid? StatusBerlakuId { get; set; }

    }
}
