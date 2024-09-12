using Microsoft.EntityFrameworkCore.Query.Internal;

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

                
                bool isSuccess = await _unitOfWork.Players.Create(player);

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
                                                    p => p.PlayerStatistics,
                                                    p => p.PlayerTransfers,
                                                    p => p.PlayerValues);


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
                                                    p => p.PlayerStatistics,
                                                    p => p.PlayerValues,
                                                    p => p.PlayerTransfers);
      
        } 

        public async Task InsertPlayersAndRelatedEntitiesAsync(IEnumerable<PlayerJsonForm> playerJsonForms)
        {
            List<Player> players = playerJsonForms.Select(p => p.MapToPlayer()).ToList();
            IEnumerable<PlayerPerformance> playerPerformances = playerJsonForms.Select(p => p.MapToPlayerPerformance()).ToImmutableList();
            IEnumerable<PlayerStatistics> playerStatistics = playerJsonForms.Select(p => p.MapPlayerStatistics()).ToImmutableList();
            IEnumerable<PlayerTransfer> playerTransfer = playerJsonForms.Select(p => p.MapPlayerTransfer()).ToImmutableList();
            IEnumerable<PlayerValue> playerValue = playerJsonForms.Select(p => p.MapPlayerValue()).ToImmutableList();

            const int batchSize = 100;
            var playerList = players.ToList();
          try{
             
             // await _genericRepo.BeginTransactionAsync();
          
                for (int i = 0; i < playerList.Count; i += batchSize)
                {
                    var batch = playerList.Skip(i).Take(batchSize).ToList();

                    foreach (var player in batch)
                    {   

                        Player existingPlayer = await GetPlayerbyName(player.FirstName, player.SecondName);
                        
                        var pp = playerPerformances.FirstOrDefault(p => p.PlayerId == player.PlayerId);
                        var ps = playerStatistics.FirstOrDefault(p => p.PlayerId == player.PlayerId);
                        var pt = playerTransfer.FirstOrDefault(p => p.PlayerId == player.PlayerId);
                        var pv = playerValue.FirstOrDefault(p => p.PlayerId == player.PlayerId);

                    

                        if(existingPlayer is null)
                        {

                            player.PlayerPerformances.Add(pp);
                            player.PlayerStatistics.Add(ps);
                            player.PlayerTransfers.Add(pt);
                            player.PlayerValues.Add(pv);
                        
                            // Insert the new player into the database
                            await CreatePlayer(player);
                        }
                        else await UpdatePlayer(existingPlayer, player, pp, ps, pt, pv);
                        
                    
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


        public async Task<bool> UpdatePlayer(Player existingPlayer, Player newPlayerData, PlayerPerformance pp, PlayerStatistics ps, PlayerTransfer pt, PlayerValue pv)
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
                await PlayerUpdateHelper.UpdateBasicPlayerProperties(existingPlayer, newPlayerData);

                existingPlayer.PlayerPerformances.Add(pp);
                existingPlayer.PlayerStatistics.Add(ps);
                existingPlayer.PlayerTransfers.Add(pt);
                existingPlayer.PlayerValues.Add(pv);

                // Save the changes to the repository
                bool isSuccess =  await _unitOfWork.Players.UpdateOne(existingPlayer);

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