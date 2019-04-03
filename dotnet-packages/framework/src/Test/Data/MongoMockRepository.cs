using Framework.Data.Common;
using Framework.Data.Extensions;

namespace Framework.Test.Data
{
    public class MongoMockRepository : IMockRepository
    {
        private readonly BaseMongoContext _db;

        public MongoMockRepository(BaseMongoContext db)
        {
            _db = db;
        }

        public void Add<T>(T e)
            where T : class
        {
            _db.Database.GetCollection<T>().AddAsync(e).Wait();
        }
    }
}
