using System.Diagnostics.CodeAnalysis;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Repositories;
using Core.Application.Resources;
using Dapper.Contrib.Extensions;
using Infra.Persistence.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infra.Persistence.Repositories
{
    [ExcludeFromCodeCoverage]
    public class GenericRepositoryAsync<TEntity, TId> : ConnectionConfig, IGenericRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        private readonly ILogger _logger;

        public GenericRepositoryAsync(IConfiguration configuration, ILogger<GenericRepositoryAsync<TEntity, TId>> logger) 
            : base(configuration)
        {
            _logger = logger;
        }

        public virtual async Task<TId?> InsertAsync(TEntity entity)
        {
            try
            {
                _logger.LogInformation(message: "Start repository GenericRepositoryAsync > method {0}.", nameof(InsertAsync));

                var id = await _connection.InsertAsync<TEntity>(entity);

                _logger.LogInformation("Finishes successfully repository GenericRepositoryAsync > method {0}.", nameof(InsertAsync));

                return (TId)Convert.ChangeType(id, typeof(TId));
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _logger.LogInformation(message: "Start repository GenericRepositoryAsync > method {0}.", nameof(DeleteAsync));

                bool deleted = await _connection.DeleteAsync<TEntity>(entity);

                _logger.LogInformation("Finishes successfully repository GenericRepositoryAsync > method {0}.", nameof(DeleteAsync));

                return deleted;
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation(message: "Start repository GenericRepositoryAsync > method {0}.", nameof(GetAllAsync));

                var entities = await _connection.GetAllAsync<TEntity>();

                _logger.LogInformation("Finishes successfully repository GenericRepositoryAsync > method {0}.", nameof(GetAllAsync));

                return entities;
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            try
            {
                _logger.LogInformation(message: "Start repository GenericRepositoryAsync > method {0}.", nameof(GetAsync));

                var entity = await _connection.GetAsync<TEntity>(id);

                _logger.LogInformation("Finishes successfully repository GenericRepositoryAsync > method {0}.", nameof(GetAsync));

                return entity;
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                _logger.LogInformation(message: "Start repository GenericRepositoryAsync > method {0}.", nameof(UpdateAsync));

                bool updated = await _connection.UpdateAsync<TEntity>(entity);

                _logger.LogInformation("Finishes successfully repository GenericRepositoryAsync > method {0}.", nameof(UpdateAsync));

                return updated;
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }
    }
}