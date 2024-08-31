

using Api.Models.TeamModels;

namespace Api.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Player> Players { get; }
        IGenericRepository<ElementTypes> ElementsTypes { get; }
        IGenericRepository<PlayerPerformance> PlayersPerformance { get; }
        IGenericRepository<PlayerStatistics> PlayersStatistics { get; }
        IGenericRepository<PlayerTransfer> PlayersTransfer { get; }
        IGenericRepository<PlayerValue> PlayersValue { get; }
        IGenericRepository<Team> Teams { get; }
        IGenericRepository<TeamPerformance> TeamsPerformance { get; }

        public Task<int> Complete();
    }
}