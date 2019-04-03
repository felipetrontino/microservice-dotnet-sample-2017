using Newtonsoft.Json;

namespace Framework.Core.Helpers
{
    public static class JsonHelper
    {
        public static JsonSerializerSettings GetSettings()
        {
            return new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                FloatFormatHandling = FloatFormatHandling.String,
                FloatParseHandling = FloatParseHandling.Decimal,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
        }

        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, GetSettings());
        }

        public static string JsonSerialize<T>(this T obj)
        {
            return Serialize(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetSettings());
        }

        public static T JsonDeserialize<T>(this string json)
        {
            return Deserialize<T>(json);
        }
    }
}
