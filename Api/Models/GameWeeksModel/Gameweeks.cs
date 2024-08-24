using Api.Models.PlayerModels;
using Api.Models.TeamModels;

namespace Api.Models.GameWeeksModel
{
    public class Gameweeks
    {
        public int GameweekId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation Properties
        public ICollection<TeamPerformance> TeamPerformances { get; set; }
        public ICollection<PlayerPerformance> PlayerPerformances { get; set; }
        public ICollection<PlayerStatistics> PlayerStatistics { get; set; }
        public ICollection<PlayerTransfer> PlayerTransfer { get; set; }
        public ICollection<PlayerValue> PlayerValue { get; set; }
    }
}