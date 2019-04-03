using FluentAssertions;
using Book.Core.Common;
using Book.Core.Enums;
using Book.Data;
using Book.Models.Message;
using Book.Models.Payload;
using Book.Models.Proxy;
using Book.Services;
using Book.Tests.Mocks.Entities;
using Book.Tests.Mocks.Message;
using Book.Tests.Mocks.Payload;
using Book.Tests.Utils;
using Framework.Core.Config;
using Framework.Core.Pagination;
using Framework.Test.Common;
using Framework.Test.Data;
using Framework.Test.Mock.Bus;
using Framework.Test.Mock.Common;
using Framework.Test.Mock.Pagination;
using System;
using System.Linq;
using Xunit;

namespace Book.Tests.Services
{
    public class BookServiceTest : BaseTest
    {
        protected DbBook Db { get; }
        protected IMockRepository MockRepository { get; }

        public BookServiceTest()
        {
            Db = MockHelper.GetDbContext<DbBook>();
            MockRepository = new EFMockRepository(Db);
        }

        #region SaveAsync

        [Fact]
        public void SaveAsync_Book_Insert_Valid()
        {
            var key = FakeHelper.Key;
            var category = BookCategoryMock.Get(key);
            MockRepository.Add(category);

            var model = BookMessageMock.Create(key)
                .Default()
                .WithCategory()
                .Build();

            var message = SaveAsync(model);
            message.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var expected = BookMock.Get(key);
            expected.Id = entity.Id;
            var expectedCategory = BookCategoryBookMock.Get(expected, entity.Categories[0].Category);
            expectedCategory.Id = entity.Categories[0].Id;
            expected.Categories.Add(expectedCategory);
            entity.Should().BeEquivalentToEntity(expected);

            var messageExpected = BookUpdateMessageMock.Get(key);
            messageExpected.Id = entity.Id;
            message.Should().BeEquivalentToMessage(messageExpected);
        }

        [Fact]
        public void SaveAsync_Book_Insert_FlagTableAtuthor_Author_NotExist()
        {
            var key = FakeHelper.Key;
            var category = BookCategoryMock.Get(key);
            MockRepository.Add(category);

            var model = BookMessageMock.Get(key);

            var settings = Settings.Empty;
            settings.FeatureFlags.TryAdd(FeatureFlags.AddTableAtuthor, true);

            var message = SaveAsync(model, settings);
            message.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var expected = BookMock.Get(key);
            expected.Id = entity.Id;
            expected.Author = BookAuthorMock.Get(key);
            expected.Author.Id = entity.Author.Id;
            expected.AuthorName = null;
            entity.Should().BeEquivalentToEntity(expected);

            var messageExpected = BookUpdateMessageMock.Get(key);
            messageExpected.Id = entity.Id;
            message.Should().BeEquivalentToMessage(messageExpected);
        }

        [Fact]
        public void SaveAsync_Book_Insert_FlagTableAtuthor_Author_Exist()
        {
            var key = FakeHelper.Key;
            var category = BookCategoryMock.Get(key);
            MockRepository.Add(category);
            var author = BookAuthorMock.Get(key);
            MockRepository.Add(author);

            var model = BookMessageMock.Get(key);

            var settings = Settings.Empty;
            settings.FeatureFlags.TryAdd(FeatureFlags.AddTableAtuthor, true);

            var message = SaveAsync(model, settings);
            message.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var expected = BookMock.Get(key);
            expected.Id = entity.Id;
            expected.Author = BookAuthorMock.Get(key);
            expected.AuthorName = null;
            entity.Should().BeEquivalentToEntity(expected);

            var messageExpected = BookUpdateMessageMock.Get(key);
            messageExpected.Id = entity.Id;
            message.Should().BeEquivalentToMessage(messageExpected);
        }

