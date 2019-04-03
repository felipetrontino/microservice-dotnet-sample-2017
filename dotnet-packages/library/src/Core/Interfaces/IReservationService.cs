using Library.Models.Message;
using System.Threading.Tasks;
using Framework.Core.Pagination;
using Library.Models.Payload;
using Library.Models.Proxy;

namespace Library.Core.Interfaces
{
    public interface IReservationService
    {
        Task RequestAsync(ReservationMessage message);

        Task ReturnAsync(ReservationReturnMessage message);

        Task CheckDueAsync();

        Task ExpireAsync(ReservationExpiredMessage message);
        Task<PagedResponse<ReservationProxy>> GetByFilterAsync(PagedRequest<ReservationFilterPayload> pagination);
    }
}
