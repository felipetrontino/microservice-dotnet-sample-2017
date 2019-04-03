using Book.Entities;
using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Data.Common;
using Framework.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Book.Tools.DbContext
{
    public class DbMigrations : EFDbContext
    {
        public DbMigrations(IConfiguration config, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
            : base(ConnectionStringNames.Sql, DatabaseProvider.Postgres, config, tenantAccessor, userAccessor)
        {
        }

        internal DbMigrations(DbContextOptions options)
          : base(options)
        {
            Schema = "book";
        }

        public DbSet<Entities.Book> Books { get; set; }

        public DbSet<BookAuthor> Authors { get; set; }

        public DbSet<BookCategory> Categories { get; set; }
    }
}
