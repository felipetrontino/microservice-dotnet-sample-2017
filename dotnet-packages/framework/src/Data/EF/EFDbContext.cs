using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Entities;
using Framework.Core.Logging;
using Framework.Core.Logging.LoggerFactory;
using Framework.Core.Utils;
using Framework.Data.Common;
using Framework.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Data.EF
{
    public abstract class EFDbContext : BaseDbContext
    {
        protected EFDbContext(string connectionStringName, DatabaseProvider provider, IConfiguration config, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
            : base(connectionStringName, provider, config, tenantAccessor, userAccessor)
        {
        }

        protected EFDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected EFDbContext(DbContextOptions options, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
           : base(options, tenantAccessor, userAccessor)
        {

        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChangesAsync(acceptAllChangesOnSuccess).GetAwaiter().GetResult();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            async Task<int> Save() =>
                await new FuncRetrier<Exception, int>
                {
                    Attempts = 3,
                    Delay = 500,
                    TaskAsync = () => TrySaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken),
                    OnAttemptError = e =>
                    {
                        LogHelper.Debug("Retrying...");
                    }
                }.RunAsync();

            return await Save();
        }

        protected virtual void OnBeforeSaveChanges()
        {
            this.ChangeTracker.Configure(DateTime.UtcNow, _userAccessorr, _tenantAccessor);
        }

        protected virtual void OnAfterSaveChanges()
        {
        }

        private async Task<int> TrySaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();

            var ret = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            OnAfterSaveChanges();

            return await ret;
        }

        protected virtual void OnEntityBuild(EntityTypeBuilder entityBuilder)
        {
            entityBuilder.HasIndex(nameof(EFEntity.Tenant));
            entityBuilder.HasIndex(nameof(EFEntity.InsertedAt));
            entityBuilder.HasIndex(nameof(EFEntity.UpdatedAt));
            entityBuilder.HasIndex(nameof(EFEntity.DeletedAt));
            entityBuilder.HasIndex(nameof(EFEntity.IsDeleted));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var type = entityType.ClrType;

                var entityBuilder = modelBuilder.Entity(type);
                OnEntityBuild(entityBuilder);

                var isVirtualDeleted = (typeof(IVirtualDeletedEntity).IsAssignableFrom(type));
                var isTenant = (typeof(IVirtualDeletedEntity).IsAssignableFrom(type));

                if (isVirtualDeleted && isTenant)
                    SetGlobalFilterMethod.MakeGenericMethod(type).Invoke(this, new[] { modelBuilder });
                else
                {
                    if (isVirtualDeleted)
                        SetVirtualDeletedFilterMethod.MakeGenericMethod(type).Invoke(this, new[] { modelBuilder });

                    if (isTenant)
                        SetTenantFilterMethod.MakeGenericMethod(type).Invoke(this, new[] { modelBuilder });
                }
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = typeof(IValueEntity).IsAssignableFrom(relationship.DeclaringEntityType.ClrType)
                    ? DeleteBehavior.Cascade
                    : DeleteBehavior.Restrict;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ReplaceService<IEntityMaterializerSource, CustomEntityMaterializerSource>();

            if (Configuration.Debugging.Get())
            {
                optionsBuilder.EnableSensitiveDataLogging();
                var log = new LoggerFactory(new[] { new CustomLoggerProvider() });
                optionsBuilder.UseLoggerFactory(log);
            }
        } 

        private class CustomEntityMaterializerSource : EntityMaterializerSource
        {
            private static readonly MethodInfo NormalizeMethod =
                typeof(DateTimeMapper).GetTypeInfo().GetMethod(nameof(DateTimeMapper.Normalize));

            private static readonly MethodInfo NormalizeNullableMethod =
                typeof(DateTimeMapper).GetTypeInfo().GetMethod(nameof(DateTimeMapper.NormalizeNullable));

            public override Expression CreateReadValueExpression
                (Expression valueBuffer, Type type, int index, IPropertyBase property)
            {
                if (type == typeof(DateTime))
                {
                    return Expression.Call(NormalizeMethod, base.CreateReadValueExpression(valueBuffer, type, index, property));
                }

                if (type == typeof(DateTime?))
                {
                    return Expression.Call(NormalizeNullableMethod,
                        base.CreateReadValueExpression(valueBuffer, type, index, property));
                }

                return base.CreateReadValueExpression(valueBuffer, type, index, property);
            }

            private static class DateTimeMapper
            {
                public static DateTime Normalize(DateTime value)
                {
                    return DateTime.SpecifyKind(value, DateTimeKind.Utc);
                }

                public static DateTime? NormalizeNullable(DateTime? value)
                {
                    return !value.HasValue ? null : (DateTime?)DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                }
            }
        }

        private static readonly MethodInfo SetGlobalFilterMethod = typeof(EFDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(t => t.IsGenericMethod && t.Name == "SetGlobalFilter");
        private static readonly MethodInfo SetTenantFilterMethod = typeof(EFDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(t => t.IsGenericMethod && t.Name == "SetTenantFilter");
        private static readonly MethodInfo SetVirtualDeletedFilterMethod = typeof(EFDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(t => t.IsGenericMethod && t.Name == "SetVirtualDeletedFilter");

        public void SetTenantFilter<T>(ModelBuilder builder)
            where T : class, ITenantEntity
        {
            builder.Entity<T>().HasQueryFilter(e => e.Tenant == Tenant);
        }

        public void SetVirtualDeletedFilter<T>(ModelBuilder builder)
            where T : class, IVirtualDeletedEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        public void SetGlobalFilter<T>(ModelBuilder builder)
            where T : class, ITenantEntity, IVirtualDeletedEntity
        {
            builder.Entity<T>().HasQueryFilter(e => e.Tenant == Tenant && !e.IsDeleted);
        }
    }
}
