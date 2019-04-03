using System;

namespace Framework.Core.Entities
{
    public abstract class EFEntity : BaseEntity, ITenantEntity, IAuditEntity, IVirtualDeletedEntity
    {
        public DateTime InsertedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Tenant { get; set; }
        public bool IsDeleted { get; set; }
    }
}
