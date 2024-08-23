namespace Api.Models
{
    public class PlayerPerformance
    {
        public int PlayerPerformanceID { get; set; }
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
        public int DreamTeamCount  { get; set; }

        public int PlayerId { get; set; }
        public Player player { get; set; }

        public int GameweekId { get; set; }
        public Gameweeks Gameweeks { get; set; }
        
    }
}