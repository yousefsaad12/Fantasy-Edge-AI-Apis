
using Api.Models.TeamModels;

namespace Api.Models.PlayerModels
{
    public class Player
    {   
        [Key]
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string WebName { get; set; }
        
        public string Status { get; set; }
        public int ?  SquadNumber { get; set; } 
        public string ?  News { get; set; }
        public DateTime ?  NewsAdded { get; set; }
        public int ? ChancePlayingNextRound { get; set; }
        public int ? ChancePlayingThisRound { get; set; }

        public int ? NowCost { get; set; }
        public int ? CostChangeEvent { get; set; }
        public int ? CostChangeStart { get; set; }
        public decimal ? SelectedByPercent { get; set; }
        public decimal ? ValueForm { get; set; }
        public decimal ? ValueSeason { get; set; }
        public int ElementTypeId { get; set; }
        public int TeamId { get; set; } 

         // Navigation Properties
        public ElementTypes elementType { get; set; }
        public Team team { get; set; } 

        public ICollection<PlayerPerformance> PlayerPerformances { get; set; }
        public ICollection<PlayerStatistics> PlayerStatistics { get; set; }

    }
}