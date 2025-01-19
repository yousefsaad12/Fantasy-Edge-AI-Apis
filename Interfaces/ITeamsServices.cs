

namespace Api.Interfaces
{
    public interface ITeamsServices
    {
        public  Task InsertTeamsAndRelatedEntitiesAsync(IEnumerable<TeamsJsonForm> TeamsJsonForm);
        public Task<bool> CreateTeam(Team team);
        public  Task<bool> UpdateTeam(Team team, Team existingTeam);
        public Task<IEnumerable<Team>> ? GetTeamsAsync();

        public Task<Team> ? GetTeamByName(string teamName);
    }
}