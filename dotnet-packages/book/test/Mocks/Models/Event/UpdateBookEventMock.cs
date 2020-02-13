using Book.Models.Message;
using Book.Tests.Utils;
using Framework.Test.Common;

namespace Book.Tests.Mocks.Models.Event
{
    public class UpdateBookEventMock : MockBuilder<UpdateBookEventMock, UpdateBookEvent>
    {
        public static UpdateBookEvent Get(string key)
        {
            return Create(key)
                .Default()
                .Build();
        }

        public UpdateBookEventMock Default()
        {
            Value.Author = Fake.GetAuthorName(Key);
            Value.Title = Fake.GetTitle(Key);
            Value.Language = Fake.GetLanguage(Key);

            return this;
        }
    }
}