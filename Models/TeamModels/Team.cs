

namespace Api.Models.TeamModels
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName  { get; set; }
        public string ShortName  { get; set; }
        
        public int strength_overall_home { get; set;}
        public int strength_overall_away { get; set;}
        public int strength_attack_home { get; set;}
        public int strength_attack_away { get; set;}
        public int strength_defence_home { get; set;}
        public int strength_defence_away { get; set;}

        public ICollection<Player> Players { get; set; }
       

    }
}