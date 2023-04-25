using Microsoft.EntityFrameworkCore;
using NBB.Api.Data;

namespace NBB.Api.Services
{
    public class DbService<T> : IDbService<T> where T : class
    {
        private readonly NbbDbContext<T> _dbContext;
  
        public DbService(NbbDbContext<T> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbContext.Entities.FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(int id, T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

    }

}
