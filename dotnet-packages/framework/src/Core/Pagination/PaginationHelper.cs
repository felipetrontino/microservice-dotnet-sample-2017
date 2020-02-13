using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.Pagination
{
    public static class PaginationHelper
    {
        public static PagedResponse<T> ToPagedResponse<T>(this IEnumerable<T> source, int page = PageValues.PageStart, int pageSize = PageValues.PageSize)
        {
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResponse<T>(items, page, pageSize, count);
        }

        public static async Task<PagedResponse<TProxy>> ToPagedResponseAsync<TEntity, TProxy>(this IQueryable<TEntity> query, IPagedRequest pagination, Func<TEntity, TProxy> map)
        {
            var recordCount = await query.CountAsync();
            var result = await query.Skip((pagination.Page - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

            var items = result.Select(s => map(s));

            return new PagedResponse<TProxy>(items, pagination.Page, pagination.PageSize, recordCount);
        }
    }
}