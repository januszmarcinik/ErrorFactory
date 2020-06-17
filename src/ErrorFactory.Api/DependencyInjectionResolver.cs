using System;
using ErrorFactory.Core.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorFactory.Api
{
    internal class DependencyInjectionResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjectionResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T ResolveOrDefault<T>() where T : class
            => _serviceProvider.GetService<T>();
    }
}
