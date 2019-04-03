using Book.Models.Message;
using Book.Tests.Utils;
using Framework.Test.Common;

namespace Book.Tests.Mocks.Message
{
    public class BookUpdateMessageMock : MockBuilder<BookUpdateMessageMock, BookUpdateMessage>
    {
        public static BookUpdateMessage Get(string key)
        {
            return Create(key)
                .Default()
                .Build();
        }

        public BookUpdateMessageMock Default()
        {
            Value.Author = Fake.GetAuthorName(Key);
            Value.Title = Fake.GetTitle(Key);
            Value.Language = Fake.GetLanguage(Key);

            return this;
        }
    }
}
