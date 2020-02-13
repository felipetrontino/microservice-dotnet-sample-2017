using Bookstore.Models.Event;
using Bookstore.Tests.Utils;
using Framework.Test.Common;
using System.Collections.Generic;

namespace Bookstore.Tests.Mocks.Models.Event
{
    public class CreateShippingEventMock : MockBuilder<CreateShippingEventMock, CreateShippingEvent>
    {
        public static CreateShippingEvent Get(string key)
        {
            return Create(key).Default().Build();
        }

        public CreateShippingEventMock Default()
        {
            Value.Number = Fake.GetOrderNumber(Key);
            Value.Status = Fake.GetStatusOrder(Key);
            Value.CreateDate = Fake.GetCreateDate(Key);
            Value.Customer = GetCustomerDetail();

            Value.Items = new List<CreateShippingEvent.OrderItemDetail>
            {
                GetOrderItemDetail()
            };

            return this;
        }

        private CreateShippingEvent.CustomerDetail GetCustomerDetail()
        {
            var ret = MockHelper.CreateModel<CreateShippingEvent.CustomerDetail>(Key);
            ret.Name = Fake.GetCustomerName(Key);
            ret.Address = Fake.GetAddress(Key);

            return ret;
        }

        private CreateShippingEvent.OrderItemDetail GetOrderItemDetail()
        {
            var ret = MockHelper.CreateModel<CreateShippingEvent.OrderItemDetail>(Key);
            ret.Name = Fake.GetOrderItemName(Key);
            ret.Price = Fake.GetPrice(Key);
            ret.Quantity = Fake.GetQuantity(Key);
            ret.Total = Fake.GetTotal(Key);

            return ret;
        }
    }
}