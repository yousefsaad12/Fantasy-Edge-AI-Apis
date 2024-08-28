


namespace Api.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        
        public Task<IEnumerable<T>>? GetAll(params Expression<Func<T, object>>[] includes);
        public Task<T> ? GetbyName(string FirstName, string LastName, params Expression<Func<T, object>>[] includes);
        public Task<bool> CreatePlayer(Player player);
        public Task<bool> UpdataPlayer(Player player, string FirstName, string SecondName);
    }
}