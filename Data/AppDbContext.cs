using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base (options) {}

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerPerformance> PlayerPerformance { get; set; }
        public DbSet<PlayerStatistics> PlayerStatistics { get; set; }
        public DbSet<ElementTypes> ElementTypes { get; set; }
        public DbSet<Team> Teams { get; set; }   
        public DbSet<User> Users { get; set; }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof (PlayerPerformanceConfigurations).Assembly);
      
            modelBuilder.ApplyConfigurationsFromAssembly(typeof (PlayerStatisticsConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof (TeamConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof (ElementTypesConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof (UserConfigurations).Assembly);
            
        }



    }
}