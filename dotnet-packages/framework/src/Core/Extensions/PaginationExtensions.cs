using Framework.Core.Pagination;
using System;
using System.Linq;

namespace Framework.Core.Extensions
{
    public static class PaginationExtensions
    {
        public static PagedResponse<TResult> Map<TSource, TResult>(this PagedResponse<TSource> model, Func<TSource, TResult> map)
        {
            var ret = new PagedResponse<TResult>
            {
                PageSize = model.PageSize,
                CurrentPage = model.CurrentPage,
                RecordCount = model.RecordCount,
                Data = model.Data?.Select(x => map(x))
            };

            return ret;
        }

        public static PagedRequest<TResult> Map<TSource, TResult>(this PagedRequest<TSource> model, Func<TSource, TResult> map)
        {
            var ret = new PagedRequest<TResult>
            {
                PageSize = model.PageSize,
                Page = model.Page,
                Filter = map(model.Filter)
            };

            return ret;
        }
    }
}