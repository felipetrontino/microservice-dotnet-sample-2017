using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Exceptions;
using Framework.Core.Extensions;
using Framework.Core.Logging;
using Framework.Core.Utils;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.Bus.RabbitMQ
{
    public class BaseBus : IDisposable
    {
        protected const string UserIdKey = "UserId";
        protected const string TenantKey = "Tenant";

        public BaseBus(IConfiguration configuration, ITenantAccessor tenantAccessor, IUserAccessor userAccessor, ILanguageAccessor languageAccessor)
        {
            TenantAccessor = tenantAccessor;
            UserAccessor = userAccessor;
            LanguageAccessor = languageAccessor;
            Configuration = configuration;
        }

        protected Dictionary<string, Task<bool>> Connections { get; } = new Dictionary<string, Task<bool>>();
        protected IConfiguration Configuration { get; private set; }
        protected IConnection Connection { get; private set; }
        protected ITenantAccessor TenantAccessor { get; private set; }
        protected IUserAccessor UserAccessor { get; private set; }
        protected ILanguageAccessor LanguageAccessor { get; private set; }

        protected string Connect(Action<IBusOptions> setOptions = null)
        {
            IBusOptions options = new BusOptions(ConnectionStringNames.Rabbit);
            setOptions?.Invoke(options);

            if (!Connections.ContainsKey(options.Key))
            {
                var config = RabbitConfig.Get(Configuration, options.ConnectionStringName, TenantAccessor.Tenant);

                if (config == null)
                    throw new HBBusException("BusConfig not exists.");

                var connectionCompletionSource = new TaskCompletionSource<bool>();
                var connectionTask = connectionCompletionSource.Task;

                Connections.Add(options.Key, connectionTask);

                Connect(config, connectionCompletionSource);
            }

            return options.Key;
        }

        private void Connect(RabbitConfig config, TaskCompletionSource<bool> taskCompletion)
        {
            Task.Run(() =>
            {
                try
                {
                    Connection = new FuncRetrier<BrokerUnreachableException, IConnection>
                    {
                        Attempts = 5,
                        Delay = 5000,
                        Task = () =>
                        {
                            var factory = GetConnectionFactory(config);
                            return factory.CreateConnection();
                        },
                        OnAttemptError = e =>
                        {
                            LogHelper.Debug($"Unable to connect to RabbitMQ (Host: {config.Host}. Message: {e.Message}). Retrying...");
                        }
                    }.Run();

                    taskCompletion.SetResult(true);
                }
                catch (InvalidOperationException)
                {
                    taskCompletion.SetResult(false);
                }
            });
        }

        protected IConnectionFactory GetConnectionFactory(RabbitConfig config)
        {
            var ret = new ConnectionFactory();
            ret.HostName = config.Host;
            ret.Port = config.Port.ToNInt() ?? AmqpTcpEndpoint.UseDefaultPort;
            ret.UserName = config.UserName;
            ret.Password = config.Password;

            if (config.VHost != null)
                ret.VirtualHost = config.VHost;

            return ret;
        }

        protected async Task<bool> IsConectedAsync(string key = null)
        {
            var connection = Connections.FirstOrDefault(x => x.Key == key).Value;
            return await connection;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Connection?.Close();
        }

        ~BaseBus()
        {
            Dispose(false);
        }

        #endregion IDisposable
    }
}