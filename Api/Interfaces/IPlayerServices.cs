using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Api.Interfaces
{
    public interface IPlayerServices
    {
        
        public Task<Player> ? GetPlayerbyName(string FirstName, string LastName);
        public Task<bool> CreatePlayer(Player player);
        public  Task InsertPlayersAndRelatedEntitiesAsync(IEnumerable<PlayerJsonForm> playerJsonForms);
    }
}