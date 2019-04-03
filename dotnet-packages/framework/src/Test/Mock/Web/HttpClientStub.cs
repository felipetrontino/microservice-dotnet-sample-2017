using System;
using System.Net.Http;

namespace Framework.Test.Mock.Web
{
    public class HttpClientStub : HttpClient
    {
        public HttpClientStub(HttpMessageHandler handler)
            : base(handler)
        {
            BaseAddress = new Uri("http://test");
        }

        public static HttpClient Create(HttpMessageHandler handler)
        {
            return new HttpClientStub(handler);
        }
    }
}
