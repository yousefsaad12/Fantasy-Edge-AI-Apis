

namespace Api.Helper
{
    public static class TeamUpdateHelper
    {
          public static async Task UpdateBasicTeamProperties(Team team, Team updatedTeam)
        {   
            try
            {
                team.TeamName = updatedTeam.TeamName;
                team.ShortName = updatedTeam.ShortName;
                team.strength_overall_home = updatedTeam.strength_overall_home;
                team.strength_overall_away = updatedTeam.strength_overall_away;
                team.strength_attack_home = updatedTeam.strength_attack_home;
                team.strength_attack_away = updatedTeam.strength_attack_away;
                team.strength_defence_home = updatedTeam.strength_defence_home;
                team.strength_defence_away = updatedTeam.strength_defence_away;
               
            }

              catch (Exception ex)
            {
                
                throw;
            }
        }

    }
}