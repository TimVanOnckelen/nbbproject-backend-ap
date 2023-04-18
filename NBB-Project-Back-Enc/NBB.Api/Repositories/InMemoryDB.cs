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
                    ReferenceNumber = "2021-00000148",
                    DepositDate = "2021-12-07",
                    ExerciseDates = new ExerciseDates
                    {
                        startDate = "2020-01-01",
                        endDate = "2020-12-31"
                    },
                    ModelType = "m02-f",
                    DepositType = "Initial",
                    Language = "NL",
                    Currency = "EUR",
                    EnterpriseName = "TAKUMI RAMEN KITCHEN ANTWERPEN",
                    Address = new Address
                    {
                        Box = null,
                        Street = "Marnixplein",
                        Number = "10",
                        City = "Antwerpen",
                        PostalCode = "2000",
                        CountryCode = "BE"
                    },
                    EnterpriseNumber = "0712657911",
                    LegalForm = "014",
                    LegalSituation = "000",
                    FullFillLegalValidation = true,
                    ActivityCode = null,
                    GeneralAssemblyDate = "2021-06-01",
                    DataVersion = "Authentic",
                    ImprovementDate = null,
                    CorrectedData = null,
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
                    ReferenceNumber = "2021-00000148",
                    DepositDate = "2021-12-07",
                    ExerciseDates = new ExerciseDates
                    {
                        startDate = "2020-01-01",
                        endDate = "2020-12-31"
                    },
                    ModelType = "m02-f",
                    DepositType = "Initial",
                    Language = "NL",
                    Currency = "EUR",
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
                    LegalForm = "014",
                    LegalSituation = "000",
                    FullFillLegalValidation = true,
                    ActivityCode = null,
                    GeneralAssemblyDate = "2021-06-01",
                    DataVersion = "Authentic",
                    ImprovementDate = null,
                    CorrectedData = null,
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