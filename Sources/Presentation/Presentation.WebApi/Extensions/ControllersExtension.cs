using System.Diagnostics.CodeAnalysis;
using Presentation.WebApi.Filters;

namespace Presentation.WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ControllersExtension
    {
        public static void AddControllerExtension(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddControllers(options => options.Filters.Add<NotificationContextFilter>());
        }
    }
}