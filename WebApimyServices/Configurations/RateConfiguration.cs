namespace WebApimyServices.Configurations
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.RatingValue)
                .HasColumnType("decimal(3, 2)")
                .IsRequired();

            builder.Property(r => r.RatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Assuming the Rate entity has a collection of Users
            builder.HasMany(r => r.Users)
                .WithMany(u => u.Rates); // Changed to singular as a user typically has one rate
        }
    }
}
