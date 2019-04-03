using Bookstore.Core.Enums;
using Framework.Core.Bus;
using System;
using System.Collections.Generic;

namespace Bookstore.Models.Dto
{
    public class ShippingDto : BaseDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public StatusOrder Status { get; set; }

        public DateTime CreateDate { get; set; }

        public CustomerDetail Customer { get; set; }

        public List<OrderItemDetail> Items { get; set; } = new List<OrderItemDetail>();

        public class CustomerDetail
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string Address { get; set; }
        }

        public class OrderItemDetail
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public double Price { get; set; }

            public double Quantity { get; set; }

            public double Total { get; set; }
        }
    }
}
