

namespace Api.Dto
{
    public class PlayerInfo
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ? Position { get; set; } 
        public string Team { get; set; }
        public string WebName { get; set; }
        
        public string Status { get; set; }
        public int ?  SquadNumber { get; set; } 
        public string ?  News { get; set; }
        public DateTime ?  NewsAdded { get; set; }
        public int ? ChancePlayingNextRound { get; set; }
        public int ? ChancePlayingThisRound { get; set; }

    }
}