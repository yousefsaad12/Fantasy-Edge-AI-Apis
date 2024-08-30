

using Api.Models.TeamModels;

namespace Api.Mapping
{
    public static class TeamMapping
    {
        public static Team MapTeam(this TeamsJsonForm teamJson)
        {
            return new Team
            {
                TeamId = teamJson.id,
                TeamName = teamJson.name,
                ShortName = teamJson.short_name,
                Code = teamJson.code,
                PulseID = teamJson.pulse_id,
                TeamDivision = teamJson.team_division.HasValue ? teamJson.team_division.Value.ToString() : null
            };
        }

    }
}