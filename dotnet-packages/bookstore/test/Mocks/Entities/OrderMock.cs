using Bookstore.Entities;
using Bookstore.Tests.Utils;
using Framework.Test.Common;
using System;
using System.Collections.Generic;

namespace Bookstore.Tests.Mocks.Entities
{
    public class OrderMock : MockBuilder<OrderMock, Order>
    {
        public static Order Get(string key)
        {
            return Create(key).Default().Build();
        }

        public static Order GetByPurchase(string key)
        {
            return Create(key).ByPurchase().Build();
        }

        public OrderMock Default()
        {
            Value.Number = Fake.GetOrderNumber(Key);
            Value.Status = Fake.GetStatusOrder(Key);
            Value.CreateDate = Fake.GetCreateDate(Key);
            Value.Customer = CustomerMock.Get(Key);

            Value.Items = new List<OrderItem>();
            Value.Items.Add(OrderItemMock.Get(Key));

            return this;
        }

        public OrderMock ByPurchase()
        {
            Value.InsertedAt = DateTime.UtcNow;
            Value.Number = Fake.GetOrderNumber(Key);
            Value.Status = Fake.GetStatusOrder(Key);
            Value.CreateDate = DateTime.UtcNow;

            Value.Customer = MockHelper.CreateModel<Customer>(Key);
            Value.Customer.InsertedAt = DateTime.UtcNow;
            Value.Customer.Name = Fake.GetCustomerName(Key);
            Value.Customer.DocumentId = FakeHelper.GetId(Key).ToString();

            Value.Items = new List<OrderItem>();

            var item = OrderItemMock.Get(Key);
            item.InsertedAt = DateTime.UtcNow;
            Value.Items.Add(item);

            return this;
        }
    }
}
