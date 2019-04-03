using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.Pagination
{
    public static class PagedHelper
    {
        public static async Task<PagedResponse<TProxy>> CreateAsync<TEntity, TProxy>(IQueryable<TEntity> query, IPagedRequest pagination, Func<TEntity, TProxy> map)
        {
            var recordCount = await query.CountAsync();

            query = query
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize);

            var list = query.ToListAsync().Result.Select(s => map(s));

            return new PagedResponse<TProxy>(pagination, list, recordCount);
        }
    }
}
