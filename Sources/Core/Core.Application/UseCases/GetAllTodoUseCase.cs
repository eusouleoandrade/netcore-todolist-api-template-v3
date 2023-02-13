using AutoMapper;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Core.Application.UseCases
{
    public class GetAllTodoUseCase : IGetAllTodoUseCase
    {
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllTodoUseCase> _logger;

        public GetAllTodoUseCase(IGenericRepositoryAsync<Todo, int> genericRepositoryAsync, IMapper mapper, ILogger<GetAllTodoUseCase> logger)
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<TodoUseCaseResponse>?> RunAsync()
        {
            _logger.LogInformation(message: "Start useCase {0} > method {1}.", nameof(GetAllTodoUseCase), nameof(RunAsync));

            var entities = await _genericRepositoryAsync.GetAllAsync();

            _logger.LogInformation("Finishes successfully useCase {0} > method {1}.", nameof(GetAllTodoUseCase), nameof(RunAsync));

            return _mapper.Map<IReadOnlyList<TodoUseCaseResponse>>(entities);
        }
    }
}