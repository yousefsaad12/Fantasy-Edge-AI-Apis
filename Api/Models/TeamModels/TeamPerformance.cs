
namespace Api.Models.TeamModels
{
    public class TeamPerformance
    {
        public int TeamPerformanceId { get; set; }
        public int Played  { get; set; }
        public int Win  { get; set; }
        public int Loss  { get; set; }
        public int Draw { get; set; }
        public int Points  { get; set; }
        public int Position   { get; set; }
        public int GameweekId { get; set; }
        public int TeamId  { get; set; }

        // Navigation Properties
        public Gameweeks gameweeks { get; set; }
        public Team team { get; set; }

    }
}