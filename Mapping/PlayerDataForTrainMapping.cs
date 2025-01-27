

namespace Api.Mapping
{
    static public class PlayerDataForTrainMapping
    {
        static public List<PlayerDataForTrain> MapToPlayerDataForTrain(this Player player)
        {
            var result = new List<PlayerDataForTrain>();

            // Ensure that PlayerPerformances is not null or empty
            if (player.PlayerPerformances == null || !player.PlayerPerformances.Any())
            {
                return result; // Return empty list if no performances exist
            }

            foreach (var performance in player.PlayerPerformances)
            {
                // Find corresponding statistics for the performance based on CreatedAt
                var statistics = player.PlayerStatistics?.FirstOrDefault(s => s.GameWeek == performance.GameWeek);

                var playerDataForTrain = new PlayerDataForTrain
                {
                    PlayerId = player.PlayerId,
                    GameWeek = performance.GameWeek,
                    FirstName = player.FirstName,
                    SecondName = player.SecondName,
                    Position = player.ElementTypeId,
                    TeamId = player.TeamId,

                    // Performance Data
                    Minutes = performance?.Minutes ?? 0,
                    EventPoints = performance?.EventPoints ?? 0,
                    TotalPoints = performance?.TotalPoints ?? 0,
                    GoalsScored = performance?.GoalsScored ?? 0,
                    Assists = performance?.Assists ?? 0,
                    CleanSheets = performance?.CleanSheets ?? 0,
                    GoalsConceded = performance?.GoalsConceded ?? 0,
                    PenaltiesSaved = performance?.PenaltiesSaved ?? 0,
                    PenaltiesMissed = performance?.PenaltiesMissed ?? 0,
                    OwnGoals = performance?.OwnGoals ?? 0,
                    YellowCards = performance?.YellowCards ?? 0,
                    RedCards = performance?.RedCards ?? 0,
                    Saves = performance?.Saves ?? 0,
                    Bonus = performance?.Bonus ?? 0,
                    BonusPointsSystem = performance?.BonusPointsSystem ?? 0,

                    // Value Data
                    NowCost = player.NowCost,
                    CostChangeEvent = player.CostChangeEvent,
                    CostChangeStart = player.CostChangeStart,
                    SelectedByPercent = player.SelectedByPercent,
                    ValueForm = player.ValueForm,
                    ValueSeason = player.ValueSeason,

                    // Statistics Data
                    Influence = statistics?.Influence ?? 0, // Use null-conditional to avoid exception if statistics is null
                    Creativity = statistics?.Creativity ?? 0,
                    Threat = statistics?.Threat ?? 0,
                    IctIndex = statistics?.IctIndex ?? 0,
                    ExpectedGoals = statistics?.ExpectedGoals ?? 0,
                    ExpectedAssists = statistics?.ExpectedAssists ?? 0,
                    ExpectedGoalInvolvements = statistics?.ExpectedGoalInvolvements ?? 0,
                    ExpectedGoalsConceded = statistics?.ExpectedGoalsConceded ?? 0,

                    strength_overall_home = player.team.strength_overall_home,
                    strength_overall_away = player.team.strength_overall_away,
                    strength_attack_home = player.team.strength_attack_home,
                    strength_attack_away = player.team.strength_attack_away,
                    strength_defence_home = player.team.strength_defence_home,
                    strength_defence_away = player.team.strength_defence_away,
                };

                result.Add(playerDataForTrain);
            }

           
            return result;
        }
    }
}