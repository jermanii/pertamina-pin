using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class DownloadPresentationViewModel
    {
        public string exportType { get; set; }
        public string template { get; set; }
        public string titleTemplate2 { get; set; }
        public string[] freeTextTemplate2 { get; set; }
        public string freeTextTemplate1 { get; set; } 
        public DownloadPresentationChartAgreementViewModel chartAgreement { get; set; }
        public DownloadPresentationChartStrategicViewModel chartStrategic { get; set; }
    }
} 
