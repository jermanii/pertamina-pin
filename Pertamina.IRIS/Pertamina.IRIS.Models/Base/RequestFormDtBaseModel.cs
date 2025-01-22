using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Models.Base
{
    public class RequestFormDtBaseModel
    {
        public RequestFormDtBaseModel()
        {
            Filters = new List<RequestFormDataTableFilter>();
        }

        public int? ParentId { get; set; }
        public string Draw { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
        public List<RequestFormDataTableFilter> Filters { get; set; }
        public int PageValue
        {
            get
            {
                int page = (this.Page / this.PageSize) + 1;
                if (page <= 0)
                {
                    page = 1;
                }
                return page;
            }
        }
    }

    public class RequestFormDataTableFilter
    {
        public RequestFormDataTableFilter(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
