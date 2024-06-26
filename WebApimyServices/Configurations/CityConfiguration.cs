namespace WebApimyServices.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(c => c.Name)
                   .HasMaxLength(30);

            builder.HasMany(c => c.User) // User has many Cities
                   .WithOne(b => b.City) // Cities belongs to one User
                   .HasForeignKey(b => b.CityId) // Define foreign key
                   .IsRequired(true); // CityId is Not Nullable in User

            builder.HasOne(c => c.Governorate) // Category has many Problems
                   .WithMany(b => b.City) // Problems belongs to one Category
                   .HasForeignKey(b => b.GovernorateId) // Define foreign key
                   .IsRequired(true); // CategoryId is Not Nullable in Problems
        }
    }
}
