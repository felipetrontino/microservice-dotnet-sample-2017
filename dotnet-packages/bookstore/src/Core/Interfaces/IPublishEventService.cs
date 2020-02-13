using Bookstore.Models.Message;
using System.Threading.Tasks;

namespace Bookstore.Core.Interfaces
{
    public interface IPublishEventService
    {
        Task PublishShippingEventAsync(ShippingEventMessage message);
    }
}