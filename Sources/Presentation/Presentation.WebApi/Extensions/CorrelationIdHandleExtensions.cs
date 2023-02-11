using System.Diagnostics.CodeAnalysis;
using Presentation.WebApi.Middlewares;

namespace Presentation.WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CorrelationIdHandleExtensions
    {
        public static IApplicationBuilder UseCorrelationIdHandleExtensions(this IApplicationBuilder app)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<CorrelationIdHandlerMiddleware>();
        }
    }
}