using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pertamina.IRIS.Models.Base;

namespace Pertamina.IRIS.Repositories.Core
{
    public class PaginatedList<T, U> : List<U> where T : class
    {
        private static IMapper _mapper;

        public int Offset { get; }
        public int Limit { get; }
        public int Total { get; }
        public PageInfoBaseModel ListInfo { get; private set; }
        private int Page { get; set; }


        public PaginatedList(List<T> source, int total, int offset, int limit, int page)
        {
            //if(_mapper == null)
            //{
            //    _mapper = Mapper.Instance;
            //}

            Offset = offset; //Start position
            Limit = limit; //Amount of records to return
            Total = total; //Total records
            Page = page;

            //TotalPages = (int)Math.Ceiling(count / (double)limit);

            //Same type no mapping needed
            if (typeof(T) == typeof(U))
            {
                if (source != null)
                {
                    AddRange((IEnumerable<U>)source);
                    CreateActionLinks();
                    return;
                }
            }
            //Dest != source, Mapping (automapper) profile needs te exist
            var retList = new List<U>();
            foreach (var item in source)
            {
                if (_mapper != null)
                    retList.Add(_mapper.Map<U>(item));
            }
            AddRange(retList);
            CreateActionLinks();
        }

        private void CreateActionLinks()
        {
            //CHECK IF HATEOAS MUST BE IMPLEMENTED HERE 
            ListInfo = new PageInfoBaseModel()
            {
                Page = Page,
                TotalRecord = Total
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="source">When the query has inner joins, use GetCountFromInnerJoin first to retrieve the correct amount of results</param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="countFromInnerJoin"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static async Task<PaginatedList<T, U>> CreateAsync(IQueryable<T> source, int page, int limit, int? countFromInnerJoin, IMapper mapper = null)
        {
            _mapper = mapper;


            //Use defaults value according to generic specs 
            if (limit < 0) limit = 100;
            var offset = (page - 1) * limit;
            if (offset < 0) offset = 0;

            int count;
            //BEWARE: the results can be incorrect, the CountAsync query has a bug, not using the inner join (https://github.com/aspnet/EntityFrameworkCore/issues/8201)
            if (countFromInnerJoin.HasValue)
            {
                count = countFromInnerJoin.Value;
            }
            else count = await source.CountAsync();

            if (count == 0)
                return new PaginatedList<T, U>(new List<T>(), 0, 0, limit, page);

            List<T> items = new List<T>();
            if (limit > 0 && offset < count)
                items = await Task.FromResult(source.Skip(offset).Take(limit).ToList());
            return new PaginatedList<T, U>(items, count, offset, limit, page);
        }

        public static async Task<PaginatedList<T, U>> CreateFromListAsync(List<T> source, int page, int limit, int? countFromInnerJoin, IMapper mapper = null)
        {
            _mapper = mapper;

            //Use defaults value according to generic specs 
            if (limit < 0) limit = 100;
            var offset = (page - 1) * limit;
            if (offset < 0) offset = 0;

            int count;
            //BEWARE: the results can be incorrect, the CountAsync query has a bug, not using the inner join (https://github.com/aspnet/EntityFrameworkCore/issues/8201)
            if (countFromInnerJoin.HasValue)
            {
                count = countFromInnerJoin.Value;
            }
            else count = source.Count();

            if (count == 0)
                return new PaginatedList<T, U>(new List<T>(), 0, 0, limit, page);

            List<T> items = new List<T>();
            if (limit > 0 && offset < count)
                items = await Task.FromResult(source.Skip(offset).Take(limit).ToList());
            return new PaginatedList<T, U>(items, count, offset, limit, page);
        }
    }
}
