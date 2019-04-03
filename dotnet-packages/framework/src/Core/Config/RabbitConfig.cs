using Framework.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Framework.Core.Config
{
    public class RabbitConfig
    {
        public string Host { get; set; }

        public string Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string VHost { get; set; }

        public static RabbitConfig Get(IConfiguration config, string conectionStringName, string tenant = null)
        {
            var conn = Settings.GetInstance(config, tenant).ConnectionStrings.GetOrDefault(conectionStringName);
            return ConnectionStringSettings.ParseTo<RabbitConfig>(conn);
        }
    }
}
