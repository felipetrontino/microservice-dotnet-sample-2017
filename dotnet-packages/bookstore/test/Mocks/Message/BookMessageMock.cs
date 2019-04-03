using Bookstore.Models.Message;
using Bookstore.Tests.Utils;
using Framework.Test.Common;

namespace Bookstore.Tests.Mocks.Message
{
    public class BookMessageMock : MockBuilder<BookMessageMock, BookMessage>
    {
        public static BookMessage Get(string key)
        {
            return Create(key).Default().Build();
        }

        public BookMessageMock Default()
        {
            Value.Author = Fake.GetAuthorName(Key);
            Value.Title = Fake.GetTitle(Key);
            Value.Language = Fake.GetLanguage(Key);

            return this;
        }
    }
}
