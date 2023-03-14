namespace NBB.Api.services
{
    public interface IDbService<TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task UpdateAsync(int id, TEntity entity);
    }
}