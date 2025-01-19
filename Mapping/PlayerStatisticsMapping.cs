
namespace Api.Mapping
{
    public static class PlayerStatisticsMapping
    {
        public static PlayerStatistics MapPlayerStatistics(this PlayerStatAndPerJson playerStatAndPerJson, int _currentWeek)
        {   
            try
            {
                   return new PlayerStatistics
                {
                    Influence = (decimal)playerStatAndPerJson.stats.influence,
                    Creativity = (decimal)playerStatAndPerJson.stats.creativity,
                    Threat = (decimal)playerStatAndPerJson.stats.threat,
                    IctIndex = (decimal)playerStatAndPerJson.stats.ict_index,
                    ExpectedGoals = (decimal)playerStatAndPerJson.stats.expected_goals,
                    ExpectedAssists = (decimal)playerStatAndPerJson.stats.expected_assists,
                    ExpectedGoalInvolvements = (decimal)playerStatAndPerJson.stats.expected_goal_involvements,
                    ExpectedGoalsConceded = (decimal)playerStatAndPerJson.stats.expected_goals_conceded,
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