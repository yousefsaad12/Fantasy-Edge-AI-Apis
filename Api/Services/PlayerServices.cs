

namespace Api.Services
{
    public class PlayerServices : IPlayerServices
    {   

        private readonly IGenericRepository<Player> _genericRepo;
        private readonly ILogger<PlayerServices> _logger;
        public PlayerServices(IGenericRepository<Player> genericRepo, ILogger<PlayerServices> logger)
        {
            _genericRepo = genericRepo;
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

                
                bool isSuccess = await _genericRepo.Create(player);

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

                Player ? player = await _genericRepo.GetbyName(FirstName, SecondName,
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

        public async Task<bool> UpdataPlayer(Player player, string FirstName, string SecondName)
        {
            try
            {
                // Check if the player exists
                var existingPlayer = await _genericRepo.GetbyName(FirstName, SecondName);
               

                // Update basic player information
                existingPlayer.FirstName = player.FirstName;
                existingPlayer.SecondName = player.SecondName;
                existingPlayer.WebName = player.WebName;
                existingPlayer.Status = player.Status;
                existingPlayer.SquadNumber = player.SquadNumber;
                existingPlayer.News = player.News;
                existingPlayer.NewsAdded = player.NewsAdded;
                existingPlayer.ChancePlayingNextRound = player.ChancePlayingNextRound;
                existingPlayer.ChancePlayingThisRound = player.ChancePlayingThisRound;
                existingPlayer.ElementTypeId = player.ElementTypeId;
                existingPlayer.TeamId = player.TeamId;

                // Update related entities (Performance, Value, Statistics, Transfers)
                // Ensure you handle these updates in a way that suits your data relationships and logic

                UpdatePlayerPerformance(existingPlayer, player.PlayerPerformances);
                UpdatePlayerValue(existingPlayer, player.PlayerValues);
                UpdatePlayerStatistics(existingPlayer, player.PlayerStatistics);
                UpdatePlayerTransfers(existingPlayer, player.PlayerTransfers);

                // Save changes
                bool isSuccess = await _genericRepo.Update(existingPlayer);

                if (isSuccess) 
                    _logger.LogInformation("Player successfully updated: {FirstName} {SecondName}", player.FirstName, player.SecondName);
                else 
                    _logger.LogWarning("Failed to update player: {FirstName} {SecondName}", player.FirstName, player.SecondName);

                return isSuccess;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating player: {PlayerId}", player.PlayerId);
                throw;
            }
        }
    }
}