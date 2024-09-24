

public class GenericRepository<T> : IGenericRepository<T> where T : class
{   
    protected readonly AppDbContext _context;
    private readonly ILogger<GenericRepository<T>> _logger;

    public GenericRepository(AppDbContext context, ILogger<GenericRepository<T>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Create(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Entity created successfully: {@Entity}", entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating entity: {@Entity}", entity);
            return false;
        }
    }

    public async Task<T?> GetByName(string name1, string? name2 = null, Expression<Func<T, object>>[]? includes = null)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply includes only if they are provided
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            T? result = null;

            if (typeof(T).GetProperty("FirstName") != null && typeof(T).GetProperty("SecondName") != null)
            {
                result = await query.FirstOrDefaultAsync(e =>
                    EF.Property<string>(e, "FirstName") == name1 &&
                    EF.Property<string>(e, "SecondName") == name2);
            }
            else if (typeof(T).GetProperty("TeamName") != null)
            {
                result = await query.FirstOrDefaultAsync(e =>
                    EF.Property<string>(e, "TeamName") == name1);
            }

            _logger.LogInformation("GetByName executed: name1 = {Name1}, name2 = {Name2}, result = {@Result}", name1, name2, result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving entity by name: name1 = {Name1}, name2 = {Name2}", name1, name2);
            return null;
        }
    }

    public async Task<IEnumerable<T>>? GetAll(params Expression<Func<T, object>>[] includes)
    {   
        try
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
                query = query.Include(include);

            var result = await query.ToListAsync();
            _logger.LogInformation("GetAll executed successfully. Total entities retrieved: {Count}", result.Count);
            return result;
        }
        catch (Exception ex)
        {   
            _logger.LogError(ex, "An error occurred while retrieving all entities.");
            return null;
        }
    } 

    public async Task<bool> UpdateOne(T entity)
    {
        try
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            bool success = await _context.SaveChangesAsync() > 0;
            _logger.LogInformation("Entity updated successfully: {@Entity}", entity);
            return success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating entity: {@Entity}", entity);
            return false;
        }
    }
}
