using Core.Application.Dtos.Requests;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Application.Resources;
using Core.Domain.Entities;
using Lib.Notification.Abstractions;
using Lib.Notification.Extensions;
using Microsoft.Extensions.Logging;

namespace Core.Application.UseCases
{
    public class SetDoneTodoUseCase : Notifiable, ISetDoneTodoUseCase
    {
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;
        private readonly IGetTodoUseCase _getTodoUseCase;
        private readonly ILogger<SetDoneTodoUseCase> _logger;

        public SetDoneTodoUseCase(IGenericRepositoryAsync<Todo, int> genericRepositoryAsync, IGetTodoUseCase getTodoUseCase, ILogger<SetDoneTodoUseCase> logger)
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _getTodoUseCase = getTodoUseCase;
            _logger = logger;
        }

        public async Task<bool> RunAsync(SetDoneTodoUseCaseRequest request)
        {
            _logger.LogInformation(message: "Start useCase {0} > method {1}.", nameof(SetDoneTodoUseCase), nameof(RunAsync));

            var getTodoUseCaseResponse = await _getTodoUseCase.RunAsync(request.Id);

            if (_getTodoUseCase.HasErrorNotification)
            {
                AddErrorNotifications(_getTodoUseCase);
                return default;
            }

            var todo = new Todo(getTodoUseCaseResponse.Id, getTodoUseCaseResponse.Title, request.Done);

            var updated = await _genericRepositoryAsync.UpdateAsync(todo);

            if (!updated)
            {
                AddErrorNotification(Msg.FAILED_TO_UPDATE_X0_COD, Msg.FAILED_TO_UPDATE_X0_TXT.ToFormat("Todo"));
                return default;
            }

            _logger.LogInformation("Finishes successfully useCase {0} > method {1}.", nameof(SetDoneTodoUseCase), nameof(RunAsync));

            return updated;
        }
    }
}