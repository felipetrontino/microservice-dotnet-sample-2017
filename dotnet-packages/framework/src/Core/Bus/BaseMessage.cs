using Framework.Core.Common;
using System;

namespace Framework.Core.Bus
{
    public abstract class BaseMessage : IBusMessage, IUserAccessor, ITenantAccessor, ILanguageAccessor
    {
        protected BaseMessage()
        {
            MessageId = Guid.NewGuid().ToString();
            ContentName = GetType().Name;
        }

        public virtual string ContentName { get; }
        public string MessageId { get; private set; }
        public string RequestId { get; set; }
        public string UserName { get; set; }
        public string Tenant { get; set; }
        public string Language { get; set; }
    }
}
