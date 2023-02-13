using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Core.Application.Dtos.Queries;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Domain.Entities;

namespace Core.Application.Mappings
{
    [ExcludeFromCodeCoverage]
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateTodoUseCaseRequest, Todo>();

            CreateMap<Todo, TodoUseCaseResponse>();

            CreateMap<CreateTodoRequest, CreateTodoUseCaseRequest>();

            CreateMap<TodoUseCaseResponse, TodoQuery>();

            CreateMap<Todo, TodoUseCaseResponse>().ReverseMap();

            CreateMap<UpdateTodoRequest, UpdateTodoUseCaseRequest>();
        }
    }
}