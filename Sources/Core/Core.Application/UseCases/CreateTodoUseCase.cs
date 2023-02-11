using AutoMapper;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Domain.Entities;
using Lib.Notification.Abstractions;
using Microsoft.Extensions.Logging;

namespace Core.Application.UseCases
{
    public class CreateTodoUseCase : Notifiable, ICreateTodoUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;
        private readonly ILogger<CreateTodoUseCase> _logger;

        public CreateTodoUseCase(IMapper mapper, ITodoRepositoryAsync todoRepositoryAsync, ILogger<CreateTodoUseCase> logger)
        {
            _mapper = mapper;
            _todoRepositoryAsync = todoRepositoryAsync;
            _logger = logger;
        }

        public async Task<CreateTodoUseCaseResponse?> RunAsync(CreateTodoUseCaseRequest request)
        {
            _logger.LogInformation(message: "Start useCase {0} > method {1}.", nameof(CreateTodoUseCase), nameof(RunAsync));

            if (request.HasErrorNotification)
            {
                AddErrorNotifications(request);
                return default;
            }

            var todo = _mapper.Map<Todo>(request);

            var todoRepositoryResponse = await _todoRepositoryAsync.CreateAsync(todo);

            _logger.LogInformation("Finishes successfully useCase {0} > method {1}.", nameof(CreateTodoUseCase), nameof(RunAsync));

            return _mapper.Map<CreateTodoUseCaseResponse>(todoRepositoryResponse);
        }
    }
}