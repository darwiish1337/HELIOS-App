namespace WebApimyServices.Configurations
{
    public class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
    {
        public void Configure(EntityTypeBuilder<Governorate> builder)
        {
            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.HasMany(c => c.City) // Gover has many Cities
                   .WithOne(g => g.Governorate) // Cities belongs to one Gover
                   .HasForeignKey(g => g.GovernorateId) // Define foreign key
                   .IsRequired(true); // GoverId is Not Nullable in City
        }
    }
}
