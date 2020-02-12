using Book.Core.Common;
using Book.Core.Enums;
using Book.Core.Extensions;
using Book.Core.Interfaces;
using Book.Data;
using Book.Models.Message;
using Book.Models.Payload;
using Book.Models.Proxy;
using Framework.Core.Bus;
using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Extensions;
using Framework.Core.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Services
{
    public class BookService : IBookService
    {
        private readonly ValueGetter<bool> FeatureAddTableAtuthor;
        private readonly DbBook _db;
        private readonly IBusPublisher _bus;

        public BookService(DbBook db, IBusPublisher bus, IConfiguration configuration, ITenantAccessor tenantAccessor)
        {
            _db = db;
            _bus = bus;

            var settings = Settings.GetInstance(configuration, tenantAccessor.Tenant);
            FeatureAddTableAtuthor = new ValueGetter<bool>(() => settings.FeatureFlags.GetOrDefault(FeatureFlags.AddTableAtuthor));
        }

        public async Task SaveAsync(BookMessage model)
        {
            bool inserted = false;
            var book = await _db.Books.FilterTitle(model.Title).FirstOrDefaultAsync();

            if (book == null)
            {
                inserted = true;
                book = new Entities.Book();
            }

            book.Title = model.Title;
            book.Language = model.Language;

            if (model.Category != null)
            {
                var category = await _db.Categories.FirstOrDefaultAsync(x => x.Name == model.Category);

                if (category == null)
                {
                    category = new Entities.BookCategory
                    {
                        Name = model.Category
                    };
                }

                if (!book.Categories.Any(x => x.Category.Name == category.Name))
                {
                    book.Categories.Add(new Entities.BookCategoryBook() { Category = category });
                }
            }

            var authorName = model.Author;

            if (FeatureAddTableAtuthor.Get())
            {
                book.Author = await _db.Authors.FirstOrDefaultAsync(x => x.Name == authorName);

                if (book.Author == null)
                {
                    book.Author = new Entities.BookAuthor
                    {
                        Name = authorName
                    };
                }
            }
            else
            {
                book.AuthorName = authorName;
            }

            if (inserted)
                await _db.AddAsync(book);
            else
                _db.Update(book);

            await _db.SaveChangesAsync();

            var message = new BookUpdateMessage
            {
                Id = book.Id,
                Title = book.Title,
                Author = authorName,
                Language = book.Language
            };

            await _bus.PublishAsync(ExchangeNames.Book, message);
        }

        public async Task<BookProxy> GetByIdAsync(Guid id)
        {
            return await _db.Books
                 .Include(x => x.Author)
                 .Where(x => x.Id == id)
                 .Select(x => MapEntityToProxy(x))
                 .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<BookProxy>> GetAllAsync(PagedRequest pagination)
        {
            var query = _db.Books
                        .Include(x => x.Author)
                        .AsQueryable();

            return await PagedHelper.CreateAsync(query, pagination, MapEntityToProxy);
        }

        public async Task<PagedResponse<BookProxy>> GetByFilterAsync(PagedRequest<BookFilterPayload> pagination)
        {
            var query = _db.Books.AsQueryable();
            query = FilterQuery(query, pagination.Filter);

            return await PagedHelper.CreateAsync(query, pagination, MapEntityToProxy);
        }

        private static IQueryable<Entities.Book> FilterQuery(IQueryable<Entities.Book> query, BookFilterPayload filter)
        {
            if (filter.Title != null)
                query = query.Where(_ => _.Title == filter.Title);

            if (filter.Category != null)
                query = query.Where(_ => _.Categories.Any(c => c.Category.Name == filter.Category));

            if (filter.Language != null && filter.Language != Language.Unknown)
                query = query.Where(_ => _.Language == filter.Language);

            return query;
        }

        private BookProxy MapEntityToProxy(Entities.Book e)
        {
            var ret = new BookProxy
            {
                Id = e.Id,
                Title = e.Title,
                Author = e.AuthorName
            };

            if (FeatureAddTableAtuthor.Get())
                ret.Author = e.Author?.Name;

            ret.Language = e.Language;

            return ret;
        }
    }
}