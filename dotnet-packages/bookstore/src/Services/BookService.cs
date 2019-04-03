using Bookstore.Core.Interfaces;
using Bookstore.Data;
using Bookstore.Models.Message;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class BookService : IBookService
    {
        private readonly DbBookstore _db;

        public BookService(DbBookstore db)
        {
            _db = db;
        }

        public async Task UpdateAsync(BookMessage message)
        {
            var book = await _db.Books.FirstOrDefaultAsync(x => x.Title == message.Title);

            if (book != null)
            {
                book.Author = message.Author;
                book.Language = message.Language;

                _db.Update(book);

                await _db.SaveChangesAsync();
            }
        }
    }
}
