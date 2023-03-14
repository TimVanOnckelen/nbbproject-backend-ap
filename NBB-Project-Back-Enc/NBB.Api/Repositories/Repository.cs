using NBB.Api.Models;
using NBB.Api.Repository;


namespace Repositories
{

    public class Repository : IRepository
    {
        private readonly List<Enterprise> _ondernemingen;
        public Repository()
        {
            _ondernemingen = new List<Enterprise>();
        }
        public IEnumerable<Enterprise> GetAll()
        {
            return _ondernemingen;
        }
        public Enterprise    Get(string ondernemingsnummer)
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
            if(current != null && updated != null) 
            {
                _ondernemingen.Remove(current);
                _ondernemingen.Add(updated);
            }
            
        }
    }
}