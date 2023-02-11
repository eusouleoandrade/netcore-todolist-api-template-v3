using Lib.Notification.Interfaces;

namespace Core.Application.Interfaces.UseCases
{
    public interface IDeleteTodoUseCase : INotifiable, IUseCase<int, bool>
    {
    }
}