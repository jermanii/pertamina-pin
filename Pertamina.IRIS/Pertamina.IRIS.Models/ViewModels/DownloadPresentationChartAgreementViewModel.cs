using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class DownloadPresentationChartAgreementViewModel
    {
        public string ShortNameSecret { get; set; }
        public string ShortNameFirst { get; set; }
        public string ShortNameIsNext { get; set; }
        public string ShortNameIsValue { get; set; }
        public string ShortNameIsOther { get; set; }
        public string ShortNameStatusBerlaku { get; set; }
        public int CountIsSecret { get; set; }
        public int CountIsFirst { get; set; }
        public int CountIsNext { get; set; }
        public int CountIsValue { get; set; }
        public int CountIsOther { get; set; }
        public int CountStatusBerlaku { get; set; }
        public int CountLNG { get; set; }
    }
}
