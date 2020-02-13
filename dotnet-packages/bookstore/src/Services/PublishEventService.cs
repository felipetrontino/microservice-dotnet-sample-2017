using Bookstore.Core.Common;
using Bookstore.Core.Interfaces;
using Bookstore.Data;
using Bookstore.Models.Event;
using Bookstore.Models.Message;
using Framework.Core.Bus;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class PublishEventService : IPublishEventService
    {
        private readonly DbBookstore _db;
        private readonly IBusPublisher _bus;

        public PublishEventService(DbBookstore db, IBusPublisher bus)
        {
            _db = db;
            _bus = bus;
        }

        public async Task PublishShippingEventAsync(ShippingEventMessage message)
        {
            var order = await _db.Orders
              .Include(x => x.Items)
              .Include(x => x.Customer)
              .FirstOrDefaultAsync(x => x.Id == message.OrderId);

            var dto = new CreateShippingEvent
            {
                Number = order.Number,
                Status = order.Status,
                CreateDate = order.CreateDate,
                Customer = new CreateShippingEvent.CustomerDetail()
                {
                    Name = order.Customer.Name,
                    Address = order.Customer.Address
                }
            };

            foreach (var item in order.Items)
            {
                var itemDto = new CreateShippingEvent.OrderItemDetail()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Total = item.Total
                };

                dto.Items.Add(itemDto);
            }

            await _bus.PublishAsync(ExchangeNames.Bookstore, dto);
        }
    }
}