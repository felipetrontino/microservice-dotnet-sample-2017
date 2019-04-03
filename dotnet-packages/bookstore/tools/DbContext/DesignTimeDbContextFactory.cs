using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bookstore.Tools.DbContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DbMigrations>
    {
        public DbMigrations CreateDbContext(string[] args)
        {           
            var builder = new DbContextOptionsBuilder<DbMigrations>();
            builder.UseSqlite($"file:{nameof(DbMigrations)}?mode=memory&cache=shared");

            return new DbMigrations(builder.Options);
        }
    }
}
