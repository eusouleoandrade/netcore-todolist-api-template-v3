namespace Core.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(TId id);

        Task<TId?> InsertAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);
    }
}