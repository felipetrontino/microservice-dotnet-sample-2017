namespace Framework.Core.Bus
{
    public interface IBusInfo
    {
        string MessageId { get; }
        string ContentName { get; }
        string RequestId { get; }
    }
}
