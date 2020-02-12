using System;

namespace Framework.Core.Bus
{
    public abstract class BaseMessage : IBusMessage
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
        public string Culture { get; set; }

        public void Setup(string userName = null, string tenant = null, string culture = null)
        {
            UserName = userName ?? UserName;
            Tenant = tenant ?? Tenant;
            Culture = culture ?? Culture;
        }
    }
}