using Book.Core.Interfaces;
using Book.Models.Payload;
using Book.Models.Proxy;
using Framework.Core.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Book.Api.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookProxy), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<BookProxy>> Get(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PagedResponse<BookProxy>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<PagedResponse<BookProxy>> GetByFilter(PagedRequest<BookFilterPayload> pagination)
        {
            return await _service.GetByFilterAsync(pagination);
        }

        [HttpPost("all")]
        [ProducesResponseType(typeof(IEnumerable<BookProxy>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<PagedResponse<BookProxy>> GetAll(PagedRequest pagination)
        {
            return await _service.GetAllAsync(pagination);
        }
    }
}
