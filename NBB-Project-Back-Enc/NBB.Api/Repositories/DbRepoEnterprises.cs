using Microsoft.EntityFrameworkCore;
using NBB.Api.Models;
using NBB.Api.Repository;
using NBB.Api.Services;

namespace NBB.Api.Repositories
{
    public class DbRepoEnterprises: IRepository
    {
            private readonly IDbService<Enterprise> _context;
            public DbRepoEnterprises(DbService<Enterprise> context)
            {
            
            _context = context;
            }
            public async Task<IEnumerable<Enterprise>>  GetAll()
            {
            return await _context.GetAllAsync();
            }
            public  async Task<Enterprise> Get(int companyId)
            {
                return await _context.GetByIdAsync(companyId);
            }
            public async void Add(Enterprise onderneming)
            {
                await _context.CreateAsync(onderneming);
            }
            public async void Delete(Enterprise onderneming)
            {
                await _context.DeleteAsync(onderneming);
            }
            public async void Update(Enterprise onderneming)
            {
                var current = await _context.GetByIdAsync(onderneming.Id);
                var updated = onderneming;
                if (current != null && updated != null)
                {
                    await _context.UpdateAsync(onderneming.Id,current);
                }

            }

        public Enterprise Get(string ondernemingsnummer)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Enterprise> IRepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
    
}
