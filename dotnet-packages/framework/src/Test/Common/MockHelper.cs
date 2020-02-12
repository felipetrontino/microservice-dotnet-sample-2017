using Framework.Core.Entities;
using Framework.Data.Common;
using Framework.Test.Mock.Common;
using Microsoft.EntityFrameworkCore;
using Mongo2Go;
using MongoDB.Driver;
using Moq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Framework.Test.Common
{
    public static class MockHelper
    {
        private static readonly string MongoTestPath = (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "/tmp/" : "") + "AppData/";

        [ThreadStatic]
        private static MongoDbRunner _runner;

        public static TDataContext GetDbContext<TDataContext>()
            where TDataContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TDataContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var options = builder.Options;

            var tenantAccessor = new TenantAccessorStub();
            var userAccessor = new UserAccessorStub();

            var db = (TDataContext)Activator.CreateInstance(typeof(TDataContext), options, tenantAccessor, userAccessor);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            return db;
        }

        public static TMongoContext GetMongoContext<TMongoContext>()
            where TMongoContext : BaseMongoContext
        {
            MockHelper.DisposeMongoDbRunner();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                _runner = MongoDbRunner.Start(MongoTestPath, "*/tools/mongodb-linux*/bin", Environment.GetEnvironmentVariable("HOME") + "/.nuget/packages/mongo2go");
            else
                _runner = MongoDbRunner.Start(MongoTestPath);

            var mongoUrlBuilder = new MongoUrlBuilder(_runner.ConnectionString)
            {
                DatabaseName = Guid.NewGuid().ToString()
            };

            var client = new MongoClient(mongoUrlBuilder.ToMongoUrl());
            var database = client.GetDatabase(mongoUrlBuilder.DatabaseName);

            var db = (TMongoContext)Activator.CreateInstance(typeof(TMongoContext), database);

            return db;
        }

        public static T CreateModel<T>(string key)
            where T : class, new()
        {
            var ret = new T();

            var idProp = typeof(T).GetProperties().FirstOrDefault(x => x.Name == nameof(EFEntity.Id));

            if (idProp != null)
            {
                var id = key != null ? FakeHelper.GetId(key) : Guid.NewGuid();
                idProp.SetValue(ret, id);
            }

            var tenantProp = typeof(T).GetProperties().FirstOrDefault(x => x.Name == nameof(EFEntity.Tenant));

            if (tenantProp != null)
            {
                var tenant = FakeHelper.GetTenant();
                tenantProp.SetValue(ret, tenant);
            }

            return ret;
        }

        public static T CreateObject<T>()
            where T : class
        {
            return new Mock<T>().Object;
        }

        public static T CreateNull<T>()
           where T : class
        {
            return default;
        }

        public static Mock<T> Create<T>()
           where T : class
        {
            return new Mock<T>();
        }

        public static void DisposeMongoDbRunner()
        {
            if (_runner != null && !_runner.Disposed)
            {
                _runner.Dispose();
                KillMongo2Go();
            }
        }

        private static void KillMongo2Go()
        {
            var processes = Process.GetProcessesByName("mongod");
            foreach (var process in processes)
            {
                if (process.MainModule.FileName.Contains("mongo2go"))
                {
                    process.Kill();
                }
            }

            Task.Delay(250).Wait();

            Directory.Delete(MongoTestPath, true);
        }
    }
}