using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mapping
{
    public static class PlayerMapping
    {
        public static Player MapToPlayer(this PlayerJsonForm playerJsonForm)
    
        {

        return new Player
        {
            PlayerId = playerJsonForm.id,
            FirstName = playerJsonForm.first_name,
            SecondName = playerJsonForm.second_name,
            WebName = playerJsonForm.web_name,
            Status = playerJsonForm.status,
            SquadNumber = playerJsonForm.squad_number,
            News = playerJsonForm.news,
            NewsAdded = playerJsonForm.news_added,
            ChancePlayingNextRound = playerJsonForm.chance_of_playing_next_round,
            ElementTypeId = playerJsonForm.element_type,
            ChancePlayingThisRound = playerJsonForm.chance_of_playing_this_round,
            TeamId = 1,

            // Initialize navigation properties to empty collections
            PlayerPerformances = new List<PlayerPerformance>(),
            PlayerValues = new List<PlayerValue>(),
            PlayerTransfers = new List<PlayerTransfer>(),
            PlayerStatistics = new List<PlayerStatistics>()
        };
        }
    }
}
    
