using Framework.Core.Entities;
using Framework.Data.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Data.Extensions
{
    public static class MongoExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase session)
        {
            var attrs = typeof(T).GetCustomAttributes(typeof(CollectionNameAttribute), false).OfType<CollectionNameAttribute>().FirstOrDefault();
            var collectionName = attrs?.Name ?? typeof(T).Name;

            return session.GetCollection<T>(collectionName);
        }

        public static IMongoQueryable<T> Query<T>(this IMongoDatabase session)
        {
            return session.GetCollection<T>().AsQueryable();
        }

        public static IMongoQueryable<T> Query<T>(this IMongoDatabase session, string name)
        {
            return session.GetCollection<T>(name).AsQueryable();
        }

        public static async Task AddAsync<T>(this IMongoCollection<T> collection, T value)
        {
            await collection.InsertOneAsync(value);
        }

        public static async Task AddOrUpdateAsync<T>(this IMongoCollection<T> collection, T value)
             where T : IEntity
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            await collection.FindOneAndReplaceAsync(filter, value, new FindOneAndReplaceOptions<T, T>() { IsUpsert = true });
        }

        public static async Task UpdateAsync<T>(this IMongoCollection<T> collection, T value)
            where T : IEntity
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            await collection.FindOneAndReplaceAsync(filter, value);
        }

        public static async Task RemoveAsync<T>(this IMongoCollection<T> collection, T value)
           where T : IEntity
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, value.Id);
            await collection.FindOneAndDeleteAsync(filter);
        }
    }
}
