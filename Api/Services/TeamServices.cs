using Api.Helper;

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
                _logger.LogInformation($"Attempting to create team with name: {team.TeamName}");

                if (team == null || string.IsNullOrWhiteSpace(team.TeamName) || string.IsNullOrWhiteSpace(team.ShortName))
                {
                    _logger.LogWarning("Invalid data to create a team");
                    throw new ArgumentException("Team data cannot be NULL or empty");
                }

                Team existingTeam = await GetTeamByName(team.TeamName);

                if (existingTeam != null)
                {
                    _logger.LogInformation("Team with this name already exists");
                    return false;
                }

                bool isSuccess = await _unitOfWork.Teams.Create(team);

                if (isSuccess)
                {
                    _logger.LogInformation("Team has been added successfully");
                }
                else
                {
                    _logger.LogWarning($"Failed to add team with name {team.TeamName}");
                }

                return isSuccess;
            }
            catch (Exception ex)
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


        public async Task InsertTeamsAndRelatedEntitiesAsync(IEnumerable<TeamsJsonForm> teamsJsonForms)
        {
            _logger.LogInformation("Starting to insert/update teams.");

            if (teamsJsonForms == null)
            {
                _logger.LogError("teamsJsonForms is null.");
                throw new ArgumentNullException(nameof(teamsJsonForms), "TeamsJsonForms cannot be null.");
            }

                var Teams = teamsJsonForms.Select(t => t.MapTeam()).ToList();

            try
            {
                // Retrieve all existing teams at once to reduce the number of database calls
                var existingTeams = (await _unitOfWork.Teams.GetAll()).ToDictionary(t => t.TeamName);

                foreach (var team in Teams)
                {

                    if (team == null)
                    {
                        _logger.LogWarning("A null team object was found and skipped.");
                        continue;
                    }

                    _logger.LogInformation($"Processing team with Name: {team.TeamName}");

                    if (existingTeams.TryGetValue(team.TeamName, out var existingTeam))
                    {
                        _logger.LogInformation($"Existing team found. Updating team with Name: {team.TeamName}");
                        await UpdateTeam(existingTeam, team);
                    }
                    else
                    {
                        _logger.LogInformation($"No existing team found. Inserting new team with Name: {team.TeamName}");
                        await _unitOfWork.Teams.Create(team);
                    }
                }

                _logger.LogInformation("All teams inserted/updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting/updating teams.");
                throw;
            }
        }



        public async Task<bool> UpdateTeam(Team existingTeam, Team updatedTeam)
        {
            try
            {
                _logger.LogInformation($"Updating team: {existingTeam?.TeamName ?? "null"} with data from: {updatedTeam.TeamName}");

                if (updatedTeam == null || string.IsNullOrWhiteSpace(updatedTeam.TeamName) || string.IsNullOrWhiteSpace(updatedTeam.ShortName))
                {
                    _logger.LogWarning("Invalid data to update a team");
                    throw new ArgumentException("Team data cannot be NULL or empty");
                }

                if (existingTeam == null)
                {
                    _logger.LogInformation("Existing team is null, update cannot be applied");
                    return false;
                }

                await TeamUpdateHelper.UpdateBasicTeamProperties(existingTeam, updatedTeam);
                

                bool isSuccess = await _unitOfWork.Teams.UpdateOne(existingTeam);

                if (isSuccess)
                {
                    _logger.LogInformation("Team has been updated successfully");
                }
                else
                {
                    _logger.LogWarning("An error occurred while updating the team");
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating team: {updatedTeam.TeamName}");
                throw;
            }
        }
    }
}