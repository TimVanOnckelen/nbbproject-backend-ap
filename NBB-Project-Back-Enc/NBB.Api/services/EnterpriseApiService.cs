using System.Configuration;
using System.Net.Http.Headers;
using NBB.Api.Models;
using Newtonsoft.Json;

namespace NBB.Api.Services
{
    public class EnterpriseApiService
    {
        /// <summary>
        /// Gedeelde HTTP client doorheen de instantie van het project + Iconfiguration voor secrets management
        /// </summary>
        private static HttpClient _httpClient = new HttpClient();
        private readonly IConfiguration _configuration;
      
        /// <summary>
        /// Initiatie van de API service + default waarden voor de HTTP client.
        /// Hier wordt een GUID gegenereerd. Waarde is niet relevant maar moet van type UUIDv4 zijn. (TL;DR: Random)
        /// </summary>
        /// <param name="configuration"></param>
        public EnterpriseApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Get key from secrets
            string PrimKey = _configuration["ApiSecret"];
            string RequestId = Guid.NewGuid().ToString();
            
            _httpClient.BaseAddress = new Uri("https://ws.uat2.cbso.nbb.be/authentic/");
            _httpClient.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
            _httpClient.DefaultRequestHeaders.Add("NBB-CBSO-Subscription-Key", PrimKey);
            _httpClient.DefaultRequestHeaders.Add("X-Request-Id", RequestId);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "AP NBB project");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/x.jsonxbrl");
        }
        /// <summary>
        /// GetEnterprise gaat informatie voor een specifiek bedrijf ophalen. Naast de logische naam en adres geeft deze ook de accounting data terug in de vorm van een URL. 
        /// </summary>
        /// <param name="legalEntityId">Deze ID is uniek voor ieder bedrijf</param>
        /// <returns>Geeft een bedrijf terug</returns>
        public async Task<Enterprise> GetEnterprise(string legalEntityId)
        {
            //Example legalEntityId: 0407239355
            var uri = $"/legalEntity/{legalEntityId}/references?2022";

            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var EnterpriseData = JsonConvert.DeserializeObject<Enterprise>(responseContent);

            return EnterpriseData;
        }

        /// <summary>
        /// Haalt de AccountingData op van een specifiek bedrijf. De ReferenceID is uniek voor de accounting data van een specifiek bedrijf gedurende een specifiek jaar.
        /// </summary>
        /// <param name="ReferenceId"></param>
        /// <returns>Accounting data van een specifiek bedrijf gedurende een specifiek jaar</returns>
        public async Task<FinancialData> getFinancialData(string ReferenceId)
        {
            //Example ReferenceId: 2021-14500450
            var uri = $"authentic/deposit/{ReferenceId}/accountingData";

            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            var FinancialData = JsonConvert.DeserializeObject<FinancialData>(responseContent);

            return FinancialData;
        }
    }
}
