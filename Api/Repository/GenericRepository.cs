

using Microsoft.EntityFrameworkCore.Storage;

namespace Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {   
        protected AppDbContext _context;
    
    
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            
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

    }
}