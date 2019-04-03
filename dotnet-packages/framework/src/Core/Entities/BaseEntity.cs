using System;

namespace Framework.Core.Entities
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
