

using Api.Models.TeamModels;

namespace Api.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Player> Players { get; }
        IGenericRepository<ElementTypes> ElementsTypes { get; }
        IGenericRepository<PlayerPerformance> PlayersPerformance { get; }
        IGenericRepository<PlayerStatistics> PlayersStatistics { get; }
        IGenericRepository<Team> Teams { get; }

        IGenericRepository<User> Users { get; }
        public Task<int> Complete();
    }
}