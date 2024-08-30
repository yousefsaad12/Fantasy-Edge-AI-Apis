
namespace Api.Mapping
{
    public  static class PlayerPerformanceMapping
    {
        public static PlayerPerformance MapToPlayerPerformance(this PlayerJsonForm playerJsonForm)
        {
            try
            {
                return new PlayerPerformance
                {
                    Minutes = playerJsonForm.minutes,
                    EventPoints = playerJsonForm.event_points,
                    TotalPoints = playerJsonForm.total_points,
                    GoalsScored = playerJsonForm.goals_scored,
                    Assists = playerJsonForm.assists,
                    CleanSheets = playerJsonForm.clean_sheets,
                    GoalsConceded = playerJsonForm.goals_conceded,
                    PenaltiesSaved = playerJsonForm.penalties_saved,
                    PenaltiesMissed = playerJsonForm.penalties_missed,
                    OwnGoals = playerJsonForm.own_goals,
                    YellowCards = playerJsonForm.yellow_cards,
                    RedCards = playerJsonForm.red_cards,
                    Saves = playerJsonForm.saves,
                    Bonus = playerJsonForm.bonus,
                    BonusPointsSystem = playerJsonForm.bps,
                    DreamTeamCount = playerJsonForm.dreamteam_count,
                    PlayerId = playerJsonForm.id  // Assuming `PlayerId` is mapped to `id`
                };
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
    }
}