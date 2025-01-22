using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IDownloadPresentationRepository
    {
        DownloadPresentationChartAgreementViewModel GetDataChartAgreement();
        DownloadPresentationChartStrategicViewModel GetDataChartStrategic();
        List<DownloadPresentationChartAgreementDescriptionViewModel> GetDataChartAgreementDescription();
        List<DownloadPresentationChartTemplate2ViewModel> GetDataChartCountryPartner();
        List<DownloadPresentationChartTemplate2ViewModel> GetDataChartBusinessStream();
        List<DownloadPresentationChartTemplate2ViewModel> GetDataChartAgreementHolder();
        List<DownloadPresentationChartTemplate2ViewModel> GetDataChartDiscussion();
    }
}
