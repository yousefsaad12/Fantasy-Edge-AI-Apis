

namespace Api.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        
        public Task<IEnumerable<T>>? GetAll(params Expression<Func<T, object>>[] includes);
        public Task<T> ? GetByName(string FirstName, string LastName, params Expression<Func<T, object>>[] includes);
        public Task<bool> Create(T entity);
        public Task<bool> UpdateOne(T entity);

        public Task<T?> GetByEmail(string email, CancellationToken cancellationToken);
    
    }
}