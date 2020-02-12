using Bookstore.Models.Dto;
using Bookstore.Tests.Utils;
using Framework.Test.Common;
using System.Collections.Generic;

namespace Bookstore.Tests.Mocks.Dto
{
    public class ShippingDtoMock : MockBuilder<ShippingDtoMock, ShippingDto>
    {
        public static ShippingDto Get(string key)
        {
            return Create(key).Default().Build();
        }

        public ShippingDtoMock Default()
        {
            Value.Number = Fake.GetOrderNumber(Key);
            Value.Status = Fake.GetStatusOrder(Key);
            Value.CreateDate = Fake.GetCreateDate(Key);
            Value.Customer = GetCustomerDetail();

            Value.Items = new List<ShippingDto.OrderItemDetail>
            {
                GetOrderItemDetail()
            };

            return this;
        }

        private ShippingDto.CustomerDetail GetCustomerDetail()
        {
            var ret = MockHelper.CreateModel<ShippingDto.CustomerDetail>(Key);
            ret.Name = Fake.GetCustomerName(Key);
            ret.Address = Fake.GetAddress(Key);

            return ret;
        }

        private ShippingDto.OrderItemDetail GetOrderItemDetail()
        {
            var ret = MockHelper.CreateModel<ShippingDto.OrderItemDetail>(Key);
            ret.Name = Fake.GetOrderItemName(Key);
            ret.Price = Fake.GetPrice(Key);
            ret.Quantity = Fake.GetQuantity(Key);
            ret.Total = Fake.GetTotal(Key);

            return ret;
        }
    }
}