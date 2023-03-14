using NBB.Api.Models;
using NBB.Api.Repository;


namespace NBB.Api.Repository
{

    public class InMemoryDB : IRepository
    {
        private readonly List<Enterprise> _ondernemingen;
        public InMemoryDB()
        {
            _ondernemingen = new List<Enterprise>()
            {
                new Enterprise
                {
                    EnterpriseName = "TAKUMI RAMEN KITCHEN ANTWERPEN",
                    Address = new Address
                    {
                        Street = "Marnixplein",
                        Number = "10",
                        City = "Antwerpen",
                        PostalCode = "2000",
                        CountryCode = "BE"
                    },
                    EnterpriseNumber = "0712.657.911"
                },
                new Enterprise
                {
                    EnterpriseName = "Takumi Groenplaats Antwerpen",
                    Address = new Address
                    {
                        Street = "Groenplaats",
                        Number = "16",
                        City = "Antwerpen",
                        PostalCode = "2000",
                        CountryCode = "BE",
                        Box = ""
                    },
                    EnterpriseNumber = "0764.896.369"
                }
            };
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