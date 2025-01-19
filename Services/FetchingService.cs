
using Newtonsoft.Json;

namespace Api.Services
{
    public class FetchingService : IFetchingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FetchingService> _logger;
        private readonly IPlayerServices _playerServices;
        private readonly ITeamsServices _teamServices;

        private readonly IConfiguration _configuration;
        public FetchingService(HttpClient httpClient, ILogger<FetchingService> logger, IPlayerServices playerServices, ITeamsServices teamServices, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _playerServices = playerServices;
            _teamServices = teamServices;
            _configuration = configuration;
        }

        public async Task<string> FetchDataAsync(int _currentWeek)
        {   
            string url = _configuration.GetValue<string>("FantasyApiSettings:BaseUrl");

            try
            {   
               
                _logger.LogInformation("Fetching data from {Url}", url);

                var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode(); // Throws if the status code is not successful

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

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

                var playerStat = await FetchPerformAsync(_currentWeek).ConfigureAwait(false);


                await _teamServices.InsertTeamsAndRelatedEntitiesAsync(fantasyForm.teamsJsonForms).ConfigureAwait(false);
                await _playerServices.InsertPlayersAndRelatedEntitiesAsync(fantasyForm.playerJsonForms, playerStat, _currentWeek).ConfigureAwait(false);
                
                return "Data had been fetched and stored in database";
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


        public async Task<ICollection<PlayerStatAndPerJson>> FetchPerformAsync(int _currentWeek)
        {   
            string url = _configuration.GetValue<string>("FantasyApiSettings:Live");
            url += $"{_currentWeek}/live/";

            try
            {   
                
                _logger.LogInformation("Fetching data from {Url}", url);

                var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode(); // Throws if the status code is not successful

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var apiData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                if (apiData == null || !apiData.ContainsKey("elements"))
                {
                    _logger.LogError("Invalid data format received from API.");
                    throw new JsonSerializationException("Missing 'elements' or 'teams' keys in API response.");
                }

                _logger.LogInformation("Successfully fetched and parsed data from API.");

                List<PlayerStatAndPerJson> statAndPerJson = JsonConvert.DeserializeObject<List<PlayerStatAndPerJson>>(apiData["elements"].ToString());

                
                return statAndPerJson;
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
