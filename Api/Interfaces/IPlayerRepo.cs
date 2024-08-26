using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Api.Interfaces
{
    public interface IPlayerRepo
    {
        public Task<Player> ? GetPlayerbyId(int Id);
        public Task<Player> ? GetPlayerbyName(string FirstNamem, string LastName);
        public Task<bool> CreatePlayer(Player player);
        public Task<bool> UpdataPlayer(Player player, string FirstName, string SecondName);
    }
}