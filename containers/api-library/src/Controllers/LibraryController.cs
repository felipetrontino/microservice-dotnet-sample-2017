using Framework.Core.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Library.Core.Interfaces;
using Library.Models.Payload;
using Library.Models.Proxy;

namespace Library.Api.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public LibraryController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("getByFilter")]
        [ProducesResponseType(typeof(PagedResponse<ReservationProxy>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<PagedResponse<ReservationProxy>> GetByFilter(PagedRequest<ReservationFilterPayload> pagination)
        {
            return await _reservationService.GetByFilterAsync(pagination);
        }
    }
}
