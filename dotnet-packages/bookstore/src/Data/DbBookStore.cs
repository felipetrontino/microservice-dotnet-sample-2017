using Bookstore.Entities;
using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Data.Common;
using Framework.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Bookstore.Data
{
    public class DbBookstore : EFDbContext
    {
        [ExcludeFromCodeCoverage]
        public DbBookstore(IConfiguration config, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
           : base(ConnectionStringNames.Sql, DatabaseProvider.InMemory, config, tenantAccessor, userAccessor)
        {
        }

        public DbBookstore(DbContextOptions options, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
           : base(options, tenantAccessor, userAccessor)
        { }

        public DbSet<Book> Books { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}