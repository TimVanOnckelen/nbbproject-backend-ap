using NBB.Api.Models;
using Newtonsoft.Json;

namespace NBB.Api.Services
{
    public class EnterpriseApiService
    {
        private readonly HttpClient _httpClient;

        public EnterpriseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Enterprise> GetWeatherDataAsync(string location)
        {
            //TODO: Add your API key here
            var response = await _httpClient.GetAsync("");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var weatherData = JsonConvert.DeserializeObject<Enterprise>(responseContent);

            return weatherData;
        }
    }
}
