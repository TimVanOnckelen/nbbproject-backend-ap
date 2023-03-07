using NBB.Api.Models;
using NBB.Api.Repository;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;


namespace Repositories
{
    public interface IRepository
    {
        void Add(Onderneming onderneming);
        void Delete(Onderneming onderneming);
        Onderneming Get(string ondernemingsnummer);
        IEnumerable<Onderneming> GetAll();
        void Update(Onderneming onderneming);
    }

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
            var toUpdate = Get(onderneming.OndernemingsNummer);
            toUpdate = onderneming;
        }
    }
}