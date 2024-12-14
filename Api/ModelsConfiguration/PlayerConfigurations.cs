namespace Api.ModelsConfiguration
{
    public class PlayerConfigurations : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players")
                .HasKey(e => e.PlayerId);

            builder.Property(p => p.PlayerId)
                .ValueGeneratedNever();

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.SecondName)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.WebName)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnType("nvarchar(2)");

            builder.Property(e => e.SquadNumber)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(e => e.ChancePlayingNextRound)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(e => e.ChancePlayingThisRound)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(e => e.News)
                .IsRequired(false)
                .HasColumnType("nvarchar(300)");

            builder.Property(e => e.NewsAdded)
                .IsRequired(false)
                .HasColumnType("datetime");

            builder.Property(e => e.NowCost)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(e => e.CostChangeEvent)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(e => e.CostChangeStart)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(e => e.SelectedByPercent)
                .IsRequired(false)
                .HasColumnType("decimal(5, 2)");

            builder.Property(e => e.ValueForm)
                .IsRequired(false)
                .HasColumnType("decimal(5, 2)");

            builder.Property(e => e.ValueSeason)
                .IsRequired(false)
                .HasColumnType("decimal(5, 2)");

            builder.HasOne(p => p.team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .IsRequired();

            builder.HasOne(e => e.elementType)
                .WithMany(et => et.Players)
                .HasForeignKey(e => e.ElementTypeId)
                .IsRequired();

            builder.HasMany(e => e.PlayerPerformances)
                .WithOne(pf => pf.player)
                .HasForeignKey(pf => pf.PlayerId);

            builder.HasMany(e => e.PlayerStatistics)
                .WithOne(pf => pf.player)
                .HasForeignKey(pf => pf.PlayerId);

            builder.HasIndex(p => new { p.FirstName, p.SecondName })
                .HasDatabaseName("idx_player_firstname_secondname");
        }
    }
}
