using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using Core.Application.Dtos.Wrappers;
using Lib.Notification.Contexts;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Presentation.WebApi.Filters
{
    [ExcludeFromCodeCoverage]
    public class NotificationContextFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationContextFilter(NotificationContext notificationContext)
            => _notificationContext = notificationContext;

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasErrorNotification)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.HttpContext.Response.ContentType = "application/json";

                var response = new Response(succeeded: false, errors: _notificationContext.ErrorNotifications);

                string notifications = JsonSerializer.Serialize(response);

                // logger
                Log.Warning("Finalizes request with notifications. Notifications: {notifications}", notifications);

                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}