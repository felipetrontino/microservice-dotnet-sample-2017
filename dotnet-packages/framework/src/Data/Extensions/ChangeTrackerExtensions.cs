using Framework.Core.Common;
using Framework.Core.Entities;
using Framework.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Data.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void Configure(this ChangeTracker tracker, DateTime date, IUserAccessor userAcessor, ITenantAccessor tenantAccessor)
        {
            var entries = tracker.Entries().Where(p => p.State == EntityState.Added
                                                                    || p.State == EntityState.Deleted
                                                                    || p.State == EntityState.Modified);

            foreach (var ent in entries)
            {
                SetTenant(ent, tenantAccessor?.Tenant);
                SetAuditInfo(ent, date);
            }
        }

        public static void Configure(this ChangeTracker tracker, DateTime date)
        {
            var entries = tracker.Entries().Where(p => p.State == EntityState.Added
                                                                    || p.State == EntityState.Deleted
                                                                    || p.State == EntityState.Modified);

            foreach (var ent in entries)
            {
                SetAuditInfo(ent, date);
            }
        }

        public static IEnumerable<AuditEntry> GetLogs(this ChangeTracker tracker, string schema, DateTime date, IUserAccessor userAcessor, ITenantAccessor tenantAccessor)
        {
            var ret = new List<AuditEntry>();

            var entries = tracker.Entries().Where(p => p.State == EntityState.Added
                                                                   || p.State == EntityState.Deleted
                                                                   || p.State == EntityState.Modified);

            foreach (var ent in entries)
            {
                var auditEntry = new AuditEntry(ent)
                {
                    State = ent.State,
                    UserId = userAcessor.UserName,
                    Tenant = tenantAccessor.Tenant,
                    Schema = schema,
                    TableName = ent.Metadata.Relational().TableName
                };

                foreach (var property in ent.Properties)
                {
                    var propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (ent.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:

                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }

                    ret.Add(auditEntry);
                }
            }

            return ret;
        }

        private static void SetAuditInfo(EntityEntry ent, DateTime date)
        {
            var eType = ent.Entity.GetType();
            bool isAudit = typeof(IAuditEntity).IsAssignableFrom(eType);
            bool isVirtualDeleted = typeof(IVirtualDeletedEntity).IsAssignableFrom(eType);

            if (!isAudit) return;

            switch (ent.State)
            {
                case EntityState.Added:
                    eType.GetProperty(nameof(EFEntity.InsertedAt))?.SetValue(ent.Entity, date);

                    break;

                case EntityState.Deleted:
                    eType.GetProperty(nameof(EFEntity.DeletedAt))?.SetValue(ent.Entity, date);

                    break;

                case EntityState.Modified:

                    var isDeleted = (bool)eType.GetProperty(nameof(EFEntity.IsDeleted))?.GetValue(ent.Entity);

                    if (isVirtualDeleted && isDeleted)
                        eType.GetProperty(nameof(EFEntity.DeletedAt))?.SetValue(ent.Entity, date);
                    else
                        eType.GetProperty(nameof(EFEntity.UpdatedAt))?.SetValue(ent.Entity, date);

                    break;

                default:
                    break;
            }
        }

        private static void SetTenant(EntityEntry ent, string tenant)
        {
            var eType = ent.Entity.GetType();
            bool isTenant = typeof(ITenantEntity).IsAssignableFrom(eType);

            if (isTenant && ent.State == EntityState.Added)
                eType.GetProperty(nameof(EFEntity.Tenant))?.SetValue(ent.Entity, tenant);
        }
    }
}