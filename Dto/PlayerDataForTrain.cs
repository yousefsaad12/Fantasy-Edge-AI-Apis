

namespace Api.Dto
{
    public class PlayerDataForTrain
    {
    public int PlayerId { get; set; }
    public int GameWeek { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public int Position { get; set; } // Player type (e.g., 1: Goalkeeper, 2: Defender, 3: Midfielder, 4: Forward)
    public int TeamId { get; set; }

    // Performance Data
    public int Minutes { get; set; }
    public int EventPoints { get; set; }
    public int TotalPoints { get; set; }
    public int GoalsScored { get; set; }
    public int Assists { get; set; }
    public int CleanSheets { get; set; }
    public int GoalsConceded { get; set; }
    public int PenaltiesSaved { get; set; }
    public int PenaltiesMissed { get; set; }
    public int OwnGoals { get; set; }
    public int YellowCards { get; set; }
    public int RedCards { get; set; }
    public int Saves { get; set; }
    public int Bonus { get; set; }
    public int BonusPointsSystem { get; set; }
    public int DreamTeamCount { get; set; }

    // Value Data
    public int ? NowCost { get; set; }
    public int ? CostChangeEvent { get; set; }
    public int ? CostChangeStart { get; set; }
    public decimal ? SelectedByPercent { get; set; }
    public decimal ? ValueForm { get; set; }
    public decimal ? ValueSeason { get; set; }

    // Transfer Data
    public int TransfersIn { get; set; }
    public int TransfersInEvent { get; set; }
    public int TransfersOut { get; set; }
    public int TransfersOutEvent { get; set; }

    // Advanced Statistics
    public decimal Influence { get; set; }
    public decimal Creativity { get; set; }
    public decimal Threat { get; set; }
    public decimal IctIndex { get; set; }
    public decimal ExpectedGoals { get; set; }
    public decimal ExpectedAssists { get; set; }
    public decimal ExpectedGoalInvolvements { get; set; }
    public decimal ExpectedGoalsConceded { get; set; }
    public decimal ExpectedGoalsPer90 { get; set; }
    public decimal ExpectedAssistsPer90 { get; set; }
    public decimal ExpectedGoalInvolvementsPer90 { get; set; }
    public decimal ExpectedGoalsConcededPer90 { get; set; }
    public decimal GoalsConcededPer90 { get; set; }
    public decimal StartsPer90 { get; set; }
    public decimal CleanSheetsPer90 { get; set; }

    public int strength_overall_home {get; set;}
    public int strength_overall_away {get; set;}
    public int strength_attack_home {get; set;}
    public int strength_attack_away {get; set;}
    public int strength_defence_home {get; set;}
    public int strength_defence_away {get; set;}
    }
}