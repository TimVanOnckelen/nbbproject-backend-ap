using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using NBB.Api.Models;
using NBB.Api.Repository;
using NBB.Api.Services;

namespace NBB.Api.services
{
    public class EnterpriseCaller
    {
        private IRepository _repository;
        private IConfiguration _configuration;
        private EnterpriseApiService EC;
        public EnterpriseCaller(IRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            EC = new EnterpriseApiService(configuration);
        }

        /// <summary>
        /// Geeft alle intern gekende bedrijven terug
        /// </summary>
        /// <returns>
        /// IEnumarable<Enterprise>
        /// </returns>
        public IEnumerable<Enterprise> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Probeert eerst in eigen DB onderneming te zoeken.
        /// Niet gevonden? Dan call plaatsen naar NBB voor zowel onderneming alsook de Financial data.
        /// </summary>
        /// <param name="ondernemingsnummer"></param>
        /// <returns>Enterprise</returns>
        public async Task<Enterprise> Get(string ondernemingsnummer)
        {
            var onderneming = _repository.Get(ondernemingsnummer);

            if (onderneming == null)
            {
                onderneming = await EC.GetEnterprise(ondernemingsnummer);
                onderneming.FinancialDataArray = new List<FinancialData>();
                var FDI = await EC.getFinancialData(onderneming.AccountingDataURL);
            }

            return onderneming;
        }
        /// <summary>
        /// Zoekt naar de financiële data van het opgegeven bedrijf.
        /// Niet gevonden? Call plaatsen naar NBB.
        /// </summary>
        /// <param name="ondernemingsnummer"></param>
        /// <param name="financialYear"></param>
        /// <returns>FinancialData</returns>
        public async Task<FinancialData> GetByYear(string ondernemingsnummer, int financialYear)
        {
            var onderneming = _repository.Get(ondernemingsnummer);
            // Als we geen financiele data hebben, opvragen bij NBB
            if (onderneming.FinancialDataArray == null)
            {
                onderneming.FinancialDataArray = new List<FinancialData>();
                var FDI = await EC.getFinancialData(onderneming.AccountingDataURL);
                onderneming.FinancialDataArray.Add(FDI);
            }

            // Als we geen finanicele data van het gewenste jaar hebben, opvragen bij NBB.
            if (onderneming.FinancialDataArray.FirstOrDefault(x => x.Year == financialYear) == null)
            {
                onderneming.FinancialDataArray = new List<FinancialData>();
                var FDI = await EC.getFinancialData(onderneming.AccountingDataURL);
                onderneming.FinancialDataArray.Add(FDI);
            }

            return onderneming.FinancialDataArray.FirstOrDefault(x => x.Year == financialYear);
        }
    }
}
