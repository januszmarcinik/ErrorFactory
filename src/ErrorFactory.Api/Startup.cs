using ErrorFactory.Core.Mediator;
using ErrorFactory.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace ErrorFactory.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver());
            
            services
                .AddSingleton<ISubjectsRepository, SubjectsRepository>(x => 
                    new SubjectsRepository(new []
                    {
                        new Subject(1, "Inżynieria systemów informatycznych"),
                        new Subject(2, "Metody wytwarzania oprogramowania"),
                        new Subject(3, "Sztuczne sieci neuronowe"),
                        new Subject(4, "Wzorce projektowe") 
                    }))
                .AddTransient<IDependencyResolver, DependencyInjectionResolver>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}