using Pertamina.IRIS.Models.ViewModels;
using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IPowerPointService
    {
        void AddTextBeforeChart(ISlide slide, ITable table, int rowIndex, int columnIndex, string text, bool isBold = false);
        void AddChartAgreement(ISlide slide, ITable table, int rowIndex, int columnIndex, double column1Width, double column1Height, DownloadPresentationChartAgreementViewModel data, string title = "", string bgcolor = "pink");
        void AddChartWaterFall(ISlide slide, ITable table, int rowIndex, int columnIndex, double column1Width, double column1Height, List<DownloadPresentationChartTemplate2ViewModel> data, string title = "", string bgcolor = "pink");
        void AddChartStrategic(ISlide slide, ITable table, int rowIndex, int columnIndex, DownloadPresentationChartStrategicViewModel data);
        void AddImageFooter(ISlide slide, ITable table, int rowIndex, int columnIndex, string imagePath);
        void AddImageToCell(ISlide slide, ITable table, int rowIndex, int columnIndex, string imagePath);
        void AddChartAgreementDescription(ISlide slide, DownloadPresentationChartAgreementViewModel data, int rowIndex, int columnIndex, double column1Width, double column1Height, string freeText);
        void AddContentListAfterChart2(ISlide slide, ITable table, int rowIndex, int columnIndex, string[] content);
        void AddChartCountryPartner(ISlide slide, ITable table, int rowIndex, int columnIndex, double column1Width, double column1Height, List<DownloadPresentationChartTemplate2ViewModel> data);
        void AddChartStatusKegiatanDescription(ISlide slide, string[] freeText, int rowIndex, int columnIndex, double column1Width, double column1Height);
    }
}
