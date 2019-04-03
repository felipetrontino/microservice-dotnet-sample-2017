using Framework.Core.Common;
using Framework.Core.Config;
using Framework.Web.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Framework.Web.Common
{
    public abstract class BaseStartup
    {
        private readonly IConfiguration _configuration;

        protected BaseStartup(IConfiguration config)
        {
            _configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();
                    });
            });

            RegisterService(services);

            services.AddMvc();

            services.AddSwaggerGen(x =>
            {
                x.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                x.SwaggerDoc("v1", new Info { Title = $"API {Configuration.Audience.Get()}", Version = "v1" });
            });

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                options.OperationFilter<FileOperation>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowCredentials();
            });

            app.UseGlobalErrorMiddleware();

            app.UseSwagger(x => x.RouteTemplate = "swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"API {Configuration.Audience.Get()} v1"));

            app.UseMvc();

            ConfigureInternal(app, env, serviceProvider, applicationLifetime);
        }

        protected virtual void ConfigureInternal(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, IApplicationLifetime applicationLifetime)
        {
        }

        protected virtual void RegisterService(IServiceCollection services)
        {
            DependencyInjector.RegisterServicesWeb(services, _configuration);
        }
    }
}
