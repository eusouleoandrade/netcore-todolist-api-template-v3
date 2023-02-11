
using System.Diagnostics.CodeAnalysis;
using Core.Application.Ioc;
using Infra.Persistence.Ioc;
using Presentation.WebApi.Extensions;
using Serilog;

namespace Presentation.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure logger
            SerilogExtension.AddSerilog(configuration: builder.Configuration);
            builder.Host.UseSerilog(Log.Logger);

            // Configure services
            builder.Services.AddPersistenceLayer();
            builder.Services.AddApplicationLayer();
            builder.Services.AddControllerExtension();
            builder.Services.AddCors();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerExtension();
            builder.Services.AddNotificationContextExtension();
            builder.Services.AddApiVersioningExtension();

            var app = builder.Build();

            // Database bootstrap setup
            IServiceScope scope = app.Services.CreateScope();
            scope.ServiceProvider.DatabaseBootstrapSetup();

            app.UseCorrelationIdHandleExtensions();

            app.UseCors(option => option.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader());

            app.UseErrorHandlingExtension();
            app.UseHttpRequestHandlingExtension();
            app.UseSwaggerExtension();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}