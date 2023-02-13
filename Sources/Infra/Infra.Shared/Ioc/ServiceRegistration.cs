using System.Diagnostics.CodeAnalysis;
using Core.Application.Interfaces.Services;
using Infra.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Shared.Ioc
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistration
    {
         public static void AddInfraSharedLayer(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<ICorrelationIdService, CorrelationIdService>();
        }
    }
}