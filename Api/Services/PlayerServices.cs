

namespace Api.Services
{
    public class PlayerServices : IPlayerServices
    {   

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PlayerServices> _logger;
        public PlayerServices(IUnitOfWork unitOfWork, ILogger<PlayerServices> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<bool> CreatePlayer(Player player)
        {   
            try{

                if (string.IsNullOrWhiteSpace(player.FirstName) || string.IsNullOrWhiteSpace(player.SecondName) || string.IsNullOrWhiteSpace(player.Status))
                {
                    _logger.LogWarning("Attempted to create a player with invalid names: {FirstName} {SecondName} {Status}", player.FirstName, player.SecondName, player.Status);
                    throw new ArgumentException("Player names cannot be null or empty.");
                }
                

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


                return player != null ? player : null;
            }
               catch(Exception ex)
            {
                throw;
            }
        }

    
        public async Task<IEnumerable<Player>> ? GetPlayersAsync()
        {
            return await _unitOfWork.Players.GetAll(p => p.PlayerPerformances, 
                                                    p => p.PlayerStatistics).ConfigureAwait(false);
      
        }

        public async Task InsertPlayersAndRelatedEntitiesAsync(IEnumerable<PlayerJsonForm> playerJsonForms, IEnumerable<PlayerStatAndPerJson> playerStatAndPerJsons, int _currentWeek)
        {
            List<Player> players = playerJsonForms.Select(p => p.MapToPlayer()).ToList();
            List<PlayerPerformance> playerPerformances = playerStatAndPerJsons.Select(p => p.MapToPlayerPerformance(_currentWeek)).ToList();
            List<PlayerStatistics> playerStatistics =playerStatAndPerJsons.Select(p => p.MapPlayerStatistics(_currentWeek)).ToList();

            const int batchSize = 100;
            var playerList = players.ToList();
          try{

             // await _genericRepo.BeginTransactionAsync();

                for (int i = 0; i < playerList.Count; i += batchSize)
                {
                    var batch = playerList.Skip(i).Take(batchSize).ToList();

                    foreach (var player in batch)
                    {   

                        Player existingPlayer = await GetPlayerbyName(player.FirstName, player.SecondName).ConfigureAwait(false);

                        var pp = playerPerformances.FirstOrDefault(p => p.PlayerId == player.PlayerId);
                        var ps = playerStatistics.FirstOrDefault(p => p.PlayerId == player.PlayerId);



                        if(existingPlayer is null)
                        {

                            player.PlayerPerformances.Add(pp);
                            player.PlayerStatistics.Add(ps);
                            // Insert the new player into the database
                            await CreatePlayer(player).ConfigureAwait(false);
                        }
                        else await UpdatePlayer(existingPlayer, player, pp, ps).ConfigureAwait(false);


                    }
                }
                // Save the changes for the current batch
                //await _genericRepo.CommitTransactionAsync();
                _logger.LogInformation("All players inserted/updated successfully.");

            }

            catch (Exception ex)
            {   
                //await _genericRepo.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred while inserting/updating players.");
                throw;
            }

        }


            public async Task<bool> UpdatePlayer(Player existingPlayer, Player newPlayerData, PlayerPerformance pp, PlayerStatistics ps)
            {
                try
                {
                    // Ensure the new player data has valid names and status
                    if (string.IsNullOrWhiteSpace(newPlayerData.FirstName) || string.IsNullOrWhiteSpace(newPlayerData.SecondName) || string.IsNullOrWhiteSpace(newPlayerData.Status))
                    {
                        _logger.LogWarning("Attempted to update a player with invalid names: {FirstName} {SecondName} {Status}", newPlayerData.FirstName, newPlayerData.SecondName, newPlayerData.Status);
                        throw new ArgumentException("Player names cannot be null or empty.");
                    }

                    // Check if the existing player is valid
                    if (existingPlayer is null)
                    {
                        _logger.LogWarning("Existing player is null, update cannot proceed.");
                        return false;
                    }

                    // Use PlayerHelper to update the existing player's properties with the new data
                    await PlayerUpdateHelper.UpdateBasicPlayerProperties(existingPlayer, newPlayerData).ConfigureAwait(false);

                    existingPlayer.PlayerPerformances.Add(pp);
                    existingPlayer.PlayerStatistics.Add(ps);
                    // Save the changes to the repository
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

        }
    }

