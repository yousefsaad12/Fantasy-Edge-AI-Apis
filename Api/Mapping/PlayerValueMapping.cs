using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mapping
{
    public static class PlayerValueMapping
    {
        public  static PlayerValue MapPlayerValue(this PlayerJsonForm playerJson)
        {   
            try
            {
                 return new PlayerValue
                {
                    NowCost = playerJson.now_cost,
                    CostChangeEvent = playerJson.cost_change_event,
                    CostChangeStart = playerJson.cost_change_start,
                    SelectedByPercent = (decimal)playerJson.selected_by_percent,
                    ValueForm = (decimal)playerJson.value_form,
                    ValueSeason = (decimal)playerJson.value_season,
                    
                    PlayerId = playerJson.id // Assuming PlayerId is mapped to id
                };
            }
            catch(Exception e)
            {
                throw;
            }
           
        }

    }
}