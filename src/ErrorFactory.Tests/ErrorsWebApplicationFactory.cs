using ErrorFactory.Api;
using ErrorFactory.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ErrorFactory.Tests
{
    public class ErrorsWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly ISubjectsRepository _repository;

        public ErrorsWebApplicationFactory(ISubjectsRepository repository) => 
            _repository = repository;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = new ServiceDescriptor(typeof(ISubjectsRepository), _repository);
                services.Replace(descriptor);
            });
        }
    }
}