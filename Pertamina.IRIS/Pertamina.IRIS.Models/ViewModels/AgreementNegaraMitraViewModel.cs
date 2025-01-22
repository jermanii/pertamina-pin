using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class AgreementNegaraMitraViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? AgreementId { get; set; }
        public Guid? NegaraMitraId { get; set; }
        public string NamaNegara { get; set; }
        public Guid? KawasanMitraId { get; set; }
        public string NamaKawasan { get; set; }

        public Guid? StatusBerlakuId { get; set; }
        public virtual AgreementViewModel Agreement { get; set; }
        public virtual NegaraMitraViewModel NegaraMitra { get; set; }
    }
}
