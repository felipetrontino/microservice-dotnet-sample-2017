using Bookstore.Core.Enums;
using Framework.Core.Entities;
using System;
using System.Collections.Generic;

namespace Bookstore.Entities
{
    public class Order : EFEntity
    {
        public string Number { get; set; }
        public Customer Customer { get; set; }

        public StatusOrder Status { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public DateTime CreateDate { get; set; }
    }
}
