

namespace Api.Interfaces
{
    public interface IPlayerServices
    {
        
        public Task<Player> ? GetPlayerbyName(string FirstName, string LastName);
        public Task<IEnumerable<Player>> ? GetPlayersAsync();
        public Task<bool> CreatePlayer(Player player);
        public  Task InsertPlayersAndRelatedEntitiesAsync(IEnumerable<PlayerJsonForm> playerJsonForms, IEnumerable<PlayerStatAndPerJson> playerStatAndPerJsons);
        public  Task<bool> UpdatePlayer(Player existingPlayer, Player newPlayerData, PlayerPerformance pp, PlayerStatistics ps);
    }
}