using AutoMapper;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Application.Resources;
using Core.Domain.Entities;
using Lib.Notification.Abstractions;
using Lib.Notification.Extensions;
using Microsoft.Extensions.Logging;

namespace Core.Application.UseCases
{
    public class GetTodoUseCase : Notifiable, IGetTodoUseCase
    {
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTodoUseCase> _logger;

        public GetTodoUseCase(IGenericRepositoryAsync<Todo, int> genericRepositoryAsync, IMapper mapper, ILogger<GetTodoUseCase> logger)
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetTodoUseCaseResponse?> RunAsync(int id)
        {
            _logger.LogInformation(message: "Start useCase {0} > method {1}.", nameof(GetTodoUseCase), nameof(RunAsync));

            Validade(id);

            if (HasErrorNotification)
                return default;

            var todo = await _genericRepositoryAsync.GetAsync(id);

            if (todo is null)
            {
                AddErrorNotification(Msg.DATA_OF_X0_X1_NOT_FOUND_COD, Msg.DATA_OF_X0_X1_NOT_FOUND_TXT.ToFormat("Todo", id));
                return default;
            }

            var useCaseResponse = _mapper.Map<GetTodoUseCaseResponse>(todo);

            _logger.LogInformation("Finishes successfully useCase {0} > method {1}.", nameof(GetTodoUseCase), nameof(RunAsync));

            return useCaseResponse;
        }

        private void Validade(int id)
        {
            if (id <= Decimal.Zero)
                AddErrorNotification(Msg.IDENTIFIER_X0_IS_INVALID_COD, Msg.IDENTIFIER_X0_IS_INVALID_TXT.ToFormat(id));
        }
    }
}