

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

        public async Task<T>? GetbyId(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public Task<T>? GetbyName(string FirstNamem, string LastName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdataPlayer(Player player, string FirstName, string SecondName)
        {
            throw new NotImplementedException();
        }
    }
}