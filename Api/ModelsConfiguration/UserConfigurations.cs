namespace Api.ModelsConfiguration
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email)
                   .HasDatabaseName("idx_userEmail");

            builder.HasIndex(u => u.UserName)
                   .HasDatabaseName("idx_UserName");
        }
    }
}
