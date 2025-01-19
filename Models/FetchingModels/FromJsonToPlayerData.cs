using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.FetchingModels
{
    public class FromJsonToPlayerData
    {
        public int id { get; set; }
        public string first_name { get; set; }

        public string second_name { get; set; }

        public string web_name { get; set; }

        public string status { get; set; }

        public int ? squad_number { get; set; }

        public string ? news { get; set; }  
        
        public int ? chance_of_playing_next_round { get; set; }

        public int ? chance_of_playing_this_round { get; set; }

        public int element_type { get; set; }

        public int team { get; set; }
        
        // player transfer data
        public int transfers_in  { get; set; }
        public int transfers_in_event  { get; set; }
        public int transfers_out   { get; set; }
        public int transfers_out_event  { get; set; }

        // player value data
        public int now_cost { get; set; }
        public int cost_change_event { get; set; }
        public int cost_change_start { get; set; }
        public decimal selected_by_percent { get; set; }
        public decimal  value_form { get; set; }
        public decimal value_season { get; set; }

        // player perf
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
        public int in_dreamteam  { get; set; }


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