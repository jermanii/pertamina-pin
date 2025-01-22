using Pertamina.IRIS.Models.Base;
using Pertamina.IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.IRIS.App
{
    public class BaseController : WebControllerBase
    {
        public BaseController(IIdamanService idamanService) : base(idamanService)
        {
        }

        protected RequestFormDtBaseModel GetDataTableComponent()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var request = new RequestFormDtBaseModel()
            {
                Draw = draw,
                Page = skip,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection,
                SearchValue = searchValue,
            };

            int c = 0;
            while (c >= 0)
            {
                if (Request.Form.ContainsKey($"columns[{c}][data]"))
                {
                    var key = Request.Form[$"columns[{c}][data]"].FirstOrDefault();
                    var val = Request.Form[$"columns[{c}][search][value]"].FirstOrDefault();
                    val = string.IsNullOrEmpty(val) ? searchValue : val;
                    if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(val))
                    {
                        request.Filters.Add(new RequestFormDataTableFilter(key, val));
                    }
                    c++;
                }
                else c = -1;
            }
            return request;
        }

        protected string GetDropDownListComponent()
        {
            var query = Request.Form["q"].FirstOrDefault();

            return query;
        }
    }
}
