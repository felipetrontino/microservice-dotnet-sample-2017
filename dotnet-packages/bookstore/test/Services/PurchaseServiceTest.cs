using FluentAssertions;
using Bookstore.Core.Common;
using Bookstore.Data;
using Bookstore.Models.Message;
using Bookstore.Services;
using Bookstore.Tests.Mocks.Entities;
using Bookstore.Tests.Mocks.Message;
using Bookstore.Tests.Utils;
using Framework.Core.Config;
using Framework.Test.Common;
using Framework.Test.Data;
using Framework.Test.Mock.Bus;
using Framework.Test.Mock.Common;
using System;
using System.Linq;
using Xunit;

namespace Bookstore.Tests.Services
{
    public class PurchaseServiceTest : BaseTest
    {
        protected DbBookstore Db { get; }
        protected IMockRepository MockRepository { get; }

        public PurchaseServiceTest()
        {
            Db = MockHelper.GetDbContext<DbBookstore>();
            MockRepository = new EFMockRepository(Db);
        }

        #region CreateAsync

        [Fact]
        public void CreateAsync_Purschase_Insert_Valid()
        {
            var key = FakeHelper.Key;

            var message = PurchaseMessageMock.Get(key);

            var (dto, proxy) = CreateAsync(message);
            dto.Should().NotBeNull();

            var entity = Db.Orders.FirstOrDefault(x => x.Number == Fake.GetOrderNumber(key));
            entity.Should().NotBeNull();

            var expected = OrderMock.GetByPurchase(key);
            expected.Id = entity.Id;
            expected.Customer.Id = entity.Customer.Id;
            expected.Items[0].Id = entity.Items[0].Id;

            entity.Should().BeEquivalentToEntity(expected);

            var dtoExpected = ShippingDtoMessageMock.Get(key);
            dtoExpected.OrderId = entity.Id;
            dto.Should().BeEquivalentToMessage(dtoExpected);
        }

        [Fact]
        public void CreateAsync_Purschase_Insert_IntegrationWithLibrary_Valid()
        {
            var key = FakeHelper.Key;

            var message = PurchaseMessageMock.Get(key);

            var settings = Settings.Empty;
            settings.Preferences.TryAdd(Preferences.IntegrationWithLibrary, "true");

            var (dto, proxy) = CreateAsync(message, settings);
            dto.Should().NotBeNull();

            var entity = Db.Orders.FirstOrDefault(x => x.Number == Fake.GetOrderNumber(key));
            entity.Should().NotBeNull();

            var expected = OrderMock.GetByPurchase(key);
            expected.Id = entity.Id;
            expected.Customer.Id = entity.Customer.Id;
            expected.Items[0].Id = entity.Items[0].Id;

            entity.Should().BeEquivalentToEntity(expected);

            var dtoExpected = ShippingDtoMessageMock.Get(key);
            dtoExpected.OrderId = entity.Id;
            dto.Should().BeEquivalentToMessage(dtoExpected);

            var proxyExpectd = DropCopyNumberMessageMock.Get(key);
            proxy.Should().BeEquivalentToMessage(proxyExpectd);
        }

        [Fact]
        public void CreateAsync_Purschase_Update_Valid()
        {
            var key = FakeHelper.Key;
            var order = OrderMock.GetByPurchase(key);

            MockRepository.Add(order);

            var message = PurchaseMessageMock.Get(key);

            var (dto, proxy) = CreateAsync(message);
            dto.Should().NotBeNull();

            var entity = Db.Orders.FirstOrDefault(x => x.Number == Fake.GetOrderNumber(key));
            entity.Should().NotBeNull();

            var expected = OrderMock.GetByPurchase(key);
            expected.UpdatedAt = DateTime.UtcNow;
            expected.Items[0].Id = entity.Items[0].Id;
            entity.Should().BeEquivalentToEntity(expected);

            var dtoExpected = ShippingDtoMessageMock.Get(key);
            dtoExpected.OrderId = entity.Id;
            dto.Should().BeEquivalentToMessage(dtoExpected);
        }

        [Fact]
        public void CreateAsync_Purschase_Update_IntegrationWithLibrary_Valid()
        {
            var key = FakeHelper.Key;
            var order = OrderMock.GetByPurchase(key);

            MockRepository.Add(order);

            var message = PurchaseMessageMock.Get(key);

            var settings = Settings.Empty;
            settings.Preferences.TryAdd(Preferences.IntegrationWithLibrary, "true");

            var (dto, proxy) = CreateAsync(message, settings);
            dto.Should().NotBeNull();

            var entity = Db.Orders.FirstOrDefault(x => x.Number == Fake.GetOrderNumber(key));
            entity.Should().NotBeNull();

            var expected = OrderMock.GetByPurchase(key);
            expected.UpdatedAt = DateTime.UtcNow;
            expected.Items[0].Id = entity.Items[0].Id;
            entity.Should().BeEquivalentToEntity(expected);

            var dtoExpected = ShippingDtoMessageMock.Get(key);
            dtoExpected.OrderId = entity.Id;
            dto.Should().BeEquivalentToMessage(dtoExpected);

            var proxyExpectd = DropCopyNumberMessageMock.Get(key);
            proxy.Should().BeEquivalentToMessage(proxyExpectd);
        }

        [Fact]
        public void CreateAsync_Purschase_Update_Customer_NotAccepted()
        {
            var key = FakeHelper.Key;
            var order = OrderMock.GetByPurchase(key);
            MockRepository.Add(order);

            var message = PurchaseMessageMock.Get(key);
            var key2 = FakeHelper.Key;
            message.CustomerName = Fake.GetCustomerName(key2);
            message.CustomerId = FakeHelper.GetId(key2).ToString();

            var (dto, proxy) = CreateAsync(message);
            dto.Should().NotBeNull();

            var entity = Db.Orders.FirstOrDefault(x => x.Number == Fake.GetOrderNumber(key));
            entity.Should().NotBeNull();

            var expected = OrderMock.GetByPurchase(key);
            expected.UpdatedAt = DateTime.UtcNow;
            expected.Items[0].Id = entity.Items[0].Id;
            entity.Should().BeEquivalentToEntity(expected);

            var dtoExpected = ShippingDtoMessageMock.Get(key);
            dtoExpected.OrderId = entity.Id;
            dto.Should().BeEquivalentToMessage(dtoExpected);
        }

        #endregion CreateAsync

        #region Utils

        private (ShippingDtoMessage Dto, DropCopyNumberMessage Proxy) CreateAsync(PurchaseMessage message, Settings settings = null)
        {
            settings = settings ?? Settings.Empty;

            var config = ConfigurationStub.Create(() =>
            {
                return settings;
            });

            var bus = BusPublisherStub.Create();

            var service = new PurchaseService(Db, bus, config, TenantAccessorStub.Create());
            service.CreateAsync(message).Wait();

            var drop = bus.Dequeue<DropCopyNumberMessage>(QueueNames.Library);
            var payload = bus.Dequeue<ShippingDtoMessage>(QueueNames.Bookstore);

            return (payload, drop);
        }

        #endregion Utils

    }
}
