using Bookstore.Core.Common;
using Bookstore.Core.Interfaces;
using Bookstore.Data;
using Bookstore.Models.Dto;
using Bookstore.Models.Message;
using Framework.Core.Bus;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class ProcessDtoService : IProcessDtoService
    {
        private readonly DbBookstore _db;
        private readonly IBusPublisher _bus;

        public ProcessDtoService(DbBookstore db, IBusPublisher bus)
        {
            _db = db;
            _bus = bus;
        }

        public async Task CreateShippingAsync(ShippingDtoMessage message)
        {
            var order = await _db.Orders
              .Include(x => x.Items)
              .Include(x => x.Customer)
              .FirstOrDefaultAsync(x => x.Id == message.OrderId);

            var dto = new ShippingDto
            {
                Id = order.Id,
                Number = order.Number,
                Status = order.Status,
                CreateDate = order.CreateDate,
                Customer = new ShippingDto.CustomerDetail()
                {
                    Id = order.Customer.Id,
                    Name = order.Customer.Name,
                    Address = order.Customer.Address
                }
            };

            foreach (var item in order.Items)
            {
                var itemDto = new ShippingDto.OrderItemDetail()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Total = item.Total
                };

                dto.Items.Add(itemDto);
            }

            await _bus.PublishAsync(ExchangeNames.DTO, dto);
        }
    }
}
