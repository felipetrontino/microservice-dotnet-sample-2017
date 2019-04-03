using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Extensions;
using Framework.Data.Mongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;

namespace Framework.Data.Common
{
    public abstract class BaseMongoContext : IMongoContext
    {
        private static readonly object _lock = new object();
        private static bool _registered = false;

        protected BaseMongoContext(string connectionStringName, IConfiguration config, ITenantAccessor tenantAccessor)
            : this(GetDatabase(connectionStringName, config, tenantAccessor))
        {
            ConfigureInternal();
        }

        protected BaseMongoContext(IMongoDatabase dataBase)
        {
            Database = dataBase;

            ConfigureInternal();
        }

        public IMongoDatabase Database { get; }

        private void ConfigureInternal()
        {
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreElements", conventionPack, type => true);

            RegisterSerializers();

            Configure();
        }

        protected virtual void Configure()
        {
        }

        protected void Register<T>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap<T>();
            }
        }

        private static void RegisterSerializers()
        {
            lock (_lock)
            {
                if (!_registered)
                {
                    BsonSerializer.RegisterSerializer(typeof(DateTime), new UtcDateTimeSerializer());
                    _registered = true;
                }
            }
        }

        private static IMongoDatabase GetDatabase(string connectionStringName, IConfiguration config, ITenantAccessor tenantAccessor)
        {
            var connectionString = Settings.GetInstance(config, tenantAccessor.Tenant).ConnectionStrings.GetOrDefault(connectionStringName);

            var mongoUrl = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(MongoClientSettings.FromUrl(mongoUrl.ToMongoUrl()));
            return client.GetDatabase(mongoUrl.DatabaseName);
        }
    }
}
