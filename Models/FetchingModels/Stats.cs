
namespace Api.Models.FetchingModels
{
    public class Stats
    {
        public int minutes { get; set; }
        public int EventPoints { get; set; }
        public int total_points { get; set; }
        public int goals_scored  { get; set; }
        public int assists { get; set; }
        public int clean_sheets { get; set; }
        public int goals_conceded { get; set; }
        public int penalties_saved { get; set; }
        public int penalties_missed { get; set; }
        public int own_goals { get; set; }
        public int yellow_cards { get; set; }
        public int red_cards { get; set; }
        public int saves { get; set; }
        public int bonus { get; set; }
        public int bps { get; set; }
        public bool in_dreamteam  { get; set; }


        // player statist

        public decimal influence  { get; set; }  
        public decimal creativity  { get; set; }  
        public decimal threat { get; set; }  
        public decimal ict_index  { get; set; }  
        public decimal expected_goals { get; set; }  
        public decimal expected_assists { get; set; }  
        public decimal expected_goal_involvements { get; set; }  
        public decimal expected_goals_conceded { get; set; }  
    }
}