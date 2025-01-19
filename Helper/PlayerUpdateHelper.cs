namespace Api.Services
{
    public static class PlayerUpdateHelper
    {
        public static void UpdateBasicPlayerProperties(Player existingPlayer, Player updatedPlayer)
        {   
            try
            {
                existingPlayer.FirstName = updatedPlayer.FirstName;
                existingPlayer.SecondName = updatedPlayer.SecondName;
                existingPlayer.WebName = updatedPlayer.WebName;
                existingPlayer.Status = updatedPlayer.Status;
                existingPlayer.SquadNumber = updatedPlayer.SquadNumber;
                existingPlayer.News = updatedPlayer.News;
                existingPlayer.NewsAdded = updatedPlayer.NewsAdded;
                existingPlayer.ChancePlayingNextRound = updatedPlayer.ChancePlayingNextRound;
                existingPlayer.ChancePlayingThisRound = updatedPlayer.ChancePlayingThisRound;
                existingPlayer.ElementTypeId = updatedPlayer.ElementTypeId;
                existingPlayer.TeamId = updatedPlayer.TeamId;
                existingPlayer.NowCost = updatedPlayer.NowCost;
                existingPlayer.CostChangeEvent = updatedPlayer.CostChangeEvent;
                existingPlayer.CostChangeStart = updatedPlayer.CostChangeStart;
                existingPlayer.SelectedByPercent = updatedPlayer.SelectedByPercent;
                existingPlayer.ValueForm = updatedPlayer.ValueForm;
                existingPlayer.ValueSeason = updatedPlayer.ValueSeason;
                existingPlayer.PlayerId = updatedPlayer.PlayerId;   

            }

              catch (Exception ex)
            {
             
                throw;
            }
        }


        
    }
}