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
    public class ProcessDtoService : IProcessDtoService
    {
        private readonly DbLibrary _db;
        private readonly IBusPublisher _bus;

        public ProcessDtoService(DbLibrary db, IBusPublisher bus)
        {
            _db = db;
            _bus = bus;
        }

        public async Task CreateReservationAsync(ReservationDtoMessage message)
        {
            var reservation = await _db.Reservations
                                       .Include(x => x.Loans)
                                         .ThenInclude(x => x.Book)
                                       .Include(x => x.Loans)
                                         .ThenInclude(x => x.Copy)
                                       .Include(x => x.Member)
                                       .FirstOrDefaultAsync(x => x.Id == message.ReservationId);

            var dto = new ReservationDto();
            dto.Id = reservation.Id;
            dto.Number = reservation.Number;
            dto.RequestDate = reservation.RequestDate;
            dto.Status = reservation.Status;

            dto.Member = new ReservationDto.MemberDetail()
            {
                Id = reservation.Member.Id,
                DocumentId = reservation.Member.DocumentId,
                Name = reservation.Member.Name
            };

            dto.Loans = new List<ReservationDto.LoanDetail>();

            foreach (var item in reservation.Loans)
            {
                var loanDto = new ReservationDto.LoanDetail()
                {
                    Id = item.Id,
                    CopyNumber = item.Copy.Number,
                    Title = item.Book.Title,
                    DueDate = item.DueDate,
                    ReturnDate = item.ReturnDate
                };

                dto.Loans.Add(loanDto);
            }

            await _bus.PublishAsync(ExchangeNames.DTO, dto);
        }
    }
}
