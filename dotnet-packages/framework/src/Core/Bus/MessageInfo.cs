namespace Framework.Core.Bus
{
    public class MessageInfo : IBusInfo
    {
        public string MessageId { get; set; }

        public string RequestId { get; set; }

        public string ContentName { get; set; }

        public string Body { get; set; }
    }
}
