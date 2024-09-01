
namespace Api.Interfaces
{
    public interface ITeamsServices
    {
        public  Task InsertTeamsAndRelatedEntitiesAsync(IEnumerable<TeamsJsonForm> TeamsJsonForm);
        public Task<bool> CreatePlayer(Player player);
    }
}