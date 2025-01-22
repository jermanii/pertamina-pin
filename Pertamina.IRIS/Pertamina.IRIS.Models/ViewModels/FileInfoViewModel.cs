using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class FileInfoViewModel: ErrorBaseModel
    {
        public string FileName {  get; set; }
        public byte[] FileLength { get; set; }
        public string FileType {  get; set; }
    }
}
