using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Pagination
{
    public class PagedResponse<T>
    {
        public static PagedResponse<T> Empty => new PagedResponse<T>();

        public PagedResponse()
            : this(null, null)
        {
        }

        public PagedResponse(IEnumerable<T> data, int? recordCount = null)
        {
            Data = data;
            CurrentPage = PageValues.PageStartIndex;
            PageSize = PageValues.PageSize;

            if (data != null)
                RecordCount = recordCount ?? data.Count();
        }

        public PagedResponse(IPagedRequest pagination, IEnumerable<T> data, int? recordCount = null)
        {
            Data = data;
            CurrentPage = pagination.Page;
            PageSize = pagination.PageSize;

            if (data != null)
                RecordCount = recordCount ?? data.Count();
        }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int RecordCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
