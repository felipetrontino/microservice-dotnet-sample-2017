using Book.Models.Message;
using Book.Models.Payload;
using Book.Models.Proxy;
using Framework.Core.Pagination;
using System;
using System.Threading.Tasks;

namespace Book.Core.Interfaces
{
    public interface IBookService
    {
        Task SaveAsync(BookMessage model);

        Task<BookProxy> GetByIdAsync(Guid id);

        Task<PagedResponse<BookProxy>> GetAllAsync(BookFilterPayload pagination);
    }
}