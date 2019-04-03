using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.Core.Common
{
    public static class ServiceAccessor
    {
        [ThreadStatic]
        private static IServiceScopeFactory _serviceScopeFactory;

        public static IServiceProvider Instance
        {
            set => _serviceScopeFactory = value.GetRequiredService<IServiceScopeFactory>();
            get { return CreateScope()?.ServiceProvider; }
        }

        private static IServiceScope CreateScope()
        {
            return _serviceScopeFactory?.CreateScope();
        }
    }
}
