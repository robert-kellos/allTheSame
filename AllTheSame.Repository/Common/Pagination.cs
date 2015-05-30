using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Accushield.Repository.Common
{
    public interface IPagination<T> : IList<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }

    public class Pagination<T> 
    {
        IList<T> _source;

        public Pagination(IQueryable<T> source, int pageIndex, int pageSize)
        {
            _source = source.ToList();

            int totalRecords = source.Count();
            this.TotalCount = totalRecords;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalPages = totalRecords / pageSize;
            
            if (totalRecords % pageSize > 0)
                TotalPages++;

            //_source.AddRange(source.Skip(PageIndex * pageSize).Take(pageSize).ToList());
        }
        public Pagination(List<T> source, int pageIndex, int pageSize)
        {
            _source = source;

            int totalRecords = source.Count();
            this.TotalCount = totalRecords;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalPages = totalRecords / pageSize;

            if (totalRecords % pageSize > 0)
                TotalPages++;

            //_source.AddRange(source.Skip(PageIndex * pageSize).Take(pageSize).ToList());
        }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

        public IPagination<T> GetPaged(Expression expression, int pageIndex, int pageSize)
        {
            var pagination = new Pagination<T>(FilterBy(expression).AsQueryable(),
                        pageIndex, pageSize);

            return pagination;
        }
    }
}
 k 
