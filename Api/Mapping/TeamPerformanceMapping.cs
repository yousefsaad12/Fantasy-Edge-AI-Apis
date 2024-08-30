

using Api.Models.TeamModels;

namespace Api.Mapping
{
    public static class TeamPerformanceMapping
    {
        public static TeamPerformance MapTeamPerformance(this TeamsJsonForm teamJson)
        {
            try
            {
                return new TeamPerformance
                {
                    Played = teamJson.played,
                    Win = teamJson.win,
                    Loss = teamJson.loss,
                    Draw = teamJson.draw,
                    Points = teamJson.points,
                    Position = teamJson.position,
                    TeamId = teamJson.id  // Assuming TeamId refers to `id` from TeamsJsonForm
                };
            }
            catch (Exception ex)
            {
                throw;
               
            }
        }
    }
}