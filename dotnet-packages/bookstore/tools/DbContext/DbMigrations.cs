using Bookstore.Entities;
using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Data.Common;
using Framework.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bookstore.Tools.DbContext
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
            Schema = "Bookstore";
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
