using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Extensions;
using Framework.Data.Common;
using Framework.Data.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Data.EF
{
    public abstract class BaseDbContext : DbContext
    {
        protected readonly ITenantAccessor _tenantAccessor;
        protected readonly IUserAccessor _userAccessorr;

        protected BaseDbContext(string connectionStringName, DatabaseProvider provider, IConfiguration config, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
        {
            _tenantAccessor = tenantAccessor;
            _userAccessorr = userAccessor;

            Schema = Configuration.Audience.Get();
            Tenant = _tenantAccessor?.Tenant;

            Provider = provider;
            ConnectionString = Settings.GetInstance(config, Tenant).ConnectionStrings.GetOrDefault(connectionStringName);
        }

        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected BaseDbContext(DbContextOptions options, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
           : this(options)
        {
            _tenantAccessor = tenantAccessor;
            _userAccessorr = userAccessor;

            Tenant = _tenantAccessor?.Tenant;
        }

        #region ConnectionInfo

        public string Tenant { get; set; }
        public string Schema { get; set; }

        public DatabaseProvider Provider { get; private set; }

        public string ConnectionString { get; private set; }

        #endregion ConnectionInfo

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public virtual void EnsureSeedData()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.RemovePluralizingTableNameConvention();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            ConfigureConnection(optionsBuilder, Provider, ConnectionString);
        }

        private void ConfigureConnection(DbContextOptionsBuilder builder, DatabaseProvider provider, string connectionString)
        {
            DbConnection connection;

            switch (provider)
            {
                case DatabaseProvider.Sqlite:
                    connection = new SqliteConnection(connectionString);
                    builder.UseSqlite(connection);
                    break;

                case DatabaseProvider.Postgres:
                    connection = new NpgsqlConnection(connectionString);
                    builder.UseNpgsql(connection);
                    break;

                case DatabaseProvider.SqlServer:
                    connection = new SqlConnection(connectionString);
                    builder.UseSqlServer(connection);
                    break;

                default:
                    builder.UseInMemoryDatabase($"InMemory_{connectionString}");
                    break;
            }
        }
    }
}