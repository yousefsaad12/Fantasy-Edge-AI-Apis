



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

                bool isSuccess = await _unitOfWork.Teams.Create(team);

                if(isSuccess) _logger.LogInformation("Team has been Add");
                else _logger.LogWarning($"Felid to added team with name {team.TeamName}");

                return isSuccess;

            }
            catch(Exception ex)
            {   
                _logger.LogError(ex, $"An error occurred while creating team: {team.TeamName}");
                throw;
            }
            
        }

        public async Task<Team>? GetTeamByName(string teamName)
        {
            return await _unitOfWork.Teams.GetByName(teamName, null ,null);
        }

        public async Task<IEnumerable<Team>>? GetTeamsAsync()
        {
            return await _unitOfWork.Teams.GetAll();
        }

        public async Task InsertTeamsAndRelatedEntitiesAsync(IEnumerable<TeamsJsonForm> TeamsJsonForm)
        {
            var Teams = TeamsJsonForm.Select(t => t.MapTeam()).ToList();
            var TeamsPerformance = TeamsJsonForm.Select(tp => tp.MapTeamPerformance()).ToList();

            const int batchSize = 100;

            try
            {
                for(int i = 0; i < Teams.Count; i += batchSize)
                {
                    var batch = Teams.Skip(i).Take(batchSize).ToList();

                    foreach(var team in batch)
                    {
                        var tp = TeamsPerformance.FirstOrDefault(tp => tp.TeamId == team.TeamId);
                        Team existingTeam = await GetTeamByName(team.TeamName);

                        if(existingTeam is null)
                        {
                            team.TeamPerformances.Add(tp);

                            await _unitOfWork.Teams.Create(team);
                        }

                        else await UpdateTeam(team, existingTeam, tp);
                    }
                }

                  _logger.LogInformation("All Teams inserted/updated successfully.");
            }

            catch(Exception ex)
            {   
                _logger.LogError(ex, "An error occurred while inserting/updating teams.");
                throw;
            }
        }

        public Task<bool> UpdateTeam(Team team, Team existingTeam, TeamPerformance teamPerformance)
        {
            throw new NotImplementedException();
        }
    }
}