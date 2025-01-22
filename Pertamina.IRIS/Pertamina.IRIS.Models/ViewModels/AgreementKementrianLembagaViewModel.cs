using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class AgreementKementrianLembagaViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? AgreementId { get; set; }
        public Guid? KementrianLembagaId { get; set; }
        public bool? DeletedStatus { get; set; }
        [NotMapped]
        public string LembagaName { get; set; }
        public virtual AgreementViewModel Agreement { get; set; }
        public virtual KementrianLembagaViewModel KementrianLembaga { get; set; }

    }
}
