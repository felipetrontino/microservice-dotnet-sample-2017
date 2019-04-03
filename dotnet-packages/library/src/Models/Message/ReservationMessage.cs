using Framework.Core.Bus;
using System.Collections.Generic;

namespace Library.Models.Message
{
    public class ReservationMessage : BusMessage
    {
        public string Number { get; set; }
        public string MemberId { get; set; }

        public string MemberName { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        public class Item
        {
            public string Number { get; set; }
            public string Name { get; set; }
        }
    }
}
