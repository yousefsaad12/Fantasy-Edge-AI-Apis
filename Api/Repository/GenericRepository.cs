

using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {   
        protected AppDbContext _context;
        private IDbContextTransaction _transaction;
    
        public GenericRepository(AppDbContext context, IDbContextTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public async Task<bool> Create(T entity)
        {
           await _context.Set<T>().AddAsync(entity);

           return await _context.SaveChangesAsync() > 0;
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
            try
            {
                IQueryable<T>query = _context.Set<T>();

                foreach(var include in includes)
                    query = query.Include(include);

                return await query.ToListAsync();
            }

            catch (Exception ex)
            {   
                
                return null;
            }
        } 

        public async Task<bool> UpdateOne(T entity)
        {
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State =  EntityState.Modified;

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return  _transaction;
        }

        public async Task CommitTransactionAsync()
        {
             if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}