namespace Framework.Core.Bus
{
    public class BusOptions : IBusOptions
    {
        public BusOptions(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
        }

        public string Key { get { return $"{ConnectionStringName}"; } }
        public string ConnectionStringName { get; set; }
    }
}
