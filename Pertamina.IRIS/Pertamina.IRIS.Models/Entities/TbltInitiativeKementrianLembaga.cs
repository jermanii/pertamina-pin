﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Pertamina.IRIS.Models.Entities
{
    public partial class TbltInitiativeKementrianLembaga
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InitiativeId { get; set; }
        public Guid? KementrianLembagaId { get; set; }
        public bool? DeletedStatus { get; set; }

        public virtual TbltInitiative Initiative { get; set; }
        public virtual TblmKementrianLembaga KementrianLembaga { get; set; }
    }
}
