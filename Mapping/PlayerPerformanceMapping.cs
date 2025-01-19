
namespace Api.Mapping
{
    public  static class PlayerPerformanceMapping
    {
         public static PlayerPerformance MapToPlayerPerformance(this PlayerStatAndPerJson playerStatAndPerJson, int _currentWeek)
        {
            try
            {
                return new PlayerPerformance
                {
                    Minutes = playerStatAndPerJson.stats.minutes,
                    EventPoints = playerStatAndPerJson.stats.EventPoints,
                    TotalPoints = playerStatAndPerJson.stats.total_points,
                    GoalsScored = playerStatAndPerJson.stats.goals_scored,
                    Assists = playerStatAndPerJson.stats.assists,
                    CleanSheets = playerStatAndPerJson.stats.clean_sheets,
                    GoalsConceded = playerStatAndPerJson.stats.goals_conceded,
                    PenaltiesSaved = playerStatAndPerJson.stats.penalties_saved,
                    PenaltiesMissed = playerStatAndPerJson.stats.penalties_missed,
                    OwnGoals = playerStatAndPerJson.stats.own_goals,
                    YellowCards = playerStatAndPerJson.stats.yellow_cards,
                    RedCards = playerStatAndPerJson.stats.red_cards,
                    Saves = playerStatAndPerJson.stats.saves,
                    Bonus =playerStatAndPerJson.stats.bonus,
                    BonusPointsSystem = playerStatAndPerJson.stats.bps,
                    IsDreamTeam = playerStatAndPerJson.stats.in_dreamteam,
                    GameWeek = _currentWeek,
                    PlayerId = playerStatAndPerJson.id
                };
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

    }
}
