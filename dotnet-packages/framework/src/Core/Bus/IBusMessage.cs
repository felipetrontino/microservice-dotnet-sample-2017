namespace Framework.Core.Bus
{
    public interface IBusMessage : IBusInfo
    {
        string UserName { get; set; }
        string Tenant { get; set; }
        string Language { get; set; }
    }
}
