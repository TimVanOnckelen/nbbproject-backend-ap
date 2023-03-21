using NBB.Api.Models;
using NBB.Api.Repository;
using NBB.Api.services;

namespace NBB.Api.Repository
{

    public class InMemoryDB : IRepository
    {
        //private InMemoryDbContext _context;
        public InMemoryDB()
        {
            using (var _context = new InMemoryDbContext())
            {
                var ondernemingen = new List<Enterprise> {
                    new Enterprise
                    {
                        Address = new Address
                        {
                            Street = "Marnixplein",
                            Number = "10",
                            City = "Antwerpen",
                            PostalCode = "2000",
                            CountryCode = "BE"
                        },
                        EnterpriseNumber = "0712657911",
                        FinancialDataArray = new List<FinancialData>()
                        {
                            new FinancialData
                            {
                                Id = 1,
                                Year = 2021,
                                Profit = 10000,
                                Revenue = 21102
                            },
                            new FinancialData
                            {
                                Id = 2,
                                Year = 2022,
                                Profit = 12345,
                                Revenue = 54321
                            }
                        }
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
                    EnterpriseNumber = "0764896369",
                    FinancialDataArray = new List<FinancialData>()
                    {
                        new FinancialData
                        {
                            Id = 1,
                            Year = 2021,
                            Profit = 3420,
                            Revenue = -1102
                        },

                    }
                }

                };
                
            };
        }
        public IEnumerable<Enterprise> GetAll()
        {
            using (var _context = new InMemoryDbContext())
            {
                return _context.Enterprise;
            }
        }
        public Enterprise Get(string ondernemingsnummer)
        {
            using (var _context = new InMemoryDbContext())
            {
                return _context.Enterprise.FirstOrDefault(x => x.EnterpriseNumber == ondernemingsnummer);
            }
        }
        public void Add(Enterprise onderneming)
        {
            using (var _context = new InMemoryDbContext())
            {
                _context.Enterprise.Add(onderneming);
                _context.SaveChanges();
            }
        }
        public void Delete(Enterprise onderneming)
        {
            using (var _context = new InMemoryDbContext())
            {
                _context.Enterprise.Remove(onderneming);
                _context.SaveChanges();
            }
        }
        public void Update(Enterprise onderneming)
        {
            using (var _context = new InMemoryDbContext())
            {
                var toUpdate = Get(onderneming.EnterpriseNumber);
                toUpdate = onderneming;
                _context.SaveChanges();
            }
        }
    }
}