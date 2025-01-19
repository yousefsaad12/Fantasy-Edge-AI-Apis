namespace Api.ModelsConfiguration
{
    public class PlayerStatisticsConfigurations : IEntityTypeConfiguration<PlayerStatistics>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistics> builder)
        {
            builder.ToTable("PlayersStatistics");
            builder.HasKey(e => e.PlayerStatisticsId);

            builder.Property(e => e.Influence).IsRequired().HasColumnType("int");
            builder.Property(e => e.Creativity).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(e => e.Threat).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(e => e.IctIndex).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(e => e.ExpectedGoals).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(e => e.ExpectedAssists).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(e => e.ExpectedGoalInvolvements).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(e => e.ExpectedGoalsConceded).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(e => e.GameWeek).IsRequired().HasColumnType("int");

            builder.HasIndex(e => e.GameWeek)
                   .HasDatabaseName("IX_GameWeek");

            builder.HasOne(e => e.player) // Changed from 'player' to 'Player'
                   .WithMany(p => p.PlayerStatistics)
                   .HasForeignKey(e => e.PlayerId)
                   .IsRequired();

            builder.HasIndex(ps => ps.PlayerId)
            .HasDatabaseName("idx_player_statistics_player");
        }
    }
}
