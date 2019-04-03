using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Data.Common;
using Framework.Data.EF;
using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Library.Tools.DbContext
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
            Schema = "library";
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Copy> Copies { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
