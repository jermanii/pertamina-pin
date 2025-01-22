using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Interfaces
{
    public interface IInternationalBusinessIntellegenceService
    {
        InternationalBusinessIntelligenceViewModel Save(InternationalBusinessIntelligenceViewModel model, string userName);
        InternationalBusinessIntelligenceCategoryViewModel BindDataIBI(string userName);
        List<InternationalBusinessIntelligenceViewModel> FilterDataIBI(InternationalBusinessIntelligenceViewModel model);
        List<InternationalBusinessIntelligenceViewModel> SearchDataIBI(InternationalBusinessIntelligenceViewModel model);
        List<InternationalBusinessIntelligenceViewModel> GetIbiAll();
        List<NegaraMitraInformationViewModel> GetNegaraMitraInformation();
        Task<SequenceCounterViewModel> GetFormatName(string featureName);
        Task<string> GetPath(string featureName);
        Task<InternationalBusinessIntelligenceViewModel> GetIbiDocument(Guid? guid);
        List<InternationalBusinessIntelligenceMyListViewModel> GetUserBookmarks(string username);
        Task<InternationalBusinessIntelligenceMyListViewModel> GetUserBookmarkById(Guid? guid, string username);
        void AddBookmark(Guid? guid, string userName);
        void UpdateBookmark(Guid? guid, string userName);
        Task<InternationalBusinessIntelligenceViewModel> DeleteIBI(Guid? guid, string userName = null);
        InternationalBusinessIntelligenceViewModel Update(InternationalBusinessIntelligenceViewModel model, string userName);
        List<object> GetFilesAsJson(Guid guid);
        List<InternationalBusinessIntelligenceDocumentViewModel> GetDocuments(Guid guid);
        Task<int?> GetFormatNameById(Guid guid);
    }
}
