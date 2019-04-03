using Framework.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Framework.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void Delete<TEntity>(this DbContext dbContext, TEntity entity)
             where TEntity : class, IVirtualDeletedEntity
        {
            entity.IsDeleted = true;
            dbContext.Update(entity);
        }

        public static void Delete<TEntity>(this DbSet<TEntity> dbSet, TEntity entity)
              where TEntity : class, IVirtualDeletedEntity
        {
            entity.IsDeleted = true;
            dbSet.Update(entity);
        }
    }
}
