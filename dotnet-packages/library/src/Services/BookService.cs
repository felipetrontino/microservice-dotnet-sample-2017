using Library.Core.Interfaces;
using Library.Data;
using Library.Models.Message;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly DbLibrary _db;

        public BookService(DbLibrary db)
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
