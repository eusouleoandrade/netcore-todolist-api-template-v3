using System.Diagnostics.CodeAnalysis;
using Lib.Notification.Contexts;

namespace Presentation.WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class NotificationContextExtension
    {
        public static void AddNotificationContextExtension(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<NotificationContext>();
        }
    }
}