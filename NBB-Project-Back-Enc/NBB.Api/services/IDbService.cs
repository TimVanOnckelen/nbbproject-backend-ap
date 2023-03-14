namespace NBB.Api.services
{
    public interface IDbService<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(int id, T entity);
    }
}