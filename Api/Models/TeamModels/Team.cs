
using Api.Models.PlayerModels;

namespace Api.Models.TeamModels
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName  { get; set; }
        public string ShortName  { get; set; }
        public int Code  { get; set; }
        public int PulseID  { get; set; }
        public string TeamDivision  { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<TeamPerformance> TeamPerformances { get; set; }

    }
}