

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
        public DbSet<PlayerTransfer> PlayerTransfer { get; set; }
        public DbSet<PlayerValue> PlayerValue { get; set; }
        public DbSet<ElementTypes> ElementTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPerformance> TeamPerformance { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // player entity
            #region 
             modelBuilder.Entity<Player>(entity => {

                entity.ToTable("Players");
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.FirstName).IsRequired().HasColumnType("nvarchar(100)");

                entity.Property(e => e.SecondName).IsRequired().HasColumnType("nvarchar(100)");

                entity.Property(e => e.WebName).IsRequired().HasColumnType("nvarchar(100)");

                entity.Property(e => e.Status).IsRequired().HasColumnType("nvarchar(2)");

                entity.Property(e => e.SquadNumber).IsRequired(false).HasColumnType("int");
                entity.Property(e => e.ChancePlayingNextRound).IsRequired(false).HasColumnType("int");
                entity.Property(e => e.ChancePlayingThisRound).IsRequired(false).HasColumnType("int");

                entity.Property(e => e.News).IsRequired(false).HasColumnType("nvarchar(300)");
                entity.Property(e => e.NewsAdded).IsRequired(false).HasColumnType("datetime");

                entity.HasOne(e => e.team)
                      .WithMany(t => t.Players)
                      .HasForeignKey(e => e.TeamId)
                      .IsRequired();

                entity.HasOne(e => e.elementType)
                      .WithMany(et => et.Players)
                      .HasForeignKey(e => e.ElementTypeId)
                      .IsRequired();

                entity.HasMany(e => e.PlayerPerformances)
                      .WithOne(pf => pf.player)
                      .HasForeignKey(pf => pf.PlayerId);

                entity.HasMany(e => e.PlayerValues)
                      .WithOne(pf => pf.player)
                      .HasForeignKey(pf => pf.PlayerId);
                
                entity.HasMany(e => e.PlayerTransfers)
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

                entity.Property(e => e.DreamTeamCount).IsRequired().HasColumnType("int");
               

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

                entity.Property(e => e.ExpectedGoalsPer90).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ExpectedAssistsPer90).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ExpectedGoalInvolvementsPer90).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ExpectedGoalsConcededPer90).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.GoalsConcededPer90).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.StartsPer90).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.CleanSheetsPer90).IsRequired().HasColumnType("decimal(5, 2)");

                
               

                entity.HasOne(e => e.player)
                      .WithMany(p => p.PlayerStatistics)
                      .HasForeignKey(e => e.PlayerId)
                      .IsRequired();

                
                
            });

            #endregion

             // PlayerTransfer entity
            #region 
             modelBuilder.Entity<PlayerTransfer>(entity => {

                entity.ToTable("PlayersTransfer");
                entity.HasKey(e => e.PlayerTransferId);

                entity.Property(e => e.TransfersIn).IsRequired().HasColumnType("int");

                entity.Property(e => e.TransfersInEvent).IsRequired().HasColumnType("int");

                entity.Property(e => e.TransfersOut).IsRequired().HasColumnType("int");


                entity.HasOne(e => e.player)
                      .WithMany(p => p.PlayerTransfers)
                      .HasForeignKey(e => e.PlayerId)
                      .IsRequired();

                

            });

            #endregion


             // PlayerValue entity
            #region 
             modelBuilder.Entity<PlayerValue>(entity => {

                entity.ToTable("PlayersValues");
                entity.HasKey(e => e.PlayerValueId);

                entity.Property(e => e.NowCost).IsRequired().HasColumnType("int");

                entity.Property(e => e.CostChangeEvent).IsRequired().HasColumnType("int");

                entity.Property(e => e.CostChangeStart).IsRequired().HasColumnType("int");

                entity.Property(e => e.SelectedByPercent).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ValueForm).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ValueSeason).IsRequired().HasColumnType("decimal(5, 2)");


                entity.HasOne(e => e.player)
                      .WithMany(p => p.PlayerValues)
                      .HasForeignKey(e => e.PlayerId)
                      .IsRequired();
                      


            });

            #endregion

            // ElementTypes entity

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

                entity.Property(e => e.TeamName).IsRequired().HasColumnType("nvarchar(150)");

                entity.Property(e => e.ShortName).IsRequired().HasMaxLength(10);

                entity.Property(e => e.Code).IsRequired().HasColumnType("int");

                entity.Property(e => e.PulseID).IsRequired().HasColumnType("int");

                entity.Property(e => e.TeamDivision).IsRequired().HasColumnType("nvarchar(150)");



                entity.HasMany(e => e.Players)
                      .WithOne(p => p.team)
                      .HasForeignKey(p => p.TeamId)
                      .IsRequired();

                entity.HasMany(e => e.TeamPerformances)
                      .WithOne(tp => tp.team)
                      .HasForeignKey(tp => tp.TeamId)
                      .IsRequired();

            });

            #endregion

             // TeamPerformance entity
             #region 
             modelBuilder.Entity<TeamPerformance>(entity => {

                entity.ToTable("TeamPerformance");
                entity.HasKey(e => e.TeamPerformanceId);

                entity.Property(e => e.Played).IsRequired().HasColumnType("int");

                entity.Property(e => e.Win).IsRequired().HasColumnType("int");

                entity.Property(e => e.Loss).IsRequired().HasColumnType("int");

                entity.Property(e => e.Draw).IsRequired().HasColumnType("int");

                entity.Property(e => e.Points).IsRequired().HasColumnType("int");

                entity.Property(e => e.Position).IsRequired().HasColumnType("int");
                

                entity.HasOne(e => e.team)
                      .WithMany(t => t.TeamPerformances)
                      .HasForeignKey(e => e.TeamId)
                      .IsRequired();

            });

            #endregion

            // Gameweeks entity


            modelBuilder.Entity<Player>()
                .HasIndex(p => new { p.FirstName, p.SecondName })
                .HasDatabaseName("idx_player_firstname_secondname");


            modelBuilder.Entity<PlayerPerformance>()
            .HasIndex(pp => pp.PlayerId)
            .HasDatabaseName("idx_player_performance_player");

            modelBuilder.Entity<PlayerValue>()
            .HasIndex(pv => pv.PlayerId)
            .HasDatabaseName("idx_player_value_player");


            modelBuilder.Entity<PlayerTransfer>()
            .HasIndex(pt => pt.PlayerId)
            .HasDatabaseName("idx_player_transfer_player");

           

            modelBuilder.Entity<PlayerStatistics>()
            .HasIndex(ps => ps.PlayerId)
            .HasDatabaseName("idx_player_statistics_player");

          

            modelBuilder.Entity<TeamPerformance>()
            .HasIndex(tp => tp.TeamId)
            .HasDatabaseName("idx_team_performance_team");
        }



    }
}