using Core.Application.Dtos.Queries;

namespace Core.Application.Interfaces.UseCases
{
    public interface IGetAllTodoUseCase : IUseCase<IReadOnlyList<TodoQuery>>
    {
    }
}