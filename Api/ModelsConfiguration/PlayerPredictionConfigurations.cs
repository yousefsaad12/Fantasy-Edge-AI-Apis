


namespace Api.ModelsConfiguration
{
    public class PlayerPredictionConfigurations : IEntityTypeConfiguration<PlayerPrediction>
    {
        public void Configure(EntityTypeBuilder<PlayerPrediction> builder)
        {
            builder.Property(p => p.playerId)
                   .ValueGeneratedOnAdd(); 

            builder.HasIndex(p => p.playerName)
                   .HasDatabaseName("idx_playerName");
        }
    }
}