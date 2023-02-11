using System.Diagnostics.CodeAnalysis;
using Presentation.WebApi.Middlewares;

namespace Presentation.WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpRequestLoggerHandlerExtension
    {
        public static void UseHttpRequestHandlingExtension(this IApplicationBuilder app)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<HttpRequestLoggerHandlerMiddleware>();
        }
    }
}