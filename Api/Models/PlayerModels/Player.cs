namespace Api.Models.PlayerModels
{
    public class Player
    {   
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string WebName { get; set; }
        public int TeamId { get; set; } 
        public int ElementTypeId { get; set; }
        public int ?  SquadNumber { get; set; } 
        public char Status { get; set; }
        public string ?  News { get; set; }
        public DateTime ?  NewsAdded { get; set; }
        public int ? ChancePlayingNextRound { get; set; }
        public int ? ChancePlayingThisRound { get; set; }

    }
}