using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Data.Common;
using Framework.Data.EF;
using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Library.Data
{
    public class DbLibrary : EFDbContext
    {
        [ExcludeFromCodeCoverage]
        public DbLibrary(IConfiguration config, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
          : base(ConnectionStringNames.Sql, DatabaseProvider.Postgres, config, tenantAccessor, userAccessor)
        {
        }
        public DbLibrary(DbContextOptions options, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
           : base(options, tenantAccessor, userAccessor)
        { }
        public DbSet<Book> Books { get; set; }

        public DbSet<Copy> Copies { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
