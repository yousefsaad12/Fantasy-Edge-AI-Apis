
namespace Api.Mapping
{
    public static class PlayerStatisticsMapping
    {
        public static PlayerStatistics MapPlayerStatistics(this PlayerJsonForm playerJson)
        {   
            try
            {
                   return new PlayerStatistics
                {
                    Influence = (decimal)playerJson.influence,
                    Creativity = (decimal)playerJson.creativity,
                    Threat = (decimal)playerJson.threat,
                    IctIndex = (decimal)playerJson.ict_index,
                    ExpectedGoals = (decimal)playerJson.expected_goals,
                    ExpectedAssists = (decimal)playerJson.expected_assists,
                    ExpectedGoalInvolvements = (decimal)playerJson.expected_goal_involvements,
                    ExpectedGoalsConceded = (decimal)playerJson.expected_goals_conceded,
                    ExpectedGoalsPer90 = (decimal)playerJson.expected_goals_per_90,
                    ExpectedAssistsPer90 = (decimal)playerJson.expected_assists_per_90,
                    ExpectedGoalInvolvementsPer90 = (decimal)playerJson.expected_goal_involvements_per_90,
                    ExpectedGoalsConcededPer90 = (decimal)playerJson.expected_goals_conceded_per_90,
                    GoalsConcededPer90 = (decimal)playerJson.goals_conceded_per_90,
                    StartsPer90 = (decimal)playerJson.starts_per_90,
                    CleanSheetsPer90 = (decimal)playerJson.clean_sheets_per_90,
                    
                    PlayerId = playerJson.id 
                };
            }
            catch (Exception e)
            {
                throw;
            }
         
        }

    }
}