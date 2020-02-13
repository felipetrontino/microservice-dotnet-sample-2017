using Framework.Core.Bus;
using System;

namespace Library.Models.Message
{
    public class ReservationEventMessage : BusMessage
    {
        public Guid ReservationId { get; set; }
    }
}