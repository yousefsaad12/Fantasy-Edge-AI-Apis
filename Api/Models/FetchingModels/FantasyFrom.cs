using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.FetchingModels
{
    public class FantasyFrom
    {
        ICollection<PlayerJsonForm> playerJsonForms;
        ICollection<TeamsJsonForm> TeamsJsonForms;
    }
}