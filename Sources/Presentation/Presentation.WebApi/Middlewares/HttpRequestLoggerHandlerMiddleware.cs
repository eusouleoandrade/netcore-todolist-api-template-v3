using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace Presentation.WebApi.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class HttpRequestLoggerHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpRequestLoggerHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext htppContext)
        {
            htppContext.Request.EnableBuffering();
            using var reader = new StreamReader(htppContext.Request.Body);
            string body = await reader.ReadToEndAsync();

            // Logger
            string? method = htppContext.Request?.Method;
            string? path = htppContext.Request?.Path.Value;
            Log.Information("Method: {method} - Path: {path} - Body: {body}", method, path, body);

            if (htppContext.Request == null)
                throw new ArgumentNullException(nameof(htppContext));
            else
                htppContext.Request.Body.Position = 0L;

            await _next(htppContext);
        }
    }
}