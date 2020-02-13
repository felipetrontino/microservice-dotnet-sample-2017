using Framework.Core.Bus;
using Library.Core.Common;
using Library.Core.Interfaces;
using Library.Data;
using Library.Models.Dto;
using Library.Models.Message;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class PublishEventService : IPublishEventService
    {
        private readonly DbLibrary _db;
        private readonly IBusPublisher _bus;

        public PublishEventService(DbLibrary db, IBusPublisher bus)
        {
            _db = db;
            _bus = bus;
        }

        public async Task PublishReservationEventAsync(ReservationEventMessage message)
        {
            var reservation = await _db.Reservations
                                       .Include(x => x.Loans)
                                         .ThenInclude(x => x.Book)
                                       .Include(x => x.Loans)
                                         .ThenInclude(x => x.Copy)
                                       .Include(x => x.Member)
                                       .FirstOrDefaultAsync(x => x.Id == message.ReservationId);

            var dto = new CreateReservationEvent
            {
                Number = reservation.Number,
                RequestDate = reservation.RequestDate,
                Status = reservation.Status,

                Member = new CreateReservationEvent.MemberDetail()
                {
                    DocumentId = reservation.Member.DocumentId,
                    Name = reservation.Member.Name
                },

                Loans = new List<CreateReservationEvent.LoanDetail>()
            };

            foreach (var item in reservation.Loans)
            {
                var loanDto = new CreateReservationEvent.LoanDetail()
                {
                    CopyNumber = item.Copy.Number,
                    Title = item.Book.Title,
                    DueDate = item.DueDate,
                    ReturnDate = item.ReturnDate
                };

                dto.Loans.Add(loanDto);
            }

            await _bus.PublishAsync(ExchangeNames.Library, dto);
        }
    }
}