using System.Diagnostics.CodeAnalysis;
using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Presentation.WebApi.Options;

namespace Presentation.WebApi.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class CorrelationIdHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CorrelationIdOptions _options;

        public CorrelationIdHandlerMiddleware(RequestDelegate next, IOptions<CorrelationIdOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options.Value;
        }

        public async Task Invoke(HttpContext httpContext, [FromServices] ICorrelationIdService correlationIdService)
        {
            // Add correlationId in the service context
            if (httpContext.Request.Headers.TryGetValue(_options.Header, out StringValues correlationIdInput))
                correlationIdService.SetCorrelationId(correlationIdInput);

            string correlationId = correlationIdService.GetCorrelationId();

            // Add correlationId in the traceIdentifier of httpContext
            httpContext.TraceIdentifier = correlationId;

            // Add correlationId in the header response
            if (_options.IncludeInResponse)
                httpContext.Response.OnStarting(() =>
                {
                    httpContext.Response.Headers.Add(_options.Header, new[] { correlationId });
                    return Task.CompletedTask;
                });

            // Add correlationId in the logger
            var logger = httpContext.RequestServices.GetRequiredService<ILogger<CorrelationIdHandlerMiddleware>>();
            using (logger.BeginScope("{@CorrelationId}", correlationId))
            {
                await _next(httpContext);
            }
        }
    }
}