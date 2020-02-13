using Library.Models.Message;
using System;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IPublishEventService
    {
        Task PublishReservationEventAsync(ReservationEventMessage message);
    }
}