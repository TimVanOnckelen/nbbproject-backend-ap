
using NBB.Api.Models;

namespace NBB.Api.Services
{
    public interface IDatasource
    {
            IEnumerable<Onderneming> GetAll();
            Onderneming Get(int id);
            void Add(Onderneming onderneming);
            void Delete(Onderneming onderneming);
            void Update(Onderneming onderneming);
    }
}
