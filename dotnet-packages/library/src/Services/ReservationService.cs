using Framework.Core.Bus;
using Framework.Core.Pagination;
using Library.Core.Common;
using Library.Core.Enums;
using Library.Core.Interfaces;
using Library.Data;
using Library.Entities;
using Library.Models.Message;
using Library.Models.Payload;
using Library.Models.Proxy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DbLibrary _db;
        private readonly IBusPublisher _bus;

        public ReservationService(DbLibrary db, IBusPublisher bus)
        {
            _db = db;
            _bus = bus;
        }

        public async Task RequestAsync(ReservationMessage message)
        {
            var reservation = await _db.Reservations
                .Include(x => x.Loans)
                .Include(x => x.Member)
                .FirstOrDefaultAsync(x => x.Number == message.Number);

            if (reservation == null)
            {
                reservation = new Reservation
                {
                    RequestDate = DateTime.UtcNow,
                    Status = StatusReservation.Opened,
                    Number = message.Number,
                    Member = await _db.Members.FirstOrDefaultAsync(x => x.DocumentId == message.MemberId)
                };

                if (reservation.Member == null)
                {
                    reservation.Member = new Member
                    {
                        DocumentId = message.MemberId,
                        Name = message.MemberName
                    };
                }

                reservation.Loans = new List<Loan>();

                foreach (var item in message.Items)
                {
                    var loan = await GetLoanAsync(_db, item, reservation.RequestDate);
                    reservation.Loans.Add(loan);
                }

                await _db.AddAsync(reservation);
            }
            else
            {
                reservation.Loans = new List<Loan>();

                foreach (var item in message.Items)
                {
                    var loan = await GetLoanAsync(_db, item, reservation.RequestDate);
                    reservation.Loans.Add(loan);
                }

                _db.Update(reservation);
            }

            await _db.SaveChangesAsync();

            await SendDtoAsync(reservation.Id);
        }

        public async Task ReturnAsync(ReservationReturnMessage message)
        {
            var reservation = await _db.Reservations
                                        .Include(x => x.Loans)
                                        .FirstOrDefaultAsync(x => x.Number == message.Number);

            reservation.Status = StatusReservation.Deliveried;

            foreach (var item in reservation.Loans)
            {
                item.ReturnDate = message.Date;
            }

            await _db.SaveChangesAsync();

            await SendDtoAsync(reservation.Id);
        }

        public async Task CheckDueAsync()
        {
            var date = DateTime.UtcNow.Date.AddDays(1).AddSeconds(-1);

            var expiredList = await _db.Reservations
                                      .Where(x => x.Loans.Any(l => l.DueDate.Date <= date))
                                      .ToListAsync();

            foreach (var item in expiredList)
            {
                var message = new ReservationExpiredMessage
                {
                    ReservationId = item.Id
                };

                await _bus.PublishAsync(QueueNames.Library, message);
            }
        }

        public async Task ExpireAsync(ReservationExpiredMessage message)
        {
            var reservation = await _db.Reservations
             .Include(x => x.Loans)
             .FirstOrDefaultAsync(x => x.Id == message.ReservationId);

            reservation.Status = StatusReservation.Expired;
            _db.Update(reservation);

            await _db.SaveChangesAsync();
        }

        public async Task<PagedResponse<ReservationProxy>> GetByFilterAsync(PagedRequest<ReservationFilterPayload> pagination)
        {
            var query = _db.Reservations.AsQueryable();

            query = FilterQuery(query, pagination.Filter);

            return await PagedHelper.CreateAsync(query, pagination, MapEntityToProxy);
        }

        private static IQueryable<Entities.Reservation> FilterQuery(IQueryable<Reservation> query, ReservationFilterPayload filter)
        {
            if (filter.Number != null)
                query = query.Where(_ => _.Number == filter.Number);

            return query;
        }

        private async Task<Loan> GetLoanAsync(DbLibrary db, ReservationMessage.Item item, DateTime createDate)
        {
            var ret = new Loan
            {
                Book = await db.Books.FirstOrDefaultAsync(x => x.Title.Contains(item.Name)),
                Copy = await db.Copies.FirstOrDefaultAsync(x => x.Number == item.Number),
                DueDate = createDate.AddDays(7)
            };

            return ret;
        }

        private async Task SendDtoAsync(Guid id)
        {
            var message = new ReservationDtoMessage
            {
                ReservationId = id
            };

            await _bus.PublishAsync(QueueNames.Library, message);
        }

        private static ReservationProxy MapEntityToProxy(Reservation e)
        {
            var ret = new ReservationProxy
            {
                MemberId = e.Member.UserId,
                Number = e.Number,
                Items = e.Loans.Select(x => new ReservationProxy.Item()
                {
                    Number = x.Copy.Number,
                    Name = x.Book.Title
                }).ToList()
            };

            return ret;
        }
    }
}