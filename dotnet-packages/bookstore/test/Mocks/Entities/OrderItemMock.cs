using Bookstore.Entities;
using Bookstore.Tests.Utils;
using Framework.Test;
using Framework.Test.Common;

namespace Bookstore.Tests.Mocks.Entities
{
    public class OrderItemMock : MockBuilder<OrderItemMock, OrderItem>
    {
        public static OrderItem Get(string key)
        {
            return Create(key).Default().Build();
        }

        public OrderItemMock Default()
        {
            Value.Name = Fake.GetOrderItemName(Key);
            Value.Price = Fake.GetPrice(Key);
            Value.Quantity = Fake.GetQuantity(Key);
            Value.Total = Fake.GetTotal(Key);

            return this;
        }
    }

  
}
