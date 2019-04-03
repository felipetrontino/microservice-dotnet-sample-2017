using Microsoft.Extensions.Configuration;

namespace Framework.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationSection TryGetSection(this IConfiguration value, string name)
        {
            var config = value.GetSection(name);
            return config.Exists() ? config : null;
        }
    }
}
