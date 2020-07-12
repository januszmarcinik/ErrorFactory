using System.Collections.Generic;
using ErrorFactory.Api.Errors;
using ErrorFactory.Api.Middleware;
using ErrorFactory.Core;
using ErrorFactory.Core.Mediator;
using ErrorFactory.Domain;
using ErrorFactory.Domain.Commands;
using ErrorFactory.Domain.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                .AddSingleton<ISubjectsRepository>(x => SubjectsRepositorySingleton.GetInstance())
                .AddHttpContextAccessor()
                .AddTransient<ErrorsFactory>()
                .AddTransient<IErrorsFactory>(provider =>
                {
                    var errorsFactory = provider.GetService<ErrorsFactory>();
                    var logger = provider.GetService<ILogger<IErrorsFactory>>();
                    return new ErrorsFactoryLoggingDecorator(errorsFactory, logger);
                })
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDependencyResolver, DependencyInjectionResolver>()
                .AddTransient<ICommandHandler<AddSubjectCommand>, AddSubjectCommandHandler>()
                .AddTransient<ICommandHandler<RemoveSubjectCommand>, RemoveSubjectCommandHandler>()
                .AddTransient<IQueryHandler<GetSubjectByIdQuery, Subject>, GetSubjectByIdQueryHandler>()
                .AddTransient<IQueryHandler<GetSubjectsQuery, IEnumerable<Subject>>, GetSubjectsQueryHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseErrorHandlerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}