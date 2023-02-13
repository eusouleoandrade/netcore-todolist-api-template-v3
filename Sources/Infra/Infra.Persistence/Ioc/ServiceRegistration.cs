using System.Diagnostics.CodeAnalysis;
using Core.Application.Interfaces.Repositories;
using Infra.Persistence.Bootstraps;
using Infra.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Persistence.Ioc
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistration
    {
        public static void DatabaseBootstrapSetup(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            // Run the scripts to create database objects
            serviceProvider.GetService<DatabaseBootstrap>()?.Setup();
        }

        public static void AddInfraPersistenceLayer(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<DatabaseBootstrap>();

            services.AddScoped(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>));
            
            services.AddScoped<ITodoRepositoryAsync, TodoRepositoryAsync>();
        }
    }
}