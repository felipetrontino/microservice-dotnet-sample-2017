using Framework.Core.Bus;
using System;

namespace Bookstore.Models.Message
{
    public class ShippingEventMessage : BusMessage
    {
        public Guid OrderId { get; set; }
    }
}