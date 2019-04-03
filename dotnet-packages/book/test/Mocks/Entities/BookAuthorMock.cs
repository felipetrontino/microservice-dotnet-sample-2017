using Book.Entities;
using Book.Tests.Utils;
using Framework.Test.Common;

namespace Book.Tests.Mocks.Entities
{
    public class BookAuthorMock : MockBuilder<BookAuthorMock, BookAuthor>
    {
        public static BookAuthor Get(string key)
        {
            return Create(key).Default().Build();
        }

        public BookAuthorMock Default()
        {
            Value.Name = Fake.GetAuthorName(Key);

            return this;
        }
    }
}
