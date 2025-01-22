using Pertamina.IRIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Repositories.Interfaces
{
    public interface IInternationalBusinessIntellegenceRepository
    {
        void Save(InternationalBusinessIntelligenceViewModel model);
        void SaveCountryIBI(InternationalBusinessIntelligenceCountryViewModel model);
        void SaveAuthors(InternationalBusinessIntelligenceAuthorViewModel model);
        void SaveDocuments(InternationalBusinessIntelligenceDocumentViewModel model);
        List<InternationalBusinessIntelligenceAuthorViewModel> GetAuthors(Guid id);
        List<InternationalBusinessIntelligenceAuthorViewModel> SearchAuthors(string SearchBar);
        List<InternationalBusinessIntelligenceCountryViewModel> GetRelatedCountry(Guid id);
        List<InternationalBusinessIntelligenceCountryViewModel> GetIBIByRelatedCountry(List<Guid?> countrySelected);
        List<InternationalBusinessIntelligenceDocumentViewModel> GetDocuments(Guid id);
        List<InternationalBusinessIntelligenceViewModel> GetIbIFilterBy(InternationalBusinessIntelligenceViewModel model, List<Guid?> idIBI);
        List<InternationalBusinessIntelligenceViewModel> GetIbiSearchBar(InternationalBusinessIntelligenceViewModel model,
            List<Guid?> idIBI, List<Guid?> idIBICountries);
        List<InternationalBusinessIntelligenceViewModel> GetIbiAll();
        List<NegaraMitraInformationViewModel> GetNegaraMitraInformation();
        List<InternationalBusinessIntelligenceCountryViewModel> GetCountDocumentIBI(Guid? negaraMitraId);
        List<InternationalBusinessIntelligenceCountryViewModel> SearchIBIRelatedCountry(string SearchBar);
        List<InternationalBusinessIntelligenceViewModel> GetIbIByCategoryOwn(string userName);
        List<InternationalBusinessIntelligenceViewModel> GetIbIByCategoryRecenly();
        Task<SequenceCounterViewModel> GetSequence(string featureName);
        Task<string> GetPath(string featureName);
        Task<InternationalBusinessIntelligenceViewModel> GetIbiDocument(Guid? guid);
        List<InternationalBusinessIntelligenceMyListViewModel> GetUserBookmarks(string username);
        Task<InternationalBusinessIntelligenceMyListViewModel> GetUserBookmarkById(Guid? guid, string username);
        void AddBookmark(InternationalBusinessIntelligenceMyListViewModel model);
        void UpdateBookmark(InternationalBusinessIntelligenceMyListViewModel model);
        List<InternationalBusinessIntelligenceViewModel> GetIbIByCategoryMyList(string userName);
        Task<InternationalBusinessIntelligenceViewModel> DeleteIBI(Guid? guid, string userName = null);
        void Update(InternationalBusinessIntelligenceViewModel model);
        void DeleteCountryIBI(InternationalBusinessIntelligenceCountryViewModel model);
        void DeleteAuthorIBI(InternationalBusinessIntelligenceAuthorViewModel model);
        void DeleteDocumentIBI(InternationalBusinessIntelligenceDocumentViewModel model);
        Task<InternationalBusinessIntelligenceDocumentViewModel> GetSingleFile(Guid id);
        Task<InternationalBusinessIntelligenceViewModel> GetIbiById(Guid guid);
    }
}
