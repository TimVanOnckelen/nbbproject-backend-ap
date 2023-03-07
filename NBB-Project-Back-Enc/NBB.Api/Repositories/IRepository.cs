using NBB.Api.Models;
using System.Linq.Expressions;

namespace NBB.Api.Repository
{
    public interface IRepository
    {
        void Add(Onderneming onderneming);
        void Delete(Onderneming onderneming);
        Onderneming Get(string ondernemingsnummer);
        IEnumerable<Onderneming> GetAll();
        void Update(Onderneming onderneming);
    }
}
