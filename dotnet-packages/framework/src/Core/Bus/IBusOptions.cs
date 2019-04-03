namespace Framework.Core.Bus
{
    public interface IBusOptions
    {
        string Key { get; }

        string ConnectionStringName { get; set; }
    }
}
