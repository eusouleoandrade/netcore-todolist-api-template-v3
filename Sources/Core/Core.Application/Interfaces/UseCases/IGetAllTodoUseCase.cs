using Core.Application.Dtos.Responses;

namespace Core.Application.Interfaces.UseCases
{
    public interface IGetAllTodoUseCase : IUseCase<IReadOnlyList<TodoUseCaseResponse>>
    {
    }
}