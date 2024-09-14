
using Api.Dto;

namespace Api.Mapping
{
    static public class PlayerDataForTrainMapping
    {
        static public PlayerDataForTrain MapToPlayerDataForTrain(this Player player)
        {
            var latestPerformance = player.PlayerPerformances.LastOrDefault();
            var latestValue = player.PlayerValues.LastOrDefault();
            var latestTransfer = player.PlayerTransfers.LastOrDefault();
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
                NowCost = latestValue?.NowCost ?? 0,
                CostChangeEvent = latestValue?.CostChangeEvent ?? 0,
                CostChangeStart = latestValue?.CostChangeStart ?? 0,
                SelectedByPercent = latestValue.SelectedByPercent,
                ValueForm = latestValue.ValueForm,
                ValueSeason = latestValue.ValueSeason,

                // Transfer Data
                TransfersIn = latestTransfer?.TransfersIn ?? 0,
                TransfersInEvent = latestTransfer?.TransfersInEvent ?? 0,
                TransfersOut = latestTransfer?.TransfersOut ?? 0,
                TransfersOutEvent = latestTransfer?.TransfersOutEvent ?? 0,

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