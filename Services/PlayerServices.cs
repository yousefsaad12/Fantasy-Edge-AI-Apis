using System.Text;
using Newtonsoft.Json;

namespace Api.Services
{
    public class PlayerServices : IPlayerServices
    {   

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PlayerServices> _logger;

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public PlayerServices(IUnitOfWork unitOfWork,IConfiguration configuration ,HttpClient httpClient , ILogger<PlayerServices> logger)
        {   
            _configuration = configuration;
            _httpClient = httpClient;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private void ValidatePlayer(Player player)
        {
            if (string.IsNullOrWhiteSpace(player.FirstName))
                throw new ArgumentException("Player FirstName cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(player.SecondName))
                throw new ArgumentException("Player SecondName cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(player.Status))
                throw new ArgumentException("Player Status cannot be null or empty.");
        }


        public async Task<bool> CreatePlayer(Player player)
        {   
            try{

                ValidatePlayer(player);

                Player ? isExists = await GetPlayerbyName(player.FirstName, player.SecondName);

                if (isExists is not null)
                {
                    _logger.LogInformation("Player already exists: {FirstName} {SecondName}", player.FirstName, player.SecondName);
                    return false; 
                }

                
                bool isSuccess = await _unitOfWork.Players.Create(player).ConfigureAwait(false);

                if (isSuccess) _logger.LogInformation("Player successfully created: {FirstName} {SecondName}", player.FirstName, player.SecondName);

                else _logger.LogWarning("Failed to create player: {FirstName} {SecondName}", player.FirstName, player.SecondName);


                return isSuccess;
            }

             catch (Exception ex)
            {
            
                _logger.LogError(ex, "An error occurred while creating player: {FirstName} {SecondName}", player.FirstName, player.SecondName);
                throw; 
            }
        }

        public async Task<Player>? GetPlayerbyName(string FirstName, string SecondName)
        {
            try{
                 
                 if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(SecondName))
                {
                    _logger.LogWarning("Attempted to create a player with invalid names: {FirstName} {SecondName}", FirstName, SecondName);
                    throw new ArgumentException("Player names cannot be null or empty.");
                }

                Player ? player = await _unitOfWork.Players.GetByName(FirstName, SecondName,
                                                    p => p.PlayerPerformances,
                                                    p => p.PlayerStatistics).ConfigureAwait(false);


                return player is null ? null : player;
            }
               catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Player>> ? GetPlayersAsync()
        {
            return await _unitOfWork.Players.GetAll(p => p.PlayerPerformances, 
                                                    p => p.PlayerStatistics, p => p.team).ConfigureAwait(false);
      
        }

        public async Task<IEnumerable<Player>> ? GetPlayersInfo()
        {
            return await _unitOfWork.Players.GetAll(p => p.elementType, p => p.team).ConfigureAwait(false);
        }

        public async Task<PlayerPredictionsResponse ?> GetPredictionFromModel(PlayerNameRequest playerPredictionReq)
        {
            try
            {
                string baseUrl = _configuration.GetValue<string>("FantasyApiSettings:Predict");

                string jsonBody = JsonConvert.SerializeObject(playerPredictionReq);

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(baseUrl, content); 
                
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Data fetched successfully: {ResponseContent}", responseContent);
                    if (string.IsNullOrWhiteSpace(responseContent) || responseContent == "null")
                    {   
                        return null; // Return null if the API returns an empty or null response
                    }     

                    var prediction = JsonConvert.DeserializeObject<PlayerPredictionsResponse>(responseContent);
                    return prediction; // Return the deserialized data
                }
                else
                {
                        _logger.LogError("Request failed. Status Code: {StatusCode}", response.StatusCode);
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        _logger.LogError("Error Response: {ErrorResponse}", errorResponse);

                        return null;
                }
            }
            catch (HttpRequestException httpEx)
            {
                    // Handles any issues with the HTTP request (e.g., connectivity issues)
                _logger.LogError(httpEx, "HTTP request error occurred.");
                return null; // Return null or handle it based on your needs
            }
            catch (TimeoutException timeoutEx)
            {
                    // Handles timeout exceptions
                _logger.LogError(timeoutEx, "The request timed out.");
                return null; // Return null or handle it based on your needs
            }
            catch (Exception ex)
            {
                    // Handles other unexpected errors
                _logger.LogError(ex, "An unexpected error occurred.");
                return null; // Return null or handle it based on your needs
            }
    }

        public async Task InsertPlayersAndRelatedEntitiesAsync(IEnumerable<PlayerJsonForm> playerJsonForms, IEnumerable<PlayerStatAndPerJson> playerStatAndPerJsons, int currentWeek)
        {
            var players = playerJsonForms.Select(p => p.MapToPlayer()).ToList();

            // Map PlayerId to Performance and Statistics
            var playerPerformances = playerStatAndPerJsons
                .Select(p => p.MapToPlayerPerformance(currentWeek))
                .ToDictionary(pp => pp.PlayerId);

            var playerStatistics = playerStatAndPerJsons
                .Select(p => p.MapPlayerStatistics(currentWeek))
                .ToDictionary(ps => ps.PlayerId);

            const int batchSize = 100;

            try
            {
                for (int i = 0; i < players.Count; i += batchSize)
                {
                    var batch = players.Skip(i).Take(batchSize).ToList();

                    foreach (var player in batch)
                    {
                        // Skip processing if performance or statistics are not found
                        if (!playerPerformances.TryGetValue(player.PlayerId, out var pp) || 
                            !playerStatistics.TryGetValue(player.PlayerId, out var ps))
                        {
                            _logger.LogWarning(
                                "Skipping PlayerId {PlayerId} due to missing performance or statistics data.", 
                                player.PlayerId);
                            continue;
                        }

                        var existingPlayer = await GetPlayerbyName(player.FirstName, player.SecondName);

                        if (existingPlayer is null)
                        {
                            // Add new player
                            player.PlayerPerformances.Add(pp);
                            player.PlayerStatistics.Add(ps);
                            await CreatePlayer(player).ConfigureAwait(false);
                        }
                        else
                        {
                            // Update existing player
                            await UpdatePlayer(existingPlayer, player, pp, ps).ConfigureAwait(false);
                        }
                    }
                }

                _logger.LogInformation("All players inserted/updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting/updating players.");
                throw;
            }
    }

        public async Task<bool> UpdatePlayer(Player existingPlayer, Player newPlayerData, PlayerPerformance pp, PlayerStatistics ps)
        {
            try
            {
                
                ValidatePlayer(newPlayerData);
                    
                if(existingPlayer is null) throw new ArgumentNullException(nameof(existingPlayer), $"player is null");
                PlayerUpdateHelper.UpdateBasicPlayerProperties(existingPlayer, newPlayerData);

                if(pp is not null && !existingPlayer.PlayerPerformances.Any(p => p.GameWeek == pp.GameWeek)) existingPlayer.PlayerPerformances.Add(pp);
                    
                if(ps is not null && !existingPlayer.PlayerStatistics.Any(p => p.GameWeek == ps.GameWeek)) existingPlayer.PlayerStatistics.Add(ps);
                  
                bool isSuccess =  await _unitOfWork.Players.UpdateOne(existingPlayer).ConfigureAwait(false);

                if (isSuccess)
                {
                    _logger.LogInformation("Player successfully updated: {FirstName} {SecondName}", newPlayerData.FirstName, newPlayerData.SecondName);
                }
                else
                {
                    _logger.LogWarning("Failed to update player: {FirstName} {SecondName}", newPlayerData.FirstName, newPlayerData.SecondName);
                }

                return isSuccess;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating player: {FirstName} {SecondName}", newPlayerData.FirstName, newPlayerData.SecondName);
                throw;
            }
        }

        public async Task<IEnumerable<PlayerSearchResponse>> GetPlayerNames()
        {
            return await _unitOfWork.PlayerRep.GetPlayerNames().ConfigureAwait(false);
        }
    }
}

