using Framework.Core.Helpers;
using System.Net.Http;
using System.Text;

namespace Framework.Web.Common
{
    public static class HttpExtensions
    {
        public static StringContent ToStringContent<T>(this T value)
        {
            return new StringContent(value.JsonSerialize(), Encoding.UTF8, HttpWebNames.JsonContentType);
        }
    }
}
