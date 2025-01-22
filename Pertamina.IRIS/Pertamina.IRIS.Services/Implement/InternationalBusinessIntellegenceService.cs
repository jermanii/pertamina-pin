using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using Pertamina.IRIS.Utility.MimeType.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class InternationalBusinessIntellegenceService : IInternationalBusinessIntellegenceService
    {
        private readonly IInternationalBusinessIntellegenceRepository _repository;
        private readonly ISequenceCounterRepository _sequenceCounterRepository;
        private readonly IMimeTypeUtility _mimeTypeUtility;
        private DateTime currentTime;
        public InternationalBusinessIntellegenceService(IInternationalBusinessIntellegenceRepository repository,
                                                        ISequenceCounterRepository sequenceCounterRepository,
                                                        IMimeTypeUtility mimeTypeUtility)
        {
            _repository = repository;
            _sequenceCounterRepository = sequenceCounterRepository;
            _mimeTypeUtility = mimeTypeUtility;
        }

        public InternationalBusinessIntelligenceViewModel Save(InternationalBusinessIntelligenceViewModel model, string userName)
        {
            try
            {
                model.IsError = false;
                DateTime now = DateTime.Now;
                model.Id = Guid.NewGuid();
                model.CreatedBy = userName;
                model.CreatedDate = now;
                model.IsDraft = false;
                //Save IBI
                _repository.Save(model);

                //Save IBI Countries
                this.SaveIBICountries(model);

                //Save IBI Authors
                this.SaveIBIAuthors(model);

                //Save IBI Documents
                this.SaveIBIDocuments(model);

            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.InnerException.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }

        private void SaveIBICountries(InternationalBusinessIntelligenceViewModel model)
        {
            foreach (var country in model.CountriesCoveredSelected)
            {
                InternationalBusinessIntelligenceCountryViewModel objCountry = new InternationalBusinessIntelligenceCountryViewModel();
                objCountry.Id = Guid.NewGuid();
                objCountry.CreatedBy = model.CreatedBy;
                objCountry.CreatedDate = model.CreatedDate;
                objCountry.InternationalBusinessIntelligenceId = model.Id;
                objCountry.NegaraMitraId = country;
                _repository.SaveCountryIBI(objCountry);
            }
        }
        private void SaveIBIAuthors(InternationalBusinessIntelligenceViewModel model)
        {
            foreach (var author in model.Authors)
            {
                if (!string.IsNullOrEmpty(author.Name))
                {
                    author.Id = Guid.NewGuid();
                    author.CreatedDate = model.CreatedDate;
                    author.CreatedBy = model.CreatedBy;
                    author.InternationalBusinessIntelligenceId = model.Id;

                    _repository.SaveAuthors(author);
                }
            }
        }
        private void SaveIBIDocuments(InternationalBusinessIntelligenceViewModel model)
        {
            foreach (var doc in model.Documents)
            {
                doc.Id = Guid.NewGuid();
                doc.CreatedDate = model.CreatedDate;
                doc.CreatedBy = model.CreatedBy;
                doc.InternationalBusinessIntelligenceId = model.Id;

                _repository.SaveDocuments(doc);
            }
        }
        private List<InternationalBusinessIntelligenceViewModel> GetIBICategoryOwn(string userName)
        {
            List<InternationalBusinessIntelligenceViewModel> dtIBI = new List<InternationalBusinessIntelligenceViewModel>();

            dtIBI = _repository.GetIbIByCategoryOwn(userName);
            foreach (var x in dtIBI)
            {
                x.Authors = _repository.GetAuthors(x.Id);
                x.RelatedCountry = _repository.GetRelatedCountry(x.Id);
                x.Documents = _repository.GetDocuments(x.Id);
            }

            return dtIBI;
        }
        private List<InternationalBusinessIntelligenceViewModel> GetIBICategoryRecenly()
        {
            List<InternationalBusinessIntelligenceViewModel> dtIBI = new List<InternationalBusinessIntelligenceViewModel>();

            dtIBI = _repository.GetIbIByCategoryRecenly();
            foreach (var x in dtIBI)
            {
                x.Authors = _repository.GetAuthors(x.Id);
                x.RelatedCountry = _repository.GetRelatedCountry(x.Id);
                x.Documents = _repository.GetDocuments(x.Id);
            }

            return dtIBI;
        }
        public InternationalBusinessIntelligenceCategoryViewModel BindDataIBI(string userName)
        {
            InternationalBusinessIntelligenceCategoryViewModel model = new InternationalBusinessIntelligenceCategoryViewModel();
            try
            {
                model.IBICategoryOwn = this.GetIBICategoryOwn(userName);
                model.IBICategoryRecently = this.GetIBICategoryRecenly();
                model.IBIMapView = this.GetNegaraMitraInformation();
                model.IBICategoryMyList = this.GetIBICategoryMyList(userName);
                model.IsError = false;
                return model;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return model;
            }
        }
        public List<InternationalBusinessIntelligenceViewModel> FilterDataIBI(InternationalBusinessIntelligenceViewModel model)
        {
            List<InternationalBusinessIntelligenceViewModel> result = new List<InternationalBusinessIntelligenceViewModel>();
            try
            {
                var idIBI = new List<Guid?>();
                if (model.CountriesCoveredSelected != null && model.CountriesCoveredSelected.Any())
                {
                    idIBI = _repository.GetIBIByRelatedCountry(model.CountriesCoveredSelected)
                        .Select(_ => _.InternationalBusinessIntelligenceId).Distinct().ToList();

                    if (idIBI.Count == 0)
                    {
                        //Set default for empty search
                        idIBI.Add(Guid.NewGuid());
                    }
                }
                if (model.CountriesCoveredSelectedMap != null && model.CountriesCoveredSelectedMap.Any())
                {
                    idIBI = model.CountriesCoveredSelectedMap;
                }

                result = _repository.GetIbIFilterBy(model, idIBI);
                foreach (var x in result)
                {
                    x.Authors = _repository.GetAuthors(x.Id);
                    x.RelatedCountry = _repository.GetRelatedCountry(x.Id);
                    x.Documents = _repository.GetDocuments(x.Id);
                }
                return result;
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
                model.IsError = true;
                return result;
            }
        }
        public List<InternationalBusinessIntelligenceViewModel> GetIbiAll()
        {
            List<InternationalBusinessIntelligenceViewModel> result = new List<InternationalBusinessIntelligenceViewModel>();

            result = _repository.GetIbiAll();
            foreach (var x in result)
            {
                x.Authors = _repository.GetAuthors(x.Id);
                x.RelatedCountry = _repository.GetRelatedCountry(x.Id);
                x.Documents = _repository.GetDocuments(x.Id);
            }

            return result;
        }
        public List<InternationalBusinessIntelligenceViewModel> SearchDataIBI(InternationalBusinessIntelligenceViewModel model)
        {
            List<InternationalBusinessIntelligenceViewModel> result = new List<InternationalBusinessIntelligenceViewModel>();

            var idIBI = new List<Guid?>();
            var idIBICountries = new List<Guid?>();
            var relatedCountries = _repository.SearchIBIRelatedCountry(model.SearchBar);
            var authors = _repository.SearchAuthors(model.SearchBar);

            if (model.CountriesCoveredSelected != null && model.CountriesCoveredSelected.Any())
            {
                idIBICountries = _repository.GetIBIByRelatedCountry(model.CountriesCoveredSelected)
                    .Select(_ => _.InternationalBusinessIntelligenceId).Distinct().ToList();

                if (idIBICountries.Count == 0)
                {
                    //Set default for empty search
                    idIBICountries.Add(Guid.NewGuid());
                }
            }
            if (model.CountriesCoveredSelectedMap != null && model.CountriesCoveredSelectedMap.Any())
            {
                idIBICountries = model.CountriesCoveredSelectedMap;
            }

            if (relatedCountries != null && relatedCountries.Any())
            {
                foreach (var c in relatedCountries)
                {
                    idIBI.Add(c.InternationalBusinessIntelligenceId);
                }
            }

            if (authors != null && authors.Any())
            {
                foreach (var a in authors)
                {
                    idIBI.Add(a.Id);
                }
            }

            result = _repository.GetIbiSearchBar(model, idIBI, idIBICountries);
            foreach (var x in result)
            {
                x.Authors = _repository.GetAuthors(x.Id);
                x.RelatedCountry = _repository.GetRelatedCountry(x.Id);
                x.Documents = _repository.GetDocuments(x.Id);
            }

            return result;
        }
        public List<NegaraMitraInformationViewModel> GetNegaraMitraInformation()
        {
            var model = new List<NegaraMitraInformationViewModel>();
            model = _repository.GetNegaraMitraInformation();
            foreach (var x in model)
            {
                var result = _repository.GetCountDocumentIBI(x.NegaraMitraId);
                var img = x.Flag.Replace("\\", "/");
                x.DocumentCount = result.Count();
                x.InternationalBusinessIntelligenceId = result.Select(_ => _.InternationalBusinessIntelligenceId).ToList();
                x.Flag = img;
            }

            return model;
        }
        public async Task<SequenceCounterViewModel> GetFormatName(string featureName)
        {
            SequenceCounterViewModel result = await _repository.GetSequence(featureName);

            return result;
        }
        public async Task<string> GetPath(string featureName)
        {
            string result = string.Empty;

            result = await _repository.GetPath(featureName);

            return result;
        }
        public async Task<InternationalBusinessIntelligenceViewModel> GetIbiDocument(Guid? guid)
        {
            InternationalBusinessIntelligenceViewModel result = new InternationalBusinessIntelligenceViewModel();

            result = await _repository.GetIbiDocument(guid);

            long totalSizeBytes = 0;
            double totalSizeMB = 1;

            foreach (var file in result.Documents)
            {
                string pathDb = await GetPath("IBI");

                int? formatName = await GetFormatNameById(guid.Value);

                string tempPath = pathDb + Convert.ToString(formatName);

                string pathFolder = tempPath.Replace(" ", "");
                pathFolder = pathFolder + "\\" + file.FileNameSystem + Path.GetExtension(file.FileNameUser);

                FileInfo fileInfo = new FileInfo(pathFolder);

                if (fileInfo.Exists)
                {
                    totalSizeBytes += fileInfo.Length;

                    totalSizeMB = Math.Round(totalSizeBytes / (1024.0 * 1024.0), 2);

                    file.FileSize = Math.Round((fileInfo.Length / 1024.0), 2);

                }
            }

            result.JoinedRelatedCountries = string.Join(", ", result.RelatedCountry.Select(x => x.NegaraMitraName));

            result.DocumentsFileSize = totalSizeMB;
            result.JoinedRelatedCountriesId = string.Join(",", result.RelatedCountry.Select(x => x.NegaraMitraId).ToList());
            result.JoinedAuthorsName = string.Join(",", result.Authors.Select(x => x.Name).ToList());

            return result;
        }
        public async Task<InternationalBusinessIntelligenceMyListViewModel> GetUserBookmarkById(Guid? guid, string userName)
        {
            InternationalBusinessIntelligenceMyListViewModel result = new InternationalBusinessIntelligenceMyListViewModel();

            result = await _repository.GetUserBookmarkById(guid, userName);

            return result;
        }
        public void AddBookmark(Guid? guid, string userName)
        {
            List<InternationalBusinessIntelligenceMyListViewModel> listBookmarks = new List<InternationalBusinessIntelligenceMyListViewModel>();

            InternationalBusinessIntelligenceMyListViewModel model = new InternationalBusinessIntelligenceMyListViewModel();

            listBookmarks = GetUserBookmarks(userName);

            model.Id = Guid.NewGuid();
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = userName;
            model.InternationalBusinessIntelligenceId = guid;
            model.Email = userName;

            if (listBookmarks.Count == 0)
            {
                model.Sequence = 1;
            }
            else
            {
                var currentMaxSequece = listBookmarks.Max(x => x.Sequence);
                model.Sequence = currentMaxSequece + 1;
            }

            _repository.AddBookmark(model);
        }
        public void UpdateBookmark(Guid? guid, string userName)
        {
            InternationalBusinessIntelligenceMyListViewModel model = new InternationalBusinessIntelligenceMyListViewModel();

            model = _repository.GetUserBookmarkById(guid, userName).Result;

            _repository.UpdateBookmark(model);

        }
        public List<InternationalBusinessIntelligenceMyListViewModel> GetUserBookmarks(string userName)
        {
            List<InternationalBusinessIntelligenceMyListViewModel> result = new List<InternationalBusinessIntelligenceMyListViewModel>();
            result = _repository.GetUserBookmarks(userName);

            return result;
        }
        private List<InternationalBusinessIntelligenceViewModel> GetIBICategoryMyList(string userName)
        {
            List<InternationalBusinessIntelligenceViewModel> dtIBI = new List<InternationalBusinessIntelligenceViewModel>();

            dtIBI = _repository.GetIbIByCategoryMyList(userName);
            foreach (var x in dtIBI)
            {
                x.Authors = _repository.GetAuthors(x.Id);
                x.RelatedCountry = _repository.GetRelatedCountry(x.Id);
                x.Documents = _repository.GetDocuments(x.Id);
            }

            return dtIBI;
        }
        public async Task<InternationalBusinessIntelligenceViewModel> DeleteIBI(Guid? guid, string userName = null)
        {
            InternationalBusinessIntelligenceViewModel model = new InternationalBusinessIntelligenceViewModel();

            model = await _repository.DeleteIBI(guid);

            return model;

        }
        public InternationalBusinessIntelligenceViewModel Update(InternationalBusinessIntelligenceViewModel model, string userName)
        {
            try
            {
                model.IsError = false;
                currentTime = DateTime.Now;
                model.Id = model.Id;
                model.UpdatedBy = userName;
                model.UpdatedDate = currentTime;

                _repository.Update(model);

                var existingCountries = _repository.GetRelatedCountry(model.Id);
                var missingCountries = existingCountries.Where(x => !model.CountriesCoveredSelected.Contains(x.NegaraMitraId)).ToList();
                var newCountries = model.CountriesCoveredSelected.Where(x => !existingCountries.Any(y => y.NegaraMitraId == x)).ToList();
                if (missingCountries.Count > 0)
                {
                    DeleteIBICountries(missingCountries);
                }
                if (newCountries.Count > 0)
                {
                    model.CountriesCoveredSelected = newCountries;
                    SaveIBICountries(model);
                }

                var existingAuthors = _repository.GetAuthors(model.Id);
                var missingAuthors = existingAuthors.Where(x => !model.Authors.Any(y => y.Id == x.Id)).ToList();
                var newAuthors = model.Authors.Where(x => x.Id == Guid.Empty).ToList();
                if (missingAuthors.Count > 0)
                {
                    DeleteIBIAuthors(missingAuthors);
                }
                if (newAuthors.Count > 0)
                {
                    model.Authors = newAuthors;
                    SaveIBIAuthors(model);
                }

                var existingDocuments = _repository.GetDocuments(model.Id);
                var missingDocuments = existingDocuments.Where(x => !model.Documents.Any(y => y.Id == x.Id)).ToList();
                var newDocuments = model.Documents.Where(x => x.Id == Guid.Empty).ToList();
                if (missingDocuments.Count > 0)
                {
                    DeleteIBIDocuments(missingDocuments);
                }
                if (newDocuments.Count > 0)
                {
                    model.Documents = newDocuments;
                    this.SaveIBIDocuments(model);
                }

            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.InnerException.Message;
                model.IsError = true;
                return model;
            }

            return model;
        }
        private void DeleteIBICountries(List<InternationalBusinessIntelligenceCountryViewModel> model)
        {
            foreach (var country in model)
            {
                InternationalBusinessIntelligenceCountryViewModel objCountry = new InternationalBusinessIntelligenceCountryViewModel();
                objCountry.Id = country.Id;
                objCountry.DeletedDate = currentTime;
                _repository.DeleteCountryIBI(objCountry);
            }
        }
        private void DeleteIBIAuthors(List<InternationalBusinessIntelligenceAuthorViewModel> model)
        {
            foreach (var author in model)
            {
                if (!string.IsNullOrEmpty(author.Name))
                {
                    author.Id = author.Id;
                    author.DeletedDate = currentTime;

                    _repository.DeleteAuthorIBI(author);
                }
            }
        }
        private void DeleteIBIDocuments(List<InternationalBusinessIntelligenceDocumentViewModel> model)
        {
            foreach (var doc in model)
            {
                doc.Id = doc.Id;
                doc.DeletedDate = currentTime;

                _repository.DeleteDocumentIBI(doc);
            }
        }
        public List<object> GetFilesAsJson(Guid guid)
        {
            var fileDetails = new List<object>();

            var documents = _repository.GetDocuments(guid);
            string pathDb = _repository.GetPath("IBI").Result;

            foreach (var item in documents)
            {

                string tempPath = pathDb + Convert.ToString(item.CreatedDate.Value.Year);
                string pathFolder = tempPath.Replace(" ", "");

                var fileName = item.FileNameUser;
                var fileBytes = System.IO.File.ReadAllBytes(pathFolder + "\\" + item.FileNameSystem + Path.GetExtension(item.FileNameUser));
                var mimeType = _mimeTypeUtility.GetMimeType(Path.GetExtension(item.FileNameUser));

                var base64Content = Convert.ToBase64String(fileBytes);

                fileDetails.Add(new
                {
                    FileName = fileName,
                    MimeType = mimeType,
                    Content = base64Content
                });
            }

            return fileDetails;
        }
        public List<InternationalBusinessIntelligenceDocumentViewModel> GetDocuments(Guid guid)
        {
            return _repository.GetDocuments(guid);
        }
        public async Task<int?> GetFormatNameById(Guid guid)
        {
            int? result = 0;

            InternationalBusinessIntelligenceViewModel model = await _repository.GetIbiById(guid);

            if (model != null)
            {
                result = model.CreatedDate.Value.Year;
            }

            return result;
        }
    }
}