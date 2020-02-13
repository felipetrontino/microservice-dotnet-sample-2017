using Book.Models.Proxy;
using Book.Tests.Utils;
using Framework.Test.Common;

namespace Book.Tests.Mocks.Models.Payload
{
    public class BookProxyMock : MockBuilder<BookProxyMock, BookProxy>
    {
        public static BookProxy Get(string key)
        {
            return Create(key).Default().Build();
        }

        public BookProxyMock Default()
        {
            Value.Author = Fake.GetAuthorName(Key);
            Value.Title = Fake.GetTitle(Key);
            Value.Language = Fake.GetLanguage(Key);

            return this;
        }
    }
}