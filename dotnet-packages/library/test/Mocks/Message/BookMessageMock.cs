using Framework.Test.Common;
using Library.Models.Message;
using Library.Tests.Utils;

namespace Library.Tests.Mocks.Message
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
