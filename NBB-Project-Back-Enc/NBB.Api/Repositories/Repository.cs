using NBB.Api.Models;
using NBB.Api.Repository;


namespace Repositories
{


    public class Repository : IRepository
    {
        private readonly List<Onderneming> _ondernemingen;
        public Repository()
        {
            _ondernemingen = new List<Onderneming>();
        }
        public IEnumerable<Onderneming> GetAll()
        {
            return _ondernemingen;
        }
        public Onderneming Get(string ondernemingsnummer)
        {
            return _ondernemingen.FirstOrDefault(x => x.OndernemingsNummer == ondernemingsnummer);
        }
        public void Add(Onderneming onderneming)
        {
            _ondernemingen.Add(onderneming);
        }
        public void Delete(Onderneming onderneming)
        {
            _ondernemingen.Remove(onderneming);
        }
        public void Update(Onderneming onderneming)
        {
            var current = Get(onderneming.OndernemingsNummer);
            var updated = onderneming;
            if(current != null && updated != null) 
            {
                _ondernemingen.Remove(current);
                _ondernemingen.Add(updated);
            }
            
        }
    }
}