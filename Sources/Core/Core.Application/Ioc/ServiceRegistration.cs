using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Core.Application.Interfaces.UseCases;
using Core.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Ioc
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IGetAllTodoUseCase, GetAllTodoUseCase>();
            services.AddScoped<ICreateTodoUseCase, CreateTodoUseCase>();
            services.AddScoped<IDeleteTodoUseCase, DeleteTodoUseCase>();
            services.AddScoped<IGetTodoUseCase, GetTodoUseCase>();
            services.AddScoped<IUpdateTodoUseCase, UpdateTodoUseCase>();
            services.AddScoped<ISetDoneTodoUseCase, SetDoneTodoUseCase>();
        }
    }
}