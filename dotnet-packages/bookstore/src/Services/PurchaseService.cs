using Bookstore.Core.Common;
using Bookstore.Core.Enums;
using Bookstore.Core.Interfaces;
using Bookstore.Data;
using Bookstore.Entities;
using Bookstore.Models.Message;
using Framework.Core.Bus;
using Framework.Core.Extensions;
using Framework.Core.Common;
using Framework.Core.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ValueGetter<bool> IntegrationWithLibrary;

        private readonly DbBookstore _db;
        private readonly IBusPublisher _bus;

        public PurchaseService(DbBookstore db, IBusPublisher bus, IConfiguration configuration, ITenantAccessor tenantAccessor)
        {
            _db = db;
            _bus = bus;

            var settings = Settings.GetInstance(configuration, tenantAccessor.Tenant);
            IntegrationWithLibrary = new ValueGetter<bool>(() => settings.Preferences.GetOrDefault(Preferences.IntegrationWithLibrary).ToBoolean());
        }

        public async Task CreateAsync(PurchaseMessage message)
        {
            var numbersDropped = new List<DropCopyNumberMessage>();

            var order = await _db.Orders
                .Include(x => x.Items)
                    .ThenInclude(x => x.Book)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Number == message.Number);

            if (order == null)
            {
                order = new Order
                {
                    Number = message.Number,
                    Status = StatusOrder.Opened,
                    CreateDate = DateTime.UtcNow,
                    Customer = await _db.Customers.FirstOrDefaultAsync(x => x.DocumentId == message.CustomerId)
                };

                if (order.Customer == null)
                {
                    order.Customer = new Customer
                    {
                        DocumentId = message.CustomerId,
                        Name = message.CustomerName
                    };
                }

                order.Items = new List<OrderItem>();

                foreach (var item in message.Items)
                {
                    var orderItem = await GetItemAsync(_db, item);

                    if (IntegrationWithLibrary.Get())
                        numbersDropped.Add(new DropCopyNumberMessage() { Number = item.Number });

                    order.Items.Add(orderItem);
                }

                await _db.AddAsync(order);
            }
            else
            {
                order.Items = new List<OrderItem>();

                foreach (var item in message.Items)
                {
                    var orderItem = await GetItemAsync(_db, item);

                    if (IntegrationWithLibrary.Get())
                        numbersDropped.Add(new DropCopyNumberMessage() { Number = item.Number });

                    order.Items.Add(orderItem);
                }

                _db.Update(order);
            }

            await _db.SaveChangesAsync();

            if (IntegrationWithLibrary.Get())
                await _bus.PublishAllAsync(QueueNames.Library, numbersDropped);

            var dto = new ShippingDtoMessage
            {
                OrderId = order.Id
            };

            await _bus.PublishAsync(QueueNames.Bookstore, dto);
        }

        private async Task<OrderItem> GetItemAsync(DbBookstore db, PurchaseMessage.Item item)
        {
            var ret = new OrderItem
            {
                Book = await db.Books.FirstOrDefaultAsync(x => x.Title.Contains(item.Name)),
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity
            };

            ret.Total = ret.Price * ret.Quantity;

            return ret;
        }
    }
}
