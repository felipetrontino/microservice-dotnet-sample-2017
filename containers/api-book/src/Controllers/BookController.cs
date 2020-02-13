using Book.Core.Interfaces;
using Book.Models.Message;
using Book.Models.Payload;
using Book.Models.Proxy;
using Framework.Web.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Book.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ApiController
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookProxy), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Save(BookMessage book)
        {
            await _service.SaveAsync(book);
            return Ok();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookProxy), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookProxy>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] BookFilterPayload filter)
        {
            var result = await _service.GetAllAsync(filter);
            return PagedOk(result);
        }
    }
}