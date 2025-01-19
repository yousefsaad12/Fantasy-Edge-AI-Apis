

using Api.Models.TeamModels;

namespace Api.Mapping
{
    public static class TeamMapping
    {   
        public static Team MapTeam(this TeamsJsonForm teamJson)
        {   
            try
            {   
                return new Team
                {   
                    TeamId = teamJson.id,
                    TeamName = teamJson.name,
                    ShortName = teamJson.short_name,
                    strength_overall_home = teamJson.strength_overall_home,
                    strength_overall_away = teamJson.strength_overall_away,
                    strength_attack_home = teamJson.strength_attack_home,
                    strength_attack_away = teamJson.strength_attack_away,
                    strength_defence_home = teamJson.strength_defence_home,
                    strength_defence_away = teamJson.strength_defence_away,

                    Players = new List<Player>()
                };
            }

            catch (Exception ex)
            {
                throw;
            }
        }

    }
}