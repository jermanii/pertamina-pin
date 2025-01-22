using AutoMapper;
using LinqKit;
using Microsoft.Net.Http.Headers;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.Style;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using Pertamina.IRIS.Utility.MimeType.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class ApplicationFileDownloadService : IApplicationFileDownloadService
    {
        IApplicationFileDownloadRepository _repository;
        IInternationalBusinessIntellegenceRepository _repositoryIBI;
        IInternationalBusinessIntellegenceService _serviceIBI;
        private readonly IMimeTypeUtility _mimeTypeUtility;
        private static IMapper _mapper;
        public ApplicationFileDownloadService(IApplicationFileDownloadRepository repository,
            IInternationalBusinessIntellegenceRepository repositoryIBI,
            IInternationalBusinessIntellegenceService serviceIBI,
            IMapper mapper,
            IMimeTypeUtility mimeTypeUtility
        )
        {
            _repository = repository;
            _repositoryIBI = repositoryIBI;
            _serviceIBI = serviceIBI;
            _mapper = mapper;
            _mimeTypeUtility = mimeTypeUtility;
        }

        public async Task<byte[]> DownloadZip(Guid idIBI, string feature)
        {
            string pathDb = await _repository.GetPath(feature);
            int? year = await _serviceIBI.GetFormatNameById(idIBI);
            string tempPath = pathDb + Convert.ToString(year);
            string pathFolder = tempPath.Replace(" ", "");

            List<InternationalBusinessIntelligenceDocumentViewModel> files = _repositoryIBI.GetDocuments(idIBI);

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
                {
                    foreach (var file in files)
                    {
                        archive.CreateEntryFromFile(pathFolder + "\\" + file.FileNameSystem + Path.GetExtension(file.FileNameUser), Path.GetFileName(file.FileNameUser));
                    }
                }

                return memoryStream.ToArray();
            }
        }

        public async Task<FileInfoViewModel> DownloadSingleFile(Guid guid, string feature)
        {
            FileInfoViewModel result = new FileInfoViewModel();
            InternationalBusinessIntelligenceDocumentViewModel document = await _repositoryIBI.GetSingleFile(guid);

            string pathDb = await _repository.GetPath(feature);
            SequenceCounterViewModel formatName = await _serviceIBI.GetFormatName(feature);
            string tempPath = pathDb + Convert.ToString(formatName.Year);
            string pathFolder = tempPath.Replace(" ", "");


            result.FileName = document.FileNameUser;
            result.FileLength = System.IO.File.ReadAllBytes(pathFolder + "\\" + document.FileNameSystem + Path.GetExtension(document.FileNameUser));
            result.FileType = _mimeTypeUtility.GetMimeType(Path.GetExtension(document.FileNameUser));

            return result;
        }
    }
}