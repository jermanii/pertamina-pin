using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pertamina.IRIS.App.ActionFilters;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace Pertamina.IRIS.App.Controllers
{
    [SessionTimeout]
    [Authorize]
    public class InternationalBusinessIntelligenceController : WebControllerBase
    {
        private readonly IInternationalBusinessIntellegenceService _service;
        public InternationalBusinessIntelligenceController(IIdamanService idamanService, IInternationalBusinessIntellegenceService service) : base(idamanService)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            InternationalBusinessIntelligenceCategoryViewModel model = new InternationalBusinessIntelligenceCategoryViewModel();

            try
            {
                model = _service.BindDataIBI(UserName);
                return View(model);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return View(model);
            }
        }
        public IActionResult Create()
        {
            try
            {
                InternationalBusinessIntelligenceViewModel model = new InternationalBusinessIntelligenceViewModel();

                return View(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return View("Error", error);
            }
        }
        [HttpPost]
        public IActionResult Add(InternationalBusinessIntelligenceViewModel model)
        {
            try
            {
                model = _service.Save(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpPost]
        [Route("UploadDocument")]
        public async Task<IActionResult> UploadDocument(List<IFormFile> files, Guid? guid = null)
        {
            List<InternationalBusinessIntelligenceDocumentViewModel> documents = new List<InternationalBusinessIntelligenceDocumentViewModel>();

            List<InternationalBusinessIntelligenceDocumentViewModel> existingDocuments = new List<InternationalBusinessIntelligenceDocumentViewModel>();

            if (guid.HasValue)
            {
                existingDocuments = _service.GetDocuments(guid.Value);
            }

            foreach (var file in files)
            {
                InternationalBusinessIntelligenceDocumentViewModel objDoc = new InternationalBusinessIntelligenceDocumentViewModel();
                if (!existingDocuments.Any(x => string.Equals(file.FileName, x.FileNameUser, StringComparison.OrdinalIgnoreCase)))
                {
                    if (file.Length > 0)
                    {
                        string pathDb = await _service.GetPath("IBI");

                        SequenceCounterViewModel formatName = await _service.GetFormatName("IBI");

                        string tempPath = pathDb + Convert.ToString(formatName.Year);

                        string pathFolder = tempPath.Replace(" ", "");
                        if (!Directory.Exists(pathFolder))
                        {
                            Directory.CreateDirectory(pathFolder);
                        }

                        string fileNameSystem = EnumBaseModel.FormatUploadFileName + "_" + formatName.Sequence;

                        using (FileStream stream = new FileStream(Path.Combine(pathFolder, fileNameSystem + Path.GetExtension(file.FileName)), FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        objDoc.FileNameSystem = fileNameSystem;
                        objDoc.FileNameUser = file.FileName;
                        documents.Add(objDoc);
                    }
                }
                else
                {
                    var doc = existingDocuments.Where(x => string.Equals(file.FileName, x.FileNameUser, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    if(doc != null)
                    documents.Add(doc);
                }
            }

            return Json(documents);
        }
        
        [HttpPost]
        public IActionResult FilterIBI(InternationalBusinessIntelligenceViewModel model)
        {
            List<InternationalBusinessIntelligenceViewModel> response = new List<InternationalBusinessIntelligenceViewModel>();
            response = _service.FilterDataIBI(model);

            return PartialView("ListDocumentsFilterPartial", response);
        }

        [HttpPost]
        public IActionResult SearchIBI(InternationalBusinessIntelligenceViewModel model)
        {
            List<InternationalBusinessIntelligenceViewModel> response = new List<InternationalBusinessIntelligenceViewModel>();
            response = _service.SearchDataIBI(model);

            return PartialView("ListDocumentsFilterPartial", response);
        }
        [HttpPost]
        public IActionResult ResetFilterIBI()
        {
            InternationalBusinessIntelligenceCategoryViewModel model = new InternationalBusinessIntelligenceCategoryViewModel();
            model = _service.BindDataIBI(UserName);
            return PartialView("ListDocumentsPartial", model);
        }

        [HttpPost]
        public IActionResult GetAllIBI()
        {
            List<InternationalBusinessIntelligenceViewModel> model = new List<InternationalBusinessIntelligenceViewModel>();
            model = _service.GetIbiAll();
            return PartialView("ListDocumentsAllPartial", model);
        }

        public async Task<IActionResult> DetailDocuments(Guid guid)
        {
            try
            {
                InternationalBusinessIntelligenceViewModel model = new InternationalBusinessIntelligenceViewModel();

                model = await _service.GetIbiDocument(guid);

                var bookmark = await _service.GetUserBookmarkById(guid, UserName);

                if (bookmark == null)
                {
                    model.IsBookmark = false;
                }
                else
                {
                    model.IsBookmark = true;
                }

                model.Bookmark = bookmark;

                return View(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> BookmarkIBI(Guid guid)
        {
            try
            {
                var model = await _service.GetUserBookmarkById(guid, UserName);

                if (model == null)
                {
                    _service.AddBookmark(guid, UserName);
                }
                else
                {
                    _service.UpdateBookmark(guid, UserName);
                }

                return Json(new { guid = guid });
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return View("Error", error);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> DeleteIBI(Guid guid)
        {
            try
            {
                InternationalBusinessIntelligenceViewModel model = new InternationalBusinessIntelligenceViewModel();

                model = await _service.DeleteIBI(guid);

                var response = false;

                if (model != null)
                {
                    if (model.DeletedDate != null)
                    {
                        response = true;
                    }
                }

                return Json(new { isSuccess = response });
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel();
                error.ErrorMessage = err.Message + err.StackTrace;
                return View("Error", error);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid guid)
        {
            try
            {
                InternationalBusinessIntelligenceViewModel model = new InternationalBusinessIntelligenceViewModel();

                model = await _service.GetIbiDocument(guid);

                return View(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpPost]
        public IActionResult SubmitUpdate(InternationalBusinessIntelligenceViewModel model)
        {
            try
            {
                model = _service.Update(model, UserName);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpGet]
        public IActionResult GetFilesAsJson(Guid guid)
        {
            try
            {
                var model = _service.GetFilesAsJson(guid);

                return Json(model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }

        [HttpGet]
        public async Task<IActionResult> PreviewDocuments(Guid guid)
        {
            try
            {
                List<InternationalBusinessIntelligenceDocumentViewModel> model = new List<InternationalBusinessIntelligenceDocumentViewModel>();
                var data = await _service.GetIbiDocument(guid);

                model = data.Documents;

                return PartialView("PreviewDocumentsPartial", model);
            }
            catch (Exception err)
            {
                ErrorModel error = new ErrorModel(); error.ErrorMessage = err.Message + err.StackTrace; return Json(error);
            }
        }
    }
}
