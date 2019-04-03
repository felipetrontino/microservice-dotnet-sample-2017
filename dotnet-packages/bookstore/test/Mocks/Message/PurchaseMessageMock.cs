using Bookstore.Models.Message;
using Bookstore.Tests.Utils;
using Framework.Test.Common;
using System.Collections.Generic;

namespace Bookstore.Tests.Mocks.Message
{
    public class PurchaseMessageMock : MockBuilder<PurchaseMessageMock, PurchaseMessage>
    {
        public static PurchaseMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public PurchaseMessageMock Default()
        {
            Value.Number = Fake.GetOrderNumber(Key);
            Value.CustomerName = Fake.GetCustomerName(Key);
            Value.CustomerId = FakeHelper.GetId(Key).ToString();
            Value.Date = Fake.GetOrderDate(Key);
            Value.Items = new List<PurchaseMessage.Item>();
            Value.Items.Add(GetItem(Key));

            return this;
        }

        private PurchaseMessage.Item GetItem(string key)
        {
            var ret = MockHelper.CreateModel<PurchaseMessage.Item>(key);
            ret.Name = Fake.GetOrderItemName(key);
            ret.Number = Fake.GetCopyNumber(key);
            ret.Price = Fake.GetPrice(key);
            ret.Quantity = Fake.GetQuantity(key);

            return ret;
        }
    }
}
