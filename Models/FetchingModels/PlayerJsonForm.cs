using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.FetchingModels
{
    public class PlayerJsonForm
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

        public PlayerStatAndPerJson playerStatAndPerJson { get; set; }
    }
}