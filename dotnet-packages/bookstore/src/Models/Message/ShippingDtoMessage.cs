using Framework.Core.Bus;
using System;

namespace Bookstore.Models.Message
{
    public class ShippingDtoMessage : BusMessage
    {
        public Guid OrderId { get; set; }
    }
}
