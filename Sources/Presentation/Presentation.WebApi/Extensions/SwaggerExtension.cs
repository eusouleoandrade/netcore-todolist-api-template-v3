using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace Presentation.WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Todo List v1",
                    Description = "Your to-do list v1."
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Todo List v2",
                    Description = "Your to-do list v2."
                });
            });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo List - V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Todo List - V2");
                c.RoutePrefix = string.Empty;
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });
        }
    }
}