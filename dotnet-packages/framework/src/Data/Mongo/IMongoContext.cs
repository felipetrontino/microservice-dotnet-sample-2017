using MongoDB.Driver;

namespace Framework.Data.Mongo
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}
