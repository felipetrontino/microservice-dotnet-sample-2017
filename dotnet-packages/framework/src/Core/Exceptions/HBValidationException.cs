using System;
using System.Runtime.Serialization;

namespace Framework.Core.Exceptions
{
    [Serializable]
    public class HBValidationException : HBException
    {
        public HBValidationException() : base()
        {
        }

        public HBValidationException(string message) : base(message)
        {
        }

        public HBValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HBValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
