using Book.Entities;
using Framework.Test.Common;
using System;

namespace Book.Tests.Mocks.Entities
{
    public class BookCategoryBookMock : MockBuilder<BookCategoryBookMock, BookCategoryBook>
    {
        public static BookCategoryBook Get(Book.Entities.Book book, BookCategory category)
        {
            var ret = Create().Default().Build();
            ret.Book = book;
            ret.Category = category;

            return ret;
        }

        public BookCategoryBookMock Default()
        {
            return this;
        }
    }
}
