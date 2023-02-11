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
    public class UpdateTodoUseCase : Notifiable, IUpdateTodoUseCase
    {
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;
        private readonly IGetTodoUseCase _getTodoUseCase;
        private readonly ILogger<UpdateTodoUseCase> _logger;

        public UpdateTodoUseCase(IGenericRepositoryAsync<Todo, int> genericRepositoryAsync, IGetTodoUseCase getTodoUseCase, ILogger<UpdateTodoUseCase> logger)
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _getTodoUseCase = getTodoUseCase;
            _logger = logger;
        }

        public async Task<bool> RunAsync(UpdateTodoUseCaseRequest request)
        {
            _logger.LogInformation(message: "Start useCase {0} > method {1}.", nameof(UpdateTodoUseCase), nameof(RunAsync));

            if (request.HasErrorNotification)
            {
                AddErrorNotifications(request);
                return default;
            }

            var getTodoUseCaseResponse = await _getTodoUseCase.RunAsync(request.Id);

            if (_getTodoUseCase.HasErrorNotification)
            {
                AddErrorNotifications(_getTodoUseCase);
                return default;
            }

            var todo = new Todo(getTodoUseCaseResponse.Id, request.Title, request.Done);

            var updated = await _genericRepositoryAsync.UpdateAsync(todo);

            if (!updated)
            {
                AddErrorNotification(Msg.FAILED_TO_UPDATE_X0_COD, Msg.FAILED_TO_UPDATE_X0_TXT.ToFormat("Todo"));
                return default;
            }

            _logger.LogInformation("Finishes successfully useCase {0} > method {1}.", nameof(UpdateTodoUseCase), nameof(RunAsync));

            return updated;
        }
    }
}