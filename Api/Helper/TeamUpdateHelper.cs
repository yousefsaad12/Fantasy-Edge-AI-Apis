using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                team.Code = updatedTeam.Code;
                team.PulseID = updatedTeam.PulseID;
                team.TeamDivision = updatedTeam.TeamDivision;
                
                if(team.TeamId != updatedTeam.TeamId)
                team.TeamId  = updatedTeam.TeamId;
            }

              catch (Exception ex)
            {
                
                throw;
            }
        }

    }
}