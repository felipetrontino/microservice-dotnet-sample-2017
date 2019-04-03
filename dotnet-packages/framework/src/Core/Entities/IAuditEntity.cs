using System;

namespace Framework.Core.Entities
{
    public interface IAuditEntity
    {
        DateTime InsertedAt { get; set; }

        DateTime? UpdatedAt { get; set; }

        DateTime? DeletedAt { get; set; }
    }
}
