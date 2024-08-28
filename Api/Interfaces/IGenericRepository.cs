
namespace Api.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        
        public Task<T> ? GetbyId(int Id);
        public Task<T> ? GetbyName(string FirstNamem, string LastName);
        public Task<bool> CreatePlayer(Player player);
        public Task<bool> UpdataPlayer(Player player, string FirstName, string SecondName);
    }
}