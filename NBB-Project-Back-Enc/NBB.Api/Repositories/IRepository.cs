using NBB.Api.Models;
using System.Linq.Expressions;

namespace NBB.Api.Repository
{
    public interface IRepository
    {
        void Add(Enterprise onderneming);
        void Delete(Enterprise onderneming);
        Task<Enterprise> Get(string ondernemingsnummer);
        Task<IEnumerable<Enterprise>> GetAll();
        void Update(Enterprise onderneming);
    }
}
