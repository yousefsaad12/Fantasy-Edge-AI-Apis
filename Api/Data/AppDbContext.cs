

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
        public DbSet<Gameweeks> Gameweeks { get; set; }
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

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.SecondName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.WebName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Status).IsRequired().HasMaxLength(2);

                entity.Property(e => e.SquadNumber).IsRequired(false);
                entity.Property(e => e.ChancePlayingNextRound).IsRequired(false);
                entity.Property(e => e.ChancePlayingThisRound).IsRequired(false);

                entity.Property(e => e.News).IsRequired(false).HasMaxLength(300);
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

                entity.Property(e => e.Minutes).IsRequired();

                entity.Property(e => e.EventPoints).IsRequired();

                entity.Property(e => e.TotalPoints).IsRequired();

                entity.Property(e => e.GoalsScored).IsRequired();

                entity.Property(e => e.Assists).IsRequired();

                entity.Property(e => e.CleanSheets).IsRequired();

                entity.Property(e => e.GoalsConceded).IsRequired();

                entity.Property(e => e.PenaltiesSaved).IsRequired();

                entity.Property(e => e.PenaltiesMissed).IsRequired();

                entity.Property(e => e.OwnGoals).IsRequired();

                entity.Property(e => e.YellowCards).IsRequired();

                entity.Property(e => e.RedCards).IsRequired();

                entity.Property(e => e.Saves).IsRequired();

                entity.Property(e => e.Bonus).IsRequired();

                entity.Property(e => e.BonusPointsSystem).IsRequired();

                entity.Property(e => e.DreamTeamCount).IsRequired();
               

                entity.HasOne(e => e.player)
                      .WithMany(p => p.PlayerPerformances)
                      .HasForeignKey(e => e.PlayerId)
                      .IsRequired();

                entity.HasOne(e => e.Gameweeks)
                      .WithMany(gw => gw.PlayerPerformances)
                      .HasForeignKey(e => e.GameweekId)
                      .IsRequired();
                
            });

            #endregion


            // PlayerStatistics entity
            #region 
             modelBuilder.Entity<PlayerStatistics>(entity => {

                entity.ToTable("PlayersStatistics");
                entity.HasKey(e => e.PlayerStatisticsId);

                entity.Property(e => e.Influence).IsRequired();

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

                entity.HasOne(e => e.Gameweeks)
                      .WithMany(gw => gw.PlayerStatistics)
                      .HasForeignKey(e => e.GameweekId)
                      .IsRequired();
                
            });

            #endregion

             // PlayerTransfer entity
            #region 
             modelBuilder.Entity<PlayerTransfer>(entity => {

                entity.ToTable("PlayersTransfer");
                entity.HasKey(e => e.PlayerTransferId);

                entity.Property(e => e.TransfersIn).IsRequired();

                entity.Property(e => e.TransfersInEvent).IsRequired();

                entity.Property(e => e.TransfersOut).IsRequired();


                entity.HasOne(e => e.player)
                      .WithMany(p => p.PlayerTransfers)
                      .HasForeignKey(e => e.PlayerId)
                      .IsRequired();

                entity.HasOne(e => e.Gameweeks)
                      .WithMany(gw => gw.PlayerTransfer)
                      .HasForeignKey(e => e.GameweekId)
                      .IsRequired();

            });

            #endregion


             // PlayerValue entity
            #region 
             modelBuilder.Entity<PlayerValue>(entity => {

                entity.ToTable("PlayersValue");
                entity.HasKey(e => e.PlayerValueId);

                entity.Property(e => e.NowCost).IsRequired();

                entity.Property(e => e.CostChangeEvent).IsRequired();

                entity.Property(e => e.CostChangeStart).IsRequired();

                entity.Property(e => e.SelectedByPercent).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ValueForm).IsRequired().HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ValueSeason).IsRequired().HasColumnType("decimal(5, 2)");


                entity.HasOne(e => e.player)
                      .WithMany(p => p.PlayerValues)
                      .HasForeignKey(e => e.PlayerId)
                      .IsRequired();
                      

                entity.HasOne(e => e.Gameweeks)
                      .WithMany(gw => gw.PlayerValue)
                      .HasForeignKey(e => e.GameweekId)
                      .IsRequired();

            });

            #endregion

             // Team entity
             #region 
             modelBuilder.Entity<Team>(entity => {

                entity.ToTable("Teams");
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.TeamName).IsRequired().HasMaxLength(150);

                entity.Property(e => e.ShortName).IsRequired().HasMaxLength(10);

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.PulseID).IsRequired();

                entity.Property(e => e.TeamDivision).IsRequired().HasMaxLength(150);



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
           

        }



    }
}