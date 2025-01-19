

namespace Api.Models.FetchingModels
{
    public class FantasyForm
    {
       public ICollection<PlayerJsonForm> playerJsonForms { get; set; }
      public ICollection<TeamsJsonForm> teamsJsonForms { get; set; }
    }
}