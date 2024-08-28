

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

                if (string.IsNullOrWhiteSpace(player.FirstName) || string.IsNullOrWhiteSpace(player.SecondName))
                {
                    _logger.LogWarning("Attempted to create a player with invalid names: {FirstName} {SecondName}", player.FirstName, player.SecondName);
                    throw new ArgumentException("Player names cannot be null or empty.");
                }

                Player ? isExists = await GetPlayerbyName(player.FirstName, player.SecondName);

                if (isExists != null)
                {
                    _logger.LogInformation("Player already exists: {FirstName} {SecondName}", player.FirstName, player.SecondName);
                    return false; 
                }

                await _context.Players.AddAsync(player);
                _logger.LogInformation("Player added to context: {FirstName} {SecondName}", player.FirstName, player.SecondName);
                
                bool isSuccess = await _context.SaveChangesAsync() > 0;

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
                                                    p => player.PlayerTransfers,
                                                    p => p.PlayerValues);


                return player == null ? null : player;
            }
               catch(Exception ex)
            {
                throw;
            }
        }

        public Task<bool> UpdataPlayer(Player player, string FirstName, string SecondName)
        {
            throw new NotImplementedException();
        }
    }
}