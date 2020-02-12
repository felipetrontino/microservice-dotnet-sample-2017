using Book.Entities;
using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Data.Common;
using Framework.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Book.Data
{
    public class DbBook : EFDbContext
    {
        [ExcludeFromCodeCoverage]
        public DbBook(IConfiguration config, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
            : base(ConnectionStringNames.Sql, DatabaseProvider.Postgres, config, tenantAccessor, userAccessor)
        {
        }

        public DbBook(DbContextOptions options, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
          : base(options, tenantAccessor, userAccessor)
        {


        }

        public DbSet<Entities.Book> Books { get; set; }

        public DbSet<BookAuthor> Authors { get; set; }

        public DbSet<BookCategory> Categories { get; set; }
    }
}
