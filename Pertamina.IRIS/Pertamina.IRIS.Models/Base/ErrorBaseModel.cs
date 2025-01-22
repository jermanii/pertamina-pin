using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.Base
{
    public class ErrorBaseModel
    {
        [NotMapped]
        public bool IsError { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }
    }
}
