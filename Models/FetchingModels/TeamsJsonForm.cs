
namespace Api.Models.FetchingModels
{
    public class TeamsJsonForm
    {
        
        public int code { get; set; }
        public int draw { get; set; }
        public string? form { get; set; }
        public int id { get; set; }
        public int loss { get; set; }
        public string name { get; set; }
        public int played { get; set; }
        public int points { get; set; }
        public int position { get; set; }
        public string short_name { get; set; }
        public int strength { get; set; }
        public string team_division { get; set; }  // Nullable int since it might be null
        public bool unavailable { get; set; }
        public int win { get; set; }
        public int strength_overall_home { get; set; }
        public int strength_overall_away { get; set; }
        public int strength_attack_home { get; set; }
        public int strength_attack_away { get; set; }
        public int strength_defence_home { get; set; }
        public int strength_defence_away { get; set; }
        public int pulse_id { get; set; }

    }
}