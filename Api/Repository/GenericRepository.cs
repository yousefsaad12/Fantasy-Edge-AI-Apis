

namespace Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {   
        protected AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<bool> CreatePlayer(Player player)
        {
            throw new NotImplementedException();
        }


        public async Task<T>? GetbyName(string FirstName, string LastName, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T>query = _context.Set<T>();

            foreach(var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "FirstName") == FirstName && EF.Property<string>(e, "SecondName") == LastName);
        }

         public async Task<IEnumerable<T>>? GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T>query = _context.Set<T>();

            foreach(var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        } 

        public Task<bool> UpdataPlayer(Player player, string FirstName, string SecondName)
        {
            throw new NotImplementedException();
        }
    }
}