using Microsoft.EntityFrameworkCore;
using NBB.Api.Models;
using NBB.Api.Repository;
using NBB.Api.Services;
using System.ComponentModel.Design;

namespace NBB.Api.Repositories
{
    public class DbRepoEnterprises: IRepository
    {
            private readonly IDbService<Enterprise> _context;
            public DbRepoEnterprises(IDbService<Enterprise> context)
            {
            
            _context = context;
            }
 
            public  async Task<Enterprise> Get(string companyId)
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
                var current = await _context.GetByIdAsync(onderneming.EnterpriseNumber);
                var updated = onderneming;
                if (current != null && updated != null)
                {
                    await _context.UpdateAsync(onderneming.Id,current);
                }

            }
        public async Task<IEnumerable<Enterprise>> GetAll()
        {
            return await _context.GetAllAsync();
        }

        async Task<Enterprise> IRepository.Get(string companyId)
        {
            return await _context.GetByIdAsync(companyId);
        }
    }
    
}
