using Bookstore.Entities;
using Bookstore.Tests.Utils;
using Framework.Test.Common;

namespace Bookstore.Tests.Mocks.Entities
{
    public class BookMock : MockBuilder<BookMock, Book>
    {
        public static Book Get(string key)
        {
            return Create(key).Default().Build();
        }

        public BookMock Default()
        {
            Value.Author = Fake.GetAuthorName(Key);
            Value.Title = Fake.GetTitle(Key);
            Value.Language = Fake.GetLanguage(Key);

            return this;
        }
    }
}
