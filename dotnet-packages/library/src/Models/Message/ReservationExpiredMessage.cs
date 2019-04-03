using Framework.Core.Bus;
using System;

namespace Library.Models.Message
{
    public class ReservationExpiredMessage : BusMessage
    {
        public Guid ReservationId { get; set; }
    }
}
