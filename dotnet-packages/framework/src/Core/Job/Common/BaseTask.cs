using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Core.Extensions;
using Framework.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.Job.Common
{
    public abstract class BaseTask : ITask
    {
        public string Tenant { get; set; }

        public async Task RunAsync(IConfiguration config)
        {
            await RunTenantsAsync(InternalRunAsync, config);
        }

        protected abstract void Configure(IConfiguration config, IServiceCollection services);

        protected abstract Task InternalRunAsync(IServiceProvider provider);

        private IServiceScope GetServiceScope(IConfiguration config, string tenant)
        {            
            tenant = (tenant != null && tenant == "*") ? null : tenant;

            var services = new ServiceCollection();
            services.AddScoped(x => config);
            services.AddScoped<IUserAccessor>(x => new UserAccessor());
            services.AddScoped<ITenantAccessor>(x => new TenantAccessor(tenant));

            Configure(config, services);

            DependencyInjector.RegisterServices(services, config);

            var provider = services.BuildServiceProvider();

            return provider.CreateScope();
        }

        private async Task RunTenantsAsync(Func<IServiceProvider, Task> actionAsync, IConfiguration config)
        {
            var output = new ConcurrentBag<IList<string>>();
            var done = new ConcurrentBag<string>();

            var tenants = Settings.GetInstance(config).Tenants;

            await tenants
                .Where(x => Tenant == null || x == Tenant)
                .OrderBy(x => x)
                .ForEachAsync(7,
                    async tenant =>
                    {
                        var tenantOutput = new List<string>();

                        using (var serviceScope = GetServiceScope(config, tenant))
                        {
                            LogHelper.Debug($"Starting tenant: {tenant}");

                            try
                            {
                                var provider = serviceScope.ServiceProvider;

                                await actionAsync(provider);
                            }
                            catch (Exception e)
                            {
                                LogHelper.Error($"Error: {e.Message}", e);
                                tenantOutput.Add($"Error: {e.Message}");
                            }
                        }

                        if (tenantOutput.Any())
                        {
                            tenantOutput.Insert(0, $"\r\nTenant: {tenant}");
                            output.Add(tenantOutput);
                        }

                        done.Add(tenant);
                        LogHelper.Debug($"End tenant: {tenant}. Missing: {tenants.Count - done.Count}");
                    });

            if (output.Any())
            {
                LogHelper.Debug(string.Join(Environment.NewLine, output.SelectMany(x => x)));
            }
        }
    }
}
