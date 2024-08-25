
using Api.Interfaces;
using Newtonsoft.Json;

namespace Api.Services
{
    public class FetchingService : IFetchingService
    {
        private readonly HttpClient _httpClient;

        public FetchingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string>  FetchDataAsync (string url)
        {
            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }

            throw new HttpRequestException("Failed to fetch data from Fantasy API");
        }
    }
}