using FluentAssertions;
using Framework.Test.Common;
using Framework.Test.Data;
using Framework.Test.Mock.Bus;
using Library.Core.Common;
using Library.Data;
using Library.Models.Dto;
using Library.Models.Message;
using Library.Services;
using Library.Tests.Mocks.Dto;
using Library.Tests.Mocks.Entities;
using Library.Tests.Mocks.Message;
using Xunit;

namespace Library.Tests.Services
{
    public class ProcessDtoServiceTest : BaseTest
    {
        protected DbLibrary Db { get; }
        protected IMockRepository MockRepository { get; }

        public ProcessDtoServiceTest()
        {
            Db = MockHelper.GetDbContext<DbLibrary>();
            MockRepository = new EFMockRepository(Db);
        }

        #region CreateReservationAsync

        [Fact]
        public void CreateReservationAsync_Dto_Valid()
        {
            var key = FakeHelper.Key;
            var reservation = ReservationMock.Get(key);
            MockRepository.Add(reservation);

            var message = ReservationDtoMessageMock.Get(key);

            var dto = CreateReservationAsync(message);
            dto.Should().NotBeNull();

            var dtoExpected = ReservationDtoMock.Get(key);
            dto.Should().BeEquivalentToMessage(dtoExpected);
        }

        #endregion CreateReservationAsync

        #region Utils

        private ReservationDto CreateReservationAsync(ReservationDtoMessage message)
        {
            var bus = BusPublisherStub.Create();

            var service = new ProcessDtoService(Db, bus);
            service.CreateReservationAsync(message).Wait();

            return bus.DequeueExchange<ReservationDto>(ExchangeNames.DTO);
        }

        #endregion Utils

    }
}
