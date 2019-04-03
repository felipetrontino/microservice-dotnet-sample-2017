using System;
using System.Collections.Generic;
using Framework.Core.Bus;

namespace Bookstore.Models.Message
{
    public class PurchaseMessage : BusMessage
    {
        public string Number { get; set; }
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public DateTime Date { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        public class Item
        {
            public string Name { get; set; }

            public string Number { get; set; }

            public int Quantity { get; set; }

            public double Price { get; set; }
        }
    }
}
