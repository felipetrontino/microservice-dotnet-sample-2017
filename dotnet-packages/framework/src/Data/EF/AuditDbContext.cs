using Framework.Core.Common;
using Framework.Data.Common;
using Framework.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Framework.Data.EF
{
    public class AuditDbContext : EFDbContext
    {
        private IEnumerable<AuditEntry> _auditLogs = new List<AuditEntry>();

        public AuditDbContext(string connectionStringName, DatabaseProvider provider, IConfiguration config, ITenantAccessor tenantAccessor, IUserAccessor userAccessor)
            : base(connectionStringName, provider, config, tenantAccessor, userAccessor)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnBeforeSaveChanges()
        {
            var date = DateTime.UtcNow;

            this.ChangeTracker.Configure(date, _userAccessorr, _tenantAccessor);
            _auditLogs = this.ChangeTracker.GetLogs(Schema, date, _userAccessorr, _tenantAccessor);
        }

        protected override void OnAfterSaveChanges()
        {
            foreach (var log in _auditLogs)
            {
                this.AuditLogs.AddAsync(log.ToAudit());
            }

            base.SaveChangesAsync();
        }
    }
}
