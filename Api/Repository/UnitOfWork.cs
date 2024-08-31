
using Api.Models.TeamModels;

namespace Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Player> Players {get; private set;}

        public IGenericRepository<ElementTypes> ElementsTypes{get; private set;}

        public IGenericRepository<PlayerPerformance> PlayersPerformance {get; private set;}

        public IGenericRepository<PlayerStatistics> PlayersStatistics {get; private set;}

        public IGenericRepository<PlayerTransfer> PlayersTransfer {get; private set;}

        public IGenericRepository<PlayerValue> PlayersValue {get; private set;}

        public IGenericRepository<Team> Teams {get; private set;}

        public IGenericRepository<TeamPerformance> TeamsPerformance {get; private set;}


        public UnitOfWork(AppDbContext context)
        {
            _context =  context;

            Players = new GenericRepository<Player>(_context);
            ElementsTypes = new GenericRepository<ElementTypes>(_context);
            PlayersPerformance = new GenericRepository<PlayerPerformance>(_context);
            PlayersStatistics = new GenericRepository<PlayerStatistics>(_context);
            PlayersTransfer = new GenericRepository<PlayerTransfer>(_context);
            PlayersValue = new GenericRepository<PlayerValue>(_context);
            Teams = new GenericRepository<Team>(_context);
            TeamsPerformance = new GenericRepository<TeamPerformance>(_context);
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
           await _context.DisposeAsync();
        }
    }
}