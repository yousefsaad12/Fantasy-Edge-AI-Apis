using Newtonsoft.Json;
namespace Api.Services
{
    public class ModelServices : IModelServices
    {   
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ModelServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<ModelResponse> RetrainModel()
        {
            string url =  _configuration.GetValue<string>("FantasyApiSettings:Retrain");

            try
            {
                var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var modelResponse = JsonConvert.DeserializeObject<ModelResponse>(json);

                if(modelResponse is null) throw new Exception("Message does not found");

                if(modelResponse is null) throw new Exception("Model does not Retrained");

                return modelResponse;
            }

             catch (JsonException ex)
            {
              throw new Exception("Error parsing the API response JSON.", ex);
            }
            catch (HttpRequestException ex)
            {
                
                throw new Exception("HTTP request to the API failed.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }

            
        }
    }
}