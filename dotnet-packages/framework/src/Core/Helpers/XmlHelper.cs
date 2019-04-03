using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Framework.Core.Helpers
{
    public static class XmlHelper
    {
        public static string Serialize<T>(T obj)
        {
            var ret = string.Empty;

            if (obj == null) return ret;

            using (var ms = new MemoryStream())
            {
                var xmlWriter = new XmlTextWriter(ms, Encoding.Default);
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(xmlWriter, obj, null);

                using (var reader = new StreamReader(ms))
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    return reader.ReadToEnd();
                }
            }
        }

        public static string XmlSerialize<T>(this T obj)
        {
            return Serialize(obj);
        }

        public static T Deserialize<T>(string xml)
        {
            T ret = default;

            if (string.IsNullOrWhiteSpace(xml)) return ret;

            var xs = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(xml))
            {
                ret = (T)xs.Deserialize(reader);
            }

            return ret;
        }

        public static T XmlDeserialize<T>(this string xml)
        {
            return Deserialize<T>(xml);
        }
    }
}
