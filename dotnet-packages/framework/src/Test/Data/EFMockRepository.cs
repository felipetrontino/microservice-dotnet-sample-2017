using Framework.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Framework.Test.Data
{
    public class EFMockRepository : IMockRepository
    {
        private readonly DbContext _db;

        public EFMockRepository(DbContext db)
        {
            _db = db;
        }

        public void Add<T>(T e)
            where T : class
        {
            _db.Add(e);
            _db.SaveChanges();
        }
    }
}
