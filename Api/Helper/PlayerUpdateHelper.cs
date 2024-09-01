namespace Api.Services
{
    public static class PlayerUpdateHelper
    {
        public static async Task UpdateBasicPlayerProperties(Player existingPlayer, Player updatedPlayer)
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
                existingPlayer.TeamId = 1;

                if(existingPlayer.PlayerId != updatedPlayer.PlayerId)
                existingPlayer.PlayerId = updatedPlayer.PlayerId;
            }

              catch (Exception ex)
            {
             
                throw;
            }
        }

        
    }
}