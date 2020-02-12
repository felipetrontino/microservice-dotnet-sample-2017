using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Framework.Data.Common
{
    public class AuditEntry
    {
        public const string InsertedState = "Inserted";
        public const string UpdatedState = "Updated";
        public const string DeletedState = "Deleted";

        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }

        public string UserId { get; set; }
        public string TableName { get; set; }
        public string Tenant { get; set; }
        public string Schema { get; set; }
        public EntityState State { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();

        public AuditLog ToAudit()
        {
            var audit = new AuditLog
            {
                UserId = UserId,
                TableName = TableName,
                Schema = Schema,
                Tenant = Tenant,
                Date = DateTime.UtcNow,
                Action = GetState(State),
                PrimaryKey = JsonConvert.SerializeObject(KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues)
            };

            return audit;
        }

        private string GetState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    return InsertedState;

                case EntityState.Modified:
                    return UpdatedState;

                case EntityState.Deleted:
                    return InsertedState;

                default:
                    return null;
            }
        }
    }
}