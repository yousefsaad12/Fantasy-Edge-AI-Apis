namespace Api.ModelsConfiguration
{
    public class ElementTypesConfigurations : IEntityTypeConfiguration<ElementTypes>
    {
        public void Configure(EntityTypeBuilder<ElementTypes> builder)
        {
            builder.ToTable("ElementTypes");
            builder.HasKey(e => e.ElementTypeId);

            builder.Property(e => e.TypeName)
                   .IsRequired()
                   .HasColumnType("nvarchar(20)");

            builder.HasMany(e => e.Players)
                   .WithOne(p => p.elementType)  // Changed from 'elementType' to 'ElementType'
                   .HasForeignKey(p => p.ElementTypeId)
                   .IsRequired();
        }
    }
}
