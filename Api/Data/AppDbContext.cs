using System.Collections.Generic;
using Api.Models.PlayerModels;
using Api.Models.TeamModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base (options) {}

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerPerformance> PlayerPerformance { get; set; }
        public DbSet<PlayerStatistics> PlayerStatistics { get; set; }
        public DbSet<ElementTypes> ElementTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // player entity
            #region 
             modelBuilder.Entity<Player>(entity => {

                entity.ToTable("Players");
                entity.HasKey(e => e.PlayerId);

                entity.Property(p => p.PlayerId)
                      .ValueGeneratedNever(); 

                entity.Property(e => e.FirstName).IsRequired().HasColumnType("nvarchar(100)");

                entity.Property(e => e.SecondName).IsRequired().HasColumnType("nvarchar(100)");

                entity.Property(e => e.WebName).IsRequired().HasColumnType("nvarchar(100)");

                entity.Property(e => e.Status).IsRequired().HasColumnType("nvarchar(2)");

                entity.Property(e => e.SquadNumber).IsRequired(false).HasColumnType("int");
                entity.Property(e => e.ChancePlayingNextRound).IsRequired(false).HasColumnType("int");
                entity.Property(e => e.ChancePlayingThisRound).IsRequired(false).HasColumnType("int");
                

                entity.Property(e => e.News).IsRequired(false).HasColumnType("nvarchar(300)");
                entity.Property(e => e.NewsAdded).IsRequired(false).HasColumnType("datetime");

                
                entity.Property(e => e.NowCost).IsRequired(false).HasColumnType("int");
                entity.Property(e => e.CostChangeEvent).IsRequired(false).HasColumnType("int");
                entity.Property(e => e.CostChangeStart).IsRequired(false).HasColumnType("int");
                
                entity.Property(e => e.SelectedByPercent).IsRequired(false).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ValueForm).IsRequired(false).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ValueSeason).IsRequired(false).HasColumnType("decimal(5, 2)");


                entity.HasOne(p => p.team)
                      .WithMany(t => t.Players)
                      .HasForeignKey(p => p.TeamId)
                      .IsRequired();

                entity.HasOne(e => e.elementType)
                      .WithMany(et => et.Players)
                      .HasForeignKey(e => e.ElementTypeId)
                      .IsRequired();

                entity.HasMany(e => e.PlayerPerformances)
                      .WithOne(pf => pf.player)
                      .HasForeignKey(pf => pf.PlayerId);


                entity.HasMany(e => e.PlayerStatistics)
                      .WithOne(pf => pf.player)
                      .HasForeignKey(pf => pf.PlayerId);
            });

            #endregion


            // PlayerPerformance entity
             #region 
             modelBuilder.Entity<PlayerPerformance>(entity => {

                entity.ToTable("PlayersPerformance");
                entity.HasKey(e => e.PlayerPerformanceId);

                entity.Property(e => e.Minutes).IsRequired().HasColumnType("int");

                entity.Property(e => e.EventPoints).IsRequired().HasColumnType("int");

                entity.Property(e => e.TotalPoints).IsRequired().HasColumnType("int");

                entity.Property(e => e.GoalsScored).IsRequired().HasColumnType("int");

                entity.Property(e => e.Assists).IsRequired().HasColumnType("int");

                entity.Property(e => e.CleanSheets).IsRequired().HasColumnType("int");

                entity.Property(e => e.GoalsConceded).IsRequired().HasColumnType("int");

                entity.Property(e => e.PenaltiesSaved).IsRequired().HasColumnType("int");

                entity.Property(e => e.PenaltiesMissed).IsRequired().HasColumnType("int");

                entity.Property(e => e.OwnGoals).IsRequired().HasColumnType("int");

                entity.Property(e => e.YellowCards).IsRequired().HasColumnType("int");

                entity.Property(e => e.RedCards).IsRequired().HasColumnType("int");

                entity.Property(e => e.Saves).IsRequired().HasColumnType("int");

                entity.Property(e => e.Bonus).IsRequired().HasColumnType("int");

                entity.Property(e => e.BonusPointsSystem).IsRequired().HasColumnType("int");

                entity.Property(e => e.IsDreamTeam).IsRequired().HasColumnType("BIT");

                entity.Property(e => e.CreatedAt).IsRequired().HasColumnType("DateTime").HasDefaultValueSql("GETDATE()");
               
                entity.HasIndex(e => e.CreatedAt)
                      .HasDatabaseName("IX_CreatedAt");

                entity.HasOne(e => e.player)
                      .WithMany(p => p.PlayerPerformances)
                      .HasForeignKey(e => e.PlayerId)
                      .IsRequired();

                
            });

            #endregion


            // PlayerStatistics entity
            #region 
             modelBuilder.Entity<PlayerStatistics>(entity => {

                entity.ToTable("PlayersStatistics");
                entity.HasKey(e => e.PlayerStatisticsId);

                entity.Property(e => e.Influence).IsRequired().HasColumnType("int");

                entity.Property(e => e.Creativity).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Threat).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.IctIndex).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ExpectedGoals).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ExpectedAssists).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ExpectedGoalInvolvements).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ExpectedGoalsConceded).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.CreatedAt).IsRequired().HasColumnType("DateTime").HasDefaultValueSql("GETDATE()");


                entity.HasIndex(e => e.CreatedAt)
                      .HasDatabaseName("IX_CreatedAt");

                entity.HasOne(e => e.player)
                      .WithMany(p => p.PlayerStatistics)
                      .HasForeignKey(e => e.PlayerId)
                      .IsRequired();

                
                
            });

            #endregion


             #region 
             modelBuilder.Entity<ElementTypes>(entity => {

                entity.ToTable("ElementTypes");
                entity.HasKey(e => e.ElementTypeId);

                entity.Property(e => e.TypeName).IsRequired().HasColumnType("nvarchar(20)");


                entity.HasMany(e => e.Players)
                      .WithOne(p => p.elementType)
                      .HasForeignKey(p => p.ElementTypeId)
                      .IsRequired();

            });

            #endregion


             // Team entity
             #region 
             modelBuilder.Entity<Team>(entity => {

                entity.ToTable("Teams");
                entity.HasKey(e => e.TeamId);

                entity.Property(p => p.TeamId)
                      .ValueGeneratedNever(); 

                entity.Property(e => e.TeamName).IsRequired().HasColumnType("nvarchar(150)");

                entity.Property(e => e.ShortName).IsRequired().HasMaxLength(10);

                entity.Property(e => e.strength_overall_home).IsRequired().HasColumnType("int");

                entity.Property(e => e.strength_overall_away).IsRequired().HasColumnType("int");

                entity.Property(e => e.strength_attack_home).IsRequired().HasColumnType("int"); 
                entity.Property(e => e.strength_attack_away).IsRequired().HasColumnType("int"); 

                entity.Property(e => e.strength_defence_home).IsRequired().HasColumnType("int"); 
                entity.Property(e => e.strength_defence_away).IsRequired().HasColumnType("int"); 



                entity.HasMany(e => e.Players)
                      .WithOne(p => p.team)
                      .HasForeignKey(p => p.TeamId)
                      .IsRequired();


            });

            #endregion




            modelBuilder.Entity<Player>()
                .HasIndex(p => new { p.FirstName, p.SecondName })
                .HasDatabaseName("idx_player_firstname_secondname");


            modelBuilder.Entity<PlayerPerformance>()
            .HasIndex(pp => pp.PlayerId)
            .HasDatabaseName("idx_player_performance_player");

           

            modelBuilder.Entity<PlayerStatistics>()
            .HasIndex(ps => ps.PlayerId)
            .HasDatabaseName("idx_player_statistics_player");

          
        }



    }
}