



namespace Api.Services
{
    public class TeamServices : ITeamsServices
    {       
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TeamServices> _logger;
        public TeamServices(IUnitOfWork unitOfWork, ILogger<TeamServices> logger)
        {   
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<bool> CreateTeam(Team team)
        {
            try
            {

                if(team is null || string.IsNullOrWhiteSpace(team.TeamName) 
                                || string.IsNullOrWhiteSpace(team.ShortName) 
                                || string.IsNullOrWhiteSpace(team.TeamDivision))
                {
                    _logger.LogWarning("Invalid data to create a team");
                     throw new ArgumentException("Team data can not be NULL");
                }

                Team ? existingTeam = await GetTeamByName(team.TeamName);

                if(existingTeam is not null)
                {
                    _logger.LogInformation("Team with this name is already created");
                    return false;
                }
                

            }
            catch(Exception e)
            {
                throw;
            }
            
        }

        public async Task<Team>? GetTeamByName(string teamName)
        {
            return await _unitOfWork.Teams.GetByName(teamName, null ,null);
        }

        public Task<IEnumerable<Team>>? GetTeamsAsync()
        {
            throw new NotImplementedException();
        }

        public Task InsertTeamsAndRelatedEntitiesAsync(IEnumerable<TeamsJsonForm> TeamsJsonForm)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTeam(Team team, TeamPerformance teamPerformance)
        {
            throw new NotImplementedException();
        }
    }
}