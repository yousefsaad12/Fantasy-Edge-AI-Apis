
namespace Api.UnitOfWork
{
    public class GenericRepository <TEntity> where TEntity : class
    {   
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity>dbSet;

        public GenericRepository(AppDbContext context)
        {
           _context = context; 
           this.dbSet = _context.Set<TEntity>();
        }
    }
}