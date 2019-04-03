using System.Linq;

namespace Framework.Core.Config
{
    public static class ConnectionStringSettings
    {
        public static T ParseTo<T>(string name)
            where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var ret = new T();

            var tokens = name.Split(';');
            var props = ret.GetType().GetProperties();

            foreach (var value in tokens)
            {
                var prop = props.FirstOrDefault(x => value.ToLower().StartsWith(x.Name.ToLower()));
                prop?.SetValue(ret, value.Split('=').LastOrDefault());
            }

            return ret;
        }
    }
}
