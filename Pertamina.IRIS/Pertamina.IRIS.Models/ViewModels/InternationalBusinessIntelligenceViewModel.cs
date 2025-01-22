using Pertamina.IRIS.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pertamina.IRIS.Models.ViewModels
{
    public class InternationalBusinessIntelligenceViewModel : ErrorBaseModel
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? TypeStudyId { get; set; }
        public DateTime? ResearchDate { get; set; }
        public string Title { get; set; }
        public Guid? EntitasPertaminaId { get; set; }
        public Guid? StreamBusinessId { get; set; }
        public Guid? ConfidentialityId { get; set; }
        public string Description { get; set; }
        public string SearchBar { get; set; }
        public string ConfidentialityName { get; set; }
        public string ConfidentialityColor { get; set; }
        public string EntitasPertaminaName { get; set; }
        public double DocumentsFileSize { get; set; }
        public string JoinedRelatedCountries { get; set; }
        public string JoinedRelatedCountriesId { get; set; }
        public string JoinedAuthorsName { get; set; }
        public bool IsBookmark { get; set; }

        public bool? IsDraft { get; set; }
        public List<Guid?> CountriesCoveredSelected { get; set; }
        public List<Guid?> CountriesCoveredSelectedMap { get; set; }
        public InternationalBusinessIntelligenceAuthorViewModel Author { get; set; }
        public List<InternationalBusinessIntelligenceAuthorViewModel> Authors { get; set; }
        public List<InternationalBusinessIntelligenceCountryViewModel> RelatedCountry { get; set; }
        public InternationalBusinessIntelligenceDocumentViewModel Document { get; set; }
        public List<InternationalBusinessIntelligenceDocumentViewModel> Documents { get; set; }
        public InternationalBusinessIntelligenceMyListViewModel Bookmark {  get; set; }
    }
}
