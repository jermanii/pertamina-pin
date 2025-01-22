using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.Style;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using Pertamina.IRIS.Utility.Filtering.Interfaces;
using Pertamina.IRIS.Utility.Pagination.Interfaces;
using Pertamina.IRIS.Utility.Sorting.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pertamina.IRIS.Services.Implement
{
    public class SignedAgreementDashboardService : ISignedAgreementDashboardService
    {
        private readonly ISortingFuncUtility _sortingFunc;
        private readonly ITakeFuncUtility _takeFunc;
        private readonly IHighPriorityFilteringUtility _filteringUtility;
        private readonly ISignedAgreementDashboardRepository _repository;
        private readonly IAgreementRepository _agreementRepository;

        public SignedAgreementDashboardService(ISortingFuncUtility sortingFunc, ITakeFuncUtility takeFunc, IHighPriorityFilteringUtility filteringUtility, ISignedAgreementDashboardRepository repository, IAgreementRepository agreementRepository)
        {
            _sortingFunc = sortingFunc;
            _takeFunc = takeFunc;
            _filteringUtility = filteringUtility;
            _repository = repository;
            _agreementRepository = agreementRepository;
        }

        public async Task<PaginationBaseModel<SignedAgreementDashboardHighPriorityViewModel>> GetListPaging(RequestFormDtBaseModel request, string wwwroot, string negaraMitra, string streamBusiness, string entitasPertamina)
        {
            var resultData = await _repository.GetList(request, wwwroot, negaraMitra, streamBusiness, entitasPertamina);

            var result = new PaginationBaseModel<SignedAgreementDashboardHighPriorityViewModel>
            {
                Data = resultData.Data,
                RecordsFiltered = resultData.PageInfo.TotalRecord,
                RecordsTotal = resultData.PageInfo.TotalRecord
            };

            return result;
        }
        public async Task<SignedAgreementDashboardViewModel> FilterHighPriority(string countryAcr)
        {
            SignedAgreementDashboardViewModel model = new SignedAgreementDashboardViewModel();
            try
            {
                model.HighPriority = await _repository.GetFilterHighPriority(countryAcr);
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
        public async Task<SignedAgreementDashboardViewModel> FilterHighPriorityBasedOnDropdown(string? streamBusiness, string? negaraMitra, string? entitasPertamina)
        {
            SignedAgreementDashboardViewModel model = new SignedAgreementDashboardViewModel();
            try
            {
                model.HighPriority = await _repository.GetFilterHighPriority(null, streamBusiness, negaraMitra, entitasPertamina);
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
        public async Task<SignedAgreementDashboardMapViewModel> GetCountriesMap()
        {
            SignedAgreementDashboardMapViewModel result = new SignedAgreementDashboardMapViewModel();

            result.CountriesAcronym = await _repository.GetCountriesMap();
            result.Pointers = await _repository.GetSubHoldingMap();

            return result;
        }
        public async Task<SignedAgreementDashboardMapViewModel> GetCountriesMap(Guid? negara, Guid? stream, Guid? entitas)
        {
            SignedAgreementDashboardMapViewModel result = new SignedAgreementDashboardMapViewModel();

            result.CountriesAcronym = await _repository.GetCountriesMap(negara, stream, entitas);
            result.Pointers = await _repository.GetSubHoldingMap();

            return result;
        }
        public List<SignedAgreementDashboardHighPriorityViewModel> GetHighPriority(string wwwroot)
        {
            List<SignedAgreementDashboardHighPriorityViewModel> result = new List<SignedAgreementDashboardHighPriorityViewModel>();

            IQueryable<SignedAgreementDashboardHighPriorityViewModel> query = null;
            query = _repository.GetHighPriority(wwwroot);

            return result = query.ToList();
        }
        public List<SignedAgreementKementriaanLembagaViewModel> GetKemntrianLembagaAgreement(List<SignedAgreementDashboardHighPriorityViewModel> highPriority)
        {
            List<SignedAgreementKementriaanLembagaViewModel> result = new List<SignedAgreementKementriaanLembagaViewModel>();

            foreach (var item in highPriority)
            {
                if (item.HasLembaga)
                {
                    result.AddRange(_repository.GetKemntrianLembagaAgreement(item.Id));
                }
            }

            return result;
        }
        public List<SignedAgreementDashboardHighPriorityViewModel> GetStrategicAgreement(string wwwroot, bool isHigh, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount)
        {
            List<SignedAgreementDashboardHighPriorityViewModel> result = new List<SignedAgreementDashboardHighPriorityViewModel>();

            result = _repository.GetStrategicPriorityAgreement(wwwroot, isHigh, isSort, isFilter, isClickMap, negara, stream, entitas, countryAcronym, category, order, pageCount);

            return result;
        }

        public int? GetCountAllStrategicAgreement(string wwwroot, bool isHigh, bool isSort, bool isFilter, bool isClickMap, Guid? negara, Guid? stream, Guid? entitas, string countryAcronym, string category, string order, int pageCount)
        {
            int? result = 0;

            result = _repository.GetCountAllStrategicAgreement(wwwroot, isHigh, isSort, isFilter, isClickMap, negara, stream, entitas, countryAcronym, category, order, pageCount);

            return result;
        }
        public Guid? GetCountryByAcronym(string countryAcronym)
        {
            Guid? result = null;

            result = _repository.GetCountryByAcronym(countryAcronym);

            return result;
        }
        public SignedAgreementDashboardHighPriorityViewModel GetSignedAgreementDashboardDetail(string wwwroot, Guid guid)
        {
            SignedAgreementDashboardHighPriorityViewModel result = new SignedAgreementDashboardHighPriorityViewModel();

            result = _repository.GetSignedAgreementDashboardDetail(wwwroot, guid);

            return result;
        }
        public List<SignedAgreementPartnerViewModel> GetSignedAgreementDashboardDetailPartners(Guid guid)
        {
            List<SignedAgreementPartnerViewModel> result = new List<SignedAgreementPartnerViewModel>();

            result = _repository.GetSignedAgreementDashboardDetailPartners(guid);

            return result;
        }
        public List<SignedAgreementLokasiProyekViewModel> GetSignedAgreementDashboardDetailProjectLocations(Guid guid)
        {
            List<SignedAgreementLokasiProyekViewModel> result = new List<SignedAgreementLokasiProyekViewModel>();

            result = _repository.GetSignedAgreementDashboardDetailProjectLocations(guid);

            return result;
        }
        public List<SignedAgreementEntitasPertaminaViewModel> GetSignedAgreementDashboardDetailPertaminaEntitas(Guid guid)
        {
            List<SignedAgreementEntitasPertaminaViewModel> result = new List<SignedAgreementEntitasPertaminaViewModel>();

            result = _repository.GetSignedAgreementDashboardDetailPertaminaEntitas(guid);

            return result;
        }
        public List<SignedAgreementStreamBusinessViewModel> GetSignedAgreementDashboardDetailStreams(Guid guid)
        {
            List<SignedAgreementStreamBusinessViewModel> result = new List<SignedAgreementStreamBusinessViewModel>();

            result = _repository.GetSignedAgreementDashboardDetailStreams(guid);

            return result;
        }
    }
}