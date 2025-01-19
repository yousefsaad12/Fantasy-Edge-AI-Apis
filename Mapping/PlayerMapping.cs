
namespace Api.Mapping
{
    public static class PlayerMapping
    {
        public static Player MapToPlayer(this PlayerJsonForm playerJsonForm)
    
        {

            try
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
                    ChancePlayingNextRound = playerJsonForm.chance_of_playing_next_round,
                    ElementTypeId = playerJsonForm.element_type,
                    ChancePlayingThisRound = playerJsonForm.chance_of_playing_this_round,
                    TeamId = playerJsonForm.team,
                    NowCost = playerJsonForm.now_cost,
                    CostChangeEvent = playerJsonForm.cost_change_event,
                    CostChangeStart = playerJsonForm.cost_change_start,
                    SelectedByPercent = playerJsonForm.selected_by_percent,
                    ValueForm = playerJsonForm.value_form,
                    ValueSeason = playerJsonForm.value_season,


                    // Initialize navigation properties to empty collections
                    PlayerPerformances = new List<PlayerPerformance>(),
                    PlayerStatistics = new List<PlayerStatistics>()
                };
            }
            catch (Exception e)
            {
                throw;
            }

       
        }
    }
}
    
