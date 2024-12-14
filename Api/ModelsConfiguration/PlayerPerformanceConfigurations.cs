namespace Api.ModelsConfiguration
{
    public class PlayerPerformanceConfigurations : IEntityTypeConfiguration<PlayerPerformance>
    {
        public void Configure(EntityTypeBuilder<PlayerPerformance> builder)
        {
            builder.ToTable("PlayersPerformance");
            builder.HasKey(e => e.PlayerPerformanceId);

            builder.Property(e => e.Minutes).IsRequired().HasColumnType("int");
            builder.Property(e => e.EventPoints).IsRequired().HasColumnType("int");
            builder.Property(e => e.TotalPoints).IsRequired().HasColumnType("int");
            builder.Property(e => e.GoalsScored).IsRequired().HasColumnType("int");
            builder.Property(e => e.Assists).IsRequired().HasColumnType("int");
            builder.Property(e => e.CleanSheets).IsRequired().HasColumnType("int");
            builder.Property(e => e.GoalsConceded).IsRequired().HasColumnType("int");
            builder.Property(e => e.PenaltiesSaved).IsRequired().HasColumnType("int");
            builder.Property(e => e.PenaltiesMissed).IsRequired().HasColumnType("int");
            builder.Property(e => e.OwnGoals).IsRequired().HasColumnType("int");
            builder.Property(e => e.YellowCards).IsRequired().HasColumnType("int");
            builder.Property(e => e.RedCards).IsRequired().HasColumnType("int");
            builder.Property(e => e.Saves).IsRequired().HasColumnType("int");
            builder.Property(e => e.Bonus).IsRequired().HasColumnType("int");
            builder.Property(e => e.BonusPointsSystem).IsRequired().HasColumnType("int");
            builder.Property(e => e.IsDreamTeam).IsRequired().HasColumnType("BIT");
            builder.Property(e => e.GameWeek).IsRequired().HasColumnType("int");

            builder.HasIndex(e => e.GameWeek)
                   .HasDatabaseName("IX_GameWeek");

            builder.HasOne(e => e.player) // Changed from 'player' to 'Player'
                   .WithMany(p => p.PlayerPerformances)
                   .HasForeignKey(e => e.PlayerId)
                   .IsRequired();

            builder.HasIndex(pp => pp.PlayerId)
            .HasDatabaseName("idx_player_performance_player");
        }
    }
}
