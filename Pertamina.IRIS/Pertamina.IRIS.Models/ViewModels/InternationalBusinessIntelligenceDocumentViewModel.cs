using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InternationalBusinessIntelligenceDocumentViewModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? InternationalBusinessIntelligenceId { get; set; }
        public string FileNameSystem { get; set; }
        public string FileNameUser { get; set; }
        [NotMapped]
        public double FileSize { get; set; }
    }
}
