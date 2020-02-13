using Book.Models.Payload;
using Framework.Test.Common;

namespace Book.Tests.Mocks.Models.Payload
{
    public class BookFilterPayloadMock : MockBuilder<BookFilterPayloadMock, BookFilterPayload>
    {
        public static BookFilterPayload Get()
        {
            return Create(null).Default().Build();
        }

        public BookFilterPayloadMock Default()
        {
            return this;
        }
    }
}