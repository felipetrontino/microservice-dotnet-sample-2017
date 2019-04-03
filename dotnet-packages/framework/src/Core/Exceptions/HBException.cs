using System;
using System.Runtime.Serialization;

namespace Framework.Core.Exceptions
{
    [Serializable]
    public class HBException : Exception
    {
        public HBException() : base()
        {
        }

        public HBException(string message) : base(message)
        {
        }

        public HBException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HBException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
