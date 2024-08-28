

namespace Api.Repository
{
    public class PlayerRepo : IPlayerRepo
    {   

        private readonly AppDbContext _context;
        private readonly ILogger<PlayerRepo> _logger;
        public PlayerRepo(AppDbContext context, ILogger<PlayerRepo> logger)
        {
            _context = context;
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

        public async Task<Player>? GetPlayerbyId(int Id)
        {   
            try{
                Player ? player = await _context.Players
                                 .Include(p => p.PlayerPerformances)
                                 .Include(p => p.PlayerStatistics)
                                 .Include(p => p.PlayerTransfers)
                                 .Include(p => p.PlayerValues)
                                 .FirstOrDefaultAsync(p => p.PlayerId == Id);

                return player == null ? null : player;
            }

            catch(Exception ex)
            {
                throw;
            }
            
        }

        public async Task<Player>? GetPlayerbyName(string FirstName, string SecondName)
        {
            try{
                
                Player ? player = await _context.Players
                                 .Include(p => p.PlayerPerformances)
                                 .Include(p => p.PlayerStatistics)
                                 .Include(p => p.PlayerTransfers)
                                 .Include(p => p.PlayerValues)
                                 .FirstOrDefaultAsync(p => p.FirstName == FirstName && p.SecondName == SecondName);

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