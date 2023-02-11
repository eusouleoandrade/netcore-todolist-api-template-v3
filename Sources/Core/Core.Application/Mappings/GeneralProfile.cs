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
            CreateMap<Todo, TodoQuery>();

            CreateMap<CreateTodoUseCaseRequest, Todo>();

            CreateMap<Todo, CreateTodoUseCaseResponse>();

            CreateMap<CreateTodoRequest, CreateTodoUseCaseRequest>();

            CreateMap<CreateTodoUseCaseResponse, CreateTodoQuery>();

            CreateMap<Todo, GetTodoUseCaseResponse>().ReverseMap();

            CreateMap<GetTodoUseCaseResponse, GetTodoQuery>();

            CreateMap<UpdateTodoRequest, UpdateTodoUseCaseRequest>();
        }
    }
}