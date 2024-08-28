

namespace Api.UnitOfWork
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<bool> CreatePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public Task<T>? GetbyId(int Id)
        {
            throw new NotImplementedException();
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