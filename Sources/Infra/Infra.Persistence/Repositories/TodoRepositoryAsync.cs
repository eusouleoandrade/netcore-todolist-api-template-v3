using System.Diagnostics.CodeAnalysis;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Repositories;
using Core.Application.Resources;
using Core.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infra.Persistence.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TodoRepositoryAsync : GenericRepositoryAsync<Todo, int>, ITodoRepositoryAsync
    {
        private readonly ILogger<TodoRepositoryAsync> _logger;

        public TodoRepositoryAsync(IConfiguration configuration, ILogger<TodoRepositoryAsync> logger)
            : base(configuration, logger)
        {
            _logger = logger;
        }

        public async Task<Todo?> CreateAsync(Todo entity)
        {
            try
            {
                _logger.LogInformation(message: "Start repository {0} > method {1}.", nameof(TodoRepositoryAsync), nameof(CreateAsync));

                string insertSql = @"INSERT INTO todo (title, done)
                                    VALUES(@title, @done)
                                    RETURNING id;";

                var id = await _connection.ExecuteScalarAsync<int>(insertSql,
                new
                {
                    title = entity.Title,
                    done = entity.Done
                });

                if (id > decimal.Zero)
                {
                    _logger.LogInformation("Finishes successfully repository {0} > method {1}.", nameof(TodoRepositoryAsync), nameof(CreateAsync));
                    return await base.GetAsync(id);
                }

                return await Task.FromResult<Todo?>(default);
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }
    }
}