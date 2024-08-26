using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Api.Services
{
    public class FetchingService : IFetchingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FetchingService> _logger;

        public FetchingService(HttpClient httpClient, ILogger<FetchingService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<PlayerJsonForm> FetchDataAsync(string url)
        {
            try
            {
                _logger.LogInformation("Fetching data from {Url}", url);

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws if the status code is not successful

                var json = await response.Content.ReadAsStringAsync();

                var apiData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                if (apiData == null || !apiData.ContainsKey("elements") || !apiData.ContainsKey("teams"))
                {
                    _logger.LogError("Invalid data format received from API.");
                    throw new JsonSerializationException("Missing 'elements' or 'teams' keys in API response.");
                }

                _logger.LogInformation("Successfully fetched and parsed data from API.");

                var fantasyForm = new FantasyForm
                {
                    playerJsonForms = JsonConvert.DeserializeObject<List<PlayerJsonForm>>(apiData["elements"].ToString()),
                    teamsJsonForms = JsonConvert.DeserializeObject<List<TeamsJsonForm>>(apiData["teams"].ToString())
                };

                PlayerJsonForm ? playerJson =  fantasyForm.playerJsonForms.FirstOrDefault(p => p.first_name == "Mohamed" && p.second_name == "Salah");
                

                return playerJson;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP error occurred while fetching data from {Url}", url);
                throw new Exception("A network error occurred while fetching data from the API.", httpEx);
            }
            catch (JsonSerializationException jsonEx)
            {
                _logger.LogError(jsonEx, "JSON deserialization error for data from {Url}", url);
                throw new Exception("An error occurred while deserializing the API response.", jsonEx);
            }
            catch (KeyNotFoundException keyEx)
            {
                _logger.LogError(keyEx, "Key not found in the response data while fetching from {Url}", url);
                throw new Exception("An expected key was not found in the response data.", keyEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching data from {Url}", url);
                throw new Exception("An unexpected error occurred during the data fetch operation.", ex);
            }
        }
    }
}
