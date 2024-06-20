namespace WebApimyServices.Configurations
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.Property(c => c.Id)
                   .ValueGeneratedOnAdd()
                   .HasMaxLength(50);

            builder.HasOne(r => r.Customer)
               .WithMany(r => r.CustomerRates)
               .HasForeignKey(r => r.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Factor)
                .WithMany(u => u.ReceivedRates)
                .HasForeignKey(r => r.FactorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.RatingValue)
                .HasColumnType("decimal(3, 2)")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
