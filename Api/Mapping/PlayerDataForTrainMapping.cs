
using Api.Dto;

namespace Api.Mapping
{
    static public class PlayerDataForTrainMapping
    {
        static public PlayerDataForTrain MapToPlayerDataForTrain(this Player player)
        {
            var latestPerformance = player.PlayerPerformances.LastOrDefault();
            var latestStatistics = player.PlayerStatistics.LastOrDefault();

            return new PlayerDataForTrain
            {
                PlayerId = player.PlayerId,
                FirstName = player.FirstName,
                SecondName = player.SecondName,
                WebName = player.WebName,
                Status = player.Status,
                SquadNumber = player.SquadNumber,
                News = player.News,
                ElementTypeId = player.ElementTypeId,
                TeamId = player.TeamId,

                // Performance Data
                Minutes = latestPerformance?.Minutes ?? 0,
                EventPoints = latestPerformance?.EventPoints ?? 0,
                TotalPoints = latestPerformance?.TotalPoints ?? 0,
                GoalsScored = latestPerformance?.GoalsScored ?? 0,
                Assists = latestPerformance?.Assists ?? 0,
                CleanSheets = latestPerformance?.CleanSheets ?? 0,
                GoalsConceded = latestPerformance?.GoalsConceded ?? 0,
                PenaltiesSaved = latestPerformance?.PenaltiesSaved ?? 0,
                PenaltiesMissed = latestPerformance?.PenaltiesMissed ?? 0,
                OwnGoals = latestPerformance?.OwnGoals ?? 0,
                YellowCards = latestPerformance?.YellowCards ?? 0,
                RedCards = latestPerformance?.RedCards ?? 0,
                Saves = latestPerformance?.Saves ?? 0,
                Bonus = latestPerformance?.Bonus ?? 0,
                BonusPointsSystem = latestPerformance?.BonusPointsSystem ?? 0,
               

                // Value Data
                NowCost = player.NowCost,
                CostChangeEvent = player.CostChangeEvent,
                CostChangeStart = player.CostChangeStart,
                SelectedByPercent = player.SelectedByPercent,
                ValueForm = player.ValueForm,
                ValueSeason = player.ValueSeason,

                // Transfer Data

                // Statistics Data
                Influence = latestStatistics.Influence,
                Creativity = latestStatistics.Creativity,
                Threat = latestStatistics.Threat,
                IctIndex = latestStatistics.IctIndex,
                ExpectedGoals = latestStatistics.ExpectedGoals,
                ExpectedAssists = latestStatistics.ExpectedAssists,
                ExpectedGoalInvolvements = latestStatistics.ExpectedGoalInvolvements,
                ExpectedGoalsConceded = latestStatistics.ExpectedGoalsConceded,

                
                
                
            };

        }
    }
}