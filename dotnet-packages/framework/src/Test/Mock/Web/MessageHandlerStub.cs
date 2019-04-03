using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Test.Mock.Web
{
    public class HttpMessageHandlerStub : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _handler;

        public HttpMessageHandlerStub(Func<HttpRequestMessage, HttpResponseMessage> handler)
        {
            _handler = handler;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_handler(request));
        }

        public static HttpMessageHandlerStub Create(Func<HttpRequestMessage, HttpResponseMessage> handler)
        {
            return new HttpMessageHandlerStub(handler);
        }
    }
}
