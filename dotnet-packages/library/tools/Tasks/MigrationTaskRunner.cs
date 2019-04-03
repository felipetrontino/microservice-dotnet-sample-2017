using Framework.Core.Job.Common;
using Library.Tools.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Library.Tools.Tasks
{
    public class MigrationTaskRunner : BaseTask
    {
        protected override void Configure(IConfiguration config, IServiceCollection services)
        {
            services.AddScoped<DbMigrations>();
        }

        protected override Task InternalRunAsync(IServiceProvider provider)
        {
            using (var context = provider.GetService<DbMigrations>())
            {
                context.Database.Migrate();
                context.EnsureSeedData();
            }

            return Task.CompletedTask;
        }
    }
}
