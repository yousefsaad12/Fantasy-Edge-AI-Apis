

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


        public async Task<T?> GetByName(string name1, string? name2 = null, Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply includes only if they are provided
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            // Check if the entity has FirstName and SecondName
            if (typeof(T).GetProperty("FirstName") != null && typeof(T).GetProperty("SecondName") != null)
            {
                // This is for entities like Player with FirstName and SecondName
                return await query.FirstOrDefaultAsync(e =>
                    EF.Property<string>(e, "FirstName") == name1 &&
                    EF.Property<string>(e, "SecondName") == name2);
            }
            // Check if the entity has TeamName
            else if (typeof(T).GetProperty("TeamName") != null)
            {
                // This is for entities like Team with TeamName
                return await query.FirstOrDefaultAsync(e =>
                    EF.Property<string>(e, "TeamName") == name1);
            }

            return null; // Return null if neither condition matches
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