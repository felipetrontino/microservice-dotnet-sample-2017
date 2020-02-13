using Bookstore.Core.Common;
using Bookstore.Data;
using Bookstore.Models.Event;
using Bookstore.Models.Message;
using Bookstore.Services;
using Bookstore.Tests.Mocks.Entities;
using Bookstore.Tests.Mocks.Message;
using Bookstore.Tests.Mocks.Models.Event;
using FluentAssertions;
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

            var dto = PublishShippingEventAsync(message);
            dto.Should().NotBeNull();

            var dtoExpected = CreateShippingEventMock.Get(key);
            dto.Should().BeEquivalentToMessage(dtoExpected);
        }

        #endregion CreateShippingAsync

        #region Utils

        private CreateShippingEvent PublishShippingEventAsync(ShippingEventMessage message)
        {
            var bus = BusPublisherStub.Create();

            var service = new PublishEventService(Db, bus);
            service.PublishShippingEventAsync(message).Wait();

            return bus.DequeueExchange<CreateShippingEvent>(ExchangeNames.Bookstore);
        }

        #endregion Utils
    }
}