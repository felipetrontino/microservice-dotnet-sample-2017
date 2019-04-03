namespace Framework.Core.Common
{
    public class RequestIdAccessor : IRequestIdAccessor
    {
        public RequestIdAccessor(string requestId = "")
        {
            RequestId = requestId;
        }

        public string RequestId { get; private set; }
    }
}
