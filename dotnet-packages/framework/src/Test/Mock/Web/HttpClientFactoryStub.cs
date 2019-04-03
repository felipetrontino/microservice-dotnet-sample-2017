using Moq;
using System;
using System.Net.Http;

namespace Framework.Test.Mock.Web
{
    public class HttpClientFactoryStub
    {
        protected HttpClientFactoryStub()
        { }

        public static IHttpClientFactory Create(HttpResponseMessage response)
        {
            var handler = HttpMessageHandlerStub.Create(req =>
            {
                return response;
            });

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(HttpClientStub.Create(handler));

            return factoryMock.Object;
        }

        public static IHttpClientFactory Create(Exception exception)
        {
            var handler = HttpMessageHandlerStub.Create(req =>
            {
                throw exception;
            });

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(HttpClientStub.Create(handler));

            return factoryMock.Object;
        }
    }
}
