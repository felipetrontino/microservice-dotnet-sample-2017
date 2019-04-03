using System;

namespace Framework.Data.Common
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string Action { get; set; }
        public string Tenant { get; set; }
        public string Schema { get; set; }
        public string TableName { get; set; }

        public string PrimaryKey { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }
    }
}
