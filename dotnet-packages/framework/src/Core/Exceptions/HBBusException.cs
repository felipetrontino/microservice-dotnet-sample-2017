using System;
using System.Runtime.Serialization;

namespace Framework.Core.Exceptions
{
    [Serializable]
    public class HBBusException : HBException
    {
        public HBBusException() : base()
        {
        }

        public HBBusException(string message) : base(message)
        {
        }

        public HBBusException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HBBusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
