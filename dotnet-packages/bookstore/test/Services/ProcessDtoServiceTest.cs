using FluentAssertions;
using Bookstore.Core.Common;
using Bookstore.Data;
using Bookstore.Models.Dto;
using Bookstore.Models.Message;
using Bookstore.Services;
using Bookstore.Tests.Mocks.Dto;
using Bookstore.Tests.Mocks.Entities;
using Bookstore.Tests.Mocks.Message;
using Framework.Test.Common;
using Framework.Test.Data;
using Framework.Test.Mock.Bus;
using Xunit;

namespace Bookstore.Tests.Services
{
    public class ProcessDtoServiceTest : BaseTest
    {
        protected DbBookstore Db { get; }
        protected IMockRepository MockRepository { get; }

        public ProcessDtoServiceTest()
        {
            Db = MockHelper.GetDbContext<DbBookstore>();
            MockRepository = new EFMockRepository(Db);
        }

        #region CreateShippingAsync

        [Fact]
        public void CreateShippingAsync_Dto_Valid()
        {
            var key = FakeHelper.Key;

            var order = OrderMock.Get(key);
            MockRepository.Add(order);

            var message = ShippingDtoMessageMock.Get(key);

            var dto = CreateShippingAsync(message);
            dto.Should().NotBeNull();

            var dtoExpected = ShippingDtoMock.Get(key);
            dto.Should().BeEquivalentToMessage(dtoExpected);
        }

        #endregion CreateShippingAsync

        #region Utils

        private ShippingDto CreateShippingAsync(ShippingDtoMessage message)
        {
            var bus = BusPublisherStub.Create();

            var service = new ProcessDtoService(Db, bus);
            service.CreateShippingAsync(message).Wait();

            return bus.DequeueExchange<ShippingDto>(ExchangeNames.DTO);
        }

        #endregion Utils

    }
}
