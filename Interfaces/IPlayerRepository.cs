public interface IPlayerRepository : IGenericRepository<Player>
{
    public Task<IEnumerable<PlayerSearchResponse>> GetPlayerNames();
    
}
