using System;
using System.Runtime.Serialization;

namespace Framework.Core.Exceptions
{
    [Serializable]
    public class HBDataException : HBException
    {
        public HBDataException() : base()
        {
        }

        public HBDataException(string message) : base(message)
        {
        }

        public HBDataException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HBDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
