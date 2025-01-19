

namespace Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ILoggerFactory _loggerFactory;

        public IGenericRepository<Player> Players { get; private set; }
        public IGenericRepository<ElementTypes> ElementsTypes { get; private set; }
        public IGenericRepository<PlayerPerformance> PlayersPerformance { get; private set; }
        public IGenericRepository<PlayerStatistics> PlayersStatistics { get; private set; }
        public IGenericRepository<Team> Teams { get; private set; }
        public IGenericRepository<User> Users { get; private set; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;

            Players = new GenericRepository<Player>(_context, _loggerFactory.CreateLogger<GenericRepository<Player>>());
            ElementsTypes = new GenericRepository<ElementTypes>(_context, _loggerFactory.CreateLogger<GenericRepository<ElementTypes>>());
            PlayersPerformance = new GenericRepository<PlayerPerformance>(_context, _loggerFactory.CreateLogger<GenericRepository<PlayerPerformance>>());
            PlayersStatistics = new GenericRepository<PlayerStatistics>(_context, _loggerFactory.CreateLogger<GenericRepository<PlayerStatistics>>());
            Teams = new GenericRepository<Team>(_context, _loggerFactory.CreateLogger<GenericRepository<Team>>());
            Users = new GenericRepository<User>(_context,_loggerFactory.CreateLogger<GenericRepository<User>>());
        }

        public async Task<int> Complete()
        {
            try
            {
                int result = await _context.SaveChangesAsync();
                var logger = _loggerFactory.CreateLogger<UnitOfWork>();
                logger.LogInformation("Changes saved successfully. Total changes: {Count}", result);
                return result;
            }
            catch (Exception ex)
            {
                var logger = _loggerFactory.CreateLogger<UnitOfWork>();
                logger.LogError(ex, "An error occurred while saving changes.");
                throw;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}