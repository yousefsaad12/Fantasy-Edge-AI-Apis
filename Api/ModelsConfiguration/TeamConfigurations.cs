namespace Api.ModelsConfiguration
{
    public class TeamConfigurations : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");
            builder.HasKey(e => e.TeamId);

            builder.Property(p => p.TeamId)
                   .ValueGeneratedNever(); 

            builder.Property(e => e.TeamName)
                   .IsRequired()
                   .HasColumnType("nvarchar(150)");

            builder.Property(e => e.ShortName)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(e => e.strength_overall_home)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(e => e.strength_overall_away)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(e => e.strength_attack_home)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(e => e.strength_attack_away)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(e => e.strength_defence_home)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(e => e.strength_defence_away)
                   .IsRequired()
                   .HasColumnType("int");

            builder.HasMany(e => e.Players)
                   .WithOne(p => p.team)  // Changed from 'team' to 'Team'
                   .HasForeignKey(p => p.TeamId)
                   .IsRequired();
        }
    }
}
