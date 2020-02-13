using Bookstore.Models.Message;
using Framework.Test.Common;

namespace Bookstore.Tests.Mocks.Message
{
    public class ShippingDtoMessageMock : MockBuilder<ShippingDtoMessageMock, ShippingEventMessage>
    {
        public static ShippingEventMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ShippingDtoMessageMock Default()
        {
            Value.OrderId = FakeHelper.GetId(Key);

            return this;
        }
    }
}
