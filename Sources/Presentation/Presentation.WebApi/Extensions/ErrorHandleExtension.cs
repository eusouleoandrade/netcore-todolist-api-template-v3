using System.Diagnostics.CodeAnalysis;
using Presentation.WebApi.Middlewares;

namespace Presentation.WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ErrorHandleExtension
    {
        public static void UseErrorHandlingExtension(this IApplicationBuilder app)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app));

            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}