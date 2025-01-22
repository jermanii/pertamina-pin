using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class LinkPowerBiViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Menu { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Url { get; set; }

        public static implicit operator string(LinkPowerBiViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
