
using System.Text.Json;
using Api.Interfaces;
using Api.Models.FetchingModels;
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

        public async Task<FantasyForm>  FetchDataAsync (string url)
        {
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            
            var apiData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            var fantasyForm = new FantasyForm
            {
                playerJsonForms = JsonConvert.DeserializeObject<List<PlayerJsonForm>>(apiData["PlayerJsonForms"].ToString()),
                teamsJsonForms = JsonConvert.DeserializeObject<List<TeamsJsonForm>>(apiData["TeamsJsonForms"].ToString())
            };

            return fantasyForm;
            

            
        }
    }
}