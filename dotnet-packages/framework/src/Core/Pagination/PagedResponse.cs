using System;
using System.Collections.Generic;

namespace Framework.Core.Pagination
{
    public class PagedResponse<T> : List<T>, IPagedRequest
    {
        public static PagedResponse<T> Empty => new PagedResponse<T>();

        protected PagedResponse()
        {
        }

        public PagedResponse(IEnumerable<T> rows, int page, int pageSize, int count)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            AddRange(rows);
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;
    }
}