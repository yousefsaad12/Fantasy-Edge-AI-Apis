
namespace Api.Repository
{
    public class PlayerRepo : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepo(AppDbContext context, ILogger<GenericRepository<Player>> logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<PlayerSearchResponse>> GetPlayerNames()
        {
            return await _context.Players.Select(p => new PlayerSearchResponse {Name = p.FirstName + " " + p.SecondName})
                                         .ToListAsync()
                                         .ConfigureAwait(false);
        }
    }
}