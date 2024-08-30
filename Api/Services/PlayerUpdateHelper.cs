namespace Api.Services
{
    public static class PlayerUpdateHelper
    {
        public static async Task UpdateBasicPlayerProperties(Player existingPlayer, Player updatedPlayer)
        {   
            try
            {
                existingPlayer.FirstName = updatedPlayer.FirstName;
                existingPlayer.SecondName = updatedPlayer.SecondName;
                existingPlayer.WebName = updatedPlayer.WebName;
                existingPlayer.Status = updatedPlayer.Status;
                existingPlayer.SquadNumber = updatedPlayer.SquadNumber;
                existingPlayer.News = updatedPlayer.News;
                existingPlayer.NewsAdded = updatedPlayer.NewsAdded;
                existingPlayer.ChancePlayingNextRound = updatedPlayer.ChancePlayingNextRound;
                existingPlayer.ChancePlayingThisRound = updatedPlayer.ChancePlayingThisRound;
                existingPlayer.ElementTypeId = updatedPlayer.ElementTypeId;
                existingPlayer.TeamId = updatedPlayer.TeamId;

                if(existingPlayer.PlayerId != updatedPlayer.PlayerId)
                existingPlayer.PlayerId = updatedPlayer.PlayerId;
            }

              catch (Exception ex)
            {
             
                throw;
            }
        }

         public static async Task UpdatePlayerPerformances(PlayerPerformance existingPerformance, PlayerPerformance updatedPerformance)
        {  
            try
            {
                existingPerformance.Minutes = updatedPerformance.Minutes;
                existingPerformance.EventPoints = updatedPerformance.EventPoints;
                existingPerformance.TotalPoints = updatedPerformance.TotalPoints;
                existingPerformance.GoalsScored = updatedPerformance.GoalsScored;
                existingPerformance.Assists = updatedPerformance.Assists;
                existingPerformance.CleanSheets = updatedPerformance.CleanSheets;
                existingPerformance.GoalsConceded = updatedPerformance.GoalsConceded;
                existingPerformance.PenaltiesSaved = updatedPerformance.PenaltiesSaved;
                existingPerformance.PenaltiesMissed = updatedPerformance.PenaltiesMissed;
                existingPerformance.OwnGoals = updatedPerformance.OwnGoals;
                existingPerformance.YellowCards = updatedPerformance.YellowCards;
                existingPerformance.RedCards = updatedPerformance.RedCards;
                existingPerformance.Saves = updatedPerformance.Saves;
                existingPerformance.Bonus = updatedPerformance.Bonus;
                existingPerformance.BonusPointsSystem = updatedPerformance.BonusPointsSystem;
                existingPerformance.DreamTeamCount = updatedPerformance.DreamTeamCount;
                
                if(existingPerformance.PlayerId != updatedPerformance.PlayerId)
                    existingPerformance.PlayerId = updatedPerformance.PlayerId;
            } 
        
             catch (Exception ex)
            {
             
                throw;
            }
        }

        public static void UpdatePlayerStatistics(PlayerStatistics existingStatistics, PlayerStatistics updatedStatistics)
        {
              try
            {
                existingStatistics.Influence = updatedStatistics.Influence;
                existingStatistics.Creativity = updatedStatistics.Creativity;
                existingStatistics.Threat = updatedStatistics.Threat;
                existingStatistics.IctIndex = updatedStatistics.IctIndex;
                existingStatistics.ExpectedGoals = updatedStatistics.ExpectedGoals;
                existingStatistics.ExpectedAssists = updatedStatistics.ExpectedAssists;
                existingStatistics.ExpectedGoalInvolvements = updatedStatistics.ExpectedGoalInvolvements;
                existingStatistics.ExpectedGoalsConceded = updatedStatistics.ExpectedGoalsConceded;
                existingStatistics.ExpectedGoalsPer90 = updatedStatistics.ExpectedGoalsPer90;
                existingStatistics.ExpectedAssistsPer90 = updatedStatistics.ExpectedAssistsPer90;
                existingStatistics.ExpectedGoalInvolvementsPer90 = updatedStatistics.ExpectedGoalInvolvementsPer90;
                existingStatistics.ExpectedGoalsConcededPer90 = updatedStatistics.ExpectedGoalsConcededPer90;
                existingStatistics.GoalsConcededPer90 = updatedStatistics.GoalsConcededPer90;
                existingStatistics.StartsPer90 = updatedStatistics.StartsPer90;
                existingStatistics.CleanSheetsPer90 = updatedStatistics.CleanSheetsPer90;
                
                if(existingStatistics.PlayerId != existingStatistics.PlayerId)
                    existingStatistics.PlayerId = existingStatistics.PlayerId;
            }
            
             catch (Exception ex)
            {
             
                throw;
            }
        }
    }
}