        [Fact]
        public void SaveAsync_Book_Insert_Category_NotExist()
        {
            var key = FakeHelper.Key;

            var model = BookMessageMock.Get(key);

            var message = SaveAsync(model);
            message.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var expected = BookMock.Get(key);
            expected.Id = entity.Id;
            entity.Should().BeEquivalentToEntity(expected);

            var messageExpected = BookUpdateMessageMock.Get(key);
            messageExpected.Id = entity.Id;
            message.Should().BeEquivalentToMessage(messageExpected);
        }

        [Fact]
        public void SaveAsync_Book_Update_Valid()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);

            MockRepository.Add(book);

            var model = BookMessageMock.Create(key)
                                        .Default()
                                        .WithCategory()
                                        .Build();

            var message = SaveAsync(model);
            message.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var expected = BookMock.Get(key);
            var expectedCategory = BookCategoryBookMock.Get(book, book.Categories[0].Category);
            expectedCategory.Id = entity.Categories[0].Id;
            expected.Categories.Add(expectedCategory);
            entity.Should().BeEquivalentToEntity(expected);

            var messageExpected = BookUpdateMessageMock.Get(key);
            messageExpected.Id = entity.Id;
            message.Should().BeEquivalentToMessage(messageExpected);
        }

        #endregion SaveAsync

        #region GetByIdAsync

        [Fact]
        public void GetByIdAsync_Book_Valid()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);
            MockRepository.Add(book);

            var proxy = GetByIdAsync(book.Id);
            proxy.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var proxyExpected = BookProxyMock.Get(key);
            proxyExpected.Id = entity.Id;
            proxy.Should().BeEquivalentToModel(proxyExpected);
        }

        [Fact]
        public void GetByIdAsync_Book_FlagTableAtuthor_WithAuthor()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);
            book.Author = BookAuthorMock.Get(key);
            MockRepository.Add(book);

            var settings = Settings.Empty;
            settings.FeatureFlags.TryAdd(FeatureFlags.AddTableAtuthor, true);

            var proxy = GetByIdAsync(book.Id, settings);
            proxy.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var proxyExpected = BookProxyMock.Get(key);
            proxyExpected.Id = entity.Id;
            proxy.Should().BeEquivalentToModel(proxyExpected);
        }

        [Fact]
        public void GetByIdAsync_Book_FlagTableAtuthor_WithoutAuthor()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);
            book.Author = null;
            MockRepository.Add(book);

            var settings = Settings.Empty;
            settings.FeatureFlags.TryAdd(FeatureFlags.AddTableAtuthor, true);

            var proxy = GetByIdAsync(book.Id, settings);
            proxy.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var proxyExpected = BookProxyMock.Get(key);
            proxyExpected.Id = entity.Id;
            proxyExpected.Author = null;
            proxy.Should().BeEquivalentToModel(proxyExpected);
        }

        [Fact]
        public void GetByIdAsync_Book_Invalid()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);
            MockRepository.Add(book);

            var book2 = BookMock.Get(FakeHelper.Key);

            var proxy = GetByIdAsync(book2.Id);
            proxy.Should().BeNull();
        }

        #endregion GetByIdAsync

        #region GetAllAsync

        [Fact]
        public void GetAllAsync_Book_Valid()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);
            MockRepository.Add(book);

            var pagedProxy = GetAllAsync();
            pagedProxy.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(key);
            proxy1.Id = entity.Id;

            var pagedProxyExpected = PagedResponseMock.Create(proxy1);
            pagedProxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetAllAsync_Book_FlagTableAtuthor_WithAuthor()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);
            book.Author = BookAuthorMock.Get(key);
            MockRepository.Add(book);

            var settings = Settings.Empty;
            settings.FeatureFlags.TryAdd(FeatureFlags.AddTableAtuthor, true);

            var pagedProxy = GetAllAsync(settings);
            pagedProxy.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(key);
            proxy1.Id = entity.Id;

            var pagedProxyExpected = PagedResponseMock.Create(proxy1);
            pagedProxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetAllAsync_Book_FlagTableAtuthor_WithoutAuthor()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);
            book.Author = null;
            MockRepository.Add(book);

            var settings = Settings.Empty;
            settings.FeatureFlags.TryAdd(FeatureFlags.AddTableAtuthor, true);

            var pagedProxy = GetAllAsync(settings);
            pagedProxy.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(key);
            proxy1.Id = entity.Id;
            proxy1.Author = null;

            var pagedProxyExpected = PagedResponseMock.Create(proxy1);
            pagedProxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetAllAsync_Book_Two_Valid()
        {
            var key = FakeHelper.Key;
            var book = BookMock.Get(key);
            MockRepository.Add(book);

            var key2 = FakeHelper.Key;
            var book2 = BookMock.Get(key2);
            MockRepository.Add(book2);

            var pagedProxy = GetAllAsync();
            pagedProxy.Should().NotBeNull();

            var entity = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key));
            entity.Should().NotBeNull();

            var entity2 = Db.Books.FirstOrDefault(x => x.Title == Fake.GetTitle(key2));
            entity2.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(key);
            proxy1.Id = entity.Id;

            var proxy2 = BookProxyMock.Get(key2);
            proxy2.Id = entity2.Id;

            var pagedProxyExpected = PagedResponseMock.Create(proxy1, proxy2);
            pagedProxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetAllAsync_Book_Empty()
        {
            var pagedProxy = GetAllAsync();
            pagedProxy.Should().NotBeNull();

            var pagedProxyExpected = PagedResponseMock.Create<BookProxy>();
            pagedProxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        #endregion GetAllAsync

        #region GetByFilterAsync

        [Fact]
        public void GetByFilterAsync_Valid()
        {
            var keys = new string[]
            {
                FakeHelper.Key,
                FakeHelper.Key
            };

            MockRepository.Add(BookMock.Get(keys[0]));
            MockRepository.Add(BookMock.Get(keys[1]));

            var pagination = PagedRequestMock.Create(BookFilterPayloadMock.Get());

            var proxy = GetByFilterAsync(pagination);
            proxy.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(keys[0]);
            var proxy2 = BookProxyMock.Get(keys[1]);

            var pagedProxyExpected = PagedResponseMock.Create(proxy1, proxy2);
            proxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetByFilterAsync_Book_FlagTableAtuthor_WithAuthor()
        {
            var keys = new string[]
            {
                FakeHelper.Key,
                FakeHelper.Key
            };
            var book1 = BookMock.Get(keys[0]);
            book1.Author = BookAuthorMock.Get(keys[0]);

            MockRepository.Add(book1);
            MockRepository.Add(BookMock.Get(keys[1]));

            var pagination = PagedRequestMock.Create(BookFilterPayloadMock.Get());

            var settings = Settings.Empty;
            settings.FeatureFlags.TryAdd(FeatureFlags.AddTableAtuthor, true);

            var pagedProxy = GetByFilterAsync(pagination, settings);
            pagedProxy.Should().NotBeNull();

            var proxy = GetByFilterAsync(pagination);
            proxy.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(keys[0]);
            var proxy2 = BookProxyMock.Get(keys[1]);

            var pagedProxyExpected = PagedResponseMock.Create(proxy1, proxy2);
            proxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetByFilterAsync_Book_FlagTableAtuthor_WithoutAuthor()
        {
            var keys = new string[]
            {
                FakeHelper.Key,
                FakeHelper.Key
            };

            var book1 = BookMock.Get(keys[0]);
            book1.Author = null;

            MockRepository.Add(book1);
            MockRepository.Add(BookMock.Get(keys[1]));

            var pagination = PagedRequestMock.Create(BookFilterPayloadMock.Get());

            var settings = Settings.Empty;
            settings.FeatureFlags.TryAdd(FeatureFlags.AddTableAtuthor, true);

            var pagedProxy = GetByFilterAsync(pagination, settings);
            pagedProxy.Should().NotBeNull();

            var proxy = GetByFilterAsync(pagination);
            proxy.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(keys[0]);
            var proxy2 = BookProxyMock.Get(keys[1]);

            var pagedProxyExpected = PagedResponseMock.Create(proxy1, proxy2);
            proxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetByFilterAsync_Filter_Title()
        {
            var keys = new string[]
            {
                FakeHelper.Key,
                FakeHelper.Key
            };

            MockRepository.Add(BookMock.Get(keys[0]));
            MockRepository.Add(BookMock.Get(keys[1]));

            var pagination = PagedRequestMock.Create(BookFilterPayloadMock.Get());
            pagination.Filter.Title = Fake.GetTitle(keys[0]);

            var proxy = GetByFilterAsync(pagination);
            proxy.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(keys[0]);

            var pagedProxyExpected = PagedResponseMock.Create(proxy1);
            proxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetByFilterAsync_Filter_Language()
        {
            var keys = new string[]
            {
                FakeHelper.Key,
                FakeHelper.Key
            };

            var book = BookMock.Get(keys[0]);
            book.Language = Language.Portuguese;

            MockRepository.Add(book);
            MockRepository.Add(BookMock.Get(keys[1]));

            var pagination = PagedRequestMock.Create(BookFilterPayloadMock.Get());
            pagination.Filter.Language = book.Language;

            var proxy = GetByFilterAsync(pagination);
            proxy.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(keys[0]);
            proxy1.Language = Language.Portuguese;

            var pagedProxyExpected = PagedResponseMock.Create(proxy1);
            proxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        [Fact]
        public void GetByFilterAsync_Filter_Category()
        {
            var keys = new string[]
            {
                FakeHelper.Key,
                FakeHelper.Key
            };

            var book = BookMock.Create(keys[0])
                                .Default()
                                .WithCategory()
                                .Build();

            MockRepository.Add(book);
            MockRepository.Add(BookMock.Get(keys[1]));

            var pagination = PagedRequestMock.Create(BookFilterPayloadMock.Get());
            pagination.Filter.Category = Fake.GetCategoryName(keys[0]);

            var proxy = GetByFilterAsync(pagination);
            proxy.Should().NotBeNull();

            var proxy1 = BookProxyMock.Get(keys[0]);

            var pagedProxyExpected = PagedResponseMock.Create(proxy1);
            proxy.Should().BeEquivalentToModel(pagedProxyExpected);
        }

        #endregion GetByFilterAsync

        private BookUpdateMessage SaveAsync(BookMessage model, Settings settings = null)
        {
            settings = settings ?? Settings.Empty;

            var config = ConfigurationStub.Create(() =>
            {
                return settings;
            });

            var bus = BusPublisherStub.Create();

            var service = new BookService(Db, bus, config, TenantAccessorStub.Create());
            service.SaveAsync(model).Wait();

            return bus.DequeueExchange<BookUpdateMessage>(ExchangeNames.Book);
        }

        private BookProxy GetByIdAsync(Guid id, Settings settings = null)
        {
            settings = settings ?? Settings.Empty;

            var config = ConfigurationStub.Create(() =>
            {
                return settings;
            });

            var service = new BookService(Db, BusPublisherStub.Create(), config, TenantAccessorStub.Create());
            return service.GetByIdAsync(id).GetAwaiter().GetResult();
        }

        private PagedResponse<BookProxy> GetAllAsync(Settings settings = null)
        {
            settings = settings ?? Settings.Empty;

            var config = ConfigurationStub.Create(() =>
            {
                return settings;
            });

            var service = new BookService(Db, BusPublisherStub.Create(), config, TenantAccessorStub.Create());
            return service.GetAllAsync(PagedRequestMock.Create()).GetAwaiter().GetResult();
        }

        private PagedResponse<BookProxy> GetByFilterAsync(PagedRequest<BookFilterPayload> pagination, Settings settings = null)
        {
            settings = settings ?? Settings.Empty;

            var config = ConfigurationStub.Create(() =>
            {
                return settings;
            });

            var service = new BookService(Db, BusPublisherStub.Create(), config, TenantAccessorStub.Create());
            return service.GetByFilterAsync(pagination).GetAwaiter().GetResult();
        }
    }
}
