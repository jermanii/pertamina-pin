using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Models.ViewModels;
using Pertamina.IRIS.Repositories.Interfaces;
using Pertamina.IRIS.Services.Interfaces;
using Pertamina.IRIS.Utility.Filtering.Interfaces;
using Pertamina.IRIS.Utility.Sorting.Interfaces;
using Pertamina.IRIS.Utility.Wording.Interfaces;
using Pertamina.IRIS.Utility.Pagination.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.Drawing;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using OfficeOpenXml.Style;

namespace Pertamina.IRIS.Services.Implement
{
    public class ExistingFootprintsDashboardService : IExistingFootprintsDashboardService
    {
        private readonly IExistingFootprintsDashboardRepository _repository;
        private readonly ISortingFuncUtility _sortingFunc;
        private readonly ITakeFuncUtility _takeFunc;
        private readonly IHighPriorityFilteringUtility _filteringUtility;

        public ExistingFootprintsDashboardService(IExistingFootprintsDashboardRepository repository, ISortingFuncUtility sortingFunc, ITakeFuncUtility takeFunc, IHighPriorityFilteringUtility filteringUtility)
        {
            _repository = repository;
            _sortingFunc = sortingFunc;
            _takeFunc = takeFunc;
            _filteringUtility = filteringUtility;
        }

        public async Task<PaginationBaseModel<ExistingFootprintsDashboardHighPriorityViewModel>> GetListPaging(RequestFormDtBaseModel request, string wwwroot, string negaraMitra, string streamBusiness, string entitasPertamina)
        {
            var resultData = await _repository.GetList(request, wwwroot, negaraMitra, streamBusiness, entitasPertamina);
            if (resultData.Data != null)
            {
                var result = new PaginationBaseModel<ExistingFootprintsDashboardHighPriorityViewModel>
                {
                    Data = resultData.Data,
                    RecordsFiltered = resultData.PageInfo.TotalRecord,
                    RecordsTotal = resultData.PageInfo.TotalRecord
                };
                return result;
            }
            return new PaginationBaseModel<ExistingFootprintsDashboardHighPriorityViewModel>();
        }
        public List<ExistingFootprintsDashboardHighPriorityViewModel> GetHighPriority(string wwwroot, bool isHigh, bool isFilter, bool isSort, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount)
        {
            List<ExistingFootprintsDashboardHighPriorityViewModel> result = new List<ExistingFootprintsDashboardHighPriorityViewModel>();

            result = _repository.GetHighPriority(wwwroot, isHigh, isFilter, isSort, isClickMap, countryAcronym, negara, stream, entitas, category, order, pageCount);

            return result;
        }
        public List<ExistingFootprintsDashboardLegendViewModel> GetLegends()
        {
            List<ExistingFootprintsDashboardLegendViewModel> result = new List<ExistingFootprintsDashboardLegendViewModel>();

            result = _repository.GetLegends();

            return result;
        }
        public async Task<ExistingFootprintDashboardMapViewModel> GetCountriesMap()
        {
            ExistingFootprintDashboardMapViewModel result = new ExistingFootprintDashboardMapViewModel();

            result.CountriesAcronym = await _repository.GetCountriesMap();
            result.Pointers = await _repository.GetSubHoldingMap();

            return result;
        }
        public async Task<ExistingFootprintDashboardMapViewModel> GetCountriesMap(Guid? negara, Guid? stream, Guid? entitas)
        {
            ExistingFootprintDashboardMapViewModel result = new ExistingFootprintDashboardMapViewModel();

            result.CountriesAcronym = await _repository.GetCountriesMap(negara, stream, entitas);
            result.Pointers = await _repository.GetSubHoldingMap(negara, stream, entitas);

            return result;
        }
        public ExistingFootprintsDashboardHighPriorityViewModel GetExistingFootprintsDashboardDetail(string wwwroot, Guid guid, int year)
        {
            ExistingFootprintsDashboardHighPriorityViewModel result = new ExistingFootprintsDashboardHighPriorityViewModel();

            result = _repository.GetExistingFootprintsDashboardDetail(wwwroot, guid, year);

            return result;
        }
        public List<ExistingFootprintsPartnersViewModel> GetExistingFootprintsPartners(Guid guid)
        {
            List<ExistingFootprintsPartnersViewModel> result = new List<ExistingFootprintsPartnersViewModel>();

            result = _repository.GetExistingFootprintsPartners(guid);

            return result;
        }
        public int? GetCountAllRecordSortHighPriority(string wwwroot, bool isHigh, bool isFilter, bool isSort, bool isClickMap, string countryAcronym, Guid? negara, Guid? stream, Guid? entitas, string category, string order, int pageCount)
        {
            int? result = 0;

            result = _repository.GetCountAllRecordSortHighPriority(wwwroot, isHigh, isFilter, isSort, isClickMap, countryAcronym, negara, stream, entitas, category, order, pageCount);

            return result;
        }
        public Guid? GetCountryByAcronym(string countryAcronym)
        {
            Guid? result = null;

            result = _repository.GetCountryByAcronym(countryAcronym);

            return result;
        }
    }
}