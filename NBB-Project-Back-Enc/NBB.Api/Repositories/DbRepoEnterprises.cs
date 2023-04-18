using Microsoft.EntityFrameworkCore;
using NBB.Api.Models;
using NBB.Api.Repository;
using NBB.Api.Services;

namespace NBB.Api.Repositories
{
    public class DbRepoEnterprises: IRepository
    {
            private readonly IDbService<Enterprise> _context;
            public DbRepoEnterprises()
            {
            _context = new DbService<Enterprise>();
            }
            public IEnumerable<Enterprise> GetAll()
            {
                return _ondernemingen;
            }
            public Enterprise Get(string ondernemingsnummer)
            {
                return _ondernemingen.FirstOrDefault(x => x.EnterpriseNumber == ondernemingsnummer);
            }
            public void Add(Enterprise onderneming)
            {
                _ondernemingen.Add(onderneming);
            }
            public void Delete(Enterprise onderneming)
            {
                _ondernemingen.Remove(onderneming);
            }
            public void Update(Enterprise onderneming)
            {
                var current = Get(onderneming.EnterpriseNumber);
                var updated = onderneming;
                if (current != null && updated != null)
                {
                    _ondernemingen.Remove(current);
                    _ondernemingen.Add(updated);
                }

            }
        }
    
}
