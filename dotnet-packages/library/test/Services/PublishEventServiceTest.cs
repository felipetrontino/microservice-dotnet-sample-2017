using FluentAssertions;
using Framework.Test.Common;
using Framework.Test.Data;
using Framework.Test.Mock.Bus;
using Library.Core.Common;
using Library.Data;
using Library.Models.Dto;
using Library.Models.Message;
using Library.Services;
using Library.Tests.Mocks.Entities;
using Library.Tests.Mocks.Models.Event;
using Library.Tests.Mocks.Models.Message;
using Xunit;

namespace Library.Tests.Services
{
    public class PublishEventServiceTest : BaseTest
    {
        protected DbLibrary Db { get; }
        protected IMockRepository MockRepository { get; }

        public PublishEventServiceTest()
        {
            Db = MockHelper.GetDbContext<DbLibrary>();
            MockRepository = new EFMockRepository(Db);
        }

        #region CreateReservationAsync

        [Fact]
        public void PublishReservationEventAsync_Dto_Valid()
        {
            var key = FakeHelper.Key;
            var reservation = ReservationMock.Get(key);
            MockRepository.Add(reservation);

            var message = ReservationEventMessageMock.Get(key);

            var dto = PublishReservationEventAsync(message);
            dto.Should().NotBeNull();

            var dtoExpected = CreateReservationEventMock.Get(key);
            dto.Should().BeEquivalentToMessage(dtoExpected);
        }

        #endregion CreateReservationAsync

        #region Utils

        private CreateReservationEvent PublishReservationEventAsync(ReservationEventMessage message)
        {
            var bus = BusPublisherStub.Create();

            var service = new PublishEventService(Db, bus);
            service.PublishReservationEventAsync(message).Wait();

            return bus.DequeueExchange<CreateReservationEvent>(ExchangeNames.Library);
        }

        #endregion Utils
    }
}