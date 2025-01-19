

namespace Api.Models.PlayerModels
{
    public class PlayerPerformance
    {
        public int PlayerPerformanceId { get; set; }
        public int Minutes { get; set; }
        public int EventPoints { get; set; }
        public int TotalPoints { get; set; }
        public int GoalsScored  { get; set; }
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
        public bool IsDreamTeam  { get; set; }
        public int GameWeek { get; set; }

        public int PlayerId { get; set; }

        

        // Navigation Properties
        public Player player { get; set; }
        
        
    }
}